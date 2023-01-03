using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Maze : MonoBehaviour
    {
        [SerializeField] int width = 2;
        [SerializeField] int height = 2;
        [SerializeField] MeshFilter meshFilter;

        MazeGenerator generator;
        MazeVisualizer visualizer;

        private void Start()
        {
            generator = new MazeGenerator(width, height);
            visualizer = new MazeVisualizer(width, height);

            List<(Position, Position)> transitions = generator.GenerateMaze();
            Mesh mesh = visualizer.GenerateMesh(transitions);
            meshFilter.mesh = mesh;
        }
    }
}
