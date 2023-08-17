using AutoMapper;
using DiceMVC.Application.Mapping;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class GetPlayerCountVm: IMapFrom<DiceMVC.Domain.Model.Game>
    {
        public int PlayerCount { get; set; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<GetPlayerCountVm, DiceMVC.Domain.Model.Game>()
                .ForMember(s => s.CurrentPlayerId, opt => opt.MapFrom(d => 0))
                .ForMember(s => s.CurrentRound, opt => opt.MapFrom(d => 1))
                .ForMember(s => s.CurrentLap, opt => opt.MapFrom(d => 0))
                .ForMember(s => s.IsActive, opt => opt.MapFrom(d => false));
        }
        public class GetPlayerCountValidator : AbstractValidator<GetPlayerCountVm>
        {
            public GetPlayerCountValidator()
            {
                RuleFor(x => x.PlayerCount).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(20);
            }

        }
    }
}
