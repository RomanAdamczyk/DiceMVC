using AutoMapper;
using DiceMVC.Application.Mapping;
using DiceMVC.Application.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class GetSavedGamesToListVm: IMapFrom<DiceMVC.Domain.Model.Game>
    {
        public int GameId { get; set; }
        public List<NewPlayerVm> Players { get; set; }
        public int PlayerCount { get; set; }
        public int CurrentRound { get; set; }
        public string ChooseGame { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.Game, GetSavedGamesToListVm>()
                .ForMember(s => s.GameId, opt => opt.MapFrom(d => d.Id))
                .ForMember(s => s.ChooseGame, opt => opt.Ignore())
                .ForMember(s => s.Players, opt => opt.Ignore());
        }
    }
}
