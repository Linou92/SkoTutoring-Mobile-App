using AppDbContext.Entities;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Http;
using StudentApi.Models.ApiModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace StudentApi.Controllers
{
    public class BaseController : ApiController
    {
       
        private SignInManager<ApplicationUser,string> _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        protected ApplicationRoleManager RoleManager
        {
            get 
            { 
            return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set { _roleManager = value; }
        }
        protected SignInManager<ApplicationUser,string> SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<SignInManager<ApplicationUser,string>>();
            }
            private set { _signInManager = value; }
        }
        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set { _userManager = value; }
        }
        protected readonly OnlineCourseDb Db;

        // new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        
        public BaseController(ApplicationUserManager manager, SignInManager<ApplicationUser,string> signInManager)
        {
            UserManager = manager;
            SignInManager = signInManager;
            Db = new OnlineCourseDb();
        }
        public BaseController()
        {
            Db = new OnlineCourseDb();
        }
        public UserApiModel CurrentUser
        {
            get; set;
        }

        public string UserId
        {
            get
            {
                return CurrentUser.Id;
            }
        }
        protected void SetUser()
        {

        }

        protected bool CheckUserPermission()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;
            else
            {
                var user = UserManager.FindByName(User.Identity.Name);
                if (user != null)
                {
                    var role = RoleManager.FindById(user.Roles.FirstOrDefault().RoleId);
                    if (role.Name == "Student" || role.Name == "SuperAdmin")
                    {
                        CurrentUser = new UserApiModel
                        {
                            Id = user.Id,
                            Role = role.Name
                        };
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }
        protected async Task<UserApiModel> GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                CurrentUser = new UserApiModel
                {
                    Id = user.Id,
                    Role = "Student",
                    Token = null
                };
                return CurrentUser;
            }
            else
                return null;
        }
    }
}