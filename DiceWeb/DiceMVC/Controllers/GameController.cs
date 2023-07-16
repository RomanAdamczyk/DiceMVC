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
                model.Dices.Add(_gameService.GetDices(gameId, model.CurrentPlayer.Id, model.Round, i));
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

            int sum;
            if (model.Dices[model.Lap - 1].Dice1IsBlocked)
            {
                model.Dices[model.Lap].Dice1 = model.Dices[model.Lap - 1].Dice1;
            } else
            {
                model.Dices[model.Lap].Dice1 = generator.Next(1, 7);
            }
            sum = model.Dices[model.Lap].Dice1;
            model.Dices[model.Lap].Dice1ImgPath = "/Images/" + model.Dices[model.Lap].Dice1.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice2IsBlocked)
            {
                model.Dices[model.Lap].Dice2 = model.Dices[model.Lap - 1].Dice2;
            }
            else
            {
                model.Dices[model.Lap].Dice2 = generator.Next(1, 7);
            }
            sum = sum + model.Dices[model.Lap].Dice2;
            model.Dices[model.Lap].Dice2ImgPath = "/Images/" + model.Dices[model.Lap].Dice2.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice3IsBlocked)
            {
                model.Dices[model.Lap].Dice3 = model.Dices[model.Lap - 1].Dice3;
            }
            else
            {
                model.Dices[model.Lap].Dice3 = generator.Next(1, 7);
            }
            sum = sum + model.Dices[model.Lap].Dice3;
            model.Dices[model.Lap].Dice3ImgPath = "/Images/" + model.Dices[model.Lap].Dice3.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice4IsBlocked)
            {
                model.Dices[model.Lap].Dice4 = model.Dices[model.Lap - 1].Dice4;
            }
            else
            {
                model.Dices[model.Lap].Dice4 = generator.Next(1, 7);
            }
            sum = sum + model.Dices[model.Lap].Dice4;
            model.Dices[model.Lap].Dice4ImgPath = "/Images/" + model.Dices[model.Lap].Dice4.ToString() + ".png";
            if (model.Dices[model.Lap - 1].Dice5IsBlocked)
            {
                model.Dices[model.Lap].Dice5 = model.Dices[model.Lap - 1].Dice5;
            }
            else
            {
                model.Dices[model.Lap].Dice5 = generator.Next(1, 7);
            }
            sum = sum + model.Dices[model.Lap].Dice5;
            model.Dices[model.Lap].Dice5ImgPath = "/Images/" + model.Dices[model.Lap].Dice5.ToString() + ".png";
           
            model.OptionalValues = new PlayerValueVM();
            model.OptionalValues.Ones = _gameService.CountValues(model.Dices[model.Lap], 1);
            model.OptionalValues.Twos = _gameService.CountValues(model.Dices[model.Lap], 2) * 2;
            model.OptionalValues.Threes = _gameService.CountValues(model.Dices[model.Lap], 3) * 3;
            model.OptionalValues.Fours = _gameService.CountValues(model.Dices[model.Lap], 4) * 4;
            model.OptionalValues.Fives = _gameService.CountValues(model.Dices[model.Lap], 5) * 5;
            model.OptionalValues.Sixs = _gameService.CountValues(model.Dices[model.Lap], 6) * 6;
            int max = 1;
            for (int i=1;i<=6;i++)
            {
                switch (_gameService.CountValues(model.Dices[model.Lap], i))
                { case 5:
                        model.OptionalValues.General = 50;
                        max = 5;
                        break;
                    case 4:
                        model.OptionalValues.Fourfold = sum;
                        max = 4;
                        break;
                    case 3:
                        model.OptionalValues.Triple = sum;
                        max = 3;
                        for (int j = 1; j <= 6; j++)
                        {
                            if (_gameService.CountValues(model.Dices[model.Lap], j) == 2)
                                model.OptionalValues.Full = 25;
                        }
                        break;
                    case 2:
                        if (max == 1) max = 2;
                        break;
                }
            }
            if (max == 1)
                if (_gameService.CountValues(model.Dices[model.Lap], 1) == 0) model.OptionalValues.HighStraight = 40;
                else if (_gameService.CountValues(model.Dices[model.Lap], 6) == 0) model.OptionalValues.SmallStraight = 30;
          
            _gameService.GetDicesToSave(model.Dices[model.Lap]);
            _gameService.NextLap(model.GameId);

            return View(model);
        }
        [HttpPost]
        public IActionResult GamePlay(PlaygameVm model)
        {
            var dices = _gameService.GetDices(model.GameId, model.CurrentPlayer.Id, model.Round, model.Lap);
            if (model.BlockedDices.Dice1IsBlocked) dices.Dice1IsBlocked = true;
            if (model.BlockedDices.Dice2IsBlocked) dices.Dice2IsBlocked = true;
            if (model.BlockedDices.Dice3IsBlocked) dices.Dice3IsBlocked = true;
            if (model.BlockedDices.Dice4IsBlocked) dices.Dice4IsBlocked = true;
            if (model.BlockedDices.Dice5IsBlocked) dices.Dice5IsBlocked = true;
            _gameService.SaveBlockedDices(dices);

         //   if (model.Lap ==2)
         //   {
         //       _gameService.NextPlayer(model.GameId);
         //   }
            return RedirectToAction("GamePlay", "Game", new { gameId = model.GameId });
        }
        [HttpGet]
        public IActionResult ChooseValue (PlayerValueVM model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult ChooseValue()
        {
            return View();
        }

    }
}
