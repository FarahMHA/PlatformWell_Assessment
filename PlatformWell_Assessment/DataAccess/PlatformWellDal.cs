using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PlatformWell_Assessment.Entities;
using PlatformWell_Assessment.Models;
using static PlatformWell_Assessment.DtoModels.PlatformWellModelDto;

namespace PlatformWell_Assessment.DataAccess
{
    public class PlatformWellDal
    {
        private readonly PlatformWellDbContext _PlatformWellDbContext;

        public PlatformWellDal(PlatformWellDbContext platformWellDbContext)
        {
            _PlatformWellDbContext = platformWellDbContext;
        }

        public void DbExecuteNonResult(List<PlatformDto> pmData)
        {
            try
            {
                if (pmData != null)
                {
                    foreach (var data in pmData)
                    {
                        var platformData = _PlatformWellDbContext.Platforms.Include(x=>x.Wells).Where(x => x.Id == data.Id).FirstOrDefault();
                        

                        //Data exist then update
                        if (platformData != null)
                        {
                            platformData.UniqueName = data.UniqueName;
                            platformData.Latitude = data.Latitude;
                            platformData.Longitude = data.Longitude;
                            platformData.CreatedAt = data.CreatedAt;
                            platformData.UpdatedAt = data.UpdatedAt;

                            _PlatformWellDbContext.Platforms.Update(platformData);

                            foreach (var wd in data.Well.Where(x=>x.PlatformId == data.Id))

                            {
                                var wellData = platformData.Wells.Where(x => x.Id == wd.Id).FirstOrDefault();
                                if( wellData!= null)
                                {
                                    wellData.UniqueName = wd.UniqueName;
                                    wellData.Latitude = wd.Latitude;
                                    wellData.Longitude = wd.Longitude;
                                    wellData.CreatedAt = wd.CreatedAt;
                                    wellData.UpdatedAt = wd.UpdatedAt;

                                    _PlatformWellDbContext.Wells.Update(wellData);
                                }

                            }
                        }

                        //Create data
                        else
                        {
                            var platform = new Platform();

                            platform.Id = data.Id;
                            platform.UniqueName = data.UniqueName;
                            platform.Latitude = data.Latitude;
                            platform.Longitude = data.Longitude;
                            platform.CreatedAt = data.CreatedAt;
                            platform.UpdatedAt = data.UpdatedAt;
                            platform.Wells = data.Well.Select(x=> new Well
                            {
                                Id = x.Id,
                                PlatformId = x.PlatformId,
                                UniqueName = x.UniqueName,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                                CreatedAt = x.CreatedAt,
                                UpdatedAt = x.UpdatedAt,
                            }).ToList();
                            _PlatformWellDbContext.Platforms.Add(platform);

     
                        }
                    }
                    _PlatformWellDbContext.SaveChanges();
                }

            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
