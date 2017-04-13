﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using AutoMapper;
using vindiniumcore.Infrastructure.Behaviors.Map;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Extensions;

namespace vindiniumcore.Infrastructure
{
    public class Server
    {
        private readonly VindiniumSettings _vindiniumSettings;
        private readonly IMapper _mapper;
        public string PlayUrl { get; set; }

        //if training mode is false, turns and DefaultMapBuilder are ignored8
        public Server(VindiniumSettings vindiniumSettings, IMapper mapper)
        {
            _vindiniumSettings = vindiniumSettings;
            _mapper = mapper;
        }

        public string ViewUrl { get; private set; }

        public IMapNode MyHero { get; private set; }

        public List<IMapNode> AllCharacters { get; set; }

        public IEnumerable<IMapNode> Villians { get; private set; }

        public int CurrentTurn { get; private set; }

        public int MaxTurns { get; private set; }

        public bool Finished { get; private set; }

        public bool Errored { get; private set; }

        public string ErrorText { get; private set; }

        public IMapNode[][] Board { get; private set; }

        //initializes a new game, its syncronised
        public void CreateGame()
        {
            Errored = false;

            string uri;

            if (_vindiniumSettings.TrainingMode)
            {
                uri = _vindiniumSettings.ServerUrl.AbsoluteUri + "/api/training";
            }
            else
            {
                uri = _vindiniumSettings.ServerUrl.AbsoluteUri + "/api/arena";
            }

            var myParameters = "key=" + _vindiniumSettings.Key;
            if (_vindiniumSettings.TrainingMode)
            {
                myParameters += "&turns=" + _vindiniumSettings.Turns;
            }
            if (_vindiniumSettings.Map!= null)
            {
                myParameters += "&map=" + _vindiniumSettings.Map;
            }

            //make the request
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("ContentType", "application/X-www-form-urlencoded");
                //client.Headers[HttpRequestHeader.ContentType] = "application/X-www-form-urlencoded";
                try
                {
                    var result = client.PostAsync(uri + "?"+ myParameters, new StringContent(string.Empty)).Result;
                    
                    Deserialize(result.Content.ReadAsStringAsync().Result);
                }
                catch (WebException exception)
                {
                    Errored = true;
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        ErrorText = reader.ReadToEnd();
                    }
                }
            }
        }
        
        public string GetDirection(CoOrdinates currentLocation, CoOrdinates moveTo)
        {
            var direction = "Stay";
            if (moveTo == null)
            {
                return direction;
            }
            if (moveTo.X > currentLocation.X)
            {
                direction = "East";
            }
            else if (moveTo.X < currentLocation.X)
            {
                direction = "West";
            }
            else if (moveTo.Y > currentLocation.Y)
            {
                direction = "South";
            }
            else if (moveTo.Y < currentLocation.Y)
            {
                direction = "North";
            }

            return direction;
        }

        public void MoveHero(string direction)
        {
            var myParameters = "key=" + _vindiniumSettings.Key + "&dir=" + direction;

            //make the request
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("ContentType", "application/X-www-form-urlencoded");
                try
                {
                    var result = client.PostAsync(PlayUrl + "?"+ myParameters,new StringContent(string.Empty)).Result;

                    Deserialize(result.Content.ReadAsStringAsync().Result);
                }
                catch (WebException exception)
                {
                    Errored = true;
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        ErrorText = reader.ReadToEnd();
                    }
                }
            }
        }

        private void CreateBoard(int size, string data)
        {
            //check to see if the Board list is already created, if it is, we just overwrite its values
            if (Board == null || Board.Length != size)
            {
                Board = new IMapNode[size][];
                for (var index = 0; index < Board.Length; index++)
                {
                    Board[index] = new IMapNode[size];
                }
            }

            var x = 0;
            var y = 0;
            var charData = data.ToCharArray();

            for (var i = 0; i < charData.Length; i += 2)
            {
                switch (charData[i])
                {
                    case '#':
                        Board[x][y] = new MapNode(Tile.IMPASSABLE_WOOD, x, y);
                        break;
                    case ' ':
                        Board[x][y] = new MapNode(Tile.FREE, x, y);
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                Board[x][y] = AllCharacters.First(h => h.Type == Tile.HERO_1);
                                break;
                            case '2':
                                Board[x][y] = AllCharacters.First(h => h.Type == Tile.HERO_2);
                                break;
                            case '3':
                                Board[x][y] = AllCharacters.First(h => h.Type == Tile.HERO_3);
                                break;
                            case '4':
                                Board[x][y] = AllCharacters.First(h => h.Type == Tile.HERO_4);
                                break;
                        }
                        break;
                    case '[':
                        Board[x][y] = new MapNode(Tile.TAVERN, x, y);

                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                Board[x][y] = new MapNode(Tile.GOLD_MINE_NEUTRAL, x, y);
                                break;
                            case '1':
                                Board[x][y] = new MapNode(Tile.GOLD_MINE_1, x, y);
                                break;
                            case '2':
                                Board[x][y] = new MapNode(Tile.GOLD_MINE_2, x, y);
                                break;
                            case '3':
                                Board[x][y] = new MapNode(Tile.GOLD_MINE_3, x, y);
                                break;
                            case '4':
                                Board[x][y] = new MapNode(Tile.GOLD_MINE_4, x, y);
                                break;
                        }
                        break;
                }

                //time to increment X and Y
                x++;
                if (x == size)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void Deserialize(string json)
        {
            var byteArray = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(byteArray);

            var ser = new DataContractJsonSerializer(typeof (GameResponse));
            var gameResponse = (GameResponse) ser.ReadObject(stream);

            PlayUrl = gameResponse.playUrl;
            ViewUrl = gameResponse.viewUrl;

            MyHero = _mapper.Map<HeroNode>(gameResponse.hero);
            Villians = _mapper.Map<List<VillianNode>>(gameResponse.game.heroes.Where(h => h.id != MyHero.Id));
            AllCharacters = new List<IMapNode> {MyHero};
            AllCharacters.AddRange(Villians);

            CurrentTurn = gameResponse.game.turn;
            MaxTurns = gameResponse.game.maxTurns;
            Finished = gameResponse.game.finished;

            CreateBoard(gameResponse.game.board.size, gameResponse.game.board.tiles);
            PopulateNodeParents();
            PopulateMovementCost();
        }

        private void PopulateMovementCost()
        {
            int depth = 0;
            MyHero.MovementCost = depth;
            depth++;

            foreach (var heroNode in MyHero.Parents)
            {
                AssignCost(depth, heroNode);
                if (heroNode.Passable)
                {
                    FindAllRoutes(depth, heroNode);
                }
            }
        }

        private void FindAllRoutes(int depth, IMapNode parentMapNode)
        {
            depth++;
            foreach (var node in parentMapNode.Parents.Where(n => n.MovementCost > depth))
            {
                AssignCost(depth, node);
                if (node.Passable)
                {
                    FindAllRoutes(depth, node);
                }
            }
        }
        private void AssignCost(int cost, IMapNode mapNode)
        {
            if (cost < mapNode.MovementCost)
            {
                if (mapNode.Type == Tile.IMPASSABLE_WOOD || mapNode.Type == this.MyTreasure())
                {
                    mapNode.Passable = false;
                    mapNode.MovementCost = -1;
                }
                else if (this.NotMyTreasure().Contains(mapNode.Type) ||
                         mapNode.Type == Tile.HERO_1 ||
                         mapNode.Type == Tile.HERO_2 ||
                         mapNode.Type == Tile.HERO_3 ||
                         mapNode.Type == Tile.HERO_4)
                {
                    mapNode.MovementCost = cost;
                    mapNode.Passable = false;
                }
                else
                {
                    mapNode.MovementCost = cost;
                    mapNode.Passable = true;
                }
            }
        }
        private void PopulateNodeParents()
        {
            foreach (IMapNode[] t in Board)
            {
                foreach (IMapNode t1 in t)
                {
                    var node = t1;
                    var parents = GetParents(t1);
                    node.Parents = parents;
                }
            }
        }

        private List<IMapNode> GetParents(IMapNode sourceMapNode)
        {
            var results = new List<IMapNode>();
            if (sourceMapNode.Location.Y - 1 >= 0)
            {
                var north = Board[sourceMapNode.Location.X][sourceMapNode.Location.Y - 1];
                results.Add(north);
            }

            if (sourceMapNode.Location.Y + 1 <= Board.Length - 1)
            {
                var south = Board[sourceMapNode.Location.X][sourceMapNode.Location.Y + 1];
                results.Add(south);
            }

            if (sourceMapNode.Location.X + 1 <= Board.Length - 1)
            {
                var east = Board[sourceMapNode.Location.X + 1][sourceMapNode.Location.Y];
                results.Add(east);
            }

            if (sourceMapNode.Location.X - 1 >= 0)
            {
                var west = Board[sourceMapNode.Location.X - 1][sourceMapNode.Location.Y];
                results.Add(west);
            }
            return results;
        }
    }
}