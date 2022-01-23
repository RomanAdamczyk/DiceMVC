using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class PlayersTurn
    {
        public PlayersTurn()
        {

        }
        public PlayersTurn(int gameId, int playerId, int turnNo)
        {
            GameId = gameId;
            PlayerId = playerId;
            TurnNo = turnNo;
        }
        public int Id { get; set; }
        public int TurnNo { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
