using Bogus;
using Bogus.DataSets;
using System;
using System.Text.Json;
using System.Text.Unicode;

namespace BogusDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            while (true)
            {
                var personFaker = new Faker<Memmber>("zh_CN")
                    .RuleFor(x => x.ID, x => x.Random.Guid())
                    .RuleFor(x=>x.NO,x=>x.Random.Long(100000000000,999999999999))
                    .RuleFor(x => x.Name, x => x.Person.LastName + x.Person.FirstName)
                    .RuleFor(x => x.Sex, x => x.Random.Enum<Sex>())
                    .RuleFor(x => x.Email, x => x.Person.Email)
                    .RuleFor(x => x.Phone, x => x.Person.Phone)
                    .RuleFor(x => x.Age, x => x.Random.Int(0, 120))
                    .RuleFor(x=>x.Balance,x=>x.Finance.Amount(0,1000))                    
                    .RuleFor(x => x.CreateOn, x => x.Date.Past(1));

                Console.WriteLine(JsonSerializer.Serialize(personFaker.Generate(), options));
                Console.ReadLine();
            }
        }
    }
    enum Sex
    {
        男,
        女
    }
    class Memmber
    {
        public Guid ID { get; set; }
        public long NO { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
