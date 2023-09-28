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
        public int AddNewPlayer(NewPlayerVm player)                                   //add new player, create approprate items in data base and get id of the game
        {
            var pl = _mapper.Map<Player>(player);                                     //map game from NewPlayerVm to Player
            var id = _playerRepo.AddPlayer(pl);                                       //add player to database and get its id
            var game = _gameRepo.GetGame(player.GameId);                              //get the game from data base

            player.PlayerCount = game.PlayerCount;                                    //set PlayerCount as count of players from the game
            player.PlayerNo = game.CurrentPlayerId;                                   //set PlayerNo as id of the current player
            var playerValue = new PlayerValue(id, player.GameId);                     //create new PlayerValue and set its GameId and PlayerId
            _playerRepo.AddPlayerValue(playerValue);                                  //add PlayerValue to data base
            var playersTurn = new PlayersTurn(player.GameId, id, player.PlayerNo);    //create new PlayerTurn and set its GameId and PlayerId
            _playerRepo.AddPlayersTurn(playersTurn);                                  //add PlayerTurn to data base
            var gamePlayer = new GamePlayer(player.GameId, pl.Id);                    //create new GamePlayer and set its GameId and PlayerId
            _playerRepo.AddGamePlayer(gamePlayer);                                    //add GamePlayer to data base
            game.CurrentPlayerId += 1;                                                //increase CurrentPlayerId
            _gameRepo.UpdateGamePlayerNo(game);                                       //save Id of the current player in data base
            return id;                                                                //return Id of the game
        }
        public ListOfPlayersVm AddPlayerToGame(ListOfPlayersVm model)                           //add chosen player to the game, create approprate items in data base and get id of the game
        {
            var game = _gameRepo.GetGame(model.GameId);                                         //get the game from data base
            int playerId = Int32.Parse(model.ChoosePlayer);                                     //get chosen player's id and convert to int 
            var playerValue = new PlayerValue(playerId, model.GameId);                          //create new PlayerValue and set its GameId and PlayerId
            _playerRepo.AddPlayerValue(playerValue);                                            //add PlayerValue to data base
            var playersTurn = new PlayersTurn(model.GameId, playerId, game.CurrentPlayerId);    //create new PlayerTurn and set its GameId and PlayerId
            _playerRepo.AddPlayersTurn(playersTurn);                                            //add PlayerTurn to data base
            var gamePlayer = new GamePlayer(model.GameId, playerId);                            //create new GamePlayer and set its GameId and PlayerId
            _playerRepo.AddGamePlayer(gamePlayer);                                              //add GamePlayer to data base
            game.CurrentPlayerId += 1;                                                          //increase CurrentPlayerId
            _gameRepo.UpdateGamePlayerNo(game);                                                 //save Id of the current player in data base
            model.Count = game.PlayerCount;                                                     //get Count as count of players
            model.PlayerNo = game.CurrentPlayerId;                                              //get PlayerNo as id of current player
            return model;                                                                       //return model (ListofPlayersVm)

        }
        public ListOfPlayersVm GetPlayersForList()                                              //get list of all players and convert to ListofPlayersVm
        {
            var players = _playerRepo.GetAllPlayers()                                           //get all players from data base...
                .ProjectTo<NewPlayerVm>(_mapper.ConfigurationProvider).ToList();                //...and convert to List of NewPlayerVm
            var playersList = new ListOfPlayersVm()                                             //create a new ListOfPlayersVm
            {
                Players = players,                                                              //set Players as List of players
                Count = players.Count(),                                                        //set Count as count of players
            };
            return playersList;                                                                 //return playerList
        }
    }
}
