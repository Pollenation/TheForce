using System;
using SQLite;
using System.Collections.Generic;
namespace TheForce.Dice
{
    public class Die
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Facenum { get; set; }
        public string Faces { get; set; }
        public Boolean numeric { get; set; }
        public string Set { get; set; }
    }

    public class RollDie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int refID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
