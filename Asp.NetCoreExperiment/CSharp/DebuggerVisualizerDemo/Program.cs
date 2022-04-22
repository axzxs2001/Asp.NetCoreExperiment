using Bogus;
using System.Text.Unicode;

var addressFaker = new Faker<Address>()
    .RuleFor(x => x.PostCode, x => x.Address.ZipCode())
    .RuleFor(x => x.Province, x => x.Address.State())
    .RuleFor(x => x.City, x => x.Address.City())
    .RuleFor(x => x.Area, x => x.Address.StreetAddress());


var personFaker = new Faker<Member>()
    .RuleFor(x => x.ID, x => x.Random.Guid())
    .RuleFor(x => x.NO, x => x.Random.Long(100000000000, 999999999999))
    .RuleFor(x => x.Name, x => x.Person.LastName + x.Person.FirstName)
    .RuleFor(x => x.Sex, x => x.Random.Enum<Sex>())
    .RuleFor(x => x.Email, x => x.Person.Email)
    .RuleFor(x => x.Phone, x => x.Person.Phone)
    .RuleFor(x => x.Age, x => x.Random.Int(0, 120))
    .RuleFor(x => x.Balance, x => x.Finance.Amount(0, 1000))
    .RuleFor(x => x.CreateOn, x => x.Date.Past(1))
    .RuleFor(x => x.Addresses, x => addressFaker.Generate(5).ToList());


var members=personFaker.Generate(20).ToList();

Console.WriteLine(members.Count);


enum Sex
{
    男,
    女
}
class Member
{
    public Guid ID { get; set; }
    public long NO { get; set; }
    public string? Name { get; set; }
    public Sex Sex { get; set; }
    public int Age { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreateOn { get; set; }

    public List<Address>? Addresses { get; set; }
}

class Address
{
    public string? PostCode { get; set; }

    public string? Province { get; set; }
    public string? City { get; set; }
    public string? Area { get; set; }
}