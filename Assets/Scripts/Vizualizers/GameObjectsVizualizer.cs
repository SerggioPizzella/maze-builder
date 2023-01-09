using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Vizualizers
{
    internal class GameObjectsVizualizer : IMazeVisualizer
    {
        GameObject mazeObject;

        public GameObjectsVizualizer()
        {
            this.mazeObject = new("maze");
        }

        public void VisualizeMaze(Camera camera, Maze maze)
        {
            GenerateTiles(maze);
            FocusMaze(camera);
        }

        public void GenerateTiles(Maze maze)
        {
            ClearMaze();
            this.mazeObject = new("maze");

            // Set the material of the tiles
            Material material = maze.Material;

            // Create a tile for each position in the maze
            for (int i = 0; i < maze.Width; i++)
            {
                for (int j = 0; j < maze.Height; j++)
                {
                    // Create a new tile game object and set its parent to the maze object
                    GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    tile.transform.SetParent(mazeObject.transform);

                    // Set the material of the tile
                    Renderer renderer = tile.GetComponent<Renderer>();
                    renderer.material = material;

                    // Set the position of the tile
                    float x = i * 2 - maze.Width / 2.0f;
                    float y = j * 2 - maze.Height / 2.0f;
                    tile.transform.position = new Vector3(x, y, 0);
                }
            }

            // Create additional tiles between positions if a transition exists in the list
            foreach ((Position start, Position end) in maze.Transitions)
            {
                // Calculate the position of the tile between the start and end positions
                float x = (start.x + end.x) - maze.Width / 2.0f;
                float y = (start.y + end.y) - maze.Height / 2.0f;

                // Create a new tile game object and set its parent to the maze object
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                tile.transform.SetParent(mazeObject.transform);

                // Set the material of the tile
                Renderer renderer = tile.GetComponent<Renderer>();
                renderer.material = material;

                // Set the position of the tile
                tile.transform.position = new Vector3(x, y, 0);
            }
        }


        private void ClearMaze()
        {
            if (mazeObject == null) return;
            Object.DestroyImmediate(mazeObject);
        }

        private void FocusMaze(Camera camera)
        {
            // Calculate the dimensions of the maze
            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minY = float.MaxValue;
            float maxY = float.MinValue;
            foreach (Transform child in mazeObject.transform)
            {
                Vector3 position = child.position;
                minX = Mathf.Min(minX, position.x);
                maxX = Mathf.Max(maxX, position.x);
                minY = Mathf.Min(minY, position.y);
                maxY = Mathf.Max(maxY, position.y);
            }

            // Get the dimensions of the maze
            float mazeWidth = maxX - minX;
            float mazeHeight = maxY - minY;
            float mazeCenterX = (minX + maxX) / 2;
            float mazeCenterY = (minY + maxY) / 2;
            float mazeAspectRatio = mazeWidth / mazeHeight;

            // Set the position of the camera to the center of the maze
            Vector3 cameraPosition = new(mazeCenterX, mazeCenterY);
            cameraPosition.z = -10; // Set the z-coordinate to -10 to position the camera in front of the maze
            camera.transform.position = cameraPosition;

            // Get the aspect ratio of the screen
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float screenAspectRatio = screenWidth / screenHeight;

            // Adjust the size of the camera's view to fit the maze
            int offset = 5;
            if (screenAspectRatio > mazeAspectRatio)
            {
                camera.orthographicSize = mazeHeight / 2 + offset;
            }
            else
            {
                camera.orthographicSize = mazeWidth / 2 + offset;
            }

            camera.aspect = screenAspectRatio;
        }
    }
}
