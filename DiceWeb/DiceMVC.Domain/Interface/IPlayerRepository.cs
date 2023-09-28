using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiceMVC.Domain.Interface
{
    public interface IPlayerRepository
    {
      //  PlayerValue GetPlayerValue(int playerId);
      //  public Player GetPlayer(int playerId);
        IQueryable<PlayerValue> GetAllPlayersValues(int gameId);            //get value from PlayerValues from the game and sort by scores descending
        int AddPlayer(Player player);                                       //add player to data base and return player's id
        void AddPlayerValue(PlayerValue playerValue);                       //add PlayerValue to data base
        void AddPlayersTurn(PlayersTurn playersTurn);                       //add PlayerTurn to database
        void AddGamePlayer(GamePlayer gamePlayer);                          //add gamePlayer to data base
        IQueryable<Player> GetAllPlayers();                                 //get all players from data base
        IQueryable<PlayerValue> GetPlayerValue(int gameId, int playerId);   //get value from the data base for the game for the players 


    }
}
