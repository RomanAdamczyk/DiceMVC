using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class PlayerValueForListVm : IMapFrom<DiceMVC.Domain.Model.PlayerValue>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ones { get; set; }
        public int Twos { get; set; }
        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public int Sixs { get; set; }
        public int Bonus { get; set; }
        public int Triple { get; set; }
        public int Fourfold { get; set; }
        public int Full { get; set; }
        public int SmallStraight { get; set; }
        public int HighStraight { get; set; }
        public int General { get; set; }
        public int Chance { get; set; }
        public int Total { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.PlayerValue, PlayerValueForListVm>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Player.Name))
                .ForMember(s => s.Total, opt => opt.MapFrom(d => 0));
        }

    }
}
