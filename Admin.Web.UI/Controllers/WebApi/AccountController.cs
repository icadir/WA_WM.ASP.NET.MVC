using Admin.BLL.Identity;
using Admin.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Http;

namespace Admin.Web.UI.Controllers.WebApi
{
    public class AccountController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetLoginInfo()
        {
            var userManager = MembershipTools.NewUserManager();
            var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            return Ok(new UserProfileViewModel()
            {
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                AvatarPath = user.AvatarPath,
                Surname = user.Surname,
                Id = user.Id
            });
        }
    }
}
