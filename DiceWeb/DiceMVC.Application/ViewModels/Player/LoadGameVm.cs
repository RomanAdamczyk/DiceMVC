using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class LoadGameVm
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}
