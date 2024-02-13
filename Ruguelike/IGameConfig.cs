using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruguelike
{
    public interface IGameConfig
    {
        int MapWidth { get; }
        int MapHeight { get; }
        Guid PlayerId { get; }
        Guid FinishId { get; }
        bool GameOver { get;}

        void SetPlayerId(Guid playerId);
        void SetFinishId(Guid playerId);
        bool ChangeGameStatus();
    }

}
