using AutoMapper;
using DiceMVC.Application.Interfaces;
using DiceMVC.Application.ViewModels.Player;
using DiceMVC.Domain.Interface;
using DiceMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly IMapper _mapper;
        public PlayerService(IPlayerRepository playerRepo, IMapper mapper )
        {
            _playerRepo = playerRepo;
            _mapper = mapper;
        }
        public int AddPlayer(NewPlayerVm player)
        {
            var pl = _mapper.Map<Player>(player);
            var id = _playerRepo.AddPlayer(pl);
            var playerValue = AddPlayerValues(id);
            _playerRepo.AddPlayerValue(playerValue);
            return id;
        }
        public PlayerValue AddPlayerValues(int playerId)
        {
            PlayerValue playerValue = new PlayerValue(playerId);
            return playerValue;
        }

        public PlayerValueForListVm ShowPlayerValues(int playerId)
        {
            var player = _playerRepo.GetPlayerValue(playerId);
            var playerVm = _mapper.Map<PlayerValueForListVm>(player);
            return playerVm;
        }
    }
}
