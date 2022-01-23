using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class Dices
    {
        public int Id { get; set; }
        public int Dice1 { get; set; }
        public bool Dice1IsBlocked { get; set; }
        public int Dice2 { get; set; }
        public bool Dice2IsBlocked { get; set; }
        public int Dice3 { get; set; }
        public bool Dice3IsBlocked { get; set; }
        public int Dice4 { get; set; }
        public bool Dice4IsBlocked { get; set; }
        public int Dice5 { get; set; }
        public bool Dice5IsBlocked { get; set; }
        public int Lap { get; set; }
        public int Round { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

    }
}
