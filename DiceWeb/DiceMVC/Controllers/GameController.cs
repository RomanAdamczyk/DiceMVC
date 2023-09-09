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
            return View(new GetPlayerCountVm());
        }
        [HttpPost]
        public IActionResult GetPlayersCount(GetPlayerCountVm model)
        {
            var id = _gameService.AddGame(model);
            return RedirectToAction("NewOrLoadPlayer", "Player", new {idGame = id } );
        }  
        [HttpGet]
        public IActionResult LoadGame()
        {
            var model = _gameService.GetGamesToList();
            List<int> listOfIdGames = new List<int>();
            foreach (GetSavedGamesToListVm game in model.Games)
            { listOfIdGames.Add(game.GameId); }
            return View(model);
        }
        [HttpPost]
        public IActionResult LoadGame(ListOfSavedGamesVm model)
        {
            return RedirectToAction("GamePlay", "Game", new { gameId = model.ChooseGame});

        }
        [HttpGet]
        public IActionResult GamePlay(int gameId)
        {

            _gameService.NextLap(gameId);

            return View(_gameService.TurnDices(gameId));
        }
        [HttpPost]
        public IActionResult GamePlay(PlaygameVm model)
        {
            var dices = _gameService.GetDices(model.GameId, model.CurrentPlayer.PlayerId, model.Round, model.Lap);
            if (model.Lap < 3)
            {
                if (model.BlockedDices.Dice1IsBlocked) dices.Dice1IsBlocked = true;
                if (model.BlockedDices.Dice2IsBlocked) dices.Dice2IsBlocked = true;
                if (model.BlockedDices.Dice3IsBlocked) dices.Dice3IsBlocked = true;
                if (model.BlockedDices.Dice4IsBlocked) dices.Dice4IsBlocked = true;
                if (model.BlockedDices.Dice5IsBlocked) dices.Dice5IsBlocked = true;
                _gameService.SaveBlockedDices(dices);
            }
           else
            {
                UpdateValuesVm newValues = new UpdateValuesVm();
                newValues.GameId = model.GameId;
                newValues.PlayerId = model.CurrentPlayer.PlayerId;
                newValues.ChooseValue = model.ChooseValue;
                newValues.CurrentValues = new PlayerValueVM();
                newValues.CurrentValues = _gameService.GetCurrentPlayerValue(model.GameId, model.CurrentPlayer.PlayerId);
                newValues.OptionalValues = new PlayerValueVM();
                newValues.OptionalValues = _gameService.CountOptionalValues(dices);
                _gameService.UpdateValue(newValues);
                model.PlayersCount = _gameService.GetPlayersCount(model.GameId);
                var playerTurn = _gameService.GetPlayerTurn(model.GameId, model.CurrentPlayer.PlayerId);
                if (playerTurn < model.PlayersCount - 1) _gameService.NextPlayer(model.GameId, playerTurn + 1);
                else
                {
                    if (model.Round < 13) _gameService.NextRound(model.GameId);
                    else
                    {//koniec gry
                    }
                }
            }
            return RedirectToAction("GamePlay", "Game", new { gameId = model.GameId });
        }
        [HttpGet]
        public IActionResult EndGame(int gameId)
        {
            return RedirectToAction("Index", "Home");
        }       
    }
}
