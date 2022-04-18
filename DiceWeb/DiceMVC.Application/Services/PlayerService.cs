using AutoMapper;
using AutoMapper.QueryableExtensions;
using DiceMVC.Application.Interfaces;
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
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepo;
        public PlayerService(IPlayerRepository playerRepo, IGameRepository gameRepo, IMapper mapper )
        {
            _playerRepo = playerRepo;
            _gameRepo = gameRepo;
            _mapper = mapper;
        }
        public int AddNewPlayer(NewPlayerVm player)
        {
            var pl = _mapper.Map<Player>(player);
            var id = _playerRepo.AddPlayer(pl);
            var game = _gameRepo.GetGame(player.GameId);
            //  player = _mapper.Map<NewPlayerVm>(game);

            player.PlayerCount = game.PlayerCount;
            player.PlayerNo = game.CurrentPlayerId;
            var playerValue = new PlayerValue(id, player.GameId);
            _playerRepo.AddPlayerValue(playerValue);
            var playersTurn = new PlayersTurn(player.GameId, id, player.PlayerNo);
            _playerRepo.AddPlayersTurn(playersTurn);
            var gamePlayer = new GamePlayer(player.GameId, pl.Id);
            _playerRepo.AddGamePlayer(gamePlayer);
            game.CurrentPlayerId += 1;
            _gameRepo.UpdateGamePlayerNo(game);
         //   AddPlayerToGame(id, player.GameId);
            return id;
        }
        public ListOfPlayersVm AddPlayerToGame(ListOfPlayersVm model)
        {
            var game = _gameRepo.GetGame(model.GameId);
            int playerId = Int32.Parse(model.ChoosePlayer);
            var playerValue = new PlayerValue(playerId, model.GameId);
            _playerRepo.AddPlayerValue(playerValue);
            var playersTurn = new PlayersTurn(model.GameId, playerId, game.CurrentPlayerId);
            _playerRepo.AddPlayersTurn(playersTurn);
            var gamePlayer = new GamePlayer(model.GameId, playerId);
            _playerRepo.AddGamePlayer(gamePlayer);
            game.CurrentPlayerId += 1;
            _gameRepo.UpdateGamePlayerNo(game);
            model.Count = game.PlayerCount;
            model.PlayerNo = game.CurrentPlayerId;
            return model;

        }
        //public PlayerValue AddPlayerValues(int playerId, int gameId)
        //{
        //    PlayerValue playerValue = new PlayerValue(playerId, gameId);
        //    return playerValue;
        //}
        //public PlayersTurn AddPlayersTurns(int gameId, int playerId, int turnNo)
        //{
        //    PlayersTurn playersTurn = new PlayersTurn(gameId, playerId, turnNo);
        //    return playersTurn;
        //}

        public PlayerValueForListVm ShowPlayerValues(int playerId)
        {
            var player = _playerRepo.GetPlayerValue(playerId);
            var playerVm = _mapper.Map<PlayerValueForListVm>(player);
            return playerVm;
        }
        public string NewOrLoadPlayer(NewOrLoadPlayerVm newOrLoadPlayerVm)
        {
            return newOrLoadPlayerVm.CreateNewPlayer;
        }
        public int GetGameId(NewOrLoadPlayerVm newOrLoadPlayerVm)
        {
            return newOrLoadPlayerVm.IdGame;
        }
        public ListOfPlayersVm GetPlayersForList()
        {
            var players = _playerRepo.GetAllPlayers()
                .ProjectTo<NewPlayerVm>(_mapper.ConfigurationProvider).ToList();
            var playersToShow = players.ToList();
            var playersList = new ListOfPlayersVm()
            {
                Players = playersToShow,
                Count = players.Count(),
            };
            return playersList; 
        }
      
    }
}
