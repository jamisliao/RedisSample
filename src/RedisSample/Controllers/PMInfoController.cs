using Microsoft.AspNetCore.Mvc;
using RedisSample.Services;

namespace RedisSample.Controllers
{
    public class PMInfoController : Controller
    {
        private IPM2Point5Service _pm2point5Service;

        public PMInfoController(IPM2Point5Service pm2point5Service)
        {
            this._pm2point5Service = pm2point5Service;
        }

        public IActionResult Index()
        {
            var result = this._pm2point5Service.GetPointHistory();
            return View();
        }
    }
}