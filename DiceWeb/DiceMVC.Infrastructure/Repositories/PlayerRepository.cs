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
        public PlayerValue GetPlayerValueById(int playerId)
        {
            var playerValue = _context.PlayerValues.FirstOrDefault(i => i.Id == playerId);
            return playerValue;
        }
        public IQueryable<PlayerValue> GetAllPlayerValues()
        {
            var playerValues = _context.PlayerValues;
            return playerValues;
        }
    }
}
