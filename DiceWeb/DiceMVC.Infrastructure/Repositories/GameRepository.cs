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
        public int GetFirstPlayerId(int gameId)
        {
            var item = _context.GamePlayer.FirstOrDefault(p => p.GameId == gameId);
            return item.PlayerId;
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
    }
}
