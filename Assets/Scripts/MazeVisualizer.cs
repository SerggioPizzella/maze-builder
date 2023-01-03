using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MazeVisualizer
    {
        public int width;
        public int height;

        public MazeVisualizer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Mesh GenerateMesh(List<(Position, Position)> transitions)
        {
            Mesh mesh = new();
            List<Vector3> vertices = new();

            for (int i = 0; i < height * 2; i++)
            {
                for (int j = 0; j < width * 2; j++)
                {
                    vertices.Add(new Vector3(j, i, 0));
                }
            }

            List<int> triangles = new();

            // Fill all the cells in the maze
            Position position = new Position();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    position.x = j * 2;
                    position.y = i * 2;
                    triangles.AddRange(FillSquare(position));
                }
            }

            // Fill all the corridors connecting the maze
            for (int i = 0; i < transitions.Count; i++)
            {
                Position start = transitions[i].Item1;
                Position end = transitions[i].Item2;

                int x = start.x * 2 - (start.x - end.x);
                int y = start.y * 2 - (start.y - end.y);
                Position coridor = new(x, y);
                triangles.AddRange(FillSquare(coridor));
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            return mesh;
        }

        private int[] FillSquare(Position position)
        {
            int gridWidth = width * 2;
            int topLeft = position.x + gridWidth * position.y;
            int bottomLeft = position.x + gridWidth * position.y + gridWidth;
            int topRight = position.x + gridWidth * position.y + 1;
            int bottomRight = position.x + gridWidth * position.y + gridWidth + 1;

            return new int[] {
                topLeft, bottomLeft, topRight,
                topRight, bottomLeft, bottomRight,
            };
        }
    }
}
