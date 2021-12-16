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
        public PlayerValue GetPlayerValue(int playerId);
      //  public Player GetPlayer(int playerId);
        public IQueryable<PlayerValue> GetAllPlayerValues();
        int AddPlayer(Player player);
        void AddPlayerValue(PlayerValue playerValue);
       
    }
}
