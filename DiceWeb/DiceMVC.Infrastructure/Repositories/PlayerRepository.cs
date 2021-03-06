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
        private readonly Context _context;
        public PlayerRepository(Context context)
        {
            _context = context;
        }
        public int AddPlayer(Player player)
        {          
            _context.Players.Add(player);
            _context.SaveChanges();
            return player.Id;
        }
        public void AddPlayerValue(PlayerValue playerValue)
        {
            _context.PlayerValues.Add(playerValue);
            _context.SaveChanges();
        }
        public void AddPlayersTurn(PlayersTurn playersTurn)
        {
            _context.PlayersTurns.Add(playersTurn);
            _context.SaveChanges();
        }
        public PlayerValue GetPlayerValue(int playerId)
        {
            var playerValue = _context.PlayerValues.FirstOrDefault(i => i.Id == playerId);
            return playerValue;
        }
        public IQueryable<PlayerValue> GetAllPlayerValues()
        {
            var playerValues = _context.PlayerValues;
            return playerValues;
        }
        public void AddGamePlayer(GamePlayer gamePlayer)
        {
            _context.GamePlayer.Add(gamePlayer);
            _context.SaveChanges();
        }
        public IQueryable<Player> GetAllPlayers()
        {
            return _context.Players;
        }
    }
}
