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
            
            PlaygameVm model = new PlaygameVm() { GameId = gameId };
            _gameService.NextLap(model.GameId);
            var game = _gameService.GetGameById(gameId);
            model.CurrentPlayer = _gameService.GetCurrentPlayerValue(gameId, game.CurrentPlayerId);
            model.Players = _gameService.GetPlayersScores(gameId);
            model.Round = game.CurrentRound;
            model.Lap = game.CurrentLap;
            model.Dices = new List<DicesVm>();
            model.Dices.Add(new DicesVm());
            model.Dices[0].Dice1IsBlocked = false;
            model.Dices[0].Dice2IsBlocked = false;
            model.Dices[0].Dice3IsBlocked = false;
            model.Dices[0].Dice4IsBlocked = false;
            model.Dices[0].Dice5IsBlocked = false;
            for (int i=1;i<model.Lap;i++)
            {
                model.Dices.Add(_gameService.GetDices(gameId, model.CurrentPlayer.PlayerId, model.Round, i));
                model.Dices[i].Dice1ImgPath = "/Images/" + model.Dices[i].Dice1.ToString() + ".png";
                model.Dices[i].Dice2ImgPath = "/Images/" + model.Dices[i].Dice2.ToString() + ".png";
                model.Dices[i].Dice3ImgPath = "/Images/" + model.Dices[i].Dice3.ToString() + ".png";
                model.Dices[i].Dice4ImgPath = "/Images/" + model.Dices[i].Dice4.ToString() + ".png";
                model.Dices[i].Dice5ImgPath = "/Images/" + model.Dices[i].Dice5.ToString() + ".png";
            }
            model.Dices.Add( new DicesVm());

            model.Dices[model.Lap].GameId = gameId;
            model.Dices[model.Lap].Lap = model.Lap;
            model.Dices[model.Lap].Round = model.Round;
            model.Dices[model.Lap].PlayerId = model.CurrentPlayer.PlayerId;

            Random generator = new Random();

            if (model.Dices[model.Lap - 1].Dice1IsBlocked)
            {
                model.Dices[model.Lap].Dice1 = model.Dices[model.Lap - 1].Dice1;
            } else
            {
                model.Dices[model.Lap].Dice1 = generator.Next(1, 7);
            }
            model.Dices[model.Lap].Dice1ImgPath = "/Images/" + model.Dices[model.Lap].Dice1.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice2IsBlocked)
            {
                model.Dices[model.Lap].Dice2 = model.Dices[model.Lap - 1].Dice2;
            }
            else
            {
                model.Dices[model.Lap].Dice2 = generator.Next(1, 7);
            }
            model.Dices[model.Lap].Dice2ImgPath = "/Images/" + model.Dices[model.Lap].Dice2.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice3IsBlocked)
            {
                model.Dices[model.Lap].Dice3 = model.Dices[model.Lap - 1].Dice3;
            }
            else
            {
                model.Dices[model.Lap].Dice3 = generator.Next(1, 7);
            }
            model.Dices[model.Lap].Dice3ImgPath = "/Images/" + model.Dices[model.Lap].Dice3.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice4IsBlocked)
            {
                model.Dices[model.Lap].Dice4 = model.Dices[model.Lap - 1].Dice4;
            }
            else
            {
                model.Dices[model.Lap].Dice4 = generator.Next(1, 7);
            }
            model.Dices[model.Lap].Dice4ImgPath = "/Images/" + model.Dices[model.Lap].Dice4.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice5IsBlocked)
            {
                model.Dices[model.Lap].Dice5 = model.Dices[model.Lap - 1].Dice5;
            }
            else
            {
                model.Dices[model.Lap].Dice5 = generator.Next(1, 7);
            }
            model.Dices[model.Lap].Dice5ImgPath = "/Images/" + model.Dices[model.Lap].Dice5.ToString() + ".png";
            model.OptionalValues = new PlayerValueVM();
            model.OptionalValues =  _gameService.CountOptionalValues(model.Dices[model.Lap]);                    
            _gameService.GetDicesToSave(model.Dices[model.Lap]);

            return View(model);
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
                return RedirectToAction("ChooseValue", "Game", new { gameId = model.GameId });
            }
            return RedirectToAction("GamePlay", "Game", new { gameId = model.GameId });
        }
        [HttpGet]
        public IActionResult ChooseValue (int gameId)
        {
            PlaygameVm model = new PlaygameVm() { GameId = gameId };
            var game = _gameService.GetGameById(gameId);
            model.CurrentPlayer = _gameService.GetCurrentPlayerValue(gameId, game.CurrentPlayerId);
            model.Players = _gameService.GetPlayersScores(gameId);
            model.Round = game.CurrentRound;
            model.Lap = game.CurrentLap;
            var dices = _gameService.GetDices(model.GameId, model.CurrentPlayer.PlayerId, model.Round, 3);
            model.Dices = new List<DicesVm>();
            model.Dices.Add(dices);
            model.OptionalValues = new PlayerValueVM();
            _gameService.CountOptionalValues(dices);
            return View(model);
        }
        [HttpPost]
        public IActionResult ChooseValue(PlaygameVm model)
        {
            UpdateValuesVm newValues = new UpdateValuesVm();
            newValues.GameId = model.GameId;
            newValues.PlayerId = model.CurrentPlayer.PlayerId;
            newValues.ChooseValue = model.ChooseValue;
            newValues.CurrentValues = new PlayerValueVM();
            newValues.CurrentValues = model.CurrentPlayer;
            newValues.OptionalValues = new PlayerValueVM();
            newValues.OptionalValues = model.OptionalValues;

            _gameService.UpdateValue(newValues);
            return View();
        }


    }
}
