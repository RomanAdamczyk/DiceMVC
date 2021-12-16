using AutoMapper;
using DiceMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class NewPlayerVm:IMapFrom<DiceMVC.Domain.Model.Player>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPlayerVm, DiceMVC.Domain.Model.Player>();
        }
    }
}
