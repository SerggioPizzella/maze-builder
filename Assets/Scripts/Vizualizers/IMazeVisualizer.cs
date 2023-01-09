using UnityEngine;

namespace Assets.Scripts
{
    internal interface IMazeVisualizer
    {
        void VisualizeMaze(Camera camera, Maze maze);
    }
}
