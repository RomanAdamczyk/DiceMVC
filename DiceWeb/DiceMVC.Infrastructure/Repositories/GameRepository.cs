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
        private readonly Context _context;
        public GameRepository(Context context)
        {
            _context = context;

        }
        public int AddGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
            return game.Id;
        }
        public Game GetGame(int gameId)
        {
            return _context.Games.FirstOrDefault(p => p.Id == gameId);
        }
        public int UpdateGamePlayerNo(Game game)
        {
            _context.Attach(game);
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;
            _context.SaveChanges();
            return game.CurrentPlayerId;       
        }
        public IQueryable<Game> GetActiveGames()
        {
            return _context.Games.Where(g => g.IsActive);
        }
        public IQueryable<Player> GetPlayersToGame(int idGame)
        {
            var players = from player in _context.Players
                        where player.GamePlayers.Any(pl => pl.GameId == idGame)
                        select player;

            return players;
        }
        public void UpdateEndingCreate(Game game)
        {
            _context.Attach(game);
            _context.Entry(game).Property("IsActive").IsModified = true;
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;
            _context.SaveChanges();
        }
        public IQueryable<int> GetFirstPlayerId(int gameId)
        {
            var playerId = from item in _context.PlayersTurns
                           where item.GameId.Equals(gameId)
                           where item.TurnNo.Equals(0)
                           select item.PlayerId;
            return playerId;
        }
        public IQueryable<Dices> GetDicesRepo(int gameId, int playerId, int round, int lap)
        {
            var previousDices = from dices in _context.Dices
                                where dices.GameId.Equals(gameId)
                                where dices.PlayerId.Equals(playerId)
                                where dices.Round.Equals(round)
                                where dices.Lap.Equals(lap)
                                select dices;
            return previousDices;
        }
        public IQueryable<int> GetLap(int gameId, int playerId, int round)
        {
            var listOfLaps = from dices in _context.Dices
                             where dices.GameId.Equals(gameId)
                             where dices.PlayerId.Equals(playerId)
                             where dices.Round.Equals(round)
                             select dices.Lap;
            return listOfLaps;
        }
        public IQueryable<int> GetPlayersCountRep(int idGame)
        {
            var playersCount = from game in _context.Games
                               where game.Id.Equals(idGame)
                               select game.PlayerCount;
            return playersCount;
        }
        public void SaveDices(Dices dices)
        {
            _context.Dices.Add(dices);
            _context.SaveChanges();
        }
        public void NextLapRepo (Game game)
        {
            _context.Attach(game);
            _context.Entry(game).Property("CurrentLap").IsModified = true;
            _context.SaveChanges();
        }
        public void NextRoundRepo(Game game)
        {
            _context.Attach(game);
            _context.Entry(game).Property("CurrentRound").IsModified = true;
            _context.Entry(game).Property("CurrentLap").IsModified = true;
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;
            _context.SaveChanges();
        }
        public void NextPlayerRepo(Game game)
        {
            _context.Attach(game);
            _context.Entry(game).Property("CurrentPlayerId").IsModified = true;
            _context.Entry(game).Property("CurrentLap").IsModified = true;
            _context.SaveChanges();
        }
        public void SaveBlockedDicesRep(Dices dices)
        {
            _context.Attach(dices);
            if (dices.Dice1IsBlocked) _context.Entry(dices).Property("Dice1IsBlocked").IsModified = true;
            if (dices.Dice2IsBlocked) _context.Entry(dices).Property("Dice2IsBlocked").IsModified = true;
            if (dices.Dice3IsBlocked) _context.Entry(dices).Property("Dice3IsBlocked").IsModified = true;
            if (dices.Dice4IsBlocked) _context.Entry(dices).Property("Dice4IsBlocked").IsModified = true;
            if (dices.Dice5IsBlocked) _context.Entry(dices).Property("Dice5IsBlocked").IsModified = true;
            _context.SaveChanges();
        }
        public IQueryable<PlayerValue> GetPlayerValue(int gameId, int playerId)
        {
            var playerValues = from values in _context.PlayerValues
                               where values.GameId.Equals(gameId)
                               where values.PlayerId.Equals(playerId)
                               select values;
            return playerValues;
        }
        public void UpdateValuesRep(PlayerValue playerValue, string chooseValue)
        {
            _context.Attach(playerValue);
            switch (chooseValue)
            {
                case "Ones":
                    _context.Entry(playerValue).Property("Ones").IsModified = true;
                    _context.Entry(playerValue).Property("OnesIsUsed").IsModified = true;
                    break;
                case "Twos":
                    _context.Entry(playerValue).Property("Twos").IsModified = true;
                    _context.Entry(playerValue).Property("TwosIsUsed").IsModified = true;
                    break;
                case "Threes":
                    _context.Entry(playerValue).Property("Threes").IsModified = true;
                    _context.Entry(playerValue).Property("ThreesIsUsed").IsModified = true;
                    break;
                case "Fours":
                    _context.Entry(playerValue).Property("Fours").IsModified = true;
                    _context.Entry(playerValue).Property("FoursIsUsed").IsModified = true;
                    break;
                case "Fives":
                    _context.Entry(playerValue).Property("Fives").IsModified = true;
                    _context.Entry(playerValue).Property("FivesIsUsed").IsModified = true;
                    break;
                case "Sixs":
                    _context.Entry(playerValue).Property("Sixs").IsModified = true;
                    _context.Entry(playerValue).Property("SixsIsUsed").IsModified = true;
                    break;
                case "Triple":
                    _context.Entry(playerValue).Property("Triple").IsModified = true;
                    _context.Entry(playerValue).Property("TripleIsUsed").IsModified = true;
                    break;
                case "Fourfold":
                    _context.Entry(playerValue).Property("Fourfold").IsModified = true;
                    _context.Entry(playerValue).Property("FourfoldIsUsed").IsModified = true;
                    break;
                case "Full":
                    _context.Entry(playerValue).Property("Full").IsModified = true;
                    _context.Entry(playerValue).Property("FullIsUsed").IsModified = true;
                    break;
                case "SmallStraight":
                    _context.Entry(playerValue).Property("SmallStraight").IsModified = true;
                    _context.Entry(playerValue).Property("SmallStraightIsUsed").IsModified = true;
                    break;
                case "HighStraight":
                    _context.Entry(playerValue).Property("HighStraight").IsModified = true;
                    _context.Entry(playerValue).Property("HighIsUsed").IsModified = true; break;
                case "General":
                    _context.Entry(playerValue).Property("General").IsModified = true;
                    _context.Entry(playerValue).Property("GeneralIsUsed").IsModified = true;
                    break; 
                case "Chance":
                    _context.Entry(playerValue).Property("Chance").IsModified = true;
                    _context.Entry(playerValue).Property("ChanceIsUsed").IsModified = true;
                    break;
            }
            _context.Entry(playerValue).Property("Total").IsModified = true;
            _context.SaveChanges();
        }
        public IQueryable<int> GetPlayerTurnRepo(int gameId, int playerId)
        {
            var playerTurn = from item in _context.PlayersTurns
                           where item.GameId.Equals(gameId)
                           where item.PlayerId.Equals(playerId)
                           select item.TurnNo;
            return playerTurn;
        }
        public IQueryable<int> GetPlayerIdFromPlayerTurn(int gameId, int playerTurn)
        {
            var playerId = from item in _context.PlayersTurns
                             where item.GameId.Equals(gameId)
                             where item.TurnNo.Equals(playerTurn)
                             select item.PlayerId;
            return playerId;
        }
    }
}
