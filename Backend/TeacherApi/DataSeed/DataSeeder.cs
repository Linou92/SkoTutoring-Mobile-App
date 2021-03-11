using AppDbContext.Entities;
using AppDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace TeacherApi.DataSeed
{
    public class DataSeeder
    {
        private OnlineCourseDb Db { get; set; }
        public DataSeeder(OnlineCourseDb db)
        {
            Db = db;
        }

        public Country SeedDefaultCountry()
        {
            try
            {
                if (Db != null)
                {
                    var existedCountry = Db.Countries.ToList();
                    if(existedCountry == null || existedCountry.Count == 0)
                    {
                        Country country = Db.Countries.Add(new Country
                        {
                            Name = "Sweden",
                        });
                        Db.SaveChanges();
                        return country;
                    }
                    else
                    {
                        return existedCountry.FirstOrDefault();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                EventLog.WriteEntry("Application", ex.Message + " " + ex.InnerException?.Message);
                return null;
            }
        }
        public void SeedLevels()
        {
            if (Db != null)
            {
                var levels = Db.Levels.ToList();
                if (levels == null || levels.Count == 0)
                {
                    Level lv = Db.Levels.Add(new Level
                    {
                        Id = (int)LevelEnums.Level10,
                        Name = LevelEnums.Level10.ToString()
                    });
                    Db.SaveChanges();

                    Level lv2 = Db.Levels.Add(new Level
                    {
                        Id = (int)LevelEnums.Level11,
                        Name = LevelEnums.Level11.ToString()
                    });
                    Db.SaveChanges();

                    Level lv3 = Db.Levels.Add(new Level
                    {
                        Id = (int)LevelEnums.Level12,
                        Name = LevelEnums.Level12.ToString()
                    });
                    Db.SaveChanges();
                }
            }
        }
    }
}