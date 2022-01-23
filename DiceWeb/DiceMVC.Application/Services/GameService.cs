using AutoMapper;
using DiceMVC.Application.Interfaces;
using DiceMVC.Application.ViewModels.Game;
using DiceMVC.Application.ViewModels.Player;
using DiceMVC.Domain.Interface;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.Services
{
    public class GameService: IGameService
    {
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepo;
        public GameService(IGameRepository gameRepo, IMapper mapper)
        {
            _gameRepo = gameRepo;
            _mapper = mapper;
        }
        public int AddGame(GetPlayerCountVm game)
        {
            var gm = _mapper.Map<Game>(game);
            var id = _gameRepo.AddGame(gm);
            return id;
        }
        public bool PlayerNoUp (int gameId)
        {
            var game = _gameRepo.GetGame(gameId);
            game.CurrentPlayerId += 1;
            var playerNo = _gameRepo.UpdateGamePlayerNo(game);
            if (game.PlayerCount > playerNo + 1) return true;
            else return false;

        }

    }
}
