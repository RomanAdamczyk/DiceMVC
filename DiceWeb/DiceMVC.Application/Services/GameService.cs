using AutoMapper;
using AutoMapper.QueryableExtensions;
using DiceMVC.Application.Interfaces;
using DiceMVC.Application.ViewModels.Game;
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
    public class GameService: IGameService
    {
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepo;
        private readonly IPlayerRepository _playerRepo; 
        public GameService(IGameRepository gameRepo, IPlayerRepository playerRepo, IMapper mapper)
        {
            _gameRepo = gameRepo;
            _playerRepo = playerRepo;
            _mapper = mapper;
        }
        public int AddGame(GetPlayerCountVm game)
        {
            var gm = _mapper.Map<Game>(game);
            var id = _gameRepo.AddGame(gm);
            return id;
        }
 /*       public bool PlayerNoUp (int gameId)
        {
            var game = _gameRepo.GetGame(gameId);
            game.CurrentPlayerId += 1;
            var playerNo = _gameRepo.UpdateGamePlayerNo(game);
            if (game.PlayerCount > playerNo + 1) return true;
            else return false;

        }*/
        public ListOfSavedGamesVm GetGamesToList()
        {

            var games = _gameRepo.GetActiveGames()
                .ProjectTo<GetSavedGamesToListVm>(_mapper.ConfigurationProvider).ToList();
            List<int> idGames = new List<int>();
            ListOfSavedGamesVm gameList = new ListOfSavedGamesVm();
            foreach (GetSavedGamesToListVm game in games)
                { var players = _gameRepo.GetPlayersToGame(game.GameId)
                    .ProjectTo<NewPlayerVm>(_mapper.ConfigurationProvider).ToList();
                game.Players = players;
            }

            gameList.Games = games;
            gameList.Count = games.Count;
            
            return gameList;
        }
        public void EndingCreate (int gameId)
        {
            var game = _gameRepo.GetGame(gameId);
            game.IsActive = true;
            var playerId = _gameRepo.GetFirstPlayerId(gameId).FirstOrDefault();
            game.CurrentPlayerId = playerId;
            _gameRepo.UpdateEndingCreate(game);
        }
        public Game GetGameById(int gameId)
        {
            var game = _gameRepo.GetGame(gameId);
            return game;
        }
        public PlayerValueVM GetCurrentPlayerValue(int gameId, int playerId)
        {
            var playerValue = _playerRepo.GetPlayerValue(gameId, playerId)
                .ProjectTo<PlayerValueVM>(_mapper.ConfigurationProvider).AsQueryable().Single();
            return playerValue;
                
        }
        public List<PlayerScoreVm> GetPlayersScores(int gameId)
        {
            var playersScores = _playerRepo.GetAllPlayersValues(gameId)
                .ProjectTo<PlayerScoreVm>(_mapper.ConfigurationProvider).ToList();
            return playersScores;
        }
        public DicesVm GetDices(int gameId, int playerId, int round, int lap)
        {
            var dices = _gameRepo.GetDicesRepo(gameId, playerId, round, lap)
                .ProjectTo<DicesVm>(_mapper.ConfigurationProvider).AsQueryable().Single();
            return dices;
        }

        public int CountValues(DicesVm dices, int value)
        {
            int sum = 0;
            if (dices.Dice1 == value) sum++;
            if (dices.Dice2 == value) sum++;
            if (dices.Dice3 == value) sum++;
            if (dices.Dice4 == value) sum++;
            if (dices.Dice5 == value) sum++;
            return sum;
        }
        public void GetDicesToSave(DicesVm dices)
        {
            var dicesToSave = _mapper.Map<Dices>(dices);
            _gameRepo.SaveDices(dicesToSave);
        }
        public void NextLap(int gameId)
        {
            var game = GetGameById(gameId);
            game.CurrentLap++;
            _gameRepo.NextLapRepo(game);
        }
        public void NextPlayer (int gameId)
        {
            var game = GetGameById(gameId);//tylko początek
        }
        public void SaveBlockedDices(DicesVm dices)
        {
            var dicesToSave = _mapper.Map<Dices>(dices);
            _gameRepo.SaveBlockedDicesRep(dicesToSave);
        }
        public PlayerValueVM CountOptionalValues (DicesVm dices)
        {
            int sum = dices.Dice1 + dices.Dice2 + dices.Dice3 + dices.Dice4 + dices.Dice5;
            var optionalValues = new PlayerValueVM();
            optionalValues.Ones = CountValues(dices, 1);
            optionalValues.Twos = CountValues(dices, 2) * 2;
            optionalValues.Threes = CountValues(dices, 3) * 3;
            optionalValues.Fours = CountValues(dices, 4) * 4;
            optionalValues.Fives = CountValues(dices, 5) * 5;
            optionalValues.Sixs = CountValues(dices, 6) * 6;
            int max = 1;
            for (int i = 1; i <= 6; i++)
            {
                switch (CountValues(dices, i))
                {
                    case 5:
                        optionalValues.General = 50;
                        max = 5;
                        break;
                    case 4:
                        optionalValues.Fourfold = sum;
                        max = 4;
                        break;
                    case 3:
                        optionalValues.Triple = sum;
                        max = 3;
                        for (int j = 1; j <= 6; j++)
                        {
                            if (CountValues(dices, j) == 2)
                                optionalValues.Full = 25;
                        }
                        break;
                    case 2:
                        if (max == 1) max = 2;
                        break;
                }
            }
            if (max == 1)
                if (CountValues(dices, 1) == 0) optionalValues.HighStraight = 40;
                else if (CountValues(dices, 6) == 0) optionalValues.SmallStraight = 30;
            return optionalValues;
        }
        public void UpdateValue(UpdateValuesVm playerValues)
        {
            var values = _gameRepo.GetPlayerValue(playerValues.GameId, playerValues.PlayerId).Single();
            switch (playerValues.ChooseValue)
            {
                case "Ones":
                    values.Ones = playerValues.OptionalValues.Ones;
                    values.OnesIsUsed = true;
                    values.Total += playerValues.CurrentValues.Ones;
                    break;
                case "Twos":
                    values.Twos = playerValues.OptionalValues.Twos;
                    values.TwosIsUsed = true;
                    values.Total += playerValues.CurrentValues.Twos;
                    break;
                case "Threes":
                    values.Threes = playerValues.OptionalValues.Threes;
                    values.ThreesIsUsed = true;
                    values.Total += playerValues.CurrentValues.Threes;
                    break;
                case "Fours":
                    values.Fours = playerValues.OptionalValues.Fours;
                    values.FoursIsUsed = true;
                    values.Total += playerValues.CurrentValues.Fours;
                    break;
                case "Fives":
                    values.Fives= playerValues.OptionalValues.Fives;
                    values.FivesIsUsed = true;
                    values.Total += playerValues.CurrentValues.Fives;
                    break;
                case "Sixs":
                    values.Sixs= playerValues.OptionalValues.Sixs;
                    values.SixsIsUsed = true;
                    values.Total += playerValues.CurrentValues.Sixs;
                    break;
                case "Triple":
                    values.Triple= playerValues.OptionalValues.Triple;
                    values.TripleIsUsed= true;
                    values.Total += playerValues.CurrentValues.Triple;
                    break;
                case "Fourfold":
                    values.Fourfold = playerValues.OptionalValues.Fourfold;
                    values.FourfoldIsUsed = true;
                    values.Total += playerValues.CurrentValues.Fourfold;
                    break;
                case "Full":
                    values.Full = playerValues.OptionalValues.Full;
                    values.FullIsUsed = true;
                    values.Total += playerValues.CurrentValues.Full;
                    break;
                case "SmallStraight":
                    values.SmallStraight = playerValues.OptionalValues.SmallStraight;
                    values.SmallStraightIsUsed = true;
                    values.Total += playerValues.CurrentValues.SmallStraight;
                    break;
                case "HighStraight":
                    values.HighStraight = playerValues.OptionalValues.HighStraight;
                    values.HighStraightIsUsed = true;
                    values.Total += playerValues.CurrentValues.HighStraight;
                    break;
                case "General":
                    values.General = playerValues.OptionalValues.General;
                    values.GeneralIsUsed= true;
                    values.Total += playerValues.CurrentValues.General;
                    break;
                case "Chance":
                    values.Chance = playerValues.OptionalValues.Chance;
                    values.ChanceIsUsed= true;
                    values.Total += playerValues.CurrentValues.Chance;
                    break;
            }
            
        }

    }
}
