using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MeshVisualizer : IMazeVisualizer
    {
        GameObject maze;
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;

        int width;
        int height;

        public MeshVisualizer()
        {
            this.maze = new("maze");
            this.meshFilter = maze.AddComponent<MeshFilter>();
            this.meshRenderer = maze.AddComponent<MeshRenderer>();
        }

        public void VisualizeMaze(Camera camera, Maze maze)
        {
            this.meshRenderer.material = maze.Material;

            CalculateMesh(maze);
            FocusMaze(camera);
        }

        private void CalculateMesh(Maze maze)
        {
            this.width = maze.Width;
            this.height = maze.Height;
            List<(Position, Position)> transitions = maze.Transitions;

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

            this.meshFilter.mesh = mesh;
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

        private void FocusMaze(Camera camera)
        {
            // Get the bounds of the mesh
            Bounds bounds = this.meshRenderer.bounds;

            // Set the position of the camera to the center of the maze
            Vector3 cameraPosition = bounds.center;
            cameraPosition.z = -10; // Set the z-coordinate to -10 to position the camera in front of the maze
            camera.transform.position = cameraPosition;

            // Get the aspect ratio of the screen
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float screenAspectRatio = screenWidth / screenHeight;

            // Get the dimensions of the maze
            float mazeWidth = bounds.size.x;
            float mazeHeight = bounds.size.y;
            float mazeAspectRatio = mazeWidth / mazeHeight;

            // Adjust the size of the camera's view to fit the maze
            if (screenAspectRatio > mazeAspectRatio)
            {
                camera.orthographicSize = mazeHeight / 2;
            }
            else
            {
                camera.orthographicSize = mazeWidth / 2;
            }

            camera.aspect = screenAspectRatio;
        }
    }
}
