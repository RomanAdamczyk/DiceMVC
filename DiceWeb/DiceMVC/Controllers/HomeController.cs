using DiceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DiceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
         public IActionResult ShowDices()
        {
            var game = new Game()
            {
                Dices = new int[5, 2]
            };
            var generator = new Random();
            for (int i = 0; i < 5; i++)
            {
                game.Dices[i, 0] = generator.Next(1, 7);
            }
            return View(game);
        }
        public IActionResult ShowPlayerValue()
        {
            var player = new Player() {
                Name = "Romek"
            };
            player.FreeValues = new Dictionary<string, bool>();
            player.FreeValues.Add("Ones", false);
            player.FreeValues.Add("Twos", true);
            player.FreeValues.Add("Threes", true);
            player.FreeValues.Add("Fours", true);
            player.FreeValues.Add("Fives", true);
            player.FreeValues.Add("Sixs", false);
            player.FreeValues.Add("Bonus", true);
            player.FreeValues.Add("Triple", true);
            player.FreeValues.Add("Fourfold", true);
            player.FreeValues.Add("Full", false);
            player.FreeValues.Add("SmallStraight", false);
            player.FreeValues.Add("HighStraight", false);
            player.FreeValues.Add("General", false);
            player.FreeValues.Add("Chance", false);
            player.FreeValues.Add("Total", true);

            player.Values = new Dictionary<string, int>();
            player.Values.Add("Ones", 0);
            player.Values.Add("Twos", 8);
            player.Values.Add("Threes", 9);
            player.Values.Add("Fours", 8);
            player.Values.Add("Fives", 25);
            player.Values.Add("Sixs", 0);
            player.Values.Add("Bonus", 0);
            player.Values.Add("Triple", 17);
            player.Values.Add("Fourfold", 18);
            player.Values.Add("Full", 0);
            player.Values.Add("SmallStraight", 0);
            player.Values.Add("HighStraight", 0);
            player.Values.Add("General", 0);
            player.Values.Add("Chance", 0);
            int total = 0;
            foreach(var item in player.Values)
            {
                total += item.Value;
            }
            player.Values.Add("Total", total);

            return View(player);
        }


    }
}
