using Api;
using Dgraph;
using Dgraph.Transactions;
using Grpc.Core;
using Newtonsoft.Json;
using System;


namespace DGraphDemo02
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new DgraphClient(Grpc.Net.Client.GrpcChannel.ForAddress(new Uri( "http://127.0.0.1:8000")));
            using (var txn = client.NewTransaction())
            {
                var alice = new Person { Name = "Alice" };
                var json = JsonConvert.SerializeObject(alice);

                var transactionResult =  txn.Mutate(new RequestBuilder().WithMutations(new MutationBuilder { SetJson = json })).Result;
            }
            var schema = "`name: string @index(exact) .";
            var result = client.Alter(new Operation { Schema = schema });
        }
    }
    class Person
    {
        public string Name { get; set; }
    }
}
