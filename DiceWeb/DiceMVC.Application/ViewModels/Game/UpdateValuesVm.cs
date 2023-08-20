using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class UpdateValuesVm
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public PlayerValueVM CurrentValues { get; set; }
        public PlayerValueVM OptionalValues { get; set; }
        public string ChooseValue { get; set; }
    }
}
