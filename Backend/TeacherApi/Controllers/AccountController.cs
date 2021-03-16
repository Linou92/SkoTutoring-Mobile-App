using AppDbContext.Entities;
using AppDbContext.Enums;
using TeacherApi.Models.ApiModels;
using TeacherApi.Results;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TeacherApi.DataSeed;

namespace TeacherApi.Controllers
{
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        public AccountController()
        {

        }
        public AccountController(ApplicationUserManager manager,SignInManager<ApplicationUser,string> _signInManager) : base(manager,_signInManager)
        {
                
        }
      
        [Route("api/Account/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<RegisterResult> Register([FromBody] RegisterTeacherApiModel model)
        {
            var ctx = HttpContext.Current;
            try
            {
                if (ModelState.IsValid)
                {
                    string filePath = "";
                    foreach (string file in ctx.Request.Files)
                    {
                        var postedFile = ctx.Request.Files[file];
                        filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                    }
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.UserName + "@courses.com",
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        await UserManager.ConfirmEmailAsync(user.Id, token);
                        var resRole = await UserManager.AddToRoleAsync(user.Id, "Teacher");

                        Db.Teachers.Add(new Teacher
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            UserId = user.Id,
                            CountryId = model.CountryId.HasValue ? model.CountryId.Value : Db.Countries.FirstOrDefault().Id,
                            ImageUrl = string.IsNullOrEmpty(filePath) ? null : filePath
                        });
                        await Db.SaveChangesAsync();
                        return new RegisterResult()
                        {
                            IsOk = true,
                            Message = new Message(MessageType.Success.ToString(), MessageType.Success),
                            Username = ""
                        };
                    } else
                    {
                    List<string> errors = new List<string>();
                        foreach(var item in result.Errors)
                        {
                            errors.Add(item);
                        }
                        return new RegisterResult()
                        {
                            IsOk = true,
                            Message = new Message(string.Join(",",errors.ToArray()), MessageType.Error),
                            Username = ""
                        };
                    }
                }
                else
                {
                    List<string> errors = new List<string>();
                    foreach(var item in ModelState.Values)
                    {
                        foreach(var error in item.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return new RegisterResult()
                    {
                        IsOk = true,
                        Message = new Message(string.Join(",",errors.ToArray()), MessageType.Error),
                        Username = null
                    };
                }
            }
            catch(Exception ex)
            {
                return new RegisterResult()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Username = null
                };
            }
            
        }  
   
        [Route("api/Account/Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginResult> Login([FromBody] LoginApiModel model,string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await UserManager.FindAsync(model.UserName, model.Password);
                    if(user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: true, true);
                        
                        if (User.Identity.IsAuthenticated)
                        {
                            var LoggedInUser = await UserManager.FindByEmailAsync(User.Identity.Name);
                            CurrentUser = new UserApiModel
                            {
                                Id = LoggedInUser.Id,
                                Role = RoleManager.FindByIdAsync(LoggedInUser.Roles.FirstOrDefault().RoleId).Result.Name,
                            };
                        }
                        return new LoginResult()
                        {
                            IsOk = true,
                            ResultCode = LoginEnumResult.Failure,
                            ResultText = LoginEnumResult.Failure.ToString(),
                            Message = new Message("Invalid Login Attempt, User Not Found", MessageType.Error),
                            Token = null
                        };
                    }
                   else
                    {
                        return new LoginResult()
                        {
                            IsOk = true,
                            ResultCode = LoginEnumResult.Failure,
                            ResultText = LoginEnumResult.Failure.ToString(),
                            Message = new Message("Invalid Login Attempt, User Not Found",MessageType.Error),
                            Token = null
                        };
                    }
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
                    return new LoginResult()
                    {
                        IsOk = true,
                        Message = new Message(errors.ToString(), MessageType.Error),
                        ResultCode = LoginEnumResult.Failure,
                        ResultText = LoginEnumResult.Failure.ToString(),
                        Token = null,
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


        [Route("api/Account/RequestToken")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginResult> RequestToken([FromBody] LoginApiModel model)
        {
            try
            {
                var ctx = HttpContext.Current;
                HttpClient client = new HttpClient();
                var url = "http://" + ctx.Request.Url.Host + "/" + ctx.Request.ApplicationPath;
                var baseAddress = ConfigurationManager.AppSettings.Get("BaselUrl");
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("grant_type", "password");
                param.Add("username", model.UserName);
                param.Add("password", model.Password);
                HttpResponseMessage response = client.PostAsync(url + "/Token",new FormUrlEncodedContent(param)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var JsonContent = response.Content.ReadAsStringAsync().Result;
                    var token = JsonConvert.DeserializeObject<RequestTokenModel>(JsonContent);
                    return new LoginResult
                    {
                        IsOk = true,
                        Token = token.access_token,
                        Message = new Message("Success", MessageType.Success)
                    };
                }
                return new LoginResult
                {
                    IsOk = true,
                    Message = new Message("An Error Occured",MessageType.Error)
                };

            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Token = null
                };
            }
        }
        [HttpGet]
        [Route("Api/Account/GetAllCountries")]
        [AllowAnonymous]
        public ListResult<CountryApiModel> GetAllCountries()
        {
             try
            {
               
                List<Country> DbCountries = Db.Countries.ToList();
                List<CountryApiModel> countries = new List<CountryApiModel>();
                foreach (var item in DbCountries)
                {
                    countries.Add(new CountryApiModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                return new ListResult<CountryApiModel>()
                {
                    IsOk = true,
                    Items = countries,
                    Message = new Message("Success", MessageType.Success)
                };
            }
            catch (Exception ex)
            {
                return new ListResult<CountryApiModel>()
                {
                    IsOk = true,
                    Items = null,
                    Message = new Message(ex.Message, MessageType.Error)
                };
            }
        }

        [HttpGet]
        [Route("Api/Account/GetUserInfo")]
        [Authorize]
        public async  Task<TResult<UserApiModel>> GetUserInfo()
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;                
                var user = await UserManager.FindByNameAsync(identity.Name);
                Teacher teacher = null;
                if(user == null)
                    return new TResult<UserApiModel>()
                    {
                        IsOk = true,
                        Item = null,
                        Message = new Message("User Not Found", MessageType.Error)
                    };
                else
                    teacher = Db.Teachers.Where(c => c.UserId == user.Id).FirstOrDefault();
                var role = await RoleManager.FindByIdAsync(user.Roles.FirstOrDefault().RoleId);
                return new TResult<UserApiModel>
                {
                    IsOk = true,
                    Item = new UserApiModel
                    {
                        Id = user.Id,
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        UserName = user.UserName,
                        EmailAddress = user.Email,
                        Role = role.Name,
                        Token = ""
                    },
                    Message = new Message("Get All Countries Succeeded", MessageType.Success)
                };
            }
            else
            {
                return new TResult<UserApiModel>()
                {
                    IsOk = true,
                    Item = null,
                    Message = new Message("You Must Loggin First", MessageType.Error)
                };
            }
        }
    }
}