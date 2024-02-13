using Ruguelike.CustomStructures;
using Ruguelike.GameObjects;

namespace Ruguelike.API
{
    public interface IPrototypeFactory
    {
        IGameObject Create(string prototypeKey, Position position);
    }

}
