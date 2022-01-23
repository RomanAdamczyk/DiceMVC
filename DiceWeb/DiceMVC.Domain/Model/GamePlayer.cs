using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class GamePlayer
    {
        public GamePlayer()
        {

        }
        public GamePlayer(int gameId, int playerId)
        {
            GameId = gameId;
            PlayerId = playerId;
        }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
