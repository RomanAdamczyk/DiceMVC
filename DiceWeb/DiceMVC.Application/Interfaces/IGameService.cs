using DiceMVC.Application.ViewModels.Game;
using DiceMVC.Application.ViewModels.Player;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.Interfaces
{
    public interface IGameService
    {
        int AddGame(GetPlayerCountVm game);
       // bool PlayerNoUp(int gameId);
        ListOfSavedGamesVm GetGamesToList();
        void EndingCreate(int gameId);
        Game GetGameById(int gameId);
        PlayerValueVM GetCurrentPlayerValue(int gameId, int playerId);
        List<PlayerScoreVm> GetPlayersScores(int gameId);
        DicesVm GetDices(int gameId, int playerId, int round, int lap);
        int CountValues(DicesVm dices, int value);
        void GetDicesToSave(DicesVm dices);
        void NextLap(int gameId);
        void NextPlayer(int gameId);
        void SaveBlockedDices(DicesVm dices);
        PlayerValueVM CountOptionalValues(DicesVm dices);


    }
}
