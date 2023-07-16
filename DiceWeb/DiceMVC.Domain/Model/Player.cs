using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class Player
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual PlayerStatistic PlayerStatistic { get; set; }
        public virtual ICollection<Dices> Dices { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
        public ICollection<PlayerValue> PlayerValues { get; set; }
        public ICollection<PlayersTurn> PlayersTurns { get; set; }

    }
}
