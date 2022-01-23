using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class PlayerStatistic
    {
        public int Id { get; set; }
        public int GamesCount { get; set; }
        public int TotalScore {get; set;}
        public int TotalWins { get; set; }
        public int PlayerRef { get; set; }
        public virtual Player Player { get; set; }
    }
}
