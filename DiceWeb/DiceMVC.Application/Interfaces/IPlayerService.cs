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
        int AddNewPlayer(NewPlayerVm player);                       //add new player, create approprate items in data base and get id of the game
        ListOfPlayersVm GetPlayersForList();                        //add chosen player to the game, create approprate items in data base and get id of the game
        ListOfPlayersVm AddPlayerToGame(ListOfPlayersVm model);     //get list of all players and convert to ListofPlayersVm

    }
}
