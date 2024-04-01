using DiceMVC.Domain.Interface;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly WorkContext _context;
        public PlayerRepository(WorkContext context)
        {
            _context = context;
        }
        public int AddPlayer(Player player)                     //add player to data base and return player's id
        {          
            _context.Players.Add(player);       //add player to data base
            _context.SaveChanges();             //save changes
            return player.Id;                   //return player's id
        }
        public void AddPlayerValue(PlayerValue playerValue)     //add PlayerValue to data base
        {
            _context.PlayerValues.Add(playerValue);             //add PlayerValue to data base
            _context.SaveChanges();                             //save changes
        }
        public void AddPlayersTurn(PlayersTurn playersTurn)     //add PlayerTurn to database
        {
            _context.PlayersTurns.Add(playersTurn);             //add PlayerTurn to database
            _context.SaveChanges();                             //save changes
        }
        public IQueryable<PlayerValue> GetPlayerValue(int gameId,int playerId)      //get value from the data base for the game for the players 
        {
            var playerValue = from value in _context.PlayerValues                   //get value from PlayerValues...
                               where value.PlayerId.Equals(playerId)                //... for the players
                               where value.GameId.Equals(gameId)                    //... from the game
                               select value;

            return playerValue;                                                     //return playerValue
        }
        public IQueryable<PlayerValue> GetAllPlayersValues(int gameId)              //get value from PlayerValues from the game and sort by scores descending
        {
            var playerValues = from value in _context.PlayerValues                  //get value from PlayerValues...
                               where value.GameId.Equals(gameId)                    //... from the game
                               orderby value.Total descending                       //sort by scores descending
                               select value;
                              
            return playerValues;                                                    //return playerValues
        }
        public void AddGamePlayer(GamePlayer gamePlayer)                            //add gamePlayer to data base
        {
            _context.GamePlayer.Add(gamePlayer);                                    //add gamePlayer to data base
            _context.SaveChanges();                                                 //save changes
        }
        public IQueryable<Player> GetAllPlayers()                                   //get all players from data base
        {
            return _context.Players;                                                //get all players from data base
        }
    }
}
