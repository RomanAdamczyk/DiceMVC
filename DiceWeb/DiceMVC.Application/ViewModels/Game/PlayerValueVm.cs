using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class PlayerValueVM : IMapFrom<DiceMVC.Domain.Model.PlayerValue>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ones { get; set; }
        public bool OnesIsUsed { get; set; }
        public int Twos { get; set; }
        public bool TwosIsUsed { get; set; }
        public int Threes { get; set; }
        public bool ThreesIsUsed { get; set; }
        public int Fours { get; set; }
        public bool FoursIsUsed { get; set; }
        public int Fives { get; set; }
        public bool FivesIsUsed { get; set; }
        public int Sixs { get; set; }
        public bool SixsIsUsed { get; set; }
        public int Bonus { get; set; }
        public int Triple { get; set; }
        public bool TripleIsUsed { get; set; }
        public int Fourfold { get; set; }
        public bool FourFoldIsUsed { get; set; }
        public int Full { get; set; }
        public bool FullIsUsed { get; set; }
        public int SmallStraight { get; set; }
        public bool SmallStraightIsUsed { get; set; }
        public int HighStraight { get; set; }
        public bool HighStraightIsUsed { get; set; }
        public int General { get; set; }
        public bool GeneralIsUsed { get; set; }
        public int Chance { get; set; }
        public bool ChanceIsUsed { get; set; }
        public int Total { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.PlayerValue, PlayerValueVM>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Player.Name))
                .ForMember(s => s.Total, opt => opt.MapFrom(d => 0));
        }

    }
}
