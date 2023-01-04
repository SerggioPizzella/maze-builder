using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{
  internal class Maze : MonoBehaviour
    {
        [SerializeField] int width = 2;
        [SerializeField] int height = 2;
        [SerializeField] MeshFilter meshFilter;

        MazeVisualizer visualizer;

        private void Start()
        {
            GenerateMaze(new DepthFirstSearch(width, height));
        }

        private void GenerateMaze(IMazeGenerator generator)
        {
            visualizer = new MazeVisualizer(width, height);

            List<(Position, Position)> transitions = generator.GenerateMaze();
            Mesh mesh = visualizer.GenerateMesh(transitions);
            meshFilter.mesh = mesh;
        }

		public void GenerateMaze()
		{
			GenerateMaze(new DepthFirstSearch(width, height));
		}

        public void SetWidthFromSlider(Slider widthSlider)
        {
            this.width = (int)widthSlider.value;
        }

        public void SetHeightFromSlider(Slider heightSlider)
        {
            this.height = (int)heightSlider.value;
        }
    }
}
