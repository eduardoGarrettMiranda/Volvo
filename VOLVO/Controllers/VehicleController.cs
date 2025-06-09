using Microsoft.AspNetCore.Mvc;
using VOLVO.BLL;
using VOLVO.Models;

namespace VOLVO.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleFactory _vehicleFactory;

        public ActionResult Index()
        {
            var vehicles = _vehicleService.ListVehicles();
            return View(vehicles);
        }

        public ActionResult Details(string chassisSeries, uint chassisNumber)
        {
            var vehicle = _vehicleService.GetVehicleByChassisId(chassisSeries, chassisNumber);
            return View(vehicle);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string type, string chassisSeries, uint chassisNumber, string color)
        {
            var chassisId = new ChassisId(chassisSeries, chassisNumber);
            var vehicle = _vehicleFactory.CreateVehicle(type, chassisId, color);

            try
            {
                _vehicleService.CreateVehicle(vehicle);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangeColor(string chassisSeries, uint chassisNumber)
        {
            var vehicle = _vehicleService.GetVehicleByChassisId(chassisSeries, chassisNumber);
            return View(vehicle);
        }

        [HttpPost]
        public ActionResult ChangeColor(string chassisSeries, uint chassisNumber, string color)
        {
            _vehicleService.ChangeColor(chassisSeries, chassisNumber, color);

            return RedirectToAction("Index");
        }

        public VehicleController(IVehicleService vehicleService, IVehicleFactory vehicleFactory)
        {
            _vehicleService = vehicleService;
            _vehicleFactory = vehicleFactory;
        }
    }
}
