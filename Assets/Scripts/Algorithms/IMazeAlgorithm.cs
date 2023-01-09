namespace Assets.Scripts
{
    internal interface IMazeAlgorithm
    {
        Maze GenerateMaze(int width, int height);
    }
}