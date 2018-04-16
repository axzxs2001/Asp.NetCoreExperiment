using System;

namespace MEDemo_Entity
{

    public interface IEntity
    {
       int ID { get; set; }
    }

    public class Entity:IEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }
    }

    public class MyEntity:Entity
    {
        public int Age { get; set; }
    }


    public class YouEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }
        public int Age { get; set; }
    }
}
