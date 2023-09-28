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
        public int AddGame(GetPlayerCountVm game)   //add game to database and take its ID
        {
            var gm = _mapper.Map<Game>(game);       //map game from GetPlayerCountVm to Game
            var id = _gameRepo.AddGame(gm);         //add game to database and get ID
            return id;                              //return id of saved game
        }
        public ListOfSavedGamesVm GetGamesToList()                                          //get active games and their players from database
        {

            var games = _gameRepo.GetActiveGames()
                .ProjectTo<GetSavedGamesToListVm>(_mapper.ConfigurationProvider).ToList();  //get active games from database and map them to list
            ListOfSavedGamesVm gameList = new ListOfSavedGamesVm();                         //create new ListofGamesVm
            foreach (GetSavedGamesToListVm game in games)                                   //get players from all acvive games
                { var players = _gameRepo.GetPlayersToGame(game.GameId)
                    .ProjectTo<NewPlayerVm>(_mapper.ConfigurationProvider).ToList();        //get players from the game with GameId
                game.Players = players;                                                     //set players to variable "game"
            }

            gameList.Games = games;                                                         //set active games to object "gameList"
            gameList.Count = games.Count;                                                   //set count of acvtive games to object "gameList"
            
            return gameList;                                                                //return obcject gameList
        }
        public void EndingCreate (int gameId)                                       //prepare game to play (get game as active, set first player)
        {
            var game = _gameRepo.GetGame(gameId);                                   //get game with gameId from database
            game.IsActive = true;                                                   //set game as active
            var playerId = _gameRepo.GetFirstPlayerId(gameId).FirstOrDefault();     //get first ID of first player
            game.CurrentPlayerId = playerId;                                        //set ID of first player as current player
            _gameRepo.UpdateEndingCreate(game);                                     //save all changes in database
        }
        public Game GetGameById(int gameId)                             //get game with gameId from database
        {
            var game = _gameRepo.GetGame(gameId);                       //get game with gameId from database
            return game;                                                //return game
        }
        public PlayerValueVM GetCurrentPlayerValue(int gameId, int playerId)                        //get the player score from the game and convert to PlayerValueVm
        {
            var playerValue = _playerRepo.GetPlayerValue(gameId, playerId)
                .ProjectTo<PlayerValueVM>(_mapper.ConfigurationProvider).AsQueryable().Single();    //get the player score from the game and convert to PlayerValueVm
            return playerValue;                                                                     //return the player score
                
        }
        public List<PlayerScoreVm> GetPlayersScores(int gameId)                         //get players scores from the game and convert to List of PlayerScoreVm
        {
            var playersScores = _playerRepo.GetAllPlayersValues(gameId)
                .ProjectTo<PlayerScoreVm>(_mapper.ConfigurationProvider).ToList();      //get players scores from the game and convert to List of PlayerScoreVm
            return playersScores;                                                       //return the player score
        }
        public DicesVm GetDices(int gameId, int playerId, int round, int lap)              //get dices from the lap of the round of the player of the game and convert to DiceVm
        {
            var dices = _gameRepo.GetDicesRepo(gameId, playerId, round, lap)
                .ProjectTo<DicesVm>(_mapper.ConfigurationProvider).AsQueryable().Single(); //get dices from the lap of the round of the player of the game and convert to DiceVm
            return dices;                                                                  //return dices
        }

        public int CountValues(DicesVm dices, int value)    //count how many "value" repeats in "dices"
        {
            int sum = 0;                                    //set "sum" as 0, in next steps sum will be increased with valuse of dices
            if (dices.Dice1 == value) sum++;                //if "Dice1" is "value" increase "sum"
            if (dices.Dice2 == value) sum++;                //if "Dice2" is "value" increase "sum"
            if (dices.Dice3 == value) sum++;                //if "Dice3" is "value" increase "sum"
            if (dices.Dice4 == value) sum++;                //if "Dice4" is "value" increase "sum"
            if (dices.Dice5 == value) sum++;                //if "Dice5" is "value" increase "sum"
            return sum;                                     //return "sum"
        }
        public void GetDicesToSave(DicesVm dices)           //concert "dices" from DicesVm to Dices and save in database
        {
            var dicesToSave = _mapper.Map<Dices>(dices);    //convert Dices to DicesVm
            _gameRepo.SaveDices(dicesToSave);               //save dices in database
        }
        public void NextLap(int gameId)             //set next lap in database
        {
            var game = GetGameById(gameId);         //get game from database
            game.CurrentLap++;                      //increase "CurrentLap"
            _gameRepo.NextLapRepo(game);            //save changes in database
        }
        public void NextRound(int gameId)                                                       //set next round, first lap and first player
        {
            var game = GetGameById(gameId);                                                     //get game from database
            game.CurrentRound++;                                                                //increase "CurrentRound"
            game.CurrentPlayerId = _gameRepo.GetFirstPlayerId(gameId).AsQueryable().Single();   //get first player, convert to int and set his ID to "CurrentPlayerId"
            game.CurrentLap = 0;                                                                //set "CurrentLap" as 0, when round will be started, "CurrentLap" will be increased
            _gameRepo.NextRoundRepo(game);                                                      //save changes in database
        }
        public void NextPlayer (int gameId, int playerTurn)                                                 //set next player and lap
        {
            var game = GetGameById(gameId);                                                                 //get game from database
            var playerId = _gameRepo.GetPlayerIdFromPlayerTurn(gameId, playerTurn).AsQueryable().Single();  //get next player's ID and convert to int
            game.CurrentPlayerId = playerId;                                                                //set next player's ID
            game.CurrentLap = 0;                                                                            //set "CurrentLap" as 0
            _gameRepo.NextPlayerRepo(game);                                                                 //save changes in database
        }
        public void SaveBlockedDices(DicesVm dices)             //save remaning dices in database
        {
            var dicesToSave = _mapper.Map<Dices>(dices);        //convert DicesVm to Dices
            _gameRepo.SaveBlockedDicesRep(dicesToSave);         //save remaning dices in database
        }
        public PlayerValueVM CountOptionalValues (DicesVm dices)                            //check values are possible with these dices
        {
            int sum = dices.Dice1 + dices.Dice2 + dices.Dice3 + dices.Dice4 + dices.Dice5;  //count sum of dices
            var optionalValues = new PlayerValueVM();                                       //create new PlayerValueVm
            optionalValues.Ones = CountValues(dices, 1);                                    //sum up all ones in dices and set as optionalValues.Ones
            optionalValues.Twos = CountValues(dices, 2) * 2;                                //sum up all ones in twos and set as optionalValues.Twos
            optionalValues.Threes = CountValues(dices, 3) * 3;                              //sum up all ones in threes and set as optionalValues.Threes
            optionalValues.Fours = CountValues(dices, 4) * 4;                               //sum up all ones in fours and set as optionalValues.Fours
            optionalValues.Fives = CountValues(dices, 5) * 5;                               //sum up all ones in fives and set as optionalValues.Fives
            optionalValues.Sixs = CountValues(dices, 6) * 6;                                //sum up all ones in sixs and set as optionalValues.Sixs
            int max = 1;
            for (int i = 1; i <= 6; i++)                                                    //check which value is repeated most often and if its is more than two, set the appropriate values 
            {
                switch (CountValues(dices, i))                                              //how many value "i" repaets
                {
                    case 5:                                                                 //value "i" repeats 5 times
                        optionalValues.General = 50;                                        //if value is repeated five times set optionalValues.General as 50 
                        max = 5;                                                            //set "max" as 5
                        break;
                    case 4:                                                                 //value "i" repeats 4 times
                        optionalValues.Fourfold = sum;                                      //if value is repeated four times set optionalValues.Fourfold as sum of dices    
                        max = 4;                                                            //set "max" as 4
                        break;
                    case 3:                                                                 //value "i" repeats 3 times
                        optionalValues.Triple = sum;                                        //if value is repeated three times set optionalValues.Triple as sum of dices
                        max = 3;                                                            //set "max" as 4
                        for (int j = 1; j <= 6; j++)                                        //check if it is full (one values is repeated 3 times, another 2 times)
                        {
                            if (CountValues(dices, j) == 2)                                 //check if some value is repeated 2 times
                                optionalValues.Full = 25;                                   //set optionalValues as 25 (full)
                        }
                        break;
                    case 2:                                                                 //value "i" repeats 2 times
                        if (max == 1) max = 2;                                              //if "max" equals 1 (is not more than 1) set "max" as 2
                        break;
                }
            }
            if (max == 1)                                                                   //check if every value equals 1 (straight is possible)
                if (CountValues(dices, 1) == 0) optionalValues.HighStraight = 40;           //if value 1 is not exist (there are values 2,3,4,5,6 - high straight) set optionalValue.HighStraight as 40
                else if (CountValues(dices, 6) == 0) optionalValues.SmallStraight = 30;     //if value 6 is not exist (there are values 1,2,3,4,5 - small straight) set optionalValue.SmallStraight as 30
            optionalValues.Chance = dices.Dice1 + dices.Dice2 + dices.Dice3 + dices.Dice4 + dices.Dice5;    //set optional.Value.Chance as sum all dices
            return optionalValues;                                                          //return all optional values (optionalValues)
        }
        public void UpdateValue(UpdateValuesVm playerValues)                                                                    //add chosen value to all points of player and blocked appropriate field
        {
            var values = _gameRepo.GetPlayerValue(playerValues.GameId, playerValues.PlayerId).Single();                         //get existing player values
            var school = playerValues.CurrentValues.Ones + playerValues.CurrentValues.Twos + playerValues.CurrentValues.Threes
                + playerValues.CurrentValues.Fours + playerValues.CurrentValues.Fives + playerValues.CurrentValues.Sixs;        //get sum player values from school (ones,twos, threes, gotrs, fives, sixs) to check the bonus  
            switch (playerValues.ChooseValue)                                                                                   //check which value has been chosen by player
            {
                case "Ones":                                                                                                    //if player has chosen ones
                    values.Ones = playerValues.OptionalValues.Ones;                                                             //set values.Ones from OptionalValues.Ones 
                    values.OnesIsUsed = true;                                                                                   //set values.OnesIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Ones;                                                           //add value to values.Total (sum of points)
                    if (playerValues.CurrentValues.Bonus == 0 && school + values.Ones >= 63)                                    //check if bonus has been granted and if now should be granted
                    {
                        values = SetBonus(values);                                                                              //if bonus hasn't been granted and should be granted take a bouns
                    }
                    break;
                case "Twos":                                                                                                    //if player has chosen twos
                    values.Twos = playerValues.OptionalValues.Twos;                                                             //set values.Twos from OptionalValues.Twos
                    values.TwosIsUsed = true;                                                                                   //set values.TwosIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Twos;                                                           //add value to values.Total (sum of points)
                    if (playerValues.CurrentValues.Bonus == 0 && school + values.Twos >= 63)                                    //check if bonus has been granted and if now should be granted
                    {
                        values = SetBonus(values);                                                                              //if bonus hasn't been granted and should be granted take a bouns
                    }
                    break;
                case "Threes":                                                                                                  //if player has chosen threes
                    values.Threes = playerValues.OptionalValues.Threes;                                                         //set values.Threes from OptionalValues.Threes
                    values.ThreesIsUsed = true;                                                                                 //set values.ThreeIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Threes;                                                         //add value to values.Total (sum of points)
                    if (playerValues.CurrentValues.Bonus == 0 && school + values.Threes >= 63)                                  //check if bonus has been granted and if now should be granted
                    {
                        values = SetBonus(values);                                                                              //if bonus hasn't been granted and should be granted take a bouns
                    }
                    break;
                case "Fours":                                                                                                   //if player has chosen fours
                    values.Fours = playerValues.OptionalValues.Fours;                                                           //set values.Fours from OptionalValues.Fours
                    values.FoursIsUsed = true;                                                                                  //set values.FoursIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Fours;                                                          //add value to values.Total (sum of points)
                    if (playerValues.CurrentValues.Bonus == 0 && school + values.Fours >= 63)                                   //check if bonus has been granted and if now should be granted
                    {
                        values = SetBonus(values);                                                                              //if bonus hasn't been granted and should be granted take a bouns
                    }
                    break;
                case "Fives":                                                                                                   //if player has chosen fives
                    values.Fives= playerValues.OptionalValues.Fives;                                                            //set values.Fives from OptionalValues.Fives
                    values.FivesIsUsed = true;                                                                                  //set values.FivesIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Fives;                                                          //add value to values.Total (sum of points)
                    if (playerValues.CurrentValues.Bonus == 0 && school + values.Fives >= 63)                                   //check if bonus has been granted and if now should be granted
                    {
                        values = SetBonus(values);                                                                              //if bonus hasn't been granted and should be granted take a bouns
                    }
                    break;
                case "Sixs":                                                                                                    //if player has chosen sixs
                    values.Sixs= playerValues.OptionalValues.Sixs;                                                              //set values.Sixs from OptionalValues.Sixs
                    values.SixsIsUsed = true;                                                                                   //set values.SixsIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Sixs;                                                           //add value to values.Total (sum of points)
                    if (playerValues.CurrentValues.Bonus == 0 && school + values.Sixs >= 63)                                    //check if bonus has been granted and if now should be granted
                    {
                        values = SetBonus(values);                                                                              //if bonus hasn't been granted and should be granted take a bouns
                    }
                    break;
                case "Triple":                                                                                                  //if player has chosen triple
                    values.Triple= playerValues.OptionalValues.Triple;                                                          //set values.Triple from OptionalValues.Triple
                    values.TripleIsUsed= true;                                                                                  //set values.TripleIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Triple;                                                         //add value to values.Total (sum of points)
                    break;  
                case "Fourfold":                                                                                                //if player has chosen fourfold
                    values.Fourfold = playerValues.OptionalValues.Fourfold;                                                     //set values.Fourfold from OptionalValues.Fourfold
                    values.FourfoldIsUsed = true;                                                                               //set values.FourfoldIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Fourfold;                                                       //add value to values.Total (sum of points)
                    break;
                case "Full":                                                                                                    //if player has chosen full
                    values.Full = playerValues.OptionalValues.Full;                                                             //set values.Full from OptionalValues.Full
                    values.FullIsUsed = true;                                                                                   //set values.FullIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Full;                                                           //add value to values.Total (sum of points)
                    break;
                case "SmallStraight":                                                                                           //if player has chosen small straight
                    values.SmallStraight = playerValues.OptionalValues.SmallStraight;                                           //set values.SmallStraight from OptionalValues.SmallStraight
                    values.SmallStraightIsUsed = true;                                                                          //set values.SmallStraightIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.SmallStraight;                                                  //add value to values.Total (sum of points)
                    break;
                case "HighStraight":                                                                                            //if player has chosen high straight
                    values.HighStraight = playerValues.OptionalValues.HighStraight;                                             //set values.HighStraight from OptionalValues.Twos
                    values.HighStraightIsUsed = true;                                                                           //set values.HighStraightIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.HighStraight;                                                   //add value to values.Total (sum of points)
                    break;
                case "General":                                                                                                 //if player has chosen general
                    values.General = playerValues.OptionalValues.General;                                                       //set values.General from OptionalValues.General
                    values.GeneralIsUsed= true;                                                                                 //set values.GeneralIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.General;                                                        //add value to values.Total (sum of points)
                    break;
                case "Chance":                                                                                                  //if player has chosen chance
                    values.Chance = playerValues.OptionalValues.Chance;                                                         //set values.Chance from OptionalValues.Chance
                    values.ChanceIsUsed= true;                                                                                  //set values.ChanceIsUsed as true (is taken)
                    values.Total += playerValues.OptionalValues.Chance;                                                         //add value to values.Total (sum of points)
                    break;
            }
            _gameRepo.UpdateValuesRep(values, playerValues.ChooseValue);                                                        //save changes in database
        }
        public int GetPlayerTurn(int gameId, int playerId)                                              //get PlayerTurn from data base (base with numbering of player in games)
        {
            var playerTurn = _gameRepo.GetPlayerTurnRepo(gameId, playerId).AsQueryable().Single();      //get PlayerTurn from data base (base with numbering of player in games)
            return playerTurn;                                                                          //return "playerTurn"
        }
        public int GetPlayersCount(int gameId)                                      //get players count of the game from data base
        {
            return _gameRepo.GetPlayersCountRep(gameId).AsQueryable().Single();     //get players count of the game from data base
        }
        public PlaygameVm TurnDices(int gameId)                                                         //draw dices
        {
            PlaygameVm model = new PlaygameVm() { GameId = gameId };                                    //create new PlayerGameVm
            var game = GetGameById(gameId);                                                             //get the game
            model.CurrentPlayer = GetCurrentPlayerValue(gameId, game.CurrentPlayerId);                  //get points of the player and set as the CurrentPlayer
            model.Players = GetPlayersScores(gameId);                                                   //get sum of points of all players of the game and set as the Players
            model.Players = WhichPlace(model.Players);                                                  //set the place numbers of players
            model.Round = game.CurrentRound;                                                            //set Round as CurrentRound
            model.Lap = game.CurrentLap;                                                                //set Lap as CurrentLap
            model.Dices = new List<DicesVm>();                                                          //create new List of Dices
            model.Dices.Add(new DicesVm());                                                             //create new DicesVM (one throw of dices) in List of Dices
            model.Dices[0].Dice1IsBlocked = false;                                                      //set dice1 as no blocked
            model.Dices[0].Dice2IsBlocked = false;                                                      //set dice2 as no blocked
            model.Dices[0].Dice3IsBlocked = false;                                                      //set dice3 as no blocked
            model.Dices[0].Dice4IsBlocked = false;                                                      //set dice4 as no blocked
            model.Dices[0].Dice5IsBlocked = false;                                                      //set dice5 as no blocked
            for (int i = 1; i < model.Lap; i++)                                                         //join dices previosus with images
            {
                model.Dices.Add(GetDices(gameId, model.CurrentPlayer.PlayerId, model.Round, i));        //get prevoius dices from database and join to List of Dices
                model.Dices[i].Dice1ImgPath = "/Images/" + model.Dices[i].Dice1.ToString() + ".png";    //join dice1 with image
                model.Dices[i].Dice2ImgPath = "/Images/" + model.Dices[i].Dice2.ToString() + ".png";    //join dice2 with image
                model.Dices[i].Dice3ImgPath = "/Images/" + model.Dices[i].Dice3.ToString() + ".png";    //join dice3 with image
                model.Dices[i].Dice4ImgPath = "/Images/" + model.Dices[i].Dice4.ToString() + ".png";    //join dice4 with image
                model.Dices[i].Dice5ImgPath = "/Images/" + model.Dices[i].Dice5.ToString() + ".png";    //join dice5 with image
            }
            model.Dices.Add(new DicesVm());                                                             //create new Dices (for current dices) and join do List of Dices
            model.Dices[model.Lap].GameId = gameId;                                                     //set GameId in current Dices
            model.Dices[model.Lap].Lap = model.Lap;                                                     //set Lap in current Dices
            model.Dices[model.Lap].Round = model.Round;                                                 //set Round in current Dices
            model.Dices[model.Lap].PlayerId = model.CurrentPlayer.PlayerId;                             //set PlayerId in current Dices

            Random generator = new Random();                                                            //create random number generator 

            if (model.Dices[model.Lap - 1].Dice1IsBlocked)                                              //if previous dice1 has blocked...
            {
                model.Dices[model.Lap].Dice1 = model.Dices[model.Lap - 1].Dice1;                        //... set current dice1 as previous dice1...
            }
            else
            {
                model.Dices[model.Lap].Dice1 = generator.Next(1, 7);                                    //... else randomize number from 1 to 6 and set as dice1
            }
            model.Dices[model.Lap].Dice1ImgPath = "/Images/" + model.Dices[model.Lap].Dice1.ToString() + ".png";    //join current dice1 with image
            if (model.Dices[model.Lap - 1].Dice2IsBlocked)                                              //if previous dice2 has blocked...
            {
                model.Dices[model.Lap].Dice2 = model.Dices[model.Lap - 1].Dice2;                        //... set current dice2 as previous dice1...
            }
            else
            {
                model.Dices[model.Lap].Dice2 = generator.Next(1, 7);                                    //... else randomize number from 1 to 6 and set as dice2
            }
            model.Dices[model.Lap].Dice2ImgPath = "/Images/" + model.Dices[model.Lap].Dice2.ToString() + ".png";    //join current dice2 with image
            if (model.Dices[model.Lap - 1].Dice3IsBlocked)                                              //if previous dice3 has blocked...
            {
                model.Dices[model.Lap].Dice3 = model.Dices[model.Lap - 1].Dice3;                        //... set current dice3 as previous dice1...
            }
            else
            {
                model.Dices[model.Lap].Dice3 = generator.Next(1, 7);                                    //... else randomize number from 1 to 6 and set as dice3
            }
            model.Dices[model.Lap].Dice3ImgPath = "/Images/" + model.Dices[model.Lap].Dice3.ToString() + ".png";    //join current dice3 with image
            if (model.Dices[model.Lap - 1].Dice4IsBlocked)                                              //if previous dice4 has blocked...
            {
                model.Dices[model.Lap].Dice4 = model.Dices[model.Lap - 1].Dice4;                        //... set current dice4 as previous dice1...
            }
            else
            {
                model.Dices[model.Lap].Dice4 = generator.Next(1, 7);                                    //... else randomize number from 1 to 6 and set as dice4
            }
            model.Dices[model.Lap].Dice4ImgPath = "/Images/" + model.Dices[model.Lap].Dice4.ToString() + ".png";    //join current dice4 with image
            if (model.Dices[model.Lap - 1].Dice5IsBlocked)                                              //if previous dice5 has blocked...
            {
                model.Dices[model.Lap].Dice5 = model.Dices[model.Lap - 1].Dice5;                        //... set current dice5 as previous dice1...
            }
            else
            {
                model.Dices[model.Lap].Dice5 = generator.Next(1, 7);                                    //... else randomize number from 1 to 6 and set as dice5
            }
            model.Dices[model.Lap].Dice5ImgPath = "/Images/" + model.Dices[model.Lap].Dice5.ToString() + ".png";    //join current dice1 with image
            model.OptionalValues = new PlayerValueVM();                                                 //create new PlayerValueVM for OptionalValue
            model.OptionalValues = CountOptionalValues(model.Dices[model.Lap]);                         //check how many points can you gey with current dices
            GetDicesToSave(model.Dices[model.Lap]);                                                     //save current dices in data base
            return model;                                                                               //return all dices with all information
        }
        public PlayerValue SetBonus(PlayerValue values)     //take a bunus
        {
            
            values.Total += 50;                             //set values.Bonus as 50 ...
            values.Bonus = 50;                              //add bonus to values.Total (sum of ponits)
            return values;                                  //return values
        }
        public List<PlayerScoreVm> WhichPlace(List<PlayerScoreVm> players)  //set the place numbers of players
        {
            int place = 0;                                                  //set 1st place (will increace)
            int score = -1;                                                 //set the "score" as a score like no other players has (-1)
            foreach(PlayerScoreVm player in players)                        //for all players...
            {
                if (score != player.Total)                                  //...if the "score" does not equal player,s score...
                {
                    place++;                                                //...increace place
                    score = player.Total;
                }
                player.Place = place;                                       //player's place equals "place"
            }
            return players;                                                 //return players
        }
        public void EndGame(int gameId)                                     //set IsActive as "false" in the game
        {
            _gameRepo.EndGameRep(gameId);                                   //set IsActive as "false" in the game
        }
    }
}
