using System;
using SQLite;
namespace TheForce.Character
{
    public class AttDice
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int dieID { get; set; }
    }

    public class SkillDice
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int dieID { get; set; }
    }
}
