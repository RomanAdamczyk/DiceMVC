using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class Game
    {
        public int Id { get; set; }
        public int CurrentPlayerId { get; set; }
        public int CurrentRound { get; set; }
        public bool IsActive { get; set; }
        public int PlayerCount { get; set; }
        public virtual ICollection<Dices> Dices { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
        public ICollection<PlayerValue> PlayerValues { get; set; }
        public ICollection<PlayersTurn> PlayersTurns { get; set; }
    }
}
