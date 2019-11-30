﻿// ********************************************************************************************************************************************
// Copyright (c) 2019
// Author: USA
// Product: CHILI
// Version: 1.0.0
// ********************************************************************************************************************************************

using Usa.chili.Common;
using Usa.chili.Data;
using Usa.chili.Domain;
using Usa.chili.Dto;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace Usa.chili.Services
{
    public class StationService: IStationService
    {
        private readonly ILogger _logger;
        private readonly ChiliDbContext _dbContext;

        static StationService()
        {
        }

        public StationService(ILogger<StationService> logger, ChiliDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<List<DropdownDto>> ListActiveStations() {
            return await _dbContext.Station
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.DisplayName)
                .Select(x => new DropdownDto {
                    Id = x.Id,
                    Text = x.DisplayName
                })
                .ToListAsync();
        }

        public async Task<List<StationMapDto>> GetStationMapData() {
            return await _dbContext.Station
                .AsNoTracking()
                .Select(x => new StationMapDto {
                    Id = x.Id,
                    DisplayName = x.DisplayName,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<List<DropdownDto>> ListAllStations() {
            return await _dbContext.Station
                .AsNoTracking()
                .OrderBy(x => x.DisplayName)
                .Select(x => new DropdownDto {
                    Id = x.Id,
                    Text = x.DisplayName
                })
                .ToListAsync();
        }

        public async Task<StationInfoDto> GetStationInfo(int stationid, DateTime? dateTime) {

            var stationInfoDto = new StationInfoDto();

            var stationInfo = await _dbContext.Station
                .AsNoTracking()
                .Where(s => s.Id == stationid)
                .FirstOrDefaultAsync();

            stationInfoDto.StationID = stationInfo.Id;
            stationInfoDto.StationTimeStamp = dateTime.GetValueOrDefault();
            stationInfoDto.StationKey = stationInfo.StationKey;
            stationInfoDto.DisplayName = stationInfo.DisplayName;
            stationInfoDto.Latitude = stationInfo.Latitude;
            stationInfoDto.Longitude = stationInfo.Longitude;
            stationInfoDto.Elevation = stationInfo.Elevation;
            stationInfoDto.BeginDate = stationInfo.BeginDate;
            stationInfoDto.EndDate = stationInfo.EndDate.GetValueOrDefault();
            stationInfoDto.IsActive = Convert.ToInt32(stationInfo.IsActive);
            
            return stationInfoDto;
        }
    }
}
