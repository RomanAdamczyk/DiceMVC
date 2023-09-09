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
        int AddGame(GetPlayerCountVm game);                             //add game to database and take its ID

        ListOfSavedGamesVm GetGamesToList();                            //get active games and their players from database
        void EndingCreate(int gameId);                                  //prepare game to play (get game as active, set first player)
        Game GetGameById(int gameId);                                   //get game with gameId from database
        PlayerValueVM GetCurrentPlayerValue(int gameId, int playerId);  //get the player score from the game and convert to PlayerValueVm
        List<PlayerScoreVm> GetPlayersScores(int gameId);               //get players scores from the game and convert to List of PlayerScoreVm
        DicesVm GetDices(int gameId, int playerId, int round, int lap); //get dices from the lap of the round of the player of the game and convert to DiceVm
        int CountValues(DicesVm dices, int value);                      //count how many "value" repeats in "dices"            
        void GetDicesToSave(DicesVm dices);                             //concert "dices" from DicesVm to Dices and save in database
        void NextLap(int gameId);                                       //set next lap in database
        void NextRound(int gameId);                                     //set next round, first lap and first player
        void NextPlayer(int gameId, int playerTurn);                    //set next player and lap
        void SaveBlockedDices(DicesVm dices);                           //save remaning dices in database
        PlayerValueVM CountOptionalValues(DicesVm dices);               //check values are possible with these dices
        void UpdateValue(UpdateValuesVm playerValues);                  //add chosen value to all points of player and blocked appropriate field
        int GetPlayerTurn(int gameId, int playerId);                    //get PlayerTurn from data base (base with numbering of player in games)
        int GetPlayersCount(int gameId);                                //get players count of the game from data base
        PlaygameVm TurnDices(int gameId);                               //draw dices
        PlayerValue SetBonus(PlayerValue values);                       //take a bunus


    }
}
