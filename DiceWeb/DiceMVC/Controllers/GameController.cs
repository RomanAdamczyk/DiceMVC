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
            return View();
        }
    }
}
