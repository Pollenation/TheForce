using System;
namespace TheForce.DieFaces
{
    public class Face
    {
        public string Name { get; set; }
        public string OpFace { get; set; }
        public Face(string name, string opface)
        {
            Name = name;
            OpFace = opface;
        }
    }
}
