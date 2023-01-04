using System.Collections.Generic;

namespace Assets.Scripts
{
	internal interface IMazeGenerator
  	{
		List<(Position, Position)> GenerateMaze();
  	}
}