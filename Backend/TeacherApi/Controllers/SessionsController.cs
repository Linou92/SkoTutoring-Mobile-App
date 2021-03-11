using AppDbContext.Entities;
using AppDbContext.Enums;
using TeacherApi.Models.ApiModels;
using TeacherApi.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System;
using System.Web.Http;

namespace TeacherApi.Controllers
{
    [Route("Api/Sessions")]
    public class SessionsController : BaseController
    {
        [HttpGet]
        [Authorize]
        [Route("Api/Sessions/GetMyPendingSessions")]
        public async Task<ListResult<SessionApiModel>> GetMyPendingSessions()
        {
            if (CheckUserPermission())
            {
                
                var dbSessions = Db.Sessions.Where(c => c.TeacherId == CurrentUser.Id).ToList();
                List<SessionApiModel> sessionModels = new List<SessionApiModel>();
                foreach (var item in dbSessions)
                {
                    Session session = Db.Sessions.Include(c => c.Availability)
                        .Include(c => c.Teacher)
                        .Include(c => c.Level)
                        .Include(c => c.Subject)
                        .Include(c => c.Language).FirstOrDefault(c => c.Id == item.Id && c.Status == (int)SessionStatusEnum.Pending);

                    SessionApiModel laApiModel = new SessionApiModel()
                    {
                        Id = session.Id,
                        AvailabilityId = session.AvailabilityId,
                        Language = session.Language.Name,
                        LangId = session.LangId,
                        Status = session.Status,
                        Subject = session.Subject.Name,
                        Date = session.Date.ToString("MM/dd/yyyy"),
                        Level = session.Level.Name,
                        LevelId = session.LevelId,
                        Notes = session.Notes,
                        Purpose = session.Purpose,
                        Student = session.Student.FirstName + " " + session.Student.LastName,
                        StudentId = session.StudentId,
                        SubjectId = session.SubjectId,
                        Teacher = session.Teacher.FirstName + " " + session.Teacher.LastName,
                        TeacherId = session.TeacherId,
                        Title = session.Title
                    };
                    sessionModels.Add(laApiModel);
                }
                return new ListResult<SessionApiModel>()
                {
                    IsOk = true,
                    Message = new Message("Success", MessageType.Success),
                    Items = sessionModels
                };
            }
            else
            {
                return new ListResult<SessionApiModel>()
                {
                    IsOk = true,
                    Message = new Message("You must logging in as teacher", MessageType.Error),
                    Items = null
                };
            }
            
        }

        [HttpGet]
        [Authorize]
        [Route("Api/Sessions/GetMyRejectedSessions")]
        public async Task<ListResult<SessionApiModel>> GetMyRejectedSessions()
        {
            try
            {
                if (CheckUserPermission())
                {
                    

                    var dbSessions = Db.Sessions.Where(c => c.TeacherId == CurrentUser.Id).ToList();
                    List<SessionApiModel> sessionModels = new List<SessionApiModel>();
                    foreach (var item in dbSessions)
                    {
                        Session session = Db.Sessions.Include(c => c.Availability)
                            .Include(c => c.Teacher)
                            .Include(c => c.Level)
                            .Include(c => c.Subject)
                            .Include(c => c.Language).FirstOrDefault(c => c.Id == item.Id && c.Status == (int)SessionStatusEnum.Rejected);

                        SessionApiModel laApiModel = new SessionApiModel()
                        {
                            Id = session.Id,
                            AvailabilityId = session.AvailabilityId,
                            Language = session.Language.Name,
                            LangId = session.LangId,
                            Status = session.Status,
                            Subject = session.Subject.Name,
                            Date = session.Date.ToString("MM/dd/yyyy"),
                            Level = session.Level.Name,
                            LevelId = session.LevelId,
                            Notes = session.Notes,
                            Purpose = session.Purpose,
                            Student = session.Student.FirstName + " " + session.Student.LastName,
                            StudentId = session.StudentId,
                            SubjectId = session.SubjectId,
                            Teacher = session.Teacher.FirstName + " " + session.Teacher.LastName,
                            TeacherId = session.TeacherId,
                            Title = session.Title
                        };
                        sessionModels.Add(laApiModel);
                    }
                    return new ListResult<SessionApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success", MessageType.Success),
                        Items = sessionModels
                    };
                }
                else
                {
                    return new ListResult<SessionApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                        Items = null
                    };
                }
            }
            catch(Exception ex)
            {
                return new ListResult<SessionApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }


