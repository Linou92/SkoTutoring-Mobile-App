using AppDbContext.Entities;
using AppDbContext.Enums;
using TeacherApi.Models.ApiModels;
using TeacherApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System.Web;
using System.Globalization;

namespace TeacherApi.Controllers
{
    [Route("api/Availability")]
    public class AvailabilityController : BaseController
    {
        [HttpPost]
        [Route("api/Availability/RegisterAvailability")]
        [Authorize]
        public Result RegisterAvailability([FromBody] CreateAvailabilityApiModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    if (ModelState.IsValid && CurrentUser != null)
                    {
                        var currentDate = DateTime.ParseExact(model.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        TimeSpan timeSpan = new TimeSpan(0, 0, 0);
                        currentDate = currentDate.Date + timeSpan;
                        var availability = Db.Availabilities.Add(new Availability
                        {
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            Date = currentDate,
                            IsClosed = false
                        });
                        availability.TeacherAvailabilities.Add(new TeacherAvailability
                        {
                            Teacher_UserId = CurrentUser.Id,
                        });
                        Db.Availabilities.Add(availability);
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Success, Availability Inserted Successfully", MessageType.Success)
                        };
                    }
                    else
                    {
                        List<string> errors = new List<string>();
                        foreach (var item in ModelState.Values)
                        {
                            foreach (var error in item.Errors)
                            {
                                errors.Add(error.ErrorMessage);
                            }
                        }
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message(errors.ToString(), MessageType.Error),
                        };
                    }
                }
                else
                {
                    return new LoginResult()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                        ResultCode = LoginEnumResult.Failure,
                        ResultText = LoginEnumResult.Failure.ToString(),
                        Token = null
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
        [Route("api/Availability/List")]
        [Authorize]
        public ListResult<AvailabilityApiModel> List()
        {
            try
            {
                if(CheckUserPermission()) 
                {
                    
                    var avIds = Db.TeacherAvailabilities.Where(c => c.Teacher_UserId == CurrentUser.Id).Select(c => c.AvailabilityId).ToList();
                    List<AvailabilityApiModel> avs = new List<AvailabilityApiModel>();
                    foreach (var item in avIds)
                    {
                        Availability av = Db.Availabilities.Find(item);
                        AvailabilityApiModel avApiModel = new AvailabilityApiModel()
                        {
                            Id = av.Id,
                            StartTime = av.StartTime,
                            EndTime = av.EndTime,
                            Date = av.Date.ToString("MM/dd/yyyy"),
                            IsClosed = av.IsClosed
                        };
                        avs.Add(avApiModel);
                    }
                    return new ListResult<AvailabilityApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success, Availability Inserted Successfully", MessageType.Success),
                        Items = avs
                    };
                } 
                else
                {
                    return new ListResult<AvailabilityApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success, Availability Inserted Successfully", MessageType.Success),
                        Items = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ListResult<AvailabilityApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/Availability/GetAvailabilityInfo")]
        public TResult<AvailabilityApiModel> GetAvailabiltyInfo(int? id)
        {
            try
            {
                if(CheckUserPermission())
                {
                    
                    var av = Db.Availabilities.Include(c => c.TeacherAvailabilities)
                    .Where(c => c.Id == id && c.TeacherAvailabilities.Any(ee => ee.Teacher_UserId == CurrentUser.Id)).FirstOrDefault();
                    if (av == null)
                        return new TResult<AvailabilityApiModel>()
                        {
                            IsOk = true,
                            Message = new Message("Availability Not Found", MessageType.Error),
                            Item = null
                        };
                    else
                    {
                        var teachers = av.TeacherAvailabilities.Select(c => c.Teacher_UserId).ToList();
                        List<TeacherApiModel> techs = new List<TeacherApiModel>();
                        foreach (var item in teachers)
                        {
                            var te = Db.Teachers.Find(item);
                            var tech = new TeacherApiModel()
                            {
                                FirsName = te.FirstName,
                                LastName = te.LastName,
                                Id = te.Id,
                                UserId = te.UserId,
                                Role = Discriminator.Teacher
                            };
                            techs.Add(tech);
                        }
                        return new TResult<AvailabilityApiModel>()
                        {
                            IsOk = true,
                            Item = new AvailabilityApiModel()
                            {
                                Id = av.Id,
                                StartTime = av.StartTime,
                                EndTime = av.EndTime,
                                Date = av.Date.ToString("MM/dd/yyyy"),
                                IsClosed = av.IsClosed
                                // Teachers = techs
                            }
                        };
                    }
                }
                else
                {
                    return new TResult<AvailabilityApiModel>()
                    {
                        Item = null,
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error)
                    };
                }
            }
            catch (Exception ex)
            {
                return new TResult<AvailabilityApiModel>()
                {
                    Item = null,
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error)
                };
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/Availability/UpdateAvailability")]
        public Result UpdateAvailability([FromBody] AvailabilityApiModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    if (ModelState.IsValid)
                    {
                        var currentDate = DateTime.ParseExact(model.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        TimeSpan timeSpan = new TimeSpan(0, 0, 0);
                        currentDate = currentDate.Date + timeSpan;
                        var av = Db.Availabilities.Include(c => c.TeacherAvailabilities)
                            .Where(cc => cc.Id == model.Id && cc.TeacherAvailabilities.Any(e => e.Teacher_UserId == CurrentUser.Id)).FirstOrDefault();
                        if (av == null)
                        {
                            return new Result()
                            {
                                IsOk = true,
                                Message = new Message("Availability not found", MessageType.Error)
                            };
                        }
                        av.StartTime = model.StartTime;
                        av.EndTime = model.EndTime;
                        av.Date = currentDate;
                        av.IsClosed = model.IsClosed;
                        Db.Entry(av).State = EntityState.Modified;
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Availability Updated Successfully", MessageType.Success)
                        };
                    }
                    else
                    {
                        List<string> errors = new List<string>();
                        foreach (var item in ModelState.Values)
                        {
                            foreach (var error in item.Errors)
                            {
                                errors.Add(error.ErrorMessage);
                            }
                        }
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message(errors.ToString(), MessageType.Error),
                        };
                    }
                }
                else
                {
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("You must logged in as teacher", MessageType.Error),
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                };
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/Availability/CloseAvailability")]
        public Result CloseAvailability(int? id)
        {
            try
            {
                if(CheckUserPermission())
                {
                    

                    var av = Db.Availabilities
                    .Include(ee => ee.TeacherAvailabilities)
                    .Where(c => c.Id == id && c.TeacherAvailabilities.Any(ee => ee.Teacher_UserId == CurrentUser.Id))
                    .FirstOrDefault();
                    if (av == null)
                    {
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Availability not found", MessageType.Error)
                        };
                    }
                    av.IsClosed = true;
                    Db.Entry(av).State = EntityState.Modified;
                    Db.SaveChanges();
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("Availability Deleted Successfully", MessageType.Success)
                    };
                }
                else
                {
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error)
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error)
                };
            }
        }
    }
}