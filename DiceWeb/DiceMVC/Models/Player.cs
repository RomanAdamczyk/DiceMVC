using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceMVC.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Dictionary<string, int> Values { get; set; }
        public Dictionary<string, bool> FreeValues { get; set; }
    }
}
