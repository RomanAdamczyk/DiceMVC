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
            return View();
        }
        [HttpPost]
        public IActionResult SaveGame(int model)
        {
            return View();
        }
    }
}
