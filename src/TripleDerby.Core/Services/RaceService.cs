using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;
using TripleDerby.Core.Specifications;

namespace TripleDerby.Core.Services
{
    public class RaceService : IRaceService
    {
        private readonly ITripleDerbyRepository _repository;

        public RaceService(
            ITripleDerbyRepository repository
        )
        {
            _repository = repository;
        }

        public async Task<RaceResult> Get(byte id)
        {
            var race = await _repository.Get(new RaceSpecification(id));

            return new RaceResult
            {
                Id = race.Id,
                Name = race.Name,
                Description = race.Description,
                Furlongs = race.Furlongs,
                SurfaceId = race.SurfaceId,
                Surface = race.Surface.Name,
                TrackId = race.TrackId,
                Track = race.Track.Name
            };
        }

        public async Task<IEnumerable<RacesResult>> GetAll()
        {
            var races = await _repository.List(new RaceSpecification());

            return races.Select(x => new RacesResult
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Furlongs = x.Furlongs,
                SurfaceId = x.SurfaceId,
                Surface = x.Surface.Name,
                TrackId = x.TrackId,
                Track = x.Track.Name
            });
        }

        public async Task<RaceRunResult> Race(byte raceId, Guid horseId)
        {
            var racers = await _repository.List(new HorseRandomRacerSpecification());

            var raceRunHorses = new List<RaceRunHorse> { new RaceRunHorse { HorseId = horseId, Lane = 1 } };
            raceRunHorses.AddRange(racers.Select(racer => new RaceRunHorse { HorseId = racer.Id, Lane = 2 })); // TODO: lane

            var raceRunTicks = new List<RaceRunTick>
            {
                new RaceRunTick
                {
                    Tick = 1,
                    Note = "Horse 1 is strong out of the gate!",
                    RaceRunTickHorses = new List<RaceRunTickHorse>
                    {
                        new RaceRunTickHorse
                        {
                            HorseId = horseId,
                            Lane = 1,
                            Distance = 1
                        }
                    }
                }
            };

            var raceRun = new RaceRun
            {
                ConditionId = ConditionId.Fast, // TODO: random condition
                Horses = raceRunHorses.OrderBy(x => Guid.NewGuid()).ToList(),
                Purse = 30000, // TODO
                RaceRunTicks = raceRunTicks
            };

            await _repository.Add(raceRun);

            return new RaceRunResult
            {
                Id = Guid.NewGuid(),
                WinHorse = "Winner!",
                PlaceHorse = "Place",
                ShowHorse = "Show",
                PlayByPlay = new List<string>
                {
                    raceRun.RaceRunTicks.SelectMany(x => x.Note).ToString()!
                }
            };
        }
    }
}
