using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class PlayerValue
    {
        public int Id { get; set; }
        public int Ones { get; set; }
        public int Twos { get; set; }
        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public int Sixs { get; set; }
        public int Bonus { get; set; }
        public int Triple { get; set; }
        public int Fourfold { get; set; }
        public int Full { get; set; }
        public int SmallStraight { get; set; }
        public int HighStraight { get; set; }
        public int General { get; set; }
        public int Chance { get; set; }
        public int Total { get; set; }
        public int PlayerRef { get; set; }
        public virtual Player Player { get; set; }
    }
}
