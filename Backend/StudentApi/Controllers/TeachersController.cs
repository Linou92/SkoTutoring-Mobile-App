using AppDbContext.Enums;
using StudentApi.Models.ApiModels;
using StudentApi.Results;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using AppDbContext.Entities;
using System;

namespace StudentApi.Controllers
{
    public class TeachersController : BaseController
    {
        [HttpGet]
        [Authorize]
        [Route("Api/Teachers/List")]
        public ListResult<TeacherApiModel> List(string language ,int? levelId, string subject)
        {
            try
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return new ListResult<TeacherApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Error ,Please Login as Student", MessageType.Error),
                        Items = null
                    };
               var dbTeachers = Db.Teachers
                   .Include(t => t.TeacherLangs)
                   .Include(t => t.TeacherLevels)
                   .Include(t => t.TeacherSubjs)
                   .Include(t => t.TeacherAvailabilities).ToList();

                if (!string.IsNullOrEmpty(language))
                {
                    var languages = Db.TeacherLangs
                               .Include(c => c.Language)
                               .Where(c => c.Language.Name.ToLower()
                               .Contains(language.ToLower())).Select(c => c.Id).ToList();
                 dbTeachers = dbTeachers
                   .Where(c => c.TeacherLangs.Any(e => languages.Contains(e.Id))).ToList();
                }
                if (levelId != null)
                {
                    var levels = Db.TeacherLevels.Include(c => c.Level)
                                          .Where(c => c.Level.Id == levelId.Value)
                                          .Select(c => c.Id).ToList();

                  dbTeachers = dbTeachers
                                      .Where(c => c.TeacherLevels.Any(e => levels.Contains(e.Id))).ToList();
                }
                if (!string.IsNullOrEmpty(subject))
                {

                    var subjects = Db.TeacherSubjs
                      .Include(c => c.Subject)
                      .Where(c => c.Subject.Name.ToLower()
                      .Contains(subject.ToLower())).Select(c => c.Id).ToList();
                    dbTeachers = dbTeachers
                                      .Where(c => c.TeacherSubjs.Any(e => subjects.Contains(e.Id))).ToList();
                }
                List<TeacherApiModel> teachers = new List<TeacherApiModel>();
                if (dbTeachers != null || dbTeachers.Count != 0)
                {
                    foreach (var item in dbTeachers)
                    {
                        List<LanguageApiModel> teacherlgs = new List<LanguageApiModel>();
                        foreach (var ls in item.TeacherLangs)
                        {
                            Language av = Db.Languages.Find(ls.LangId);
                            teacherlgs.Add(new LanguageApiModel
                            {
                              Id = av.Id,
                              Name = av.Name
                            });
                        }
                        List<AvailabilityApiModel> teacherAvs = new List<AvailabilityApiModel>();
                        foreach (var avs in item.TeacherAvailabilities)
                        {
                            Availability av = Db.Availabilities.Find(avs.AvailabilityId);
                            teacherAvs.Add(new AvailabilityApiModel
                            {
                                Id = av.Id,
                                Date = av.Date.ToString("MM/dd/yyyy"),
                                StartTime = av.StartTime,
                                EndTime = av.EndTime,
                                IsClosed = av.IsClosed
                            });
                        }
                        List<SubjectApiModel> teacherSujs = new List<SubjectApiModel>();
                        foreach (var su in item.TeacherSubjs)
                        {
                            Subject sub = Db.Subjects.Find(su.SubjectId);
                            teacherSujs.Add(new SubjectApiModel
                            {
                                Id = sub.Id,
                                Name = sub.Name
                            });
                        }
                        List<LevelApiModel> teacherLvs = new List<LevelApiModel>();
                        foreach (var su in item.TeacherLevels)
                        {
                            var lv = Db.Levels.Find(su.LevelId);
                            teacherLvs.Add(new LevelApiModel
                            {
                                Id = lv.Id,
                                Name = lv.Name
                            });
                        }
                        TeacherApiModel tApiModel = new TeacherApiModel()
                        {
                            Id = item.Id,
                            FirsName = item.FirstName,
                            LastName = item.LastName,
                            Levels = teacherLvs,
                            Role = Discriminator.Teacher.ToString(),
                            UserId = item.UserId,
                            Availabilities = teacherAvs,
                            Subjects = teacherSujs,
                            Languages = teacherlgs
                        };
                        teachers.Add(tApiModel);
                    }
                }
                return new ListResult<TeacherApiModel>()
                {
                    IsOk = true,
                    Message = new Message("Success, Get Teachers Successfully", MessageType.Success),
                    Items = teachers
                };
            }
            catch (Exception ex)
            {
                return new ListResult<TeacherApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }
    }
}