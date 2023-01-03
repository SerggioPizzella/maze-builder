using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMaze : MonoBehaviour
{
    [SerializeField] Transform maze;

    void Update()
    {
        // Get the bounds of the maze
        Bounds bounds = maze.GetComponent<Renderer>().bounds;

        // Set the position of the camera to the center of the maze
        Vector3 cameraPosition = bounds.center;
        cameraPosition.z = -10; // Set the z-coordinate to -10 to position the camera in front of the maze
        transform.position = cameraPosition;

        // Get the aspect ratio of the screen
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenAspectRatio = screenWidth / screenHeight;

        // Get the dimensions of the maze
        float mazeWidth = bounds.size.x;
        float mazeHeight = bounds.size.y;
        float mazeAspectRatio = mazeWidth / mazeHeight;

        // Adjust the size of the camera's view to fit the maze
        Camera camera = GetComponent<Camera>();
        if (screenAspectRatio > mazeAspectRatio)
        {
            camera.orthographicSize = mazeHeight / 2;
            camera.aspect = screenAspectRatio;
        }
        else
        {
            camera.orthographicSize = mazeWidth / 2;
            camera.aspect = screenAspectRatio;
        }
    }
}
