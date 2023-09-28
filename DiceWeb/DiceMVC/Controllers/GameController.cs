using DiceMVC.Application.Interfaces;
using DiceMVC.Application.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceMVC.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public IActionResult GetPlayersCount()
        {
            return View(new GetPlayerCountVm());        //http with selecting the number of player
        }
        [HttpPost]
        public IActionResult GetPlayersCount(GetPlayerCountVm model)
        {
            var id = _gameService.AddGame(model);                                       //add game to data base and get its id
            return RedirectToAction("NewOrLoadPlayer", "Player", new { idGame = id });  //http with selecting new or saved player
        }
        [HttpGet]
        public IActionResult LoadGame()
        {
            var model = _gameService.GetGamesToList();              //set all active games
            List<int> listOfIdGames = new List<int>();              //create new List of int
            foreach (GetSavedGamesToListVm game in model.Games)     //for all games...
            { listOfIdGames.Add(game.GameId); }                     //...add its id to list
            return View(model);                                     //http with selecting game
        }
        [HttpPost]
        public IActionResult LoadGame(ListOfSavedGamesVm model)
        {
            return RedirectToAction("GamePlay", "Game", new { gameId = model.ChooseGame });  //http with game
        }
        [HttpGet]
        public IActionResult GamePlay(int gameId)
        {
            _gameService.NextLap(gameId);                   //increase lap
            return View(_gameService.TurnDices(gameId));    //http with game
        }
        [HttpPost]
        public IActionResult GamePlay(PlaygameVm model)
        {
            var dices = _gameService.GetDices(model.GameId, model.CurrentPlayer.PlayerId, model.Round, model.Lap);          //get all dices from the game from the round from the player  
            if (model.Lap < 3)                                                                                              //if lap <3
            {
                if (model.BlockedDices.Dice1IsBlocked) dices.Dice1IsBlocked = true;                                         //if dice1 has blocked set Dice1IsBlocked as true   
                if (model.BlockedDices.Dice2IsBlocked) dices.Dice2IsBlocked = true;                                         //if dice2 has blocked set Dice1IsBlocked as true
                if (model.BlockedDices.Dice3IsBlocked) dices.Dice3IsBlocked = true;                                         //if dice3 has blocked set Dice1IsBlocked as true
                if (model.BlockedDices.Dice4IsBlocked) dices.Dice4IsBlocked = true;                                         //if dice4 has blocked set Dice1IsBlocked as true
                if (model.BlockedDices.Dice5IsBlocked) dices.Dice5IsBlocked = true;                                         //if dice5 has blocked set Dice1IsBlocked as true
                _gameService.SaveBlockedDices(dices);                                                                       //save changes
            }
            else                                                                                                             //if lap is equal 3
            {
                UpdateValuesVm newValues = new UpdateValuesVm();                                                            //create new UpdateValuesVm
                newValues.GameId = model.GameId;                                                                            //set GameId as id of the game
                newValues.PlayerId = model.CurrentPlayer.PlayerId;                                                          //set PlayerId as id uf the current player
                newValues.ChooseValue = model.ChooseValue;                                                                  //set ChooseValue as value selected by the player
                newValues.CurrentValues = new PlayerValueVM();                                                              //create new PlayerValueVM
                newValues.CurrentValues = _gameService.GetCurrentPlayerValue(model.GameId, model.CurrentPlayer.PlayerId);   //set CurrenValues as the scores of the palyer
                newValues.OptionalValues = new PlayerValueVM();                                                             //create PlayerValueVM
                newValues.OptionalValues = _gameService.CountOptionalValues(dices);                                         //set OptionalValues as scores which can be ganed with current dices
                _gameService.UpdateValue(newValues);                                                                        //add chosen value to all points of player and blocked appropriate field
                model.PlayersCount = _gameService.GetPlayersCount(model.GameId);                                            //set PlayersCount as count of players
                var playerTurn = _gameService.GetPlayerTurn(model.GameId, model.CurrentPlayer.PlayerId);                    //get the player's number 
                if (playerTurn < model.PlayersCount - 1) _gameService.NextPlayer(model.GameId, playerTurn + 1);             //if the player is not the last player chnange the current player
                else                                                                                                        //if the player is the last player...
                {
                    if (model.Round < 13) _gameService.NextRound(model.GameId);                                             //... id the round is not the last round
                    else
                    {
                        return RedirectToAction("EndGame", "Game", new { gameId = model.GameId });                          //http with ending ranking
                    }
                }
            }
            return RedirectToAction("GamePlay", "Game", new { gameId = model.GameId });                                     //http with game
        }
        [HttpGet]
        public IActionResult EndGame(int gameId)
        {
            var values = new List<PlayerScoreVm>();                                                             //create new UpdateValuesVm
            values = _gameService.GetPlayersScores(gameId);                                                     //get scores of all players
            values = _gameService.WhichPlace(values);                                                           //set the place numbers of players
            _gameService.EndGame(gameId);                                                                       //set IsActive as "false" in the game
            return View(values);                                                                                //http with ending ranking
        }
        [HttpPost]
        public IActionResult EndGame(List<PlayerScoreVm> players)                                               //http with menu 
        {
            return RedirectToAction("Index", "Home");                                                           //http with menu 
        }
        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }
       // [HttpPost]
       // public IActionResult Information()
       // {
       //     return RedirectToAction("Index", "Home");
       // }
    }
}
