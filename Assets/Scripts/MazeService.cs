using Assets.Scripts.Vizualizers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class MazeService : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] Material mainMaterial;
        [SerializeField] Maze maze;

        IMazeAlgorithm algorithm;
        IMazeVisualizer visualizer;

        private void Start()
        {
            this.maze = new(10, 10);
            this.algorithm = new DepthFirstSearch();
            this.visualizer = new GameObjectsVizualizer();

            GenerateMaze();
        }

        public void GenerateMaze()
        {
            this.maze = algorithm.GenerateMaze(maze.Width, maze.Height);

            // Add Material to the maze before vizualizing
            maze.Material = mainMaterial;

            visualizer.VisualizeMaze(mainCamera, maze);
        }

        public void SetMazeAlgorithm(IMazeAlgorithm mazeAlgorithm)
        {
            this.algorithm = mazeAlgorithm;
        }

        public void SetMazeVizualizer(IMazeVisualizer visualizer)
        {
            this.visualizer = visualizer;
        }

        public void SetWidthFromSlider(Slider widthSlider)
        {
            this.maze.Width = (int)widthSlider.value;
        }

        public void SetHeightFromSlider(Slider heightSlider)
        {
            this.maze.Height = (int)heightSlider.value;
        }
    }
}
