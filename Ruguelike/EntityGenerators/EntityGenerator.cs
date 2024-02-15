using Ruguelike.API;
using Ruguelike.CustomStructures;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.EntityGenerators
{
    public class EntityGenerator(IGameConfig config, IGameSceneRepository gameScene, IPrototypeFactory factory) : IEntityGenerator
    {
        private readonly IGameConfig config = config;
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly IPrototypeFactory factory = factory;

        public void Generate(int zombiesCount, int archersCount)
        {
            var emptyCells = FindEmptyCells();

            if (emptyCells.Count < zombiesCount + archersCount)
                throw new InvalidOperationException("Недостаточно пустых клеток для размещения сущностей");

            PlaceEntities("Zombie", zombiesCount, emptyCells);
            PlaceEntities("Archer", archersCount, emptyCells);
        }

        private List<Position> FindEmptyCells()
        {
            var emptyCells = new List<Position>();
            var allObjects = gameScene.GameObjects(obj => true).ToList();

            for (int x = 1; x < config.MapWidth - 1; x++)
            {
                for (int y = 1; y < config.MapHeight - 1; y++)
                {
                    var position = new Position(x, y);
                    if (!allObjects.Any(obj => obj.Position == position))
                        emptyCells.Add(position);
                }
            }

            return emptyCells;
        }

        private void PlaceEntities(string entityType, int count, List<Position> emptyCells)
        {
            for (int i = 0; i < count; i++)
            {
                int index = Random.Shared.Next(emptyCells.Count);
                Position position = emptyCells[index];
                emptyCells.RemoveAt(index);

                gameScene.Add(factory.Create(entityType, position));
            }
        }
    }
}
