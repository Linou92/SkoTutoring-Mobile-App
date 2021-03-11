using AppDbContext.Entities;
using AppDbContext.Enums;
using TeacherApi.Models.ApiModels;
using TeacherApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System.Threading.Tasks;

namespace TeacherApi.Controllers
{
    [Route("Api/Levels")]
    public class LevelsController : BaseController
    {
        [HttpPost]
        [Route("api/Levels/RegisterMyLevel")]
        [Authorize]
        public async Task<Result> RegisterMyLevel(int? LevelId)
        {
            try
            {
                if (CheckUserPermission())
                {

                    if (LevelId != null)
                    {
                        var level = Db.Levels.FirstOrDefault(c => c.Id == LevelId);
                        if(level == null)
                            return new Result()
                            {
                                IsOk = true,
                                Message = new Message("Error, Level Not Found", MessageType.Error)
                            };
                        else
                        {
                            Db.TeacherLevels.Add(new TeacherLevel 
                            { 
                            LevelId = level.Id,
                            Teacher_UserId = CurrentUser.Id
                            });
                        }
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Success, Level Inserted Successfully", MessageType.Success)
                        };
                    }
                    else
                    {
                        
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Level must be not null", MessageType.Error),
                        };
                    }
                }
                else
                {
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                    };
                }

            }
            catch (Exception ex)
            {
                return new LoginResult()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    ResultCode = LoginEnumResult.Failure,
                    ResultText = LoginEnumResult.Failure.ToString(),
                    Token = null
                };
            }
        }

        [HttpGet]
        [Route("Api/Levels/List")]
        [Authorize]
        public ListResult<LevelApiModel> List()
        {
            try
            {
                if (CheckUserPermission())
                {

                    var dbLevels = Db.Levels.ToList();
                    List<LevelApiModel> levelsModel = new List<LevelApiModel>();
                    foreach (var item in dbLevels)
                    {
                        LevelApiModel lApiModel = new LevelApiModel()
                        {
                            Id = item.Id,
                            Name = item.Name
                        };
                        levelsModel.Add(lApiModel);
                    }
                    return new ListResult<LevelApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success, Get Levels List Successfully", MessageType.Success),
                        Items = levelsModel
                    };
                }
                else
                {
                    return new ListResult<LevelApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Success),
                        Items = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ListResult<LevelApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }


        [HttpGet]
        [Route("Api/Levels/MyList")]
        [Authorize]
        public ListResult<LevelApiModel> MyList()
        {
            try
            {
                if (CheckUserPermission())
                {

                    var dbLevels = Db.TeacherLevels
                    .Include(cc => cc.Teacher)
                    .Include(cc => cc.Level)
                    .Where(c => c.Teacher.UserId == CurrentUser.Id)
                    .Select(e => e.Level).ToList();

                    List<LevelApiModel> levelsModel = new List<LevelApiModel>();
                    foreach (var item in dbLevels)
                    {
                        LevelApiModel lApiModel = new LevelApiModel()
                        {
                            Id = item.Id,
                            Name = item.Name
                        };
                        levelsModel.Add(lApiModel);
                    }
                    return new ListResult<LevelApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success, Get Levels List Successfully", MessageType.Success),
                        Items = levelsModel
                    };
                }
                else
                {
                    return new ListResult<LevelApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                        Items = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ListResult<LevelApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }
    }
}