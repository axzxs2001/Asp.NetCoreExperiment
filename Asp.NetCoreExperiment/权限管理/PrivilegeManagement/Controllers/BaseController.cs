using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrivilegeManagement.Controllers
{
    [Authorize]
    public class BaseController:Controller
    {
    }
}
