using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class DicesVm:IMapFrom<DiceMVC.Domain.Model.Dices>
    {
        public int Id { get; set; }
        public int Dice1 { get; set; }
        public bool Dice1IsBlocked { get; set; }
        public string Dice1ImgPath { get; set; }
        public int Dice2 { get; set; }
        public bool Dice2IsBlocked { get; set; }
        public string Dice2ImgPath { get; set; }
        public int Dice3 { get; set; }
        public bool Dice3IsBlocked { get; set; }
        public string Dice3ImgPath { get; set; }
        public int Dice4 { get; set; }
        public bool Dice4IsBlocked { get; set; }
        public string Dice4ImgPath { get; set; }
        public int Dice5 { get; set; }
        public bool Dice5IsBlocked { get; set; }
        public string Dice5ImgPath { get; set; }
        public int Lap { get; set; }
        public int Round { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.Dices, DicesVm>();
            profile.CreateMap<DicesVm, DiceMVC.Domain.Model.Dices>().ReverseMap();
        }
    }
}
