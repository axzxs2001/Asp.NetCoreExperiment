using Akka;
using Akka.Actor;
using Akka.Persistence;
using Akka.Persistence.Query;
using Akka.Persistence.Query.Sql;
using Akka.Streams;
using Akka.Streams.Dsl;
using System;

namespace Demo08
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("query");

            // obtain read journal by plugin id
            var readJournal = PersistenceQuery.Get(actorSystem)
                .ReadJournalFor<SqlReadJournal>("akka.persistence.query.my-read-journal");

            // issue query to journal
            Source<EventEnvelope, NotUsed> source = readJournal
                .EventsByPersistenceId("user-1337", 0, long.MaxValue);

            // materialize stream, consuming events
            var mat = ActorMaterializer.Create(actorSystem);
            source.RunForeach(envelope =>
            {
                Console.WriteLine($"event {envelope}");
            }, mat);
        }
    }
}
