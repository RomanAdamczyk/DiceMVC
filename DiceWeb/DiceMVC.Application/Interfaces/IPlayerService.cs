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
       // PlayerValueVM ShowPlayerValues(int playerId);
        int AddNewPlayer(NewPlayerVm player);
        //PlayerValue AddPlayerValues(int playerId, int GameId);
        string NewOrLoadPlayer(NewOrLoadPlayerVm newOrLoadPlayerVm);
        int GetGameId(NewOrLoadPlayerVm newOrLoadPlayerVm);
        //PlayersTurn AddPlayersTurns(int gameId, int playerId, int turnNo);
        ListOfPlayersVm GetPlayersForList();
        ListOfPlayersVm AddPlayerToGame(ListOfPlayersVm model);

        
    }
}
