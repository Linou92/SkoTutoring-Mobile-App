using AppDbContext.Entities;
using AppDbContext.Enums;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using StudentApi.Models.ApiModels;
using StudentApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace StudentApi.Controllers
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
                        var resRole = await UserManager.AddToRoleAsync(user.Id, "Student");

                        Db.Students.Add(new AppDbContext.Entities.Student
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
                    } 
                    else
                    {
                        return new RegisterResult()
                        {
                            IsOk = true,
                            Message = new Message(result.Errors.ToString(), MessageType.Error),
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
                        Message = new Message(errors.ToString(), MessageType.Error),
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
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("grant_type", "password");
                param.Add("username", model.UserName);
                param.Add("password", model.Password);
                HttpResponseMessage response = client.PostAsync(url + "/Token", new FormUrlEncodedContent(param)).Result;
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
                    Message = new Message("An Error Occured", MessageType.Error)
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
        [Authorize]
        public ListResult<Country> GetAllCountries()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var b = identity.Name;
                return new ListResult<Country>()
                {
                    IsOk = true,
                    Items = Db.Countries.ToList(),
                    Message = new Message("Get All Countries Succeeded", MessageType.Success)
                };
            }
            else 
            {
                return new ListResult<Country>()
                {
                    IsOk = true,
                    Items = null,
                    Message = new Message("You Must Logging First", MessageType.Error)
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
                var b = identity.Name;
                var user = await UserManager.FindByEmailAsync(identity.Name);
                var role = await RoleManager.FindByIdAsync(user.Roles.FirstOrDefault().RoleId);
                return new TResult<UserApiModel>
                {
                    IsOk = true,
                    Item = new UserApiModel
                    {
                        Id = user.Id,
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
                    Message = new Message("You Must Logging First", MessageType.Error)
                };
            }
        }
    }
}