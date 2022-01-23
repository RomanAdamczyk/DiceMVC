using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Domain.Interface
{
    public interface IGameRepository
    {
        int AddGame(Game game);
        Game GetGame(int gameId);
        int UpdateGamePlayerNo(Game game);
        
    }
}
