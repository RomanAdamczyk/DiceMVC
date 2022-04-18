using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class ListOfSavedGamesVm
    {
        public List<GetSavedGamesToListVm> Games { get; set; }
        public int Count { get; set; }

    }
}
