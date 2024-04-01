using DiceMVC.Domain.Interface;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Infrastructure.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly WorkContext _context;
        public GameRepository(WorkContext context)
        {
            _context = context;

        }
        public int AddGame(Game game)   //create new game and save in data base
        {
            _context.Games.Add(game);   //add new game to data base
            _context.SaveChanges();     //save changes
            return game.Id;             //return id of new game
        }
        public Game GetGame(int gameId)                                 //get game from data base
        {
            return _context.Games.FirstOrDefault(p => p.Id == gameId);  //get game from data base
        }
        public int UpdateGamePlayerNo(Game game)                                    //save changed "CurrentPlayerId" in database and return id of player
        {
            _context.Attach(game);                                                  //attach the "game" in data base
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;     //agree changes in "CurrentPlayerId"
            _context.SaveChanges();                                                 //save changes
            return game.CurrentPlayerId;                                            //return id of player
        }
        public IQueryable<Game> GetActiveGames()                                    //get active games from data base
        {
            return _context.Games.Where(g => g.IsActive);                           //get active games from data base
        }
        public IQueryable<Player> GetPlayersToGame(int idGame)                      //get player from the game from data base
        {
            var players = from player in _context.Players                           //get players from data base... 
                        where player.GamePlayers.Any(pl => pl.GameId == idGame)     //... from the game
                        select player;

            return players;
        }
        public void UpdateEndingCreate(Game game)                                   //save changes in "IsActive" and set first player
        {
            _context.Attach(game);                                                  //attach the "game" in data base
            _context.Entry(game).Property("IsActive").IsModified = true;            //agree changes in "IsAcvtive"
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;     //agree changes in "CurrentPlayerId"
            _context.SaveChanges();                                                 //save all changes in data base
        }
        public IQueryable<int> GetFirstPlayerId(int gameId)         //set first player in data base and return his id (IQueryalble)
        {
            var playerId = from item in _context.PlayersTurns       //get id of player from data base (PlayerTurn)... 
                           where item.GameId.Equals(gameId)         //...from the game...
                           where item.TurnNo.Equals(0)              //...where TurnNo equals 0 (first player)...
                           select item.PlayerId;
            return playerId;                                        //return Id of first player
        }
        public IQueryable<Dices> GetDicesRepo(int gameId, int playerId, int round, int lap)     //get dices from data base for the game, the player, the round, the lap (IQueryalble)
        {
            var previousDices = from dices in _context.Dices                                    //get dices from data base...
                                where dices.GameId.Equals(gameId)                               //..from the game...
                                where dices.PlayerId.Equals(playerId)                           //...for the player...
                                where dices.Round.Equals(round)                                 //...for the round...
                                where dices.Lap.Equals(lap)                                     //...for the lap 
                                select dices;
            return previousDices;
        }
        public IQueryable<int> GetLap(int gameId, int playerId, int round)      //get current lap for the game, the player, the round (IQueryable)
        {
            var listOfLaps = from dices in _context.Dices                       //get current lap...
                             where dices.GameId.Equals(gameId)                  //...for the game...
                             where dices.PlayerId.Equals(playerId)              //...for the player...
                             where dices.Round.Equals(round)                    //...for the round
                             select dices.Lap;
            return listOfLaps;
        }
        public IQueryable<int> GetPlayersCountRep(int idGame)       //get the count of player from the game (IQueryable)
        {
            var playersCount = from game in _context.Games          //get the count of player... 
                               where game.Id.Equals(idGame)         //from the game
                               select game.PlayerCount;
            return playersCount;
        }
        public void SaveDices(Dices dices)      //add the "dices" to data base
        {
            _context.Dices.Add(dices);          //add the "dices" to data base
            _context.SaveChanges();             //save changes
        }
        public void NextLapRepo (Game game)                                     //confirm next lap in data base
        {
            _context.Attach(game);                                              //attach the "game" in data base
            _context.Entry(game).Property("CurrentLap").IsModified = true;      //agree changes in "CurrentLap"
            _context.SaveChanges();                                             //save changes
        }
        public void NextRoundRepo(Game game)                                    //confirm next round in data base
        {
            _context.Attach(game);                                              //attach the "game" in data base
            _context.Entry(game).Property("CurrentRound").IsModified = true;    //agree changes in "CurrentRound"
            _context.Entry(game).Property("CurrentLap").IsModified = true;      //agree changes in "CurrentLap"
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true; //agree changes in "CurrentPlayerId"
            _context.SaveChanges();                                             //save changes
        }
        public void NextPlayerRepo(Game game)                                       //confirm next player in dat base
        {
            _context.Attach(game);                                                  //attach the "game" in data base
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;     //agree changes in "CurrentPlayerId"
            _context.Entry(game).Property("CurrentLap").IsModified = true;          //agree changes in "CurrentLap"
            _context.SaveChanges();                                                 //save changes
        }
        public void SaveBlockedDicesRep(Dices dices)                                                        //block chosen dices in data base
        {
            _context.Attach(dices);                                                                         //attach the "game" in data base
            if (dices.Dice1IsBlocked) _context.Entry(dices).Property("Dice1IsBlocked").IsModified = true;   //agree changes in block dice1 in data base if player has chosen it
            if (dices.Dice2IsBlocked) _context.Entry(dices).Property("Dice2IsBlocked").IsModified = true;   //agree changes in block dice2 in data base if player has chosen it
            if (dices.Dice3IsBlocked) _context.Entry(dices).Property("Dice3IsBlocked").IsModified = true;   //agree changes in block dice3 in data base if player has chosen it
            if (dices.Dice4IsBlocked) _context.Entry(dices).Property("Dice4IsBlocked").IsModified = true;   //agree changes in block dice4 in data base if player has chosen it
            if (dices.Dice5IsBlocked) _context.Entry(dices).Property("Dice5IsBlocked").IsModified = true;   //agree changes in block dice5 in data base if player has chosen it
            _context.SaveChanges();                                                                         //save changes
        }
        public IQueryable<PlayerValue> GetPlayerValue(int gameId, int playerId)     //get points of players from the game
        {
            var playerValues = from values in _context.PlayerValues                 //get points...
                               where values.GameId.Equals(gameId)                   //...from the game...
                               where values.PlayerId.Equals(playerId)               //...of players
                               select values;
            return playerValues;
        }
        public void UpdateValuesRep(PlayerValue playerValue, string chooseValue)                    //agree all saving changes related to player selection
        {
            _context.Attach(playerValue);                                                           //attach "playerValue" of the player
            switch (chooseValue)                                                                    //check which value has been chosen by player
            {
                case "Ones":                                                                        //if player has chosen ones...
                    _context.Entry(playerValue).Property("Ones").IsModified = true;                 //...agree changes in Ones
                    _context.Entry(playerValue).Property("OnesIsUsed").IsModified = true;           //agree changes in OneIsUsed
                    _context.Entry(playerValue).Property("Bonus").IsModified = true;                //agree changes in Bonus
                    break;  
                case "Twos":                                                                        //if player has chosen twos...
                    _context.Entry(playerValue).Property("Twos").IsModified = true;                 //...agree changes in Twos
                    _context.Entry(playerValue).Property("TwosIsUsed").IsModified = true;           //agree changes in TwosIsUsed
                    _context.Entry(playerValue).Property("Bonus").IsModified = true;                //agree changes in Bonus
                    break;
                case "Threes":                                                                      //if player has chosen threes...
                    _context.Entry(playerValue).Property("Threes").IsModified = true;               //...agree changes in Threes
                    _context.Entry(playerValue).Property("ThreesIsUsed").IsModified = true;         //agree changes in ThreesIsUsed
                    _context.Entry(playerValue).Property("Bonus").IsModified = true;                //agree changes in Bonus
                    break;
                case "Fours":                                                                       //if player has chosen fours...
                    _context.Entry(playerValue).Property("Fours").IsModified = true;                //...agree changes in Fours
                    _context.Entry(playerValue).Property("FoursIsUsed").IsModified = true;          //agree changes in FoursIsUsed
                    _context.Entry(playerValue).Property("Bonus").IsModified = true;                //agree changes in Bonus
                    break;
                case "Fives":                                                                       //if player has chosen fives...
                    _context.Entry(playerValue).Property("Fives").IsModified = true;                //...agree changes in Fives
                    _context.Entry(playerValue).Property("FivesIsUsed").IsModified = true;          //agree changes in FivesIsUsed
                    _context.Entry(playerValue).Property("Bonus").IsModified = true;                //agree changes in Bonus
                    break;
                case "Sixs":                                                                        //if player has chosen sixs...
                    _context.Entry(playerValue).Property("Sixs").IsModified = true;                 //...agree changes in Sixs
                    _context.Entry(playerValue).Property("SixsIsUsed").IsModified = true;           //agree changes in SixsIsUsed
                    _context.Entry(playerValue).Property("Bonus").IsModified = true;                //agree changes in Bonus
                    break;
                case "Triple":                                                                      //if player has chosen triple...
                    _context.Entry(playerValue).Property("Triple").IsModified = true;               //...agree changes in Triple
                    _context.Entry(playerValue).Property("TripleIsUsed").IsModified = true;         //agree changes in TripleIsUsed
                    break;
                case "Fourfold":                                                                    //if player has chosen fourfold...
                    _context.Entry(playerValue).Property("Fourfold").IsModified = true;             //...agree changes in Fourfold
                    _context.Entry(playerValue).Property("FourfoldIsUsed").IsModified = true;       //agree changes in FourfoldIsUsed
                    break;
                case "Full":                                                                        //if player has chosen full...
                    _context.Entry(playerValue).Property("Full").IsModified = true;                 //...agree changes in Full
                    _context.Entry(playerValue).Property("FullIsUsed").IsModified = true;           //agree changes in FullIsUsed
                    break;
                case "SmallStraight":                                                               //if player has chosen ones small straight...
                    _context.Entry(playerValue).Property("SmallStraight").IsModified = true;        //...agree changes in SmallStraight
                    _context.Entry(playerValue).Property("SmallStraightIsUsed").IsModified = true;  //agree changes in SmallStraightIsUsed
                    break;
                case "HighStraight":                                                                //if player has chosen high straight...
                    _context.Entry(playerValue).Property("HighStraight").IsModified = true;         //...agree changes in HighStraight
                    _context.Entry(playerValue).Property("HighStraightIsUsed").IsModified = true;   //agree changes in HighStraightIsUsed
                    break;
                case "General":                                                                     //if player has chosen general...
                    _context.Entry(playerValue).Property("General").IsModified = true;              //...agree changes in General
                    _context.Entry(playerValue).Property("GeneralIsUsed").IsModified = true;        //agree changes in GeneralIsUsed
                    break; 
                case "Chance":                                                                      //if player has chosen chance...
                    _context.Entry(playerValue).Property("Chance").IsModified = true;               //...agree changes in Chance
                    _context.Entry(playerValue).Property("ChanceIsUsed").IsModified = true;         //agree changes ChanceIsUsed
                    break;
            }
            _context.Entry(playerValue).Property("Total").IsModified = true;                        //agree changes in Total
            _context.SaveChanges();                                                                 //save all changes
        }
        public IQueryable<int> GetPlayerTurnRepo(int gameId, int playerId)      //get current player's turn number in the game (IQueryable)
        {
            var playerTurn = from item in _context.PlayersTurns                 //get current player's turn number...
                           where item.GameId.Equals(gameId)                     //...in the game...
                           where item.PlayerId.Equals(playerId)                 //...for the players
                           select item.TurnNo;                                  //get turn number
            return playerTurn;                                                  //return turn number
        }
        public IQueryable<int> GetPlayerIdFromPlayerTurn(int gameId, int playerTurn)    //get id of current player in the game (IQueryable)
        {
            var playerId = from item in _context.PlayersTurns                           //get id of current player...
                             where item.GameId.Equals(gameId)                           //...in the game
                             where item.TurnNo.Equals(playerTurn)                       //...for the turn number
                             select item.PlayerId;                                      //get player's id
            return playerId;                                                            //get player's id
        }
        public void EndGameRep(int gameId)                                      //set IsActive as "false" in the game
        {
            var game = GetGame(gameId);                                         //get the game from data base
            game.IsActive = false;                                              //set IsActive as "false"
            _context.Entry(game).Property("IsActive").IsModified = true;        //agree change
            _context.SaveChanges();                                             //save changes
        }
    }
}
