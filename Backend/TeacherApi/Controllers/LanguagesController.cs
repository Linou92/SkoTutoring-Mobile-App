using AppDbContext.Entities;
using AppDbContext.Enums;
using TeacherApi.Models.ApiModels;
using TeacherApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Web;
using System.Threading.Tasks;

namespace TeacherApi.Controllers
{
    [Route("Api/Languages")]
    public class LanguagesController : BaseController
    {
        [HttpPost]
        [Route("api/Languages/RegisterLanguage")]
        [Authorize]
        public async Task<Result> RegisterLanguage([FromBody] LanguageApiModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    if (ModelState.IsValid)
                    {
                        var language = Db.Languages.Add(new Language()
                        {
                            Name = model.Name
                        });
                        language.TeacherLangs.Add(new TeacherLang
                        {
                            Teacher_UserId = CurrentUser.Id
                        });
                        Db.Languages.Add(language);
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Success, Language Inserted Successfully", MessageType.Success)
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
        [Route("api/Languages/List")]
        [Authorize]
        public ListResult<LanguageApiModel> List()
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    var langIds = Db.TeacherLangs.Where(c => c.Teacher_UserId == CurrentUser.Id).Select(c => c.LangId).ToList();
                    List<LanguageApiModel> langs = new List<LanguageApiModel>();
                    foreach (var item in langIds)
                    {
                        Language language = Db.Languages.Find(item);
                        LanguageApiModel laApiModel = new LanguageApiModel()
                        {
                            Id = language.Id,
                            Name = language.Name,
                        };
                        langs.Add(laApiModel);
                    }
                    return new ListResult<LanguageApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Success, Language Inserted Successfully", MessageType.Success),
                        Items = langs
                    };
                }
                else
                {
                    return new ListResult<LanguageApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("You must logging in as teacher", MessageType.Error),
                        Items = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ListResult<LanguageApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }

        [HttpGet]
        [Route("api/Languages/GetLanguageInfo")]
        [Authorize]
        public TResult<LanguageApiModel> GetLanguageInfo(int? id)
        {
            try
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return new TResult<LanguageApiModel>
                    {
                        IsOk = true,
                        Message = new Message("Error ,Please Login as Teacher", MessageType.Error),
                        Item = null
                    };
                var user = UserManager.FindByEmailAsync(User.Identity.Name).Result;
                CurrentUser = new UserApiModel
                {
                    Id = user.Id,
                    Role = "Teacher",
                    Token = null
                };
                var lang = Db.Languages.Include(c => c.TeacherLangs)
                    .Where(ee => ee.Id == id && ee.TeacherLangs.Any(t => t.Teacher_UserId == CurrentUser.Id)).FirstOrDefault();
                if (lang == null)
                    return new TResult<LanguageApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Language Not Found", MessageType.Error),
                        Item = null
                    };
                else
                {
                    var teachers = lang.TeacherLangs.Select(c => c.Teacher_UserId).ToList();
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
                    return new TResult<LanguageApiModel>()
                    {
                        IsOk = true,
                        Item = new LanguageApiModel()
                        {
                            Id = lang.Id,
                            Name = lang.Name,
                           // Teachers = techs
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new TResult<LanguageApiModel>()
                {
                    Item = null,
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error)
                };
            }
        }

        [HttpPost]
        [Route("api/Languages/UpdateLanguage")]
        [Authorize]
        public Result UpdateLanguage([FromBody] LanguageApiModel model)
        {
            try
            {
                if (CheckUserPermission())
                {
                    
                    if (ModelState.IsValid)
                    {
                        var lang = Db.Languages.Find(model.Id);
                        if (lang == null)
                        {
                            return new Result()
                            {
                                IsOk = true,
                                Message = new Message("Language not found", MessageType.Error)
                            };
                        }
                        lang.Name = model.Name;
                        Db.Entry(lang).State = EntityState.Modified;
                        Db.SaveChanges();
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Language Updated Successfully", MessageType.Error)
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
        [Route("api/Languages/DeleteLanguage")]
        [Authorize]
        public Result DeleteLanguage(int? id)
        {
            try
            {
               if(CheckUserPermission())
                {
                    
                    var lang = Db.Languages.Find(id);
                    if (lang == null)
                    {
                        return new Result()
                        {
                            IsOk = true,
                            Message = new Message("Language not found", MessageType.Error)
                        };
                    }
                    var list = Db.TeacherLangs.Where(c => c.LangId == lang.Id).ToList();
                    foreach (var item in list)
                    {
                        Db.TeacherLangs.Remove(item);
                        Db.SaveChanges();
                    }
                    Db.Languages.Remove(lang);
                    Db.SaveChanges();
                    return new Result()
                    {
                        IsOk = true,
                        Message = new Message("Language Deleted Successfully", MessageType.Error)
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