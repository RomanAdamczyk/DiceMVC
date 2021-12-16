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
        public IActionResult AddPlayer()
        {
            return View(new NewPlayerVm());
        }
        [HttpPost]
        public IActionResult AddPlayer(NewPlayerVm model)
        {
            var id = _playerService.AddPlayer(model);
            var playerValue = _playerService.AddPlayerValues(id);
            return RedirectToAction("ViewPlayerValues");
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
