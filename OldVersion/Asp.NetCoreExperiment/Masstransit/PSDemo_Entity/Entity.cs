using System;

namespace PSDemo_Entity
{
    public class Entity
    {
        public string Name
        { get; set; }

        public DateTime Time
        { get; set; }
    }
    public class ChildEntity
    {
        public string Name
        { get; set; }

        public DateTime Time
        { get; set; }
        public int Age
        { get; set; }

  
    }

}
