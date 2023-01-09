using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Material Material { get; set; }
        public List<(Position, Position)> Transitions { get; }


        public Maze(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public Maze (int width, int height, Material material) : this(width, height)
        {
            this.Material = material;
        }

        public Maze(int width, int height, List<(Position, Position)> transitions) : this(width, height)
        {
            this.Transitions = transitions;
        }
    }
}
