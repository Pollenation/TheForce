using System;
using SQLite;
namespace TheForce.Character
{
    public class Skill
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int attID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }

    public class Att
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
