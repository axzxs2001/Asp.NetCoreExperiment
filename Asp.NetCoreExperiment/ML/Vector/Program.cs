

using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;
using Microsoft.SemanticKernel.Connectors.Redis;
using Microsoft.SemanticKernel.Embeddings;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Data;
using Npgsql;
using Dapper;

#pragma warning disable SKEXP0020 // 异步方法缺少 "await" 运算符，将以同步方��运行

//var vectorStore = new InMemoryVectorStore();

var hotelData = new List<Hotel>
{
    new Hotel
    {
        HotelId = "1",
        HotelName = "酒店A",
        Description = "这是一个非常好的酒店，位于市中心，距离购物中心仅几步之遥。",
        Tags = new[] { "商务", "休闲", "购物" }
    },
    new Hotel
    {
        HotelId = "2",
        HotelName = "酒店B",
        Description = "这家酒店提供豪华的设施和一流的服务，适合高端客户。",
        Tags = new[] { "豪华", "服务", "高端" }
    },
    new Hotel
    {
        HotelId = "3",
        HotelName = "酒店C",
        Description = "这家酒店靠近海滩，提供美丽的海景和各种水上活动。",
        Tags = new[] { "海滩", "海景", "水上活动" }
    },
    new Hotel
    {
        HotelId = "4",
        HotelName = "酒店D",
        Description = "这家酒店位于安静的郊区，适合家庭度假。",
        Tags = new[] { "家庭", "度假", "安静" }
    },
    new Hotel
    {
        HotelId = "5",
        HotelName = "酒店E",
        Description = "这家酒店以其独特的设计和艺术氛围而闻名。",
        Tags = new[] { "设计", "艺术", "独特" }
    },
    new Hotel
    {
        HotelId = "6",
        HotelName = "酒店F",
        Description = "这家酒店提供各种健身设施和活动，适合健身爱好者。",
        Tags = new[] { "健身", "活动", "健身爱好者" }
    },
    new Hotel
    {
        HotelId = "7",
        HotelName = "酒店G",
        Description = "这家酒店提供各种美食和饮料，适合美食爱好者。",
        Tags = new[] { "美食", "饮料", "美食爱好者" }
    },
};
//var hotels = vectorStore.GetCollection<int, Hotel>("hotelsk");


var redisConfiguration = new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    User = "",
    Password = "",
    ConnectTimeout = 1000,
    ConnectRetry = 3
};
var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(redisConfiguration);
var database = connectionMultiplexer.GetDatabase();
var vectorStore = new RedisVectorStore(database);

//var vectorStore = new RedisVectorStore(ConnectionMultiplexer.Connect("localhost:6379").GetDatabase());

var hotelsStore = vectorStore.GetCollection<string, Hotel>("hotels");

var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "mxbai-embed-large");
foreach (var hotel in hotelData)
{
    hotel.DescriptionEmbedding = await generator.GenerateEmbeddingVectorAsync(hotel.Description);
    InsertImageVector(hotel);
    //await hotelsStore.UpsertAsync(hotel);
};
Console.WriteLine("开始搜索1");
//var vectorSearchOptions = new VectorSearchOptions
//{
//    VectorPropertyName = nameof(Hotel.Description),
//    Top = 3
//};



var searchVector = await generator.GenerateEmbeddingVectorAsync("找一个能健身的酒店");
foreach (var item in QueryImageVector(searchVector.ToArray()))
{
    Console.WriteLine(item.Id + "  " + item.Name+" "+item.Result );
}
Console.WriteLine("完成搜索2");
//var results = await hotelsStore.VectorizedSearchAsync(searchVector, vectorSearchOptions);

//await foreach (var result in results.Results)
//{
//    Console.WriteLine($"Title: {result.Record.HotelName}");
//    Console.WriteLine($"Description: {result.Record.Description}");
//    Console.WriteLine($"Score: {result.Score}");
//    Console.WriteLine();
//}

IEnumerable<FFF> QueryImageVector(float[] imageVector)
{
    var ds = new List<double>();
    foreach (var item in imageVector)
    {
        ds.Add((double)item);
    }
    using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
    {
        string sqlQuery = "select id,name,1-(cast(@embedding as vector) <=> embedding) as result from public.imagevector order by 1-(cast(@embedding as vector) <=> embedding) desc ";
        return db.Query<FFF>(sqlQuery, new { embedding = ds });

    }
}

void InsertImageVector(Hotel imageVector)
{
    using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
    {
        string sqlQuery = @"
                INSERT INTO public.imagevector (name, embedding) 
                VALUES (@Name, @Embedding) 
                RETURNING id;";
        var ds = new List<double>();
        foreach (var item in imageVector.DescriptionEmbedding.Value.ToArray())
        {
            ds.Add((double)item);
        }
        var parameters = new
        {
            Name = imageVector.HotelName,
            Embedding = ds.AsReadOnly<double>()
        };

        var id = db.ExecuteScalar<int>(sqlQuery, parameters); // ExecuteScalar returns the inserted id

    }
}
class FFF
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
}

public class Hotel
{
    [VectorStoreRecordKey]
    public string HotelId { get; set; }

    [VectorStoreRecordData(IsFilterable = true)]
    public string HotelName { get; set; }

    [VectorStoreRecordData(IsFullTextSearchable = true)]
    public string Description { get; set; }

    [VectorStoreRecordVector(1024, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }

    [VectorStoreRecordData(IsFilterable = true)]
    public string[] Tags { get; set; }
}


//public class Hotel
//{
//    [VectorStoreRecordKey]
//    public ulong HotelId { get; set; }

//    [VectorStoreRecordData(IsFilterable = true, StoragePropertyName = "hotel_name")]
//    public string HotelName { get; set; }

//    [VectorStoreRecordData(IsFullTextSearchable = true, StoragePropertyName = "hotel_description")]
//    public string Description { get; set; }

//    [VectorStoreRecordVector(Dimensions: 4, DistanceFunction.CosineDistance, IndexKind.Hnsw, StoragePropertyName = "hotel_description_embedding")]
//    public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }
//}