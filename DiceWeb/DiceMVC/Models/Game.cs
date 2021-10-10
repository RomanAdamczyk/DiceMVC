using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DiceMVC.Models
{
    public class Game
    {
        [DisplayName ("Wartość")]
        public int[,] Dices { get; set; }
        
    }
}
