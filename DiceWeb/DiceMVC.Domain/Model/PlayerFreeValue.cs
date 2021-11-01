using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Model
{
    public class PlayerFreeValue
    {
        public int Id { get; set; }
        public Dictionary<string, bool> FreeValues { get; set; }
        public int PlayerRef { get; set; }
        public virtual Player Player { get; set; }
    }
}
