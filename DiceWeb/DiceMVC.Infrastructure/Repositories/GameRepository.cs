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
        public List<Player> GetPlayersToGame(int idGame)
        {
            var players = from player in _context.Players
                        where player.GamePlayers.Any(pl => pl.GameId == idGame)
                        select player;

            return players.ToList();
        }

    }
}
