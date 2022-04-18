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
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        //public IActionResult Index()
        //{
        //    // widok akcji
        //    // tabela z wynikami
        //    var model = _playerService.GetAllPlayersForList();
        //    return View(model);
        //}
        [HttpGet]
        public IActionResult AddNewPlayer(int idGame)
        {
            return View(new NewPlayerVm(idGame));
        }
        [HttpPost]
        public IActionResult AddNewPlayer(NewPlayerVm model)
        {
            var id = _playerService.AddNewPlayer(model);

            if (model.PlayerCount > model.PlayerNo + 1) 
                return RedirectToAction("NewOrLoadPlayer", new {idGame = model.GameId });
            else return RedirectToAction("Game","NewRound");
        }
        [HttpGet]
        public IActionResult NewOrLoadPlayer(int idGame)
        {
            return View(new NewOrLoadPlayerVm(idGame));
        }
        [HttpPost]
        public IActionResult NewOrLoadPlayer(NewOrLoadPlayerVm model)
        {
            var choose = _playerService.NewOrLoadPlayer(model);
            var idGame = _playerService.GetGameId(model);
            switch (choose)
            {
                case "New":
                    return RedirectToAction("AddNewPlayer", new { idGame = idGame });
                    break;
                case "Load":
                    return RedirectToAction("LoadPlayer", new { gameId = idGame });
                    break;
                default:
                    return RedirectToAction("");
            }            
        }
        [HttpGet]
        public IActionResult LoadPlayer(int gameId)
        {
            var model = _playerService.GetPlayersForList();
            model.GameId = gameId;
            return View(model);
        }
        [HttpPost]
        public IActionResult LoadPlayer(ListOfPlayersVm model)
        {
            string choosenPlayer = model.ChoosePlayer;
            model = _playerService.AddPlayerToGame(model);
            if (model.Count > model.PlayerNo)
                return RedirectToAction("NewOrLoadPlayer", new { idGame = model.GameId });
            else return RedirectToAction("Game", "NewRound");
            
        }

        [HttpGet]
        public IActionResult ViewPlayerValues()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult ViewPlayerValues(PlayerValueModel model)
        //{
        //    _playerService.ShowPlayerValues();
        //    return View();
        //}
        public IActionResult ViewPlayerValues(int playerId)
        {
            return View();
        }
    }
}
