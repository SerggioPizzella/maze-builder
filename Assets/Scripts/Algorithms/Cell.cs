namespace Assets.Scripts
{
    internal class Cell
    {
        public Position Position { get; private set; }
        public bool Visited { get; set; }

        public Cell(int positionX, int positionY)
        {
            Position = new Position(positionX, positionY);
            Visited = false;
        }
    }
}
