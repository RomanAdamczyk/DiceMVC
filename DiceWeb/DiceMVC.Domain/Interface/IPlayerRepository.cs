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
        PlayerValue GetPlayerValue(int playerId);
      //  public Player GetPlayer(int playerId);
        IQueryable<PlayerValue> GetAllPlayerValues();
        int AddPlayer(Player player);
        void AddPlayerValue(PlayerValue playerValue);
        void AddPlayersTurn(PlayersTurn playersTurn);
        void AddGamePlayer(GamePlayer gamePlayer);
        IQueryable<Player> GetAllPlayers();
        
       
    }
}
