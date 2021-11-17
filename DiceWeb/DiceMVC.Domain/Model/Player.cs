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
        public int PlayerValueId { get; set; }
        public virtual PlayerValue PlayerValue { get; set; }

    }
}
