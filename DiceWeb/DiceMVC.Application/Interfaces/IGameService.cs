﻿using DiceMVC.Application.ViewModels.Game;
using DiceMVC.Application.ViewModels.Player;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.Interfaces
{
    public interface IGameService
    {
        int AddGame(GetPlayerCountVm game);
       // bool PlayerNoUp(int gameId);
        ListOfSavedGamesVm GetGamesToList();
        void EndingCreate(int gameId);
        Game GetGameById(int gameId);
        PlayerValueVM GetCurrentPlayerValue(int gameId, int playerId);
        List<PlayerScoreVm> GetPlayersScores(int gameId);
    }
}
