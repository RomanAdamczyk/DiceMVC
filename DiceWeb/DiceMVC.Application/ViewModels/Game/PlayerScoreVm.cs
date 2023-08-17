using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class PlayerScoreVm: IMapFrom<DiceMVC.Domain.Model.PlayerValue>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        //public int PlayerNo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.PlayerValue, PlayerScoreVm>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Player.Name));
        }


    }
}
