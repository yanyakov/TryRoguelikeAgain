using Ruguelike.API;
using Ruguelike.CustomStructures;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.MazeGenerator
{
    public class MazeGenerator(
        IGameConfig config, 
        IGameSceneRepository gameScene, 
        IPrototypeFactory factory
        ) : IMazeGenerator
    {
        private readonly IGameConfig config = config;
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly IPrototypeFactory factory = factory;

        public void Generate()
        {
            bool[,] maze = new bool[config.MapWidth, config.MapHeight];
            InitializeMaze(maze);

            Position start = GetPositionById(config.PlayerId, "Игрок не был добавлен на карту");
            Position finish = GetPositionById(config.FinishId, "Финиш не был добавлен на карту");

            CarvePassagesFrom(start, maze);
            EnsureFinishIsAccessible(finish, maze);
            ConstructWalls(maze);
        }

        private void InitializeMaze(bool[,] maze)
        {
            for (int x = 0; x < config.MapWidth; x++)
                for (int y = 0; y < config.MapHeight; y++)
                    maze[x, y] = true;
        }

        private Position GetPositionById(Guid id, string errorMessage) => gameScene.FindById(id)?.Position ?? throw new InvalidOperationException(errorMessage);

        private void CarvePassagesFrom(Position current, bool[,] maze)
        {
            maze[current.X, current.Y] = false;
            var directions = new Position[] { new(0, -2), new(2, 0), new(0, 2), new(-2, 0) };
            directions = Shuffle(directions);

            foreach (var direction in directions)
            {
                Position newPos = current + direction;
                Position between = current + direction / 2;

                if (IsInsideBounds(newPos) && maze[newPos.X, newPos.Y])
                {
                    maze[between.X, between.Y] = false;
                    CarvePassagesFrom(newPos, maze);
                }
            }
        }

        private void EnsureFinishIsAccessible(Position finish, bool[,] maze)
        {
            foreach (var direction in new Position[] { new(0, -1), new(1, 0), new(0, 1), new(-1, 0), new(0, 0) })
            {
                Position newPos = finish + direction;

                if (IsInsideBounds(newPos))
                    maze[newPos.X, newPos.Y] = false;
            }
        }

        private void ConstructWalls(bool[,] maze)
        {
            for (int x = 0; x < config.MapWidth; x++)
                for (int y = 0; y < config.MapHeight; y++)
                    if (maze[x, y])
                        gameScene.Add(factory.Create("Wall", new Position(x, y)));
        }

        private static Position[] Shuffle(Position[] positions)
        {
            for (int i = positions.Length - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);
                (positions[i], positions[j]) = (positions[j], positions[i]);
            }
            return positions;
        }
        private bool IsInsideBounds(Position position) => 
            position.X > 0 && position.X < config.MapWidth - 1 && position.Y > 0 && position.Y < config.MapHeight - 1;
    }
}