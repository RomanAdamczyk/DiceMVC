using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class NewOrLoadPlayerVm: IMapFrom<DiceMVC.Domain.Model.Game>
    {
        public NewOrLoadPlayerVm()
        {

        }
        public NewOrLoadPlayerVm(int idGame)
        {
            IdGame = idGame;
        }
        public int IdGame { get; set; }
        public int TurnNo { get; set; }
        public string CreateNewPlayer { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.Game, NewOrLoadPlayerVm>();
        }
    }
}
