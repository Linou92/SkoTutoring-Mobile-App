using AppDbContext.Entities;
using AppDbContext.Enums;
using StudentApi.Models.ApiModels;
using StudentApi.Results;
using System;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;

namespace StudentApi.Controllers
{
    [Route("Api/Sessions")]
    public class SessionsController : BaseController
    {
        [Route("Api/Sessions/Post")]
        [HttpPost]
        [Authorize]
        public Result Post([FromBody] RequestSessionModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    if(ModelState.IsValid)
                    {

                        var teacher = Db.Teachers
                            .Include(c => c.TeacherAvailabilities)
                            .Include(c => c.TeacherLangs)
                            .Include(c => c.TeacherSubjs)
                            .Include(c => c.TeacherLevels)
                            .FirstOrDefault(cc => cc.TeacherAvailabilities.Any(c => c.AvailabilityId == model.AvailabilityId));
                        var av = Db.Availabilities.Find(model.AvailabilityId);

                        if (teacher.TeacherAvailabilities.Count > 0 && teacher.TeacherLangs.Count > 0 
                            && teacher.TeacherLevels.Count > 0 && teacher.TeacherSubjs.Count > 0)
                        {

                            var subject = Db.Subjects.FirstOrDefault(cc => cc.TeacherSubjs.Any(c => c.Teacher_UserId == teacher.UserId));

                            var lang = Db.Languages.FirstOrDefault(c => c.TeacherLangs.Any(cc => cc.Teacher_UserId == teacher.UserId));

                            var Level = teacher.TeacherLevels.First(c => c.LevelId == model.Level);

                            if (av != null && subject != null
                                && lang != null && Level != null)
                            {
                                Db.Sessions.Add(new Session
                                {
                                    AvailabilityId = model.AvailabilityId,
                                    Title = model.Title,
                                    TeacherId = teacher.UserId,
                                    SubjectId = subject.Id,
                                    LangId = lang.Id,
                                    Date = av.Date,
                                    LevelId = Level.LevelId,
                                    Notes = "Session Has Been Requested",
                                    Purpose = (int)PurposeEnum.Course,
                                    Status = (int)SessionStatusEnum.Pending,
                                    StudentId = CurrentUser.Id
                                });
                                Db.SaveChanges();
                                return new Result()
                                {
                                    IsOk = true,
                                    Message = new Message("Success, Session has been requested wait for response..", MessageType.Success),
                                };
                            }
                            else
                            {
                                return new Result()
                                {
                                    IsOk = true,
                                    Message = new Message("Sorry, One Or More Entities Not Found", MessageType.Error),
                                };
                            }
                        }
                        else
                        {
                            return new Result()
                            {
                                IsOk = true,
                                Message = new Message("Sorry, Teacher has not registered any subjects or levels or availabilities", MessageType.Success),
                            };
                        }
                    }
                    else
                    {
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Error, You must select subject and availability and teacher", MessageType.Error),
                        };
                    }
                }
                else
                {
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("Error, Please Login as Student", MessageType.Error),
                    };
                }
            }
            catch(Exception ex)
            {
                return new Result
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error)
                };
            }
        }
    }
}