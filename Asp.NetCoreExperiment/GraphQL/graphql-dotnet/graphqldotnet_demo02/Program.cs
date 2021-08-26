using GraphQL;
using GraphQL.Types;
using GraphQL.SystemTextJson;

Console.WriteLine("--------F1()--------");
await F1();

Console.WriteLine("--------F2()--------");
await F2();

Console.WriteLine("--------F3()--------");
await F3();


Console.WriteLine("--------F4()--------");
await F4();

static async Task F4()
{
    var schema = Schema.For(@"
          type Droid {
            id: String!
            name: String!
            friend: Character
          }

          type Character {
            name: String!
          }

          type Query {
            hero: Droid
          }
        ", _ =>
    {
        _.Types.Include<DroidType1>();
        _.Types.Include<Query>();
    });

    var json = await schema.ExecuteAsync(_ =>
    {
        _.Query = "{ hero { id name friend { name } } }";
    });

    Console.WriteLine(json);
}

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

public class Character
{
    public string Name { get; set; }
}
[GraphQLMetadata("Droid", IsTypeOf = typeof(Droid))]
public class DroidType1
{
    public string Id(Droid droid) => droid.Id;
    public string Name(Droid droid) => droid.Name;

    // these two parameters are optional
    // IResolveFieldContext provides contextual information about the field
    public Character Friend(IResolveFieldContext context, Droid source)
    {
        return new Character { Name = "C3-PO" };
    }
}