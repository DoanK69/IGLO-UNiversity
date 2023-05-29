using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace IGLOUniversity.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetUsernameRole(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    ViewBag.Username = claim.Value;
                    ViewBag.Id = claim.Value;
                    ViewBag.Nim = claim.Value;
                }
                if (claim.Type == ClaimTypes.Role)
                {
                    ViewBag.Role = claim.Value;
                    ViewBag.Menus = GlobalConfiguration.Menus().Where(m => m.Role == claim.Value);
                }
            }
        }

        protected string GetUsername(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
        }
        protected IEnumerable<ValidationViewModel> GetValidationMessage(ModelStateDictionary modelState)
        {
            var result = new List<ValidationViewModel>();
            foreach (KeyValuePair<string, ModelStateEntry> item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    var propertyItem = item.Key;
                    var errorMessage = item.Value.Errors.FirstOrDefault().ErrorMessage;
                    result.Add(new ValidationViewModel
                    {
                        PropertyName = propertyItem,
                        MessageError = errorMessage
                    });
                }
            }
            return result;
        }
    }
}
