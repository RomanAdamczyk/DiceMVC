using AutoMapper;
using DiceMVC.Application.Mapping;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class NewPlayerVm:IMapFrom<DiceMVC.Domain.Model.Player>
    {
        public NewPlayerVm()
        {

        }
        public NewPlayerVm(int gameId)
        {
            GameId = gameId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public int PlayerNo { get; set; }
        public int PlayerCount { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiceMVC.Domain.Model.Game, NewPlayerVm>()
                .ForMember(s => s.PlayerNo, opt => opt.MapFrom (d => d.CurrentPlayerId));
            profile.CreateMap<NewPlayerVm, DiceMVC.Domain.Model.Player>().ReverseMap();

        }
    }
    public class NewPlayerValidator: AbstractValidator<NewPlayerVm> 
    {
        public NewPlayerValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().MaximumLength(20);
        }
    }
}
