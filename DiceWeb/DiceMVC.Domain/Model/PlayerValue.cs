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
        public Dictionary<string, int> Values { get; set; }
        public int PlayerRef { get; set; }
        public virtual Player Player { get; set; }

    }
}
