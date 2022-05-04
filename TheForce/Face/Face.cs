using System;
using SQLite;
namespace TheForce.Face
{
    public class DieFace
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Faces { get; set; }
        public int refID { get; set; }
    }

    public class FacePool
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string OpFace { get; set; }
    }
}
