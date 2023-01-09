# maze-builder
This project was created as part of an internship assessment and provides a maze generation service that you can use to configure your own maze.

The MazeService game object includes the MazeService script, which uses two interfaces to generate and visualize the maze: the IMazeAlgorithm interface for determining the maze generation algorithm and the IMazeVizualizer interface for determining the representation of the maze as a game object.

One algorithm is included in this project:
 - Depth first search

Two vizualizers are included in this project:

 - A mesh vizualizer that generates the maze as a single mesh. This implementation is fast, but may stop working once the mesh grows too large due to the maximum number of triangles per mesh.
 - A quad vizualizer that generates the maze as a series of quad game objects, which are children of a single parent maze game object. This implementation is somewhat slow and can be optimized by reusing the same quad game objects instead of creating new ones each time.
