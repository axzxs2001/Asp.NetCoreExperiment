using GraphQL;
using GraphQL.Types;
using GraphQL.SystemTextJson;


//await F1();
//await F2();
await F2();


static async Task F3()
{
    var schema = new Schema { Query = new StarWarsQuery() };
    var json = await schema.ExecuteAsync(_ =>
    {
        _.Query = "{ hero { id name } }";
    });

    Console.WriteLine(json);
}

static async Task F2()
{
    var schema = Schema.For(@"
type Droid {
    id: String!
    name: String!
  }
  type Query {
    hero: Droid
  }", _ =>
    {
        _.Types.Include<Query>();
    });

    var json = await schema.ExecuteAsync(_ =>
    {
        _.Query = "{ hero { id name } }";
    });
    Console.WriteLine(json);
}

static async Task F1()
{
    var schema = Schema.For(@"
      type Query {
        hello: String
      }
    ");

    var json = await schema.ExecuteAsync(_ =>
    {
        _.Query = "{ hello }";
        _.Root = new { Hello = "Hello World!" };
    });

    Console.WriteLine(json);
}

public class Droid
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Query
{
    [GraphQLMetadata("hero")]
    public Droid GetHero()
    {
        return new Droid { Id = "1", Name = "R2-D2" };
    }
}

public class DroidType : ObjectGraphType<Droid>
{
    public DroidType()
    {
        Field(x => x.Id).Description("The Id of the Droid.");
        Field(x => x.Name).Description("The name of the Droid.");
    }
}

public class StarWarsQuery : ObjectGraphType
{
    public StarWarsQuery()
    {
        Field<DroidType>(
          "hero",
          resolve: context => new Droid { Id = "1", Name = "R2-D2" }
        );
    }
}