using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.GameRenderer
{
    public class GameRender(IGameSceneRepository gameScene, IGameConfig config) : IGameRender
    {
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly IGameConfig config = config;
        private char[,] currentBuffer = new char[config.MapWidth, config.MapHeight];
        private char[,] previousBuffer = new char[config.MapWidth, config.MapHeight];

        private void InitializeBuffer(char[,] buffer)
        {
            for (int y = 0; y < config.MapHeight; y++)
            {
                for (int x = 0; x < config.MapWidth; x++)
                {
                    buffer[x, y] = ' ';
                }
            }
        }
        public void Render()
        {
            InitializeBuffer(currentBuffer);

            foreach (var obj in gameScene.GameObjects(_ => true))
            {
                currentBuffer[obj.Position.X, obj.Position.Y] = obj.Sprite;
            }

            for (int y = 0; y < config.MapHeight; y++)
            {
                for (int x = 0; x < config.MapWidth; x++)
                {
                    if (currentBuffer[x, y] != previousBuffer[x, y])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(currentBuffer[x, y]);
                    }
                }
            }

            Console.SetCursorPosition(0, config.MapHeight);

            (currentBuffer, previousBuffer) = (previousBuffer, currentBuffer);

            Console.CursorVisible = false;
        }
    }
}
