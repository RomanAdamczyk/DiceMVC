using DiceMVC.Application.ViewModels.Player;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.Interfaces
{
    public interface IPlayerService
    {
        PlayerValueForListVm ShowPlayerValues(int playerId);
        int AddPlayer(NewPlayerVm player);
        PlayerValue AddPlayerValues(int playerId);
        
    }
}