        [HttpGet]
        [Authorize]
        [Route("Api/Sessions/GetMyApprovedSessions")]
        public async Task<ListResult<SessionApiModel>> GetMyApprovedSessions()
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    var dbSessions = Db.Sessions.Where(c => c.TeacherId == CurrentUser.Id).ToList();
                    List<SessionApiModel> sessionModels = new List<SessionApiModel>();
                    foreach (var item in dbSessions)
                    {
                        Session session = Db.Sessions.Include(c => c.Availability)
                            .Include(c => c.Teacher)
                            .Include(c => c.Level)
                            .Include(c => c.Subject)
                            .Include(c => c.Language).FirstOrDefault(c => c.Id == item.Id && c.Status == (int)SessionStatusEnum.Approved);

                        SessionApiModel laApiModel = new SessionApiModel()
                        {
                            Id = session.Id,
                            AvailabilityId = session.AvailabilityId,
                            Language = session.Language.Name,
                            LangId = session.LangId,
                            Status = session.Status,
                            Subject = session.Subject.Name,
                            Date = session.Date.ToString("MM/dd/yyyy"),
                            Level = session.Level.Name,
                            LevelId = session.LevelId,
                            Notes = session.Notes,
                            Purpose = session.Purpose,
                            Student = session.Student.FirstName + " " + session.Student.LastName,
                            StudentId = session.StudentId,
                            SubjectId = session.SubjectId,
                            Teacher = session.Teacher.FirstName + " " + session.Teacher.LastName,
                            TeacherId = session.TeacherId,
                            Title = session.Title
                        };
                        sessionModels.Add(laApiModel);
                    }
                    return new ListResult<SessionApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success", MessageType.Success),
                        Items = sessionModels
                    };
                }
                else
                {
                    return new ListResult<SessionApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                        Items = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ListResult<SessionApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }

            
        }


        [HttpPost]
        [Authorize]
        [Route("Api/Sessions/ApproveSession")]
        public async Task<Result> ApproveSession(int? id)
        {
            try
            {
                if(CheckUserPermission())
                {
                    
                    var dbSessions = Db.Sessions.Include(c => c.Availability)
                   .Where(c => c.TeacherId == CurrentUser.Id).FirstOrDefault();
                    dbSessions.Status = (int)SessionStatusEnum.Approved;
                    var dbAvailability = dbSessions.Availability;
                    dbAvailability.IsClosed = true;
                    Db.Entry(dbAvailability).State = EntityState.Modified;
                    Db.Entry(dbSessions).State = EntityState.Modified;
                    Db.SaveChanges();
                    return new Result
                    {
                        IsOk = true,
                        Message = new Message("Closed Successfully", MessageType.Success)
                    };
                }
                else
                {
                    return new Result
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error)
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error)
                };
            }
        }


        [HttpPost]
        [Authorize]
        [Route("Api/Sessions/RejectSession")]
        public async Task<Result> RejectSession(int? id)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    var dbSessions = Db.Sessions.Include(c => c.Availability)
                    .Where(c => c.TeacherId == CurrentUser.Id).FirstOrDefault();
                    dbSessions.Status = (int)SessionStatusEnum.Rejected;
                    var dbAvailability = dbSessions.Availability;
                    dbAvailability.IsClosed = true;
                    Db.Entry(dbAvailability).State = EntityState.Modified;
                    Db.Entry(dbSessions).State = EntityState.Modified;
                    Db.SaveChanges();
                    return new Result
                    {
                        IsOk = true,
                        Message = new Message("Closed Successfully", MessageType.Success)
                    };
                }
                else
                {
                    return new Result
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Success)
                    };
                }
            }
            catch (Exception ex)
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