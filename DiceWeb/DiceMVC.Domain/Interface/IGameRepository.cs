using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Interface
{
    public interface IGameRepository
    {
        int AddGame(Game game);                                                         //create new game and save in data base
        Game GetGame(int gameId);                                                       //get game from data base
        int UpdateGamePlayerNo(Game game);                                              //save changed "CurrentPlayerId" in database and return id of player
        IQueryable<Game> GetActiveGames();                                              //get active games from data base
        IQueryable<Player> GetPlayersToGame(int idGame);                                //get player from the game from data base
        void UpdateEndingCreate(Game game);                                             //save changes in "IsActive" and set first player
        IQueryable<int> GetFirstPlayerId(int gameId);                                   //set first player in data base and return his id (IQueryalble)
        IQueryable<Dices> GetDicesRepo(int gameId, int playerId, int round, int lap);   //get dices from data base for the game, the player, the round, the lap (IQueryalble)
        IQueryable<int> GetLap(int gameId, int playerId, int round);                    //get current lap for the game, the player, the round (IQueryable)
        IQueryable<int> GetPlayersCountRep(int idGame);                                 //get the count of player from the game (IQueryable)
        void SaveDices(Dices dices);                                                    //add the "dices" to data base
        void NextLapRepo(Game game);                                                    //confirm next lap in data base
        void NextRoundRepo(Game game);                                                  //confirm next round in data base
        void NextPlayerRepo(Game game);                                                 //confirm next player in dat base
        void SaveBlockedDicesRep(Dices dices);                                          //block chosen dices in data base
        void UpdateValuesRep(PlayerValue playerValue, string chooseValue);              //get points of players from the game
        IQueryable<PlayerValue> GetPlayerValue(int gameId, int playerId);               //agree all saving changes related to player selection
        IQueryable<int> GetPlayerTurnRepo(int gameId, int playerId);                    //get current player's turn number in the game (IQueryable)
        IQueryable<int> GetPlayerIdFromPlayerTurn(int gameid, int playerTurn);          //get id of current player in the game (IQueryable)
        void EndGameRep(int gameId);                                                    //set IsActive as "false" in the game
    }
}
