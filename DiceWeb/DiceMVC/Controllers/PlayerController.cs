using DiceMVC.Application.Interfaces;
using DiceMVC.Application.ViewModels.Player;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceMVC.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        public PlayerController(IPlayerService playerService, IGameService gameService)
        {
            _playerService = playerService;
            _gameService = gameService;
        }
        [HttpGet]
        public IActionResult AddNewPlayer(int idGame)
        {
            return View(new NewPlayerVm(idGame));               //http with choice new or saved player
        }
        [HttpPost]
        public IActionResult AddNewPlayer(NewPlayerVm model)
        {
            var id = _playerService.AddNewPlayer(model);                                        //add new player, create approprate items in data base and get id of the game

            if (model.PlayerCount > model.PlayerNo + 1)                                         //check if player number is lower than count of player (players numbers start from 0 so it is increased by 1)...
                return RedirectToAction("NewOrLoadPlayer", new {idGame = model.GameId });       //...if player number is lower than html with choice new or saved player
            else                                                                                //...if player number is equal number of players
            {   _gameService.EndingCreate(model.GameId);                                        //create approprate items in data base and set approprate values
                return RedirectToAction("Game", "PlayGame", new { idGame = model.GameId });     //http with the game
            }
        }
        [HttpGet]
        public IActionResult NewOrLoadPlayer(int idGame)
        {
            return View(new NewOrLoadPlayerVm(idGame));                 //http with choice new or saved player
        }
        [HttpPost]
        public IActionResult NewOrLoadPlayer(NewOrLoadPlayerVm model)
        {
            var choose = model.CreateNewPlayer;                                         //set player's choice as "choose"
            var idGame = model.IdGame;                                                  //get the id of the game
            switch (choose)                                                             //check the player's choice...
            {
                case "New":                                                             //...if player's choice is "New"...
                    return RedirectToAction("AddNewPlayer", new { idGame = idGame });   //...http with creating new player
                    break;
                case "Load":                                                            //...if player's choic is "Load"...
                    return RedirectToAction("LoadPlayer", new { gameId = idGame });     ///...http with loading player
                    break;
                default:
                    return RedirectToAction("");
            }            
        }
        [HttpGet]
        public IActionResult LoadPlayer(int gameId)
        {
            var model = _playerService.GetPlayersForList();         //get all players to list
            model.GameId = gameId;                                  //set GameId as id of the game
            return View(model);                                     //http with loading player
        }
        [HttpPost]
        public IActionResult LoadPlayer(ListOfPlayersVm model)
        {
            var choosenPlayer = model.ChoosePlayer;                                         //set "choosenPlayer" as the player's choice
            model = _playerService.AddPlayerToGame(model);                                  //add chosen player to the game
            if (model.Count > model.PlayerNo)                                               //check if player number is lower than count of player...
                return RedirectToAction("NewOrLoadPlayer", new { idGame = model.GameId });  //...if player number is lower than html with choice new or saved player
            else                                                                            //...if player number is equal number of players
            {
                _gameService.EndingCreate(model.GameId);                                    //create approprate items in data base and set approprate values
                return RedirectToAction("GamePlay", "Game", new { gameId = model.GameId }); //http with the game
            }
        }
    }
}
