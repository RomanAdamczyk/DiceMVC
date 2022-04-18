using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class ListOfPlayersVm: IMapFrom<DiceMVC.Domain.Model.Game>
    {
        public ListOfPlayersVm()
        {

        }
        public ListOfPlayersVm(int gameId)
        {
            GameId = gameId;
        }
        public List<NewPlayerVm> Players { get; set; }
        public int Count { get; set; }
        public string ChoosePlayer { get; set; }
        public int GameId { get; set; }
        public int PlayerNo { get; set; }

    }
}
