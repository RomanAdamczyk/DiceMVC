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
        IQueryable<Game> GetActiveGames();
        IQueryable<Player> GetPlayersToGame(int idGame);
        void UpdateEndingCreate(Game game);
        IQueryable<int> GetFirstPlayerId(int gameId);
        IQueryable<Dices> GetDicesRepo(int gameId, int playerId, int round, int lap);
        IQueryable<int> GetLap(int gameId, int playerId, int round);
        IQueryable<int> GetPlayersCountRep(int idGame);
        void SaveDices(Dices dices);
        void NextLapRepo(Game game);
        void NextRoundRepo(Game game);
        void NextPlayerRepo(Game game);
        void SaveBlockedDicesRep(Dices dices);
        void UpdateValuesRep(PlayerValue playerValue, string chooseValue);
        IQueryable<PlayerValue> GetPlayerValue(int gameId, int playerId);
        IQueryable<int> GetPlayerTurnRepo(int gameId, int playerId);
        IQueryable<int> GetPlayerIdFromPlayerTurn(int gameid, int playerTurn);
    }
}
