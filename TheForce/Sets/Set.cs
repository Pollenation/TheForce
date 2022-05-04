using System;
using SQLite;
namespace TheForce.Sets
{
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ActiveSet
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int refID { get; set; }
        public string Name { get; set; }
    }
}
