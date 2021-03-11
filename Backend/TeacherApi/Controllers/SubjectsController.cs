using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using TeacherApi.Results;
using TeacherApi.Models.ApiModels;
using AppDbContext.Enums;
using AppDbContext.Entities;
using System.Web;

namespace TeacherApi.Controllers
{
    [Route("Api/Subjects")]
    public class SubjectsController : BaseController
    {
        [HttpPost]
        [Route("Api/Subjects/RegisterSubject")]
        [Authorize]
        public Result RegisterSubject([FromBody] SubjectApiModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    if (ModelState.IsValid && CurrentUser != null)
                    {
                        var subject = Db.Subjects.Add(new Subject()
                        {
                            Name = model.Name
                        });
                        Db.SaveChanges();
                        Db.TeacherSubjs.Add(new TeacherSubj
                        {
                            SubjectId = subject.Id,
                            Teacher_UserId = CurrentUser.Id
                        });
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Success, Subject Inserted Successfully", MessageType.Success)
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
        [Route("Api/Subjects/List")]
        [Authorize]
        public ListResult<SubjectApiModel> List()
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    var subjIds = Db.TeacherSubjs.Where(c => c.Teacher_UserId == CurrentUser.Id).Select(c => c.SubjectId).ToList();
                    List<SubjectApiModel> subjects = new List<SubjectApiModel>();
                    foreach (var item in subjIds)
                    {
                        Subject subject = Db.Subjects.Find(item);
                        SubjectApiModel suApiModel = new SubjectApiModel()
                        {
                            Id = subject.Id,
                            Name = subject.Name,
                        };
                        subjects.Add(suApiModel);
                    }
                    return new ListResult<SubjectApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success, Subjects Returned Successfully ", MessageType.Success),
                        Items = subjects
                    };
                }
                else
                {
                    return new ListResult<SubjectApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                        Items = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ListResult<SubjectApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }

        [HttpGet]
        [Route("Api/Subjects/GetSubjectInfo")]
        [Authorize]
        public TResult<SubjectApiModel> GetSubjectInfo(int? id)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    var sub = Db.Subjects.Include(c => c.TeacherSubjs).Where(ee => ee.Id == id && ee.TeacherSubjs.Any(t => t.Teacher_UserId == CurrentUser.Id)).FirstOrDefault();
                    if (sub == null)
                        return new TResult<SubjectApiModel>()
                        {
                            IsOk = true,
                            Message = new Message("Subject Not Found", MessageType.Error),
                            Item = null
                        };
                    else
                    {
                        var teachers = sub.TeacherSubjs.Select(c => c.Teacher_UserId).ToList();
                        List<TeacherApiModel> techs = new List<TeacherApiModel>();
                        foreach (var item in teachers)
                        {
                            var te = Db.Teachers.Find(item);
                            var tech = new TeacherApiModel()
                            {
                                FirsName = te.FirstName,
                                LastName = te.LastName,
                                Id = te.Id,
                                Role = Discriminator.Teacher
                            };
                            techs.Add(tech);
                        }
                        return new TResult<SubjectApiModel>()
                        {
                            IsOk = true,
                            Message = new Message("Success",MessageType.Success),
                            Item = new SubjectApiModel()
                            {
                                Id = sub.Id,
                                Name = sub.Name,
                                Teachers = techs
                            }
                        };
                    }
                }
                else
                {
                    return new TResult<SubjectApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher",MessageType.Error),
                        Item = null
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new TResult<SubjectApiModel>()
                {
                    Item = null,
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error)
                };
            }
        }

        [HttpPost]
        [Route("Api/Subjects/UpdateSubject")]
        [Authorize]
        public Result UpdateSubject([FromBody] SubjectApiModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    if (ModelState.IsValid)
                    {
                        var subject = Db.Subjects.Find(model.Id);
                        if (subject == null)
                        {
                            return new Result()
                            {
                                IsOk = true,
                                Message = new Message("Subject not found", MessageType.Error)
                            };
                        }
                        subject.Name = model.Name;
                        Db.Entry(subject).State = EntityState.Modified;
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Subject Updated Successfully", MessageType.Success)
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
                        Message = new Message("You must logging in as teacher", MessageType.Error),
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
        [Route("Api/Subjects/DeleteSubject")]
        [Authorize]
        public Result DeleteSubject(int? id)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    var subject = Db.Subjects.Find(id);
                    if (subject == null)
                    {
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Subject not found", MessageType.Error)
                        };
                    }
                    var list = Db.TeacherSubjs.Where(c => c.SubjectId == subject.Id && c.Teacher_UserId == CurrentUser.Id).ToList();
                    foreach (var item in list)
                    {
                        Db.TeacherSubjs.Remove(item);
                        Db.SaveChanges();
                    }
                    Db.Subjects.Remove(subject);
                    Db.SaveChanges();
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("Subject Deleted Successfully", MessageType.Success)
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
            catch(Exception ex)
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
