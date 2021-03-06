﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Extensions;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;
using TripleDerby.Core.Specifications;

namespace TripleDerby.Core.Services
{
    public class HorseService : IHorseService
    {
        private readonly ITripleDerbyRepository _repository;

        public HorseService(
            ITripleDerbyRepository repository
        )
        {
            _repository = repository;
        }

        public async Task<HorseResult> Get(Guid id)
        {
            var horse = await _repository.Get(new HorseSpecification(id));

            return new HorseResult
            {
                Id = horse.Id,
                Name = horse.Name,
                Color = horse.Color.Name,
                Earnings = horse.Earnings,
                RacePlace = horse.RacePlace,
                RaceShow = horse.RaceShow,
                RaceStarts = horse.RaceStarts,
                RaceWins = horse.RaceWins,
                Sire = horse.Sire?.Name,
                Dam = horse.Dam?.Name
            };
        }

        public async Task<HorsesResult> GetAll(int pageIndex, int itemsPage)
        {
            var paginatedSpecification = new HorsesPaginatedSpecification(itemsPage * pageIndex, itemsPage);

            var paginatedHorses = await _repository.List(paginatedSpecification);
            var totalItems = await _repository.Count<Horse>();

            return new HorsesResult
            {
                Horses = paginatedHorses.Select(x => new HorsesResult.Horse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color.Name,
                    Earnings = x.Earnings,
                    RacePlace = x.RacePlace,
                    RaceShow = x.RaceShow,
                    RaceStarts = x.RaceStarts,
                    RaceWins = x.RaceWins,

                }),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = paginatedHorses.Count,
                    TotalItems = totalItems,
                    TotalPages =
                        int.Parse(Math.Ceiling((decimal)totalItems / itemsPage)
                            .ToString(CultureInfo.InvariantCulture))
                }
            };
        }

        public async Task Update(Guid id, JsonPatchDocument<HorsePatch> patch)
        {
            var horse = await _repository.Get(new HorseSpecification(id));

            patch.Map<HorsePatch, Horse>().ApplyTo(horse);

            await _repository.Save();
        }
    }
}
