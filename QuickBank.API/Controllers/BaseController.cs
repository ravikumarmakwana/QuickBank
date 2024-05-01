using Microsoft.AspNetCore.Mvc;
using QuickBank.API.Filters;

namespace QuickBank.API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ExceptionFilter))]
    public class BaseController : Controller
    { }
}
