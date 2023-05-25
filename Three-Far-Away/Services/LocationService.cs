﻿using System;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public Location Create(Location entity)
        {
            return _locationRepository.Create(entity);
        }

        public Location Read(Guid id)
        {
            return _locationRepository.Read(id);
        }

        public Location Update(Location entity)
        {
            return _locationRepository.Update(entity);
        }

        public Location Delete(Guid id)
        {
            return _locationRepository.Delete(id);
        }
    }
}
