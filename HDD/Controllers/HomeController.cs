using HDD.Areas.Identity.Data;
using HDD.Data;
using HDD.FileUploads;
using HDD.Models;
using HDD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace HDD.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHDDRepository _dataService;
        private readonly IFileUploadService _uploadService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVinService _vinService;
        private readonly IVinOwnershipService _vinOwnershipService;
        private string? userId;
        private ApplicationUser? user;

        public HomeController(ILogger<HomeController> logger, IHDDRepository dataService
            , IFileUploadService uploadService, UserManager<ApplicationUser> userManager
            , IVinService vinService, IVinOwnershipService vinOwnershipService)
        {
            _logger = logger;
            _dataService = dataService;
            _uploadService = uploadService;
            _userManager = userManager;
            _vinService = vinService;
            _vinOwnershipService = vinOwnershipService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            userId = _userManager.GetUserId(User); // get user Id
            user = await _userManager.GetUserAsync(User); // get user's all data
            if (user == null)
            {
                return View();
            }

            return RedirectToAction("GetVins", "Home");
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Vin,Plate")] VinPlateEmailViewModel vpevm)
        {
            var isRegulated = _dataService.IsPlateRegulated(vpevm.Plate);
            if (isRegulated)
            {
                ModelState.AddModelError(String.Empty, "Plate is regulated.  Please register or log in to claim/manage plate/vin.");
            }
            else
            {
                ModelState.AddModelError("Plate", "Plate is not regulated.");
            }
            return View(vpevm);
            //var avm = new ApplicationViewModel();
            //avm.Vin = vpevm.Vin;
            //avm.Plate = vpevm.Plate;
            ////avm.Email = vpevm.Email;
            //return RedirectToAction(nameof(Details), new { vin = vpevm.Vin, plate = vpevm.Plate });
        }
        [Authorize]
        public async Task<IActionResult> GetVins()
        {
            userId = _userManager.GetUserId(User); // get user Id
            user = await _userManager.GetUserAsync(User); // get user's all data
            var result = _dataService.GetVins(userId);

            //var x = await _vinOwnershipService.GetSecondaryOwners("aaf7efd0-ae13-48e9-9d72-83e713ef8100");
            return View("GetVins",result);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ClaimAVin()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClaimAVin([Bind("Vin,Plate")] VinPlateEmailViewModel vpevm)
        {
            userId = _userManager.GetUserId(User); // get user Id
            user = await _userManager.GetUserAsync(User); // get user's all data
            bool isClaimable;
            if (vpevm.Vin == null)
            {
                isClaimable = _vinService.IsPlateEligibleToClaim(vpevm.Plate);
            } else
            {
                isClaimable = _vinService.IsVinEligibleToClaim(vpevm.Vin);
            }
                
            if (isClaimable)
            {
                await _vinService.ClaimVin(userId, vpevm.Vin, "yes");
                TempData["success"] = (vpevm.Vin == null? vpevm.Plate : vpevm.Vin) + " claimed!";
            }
            else
            {
                ModelState.AddModelError(String.Empty, "VIN/Plate is not claimable.");
                ModelState.AddModelError("vpevm.Vin", "VIN is not claimable.");
                ModelState.AddModelError("vpevm.Plate", "Plate is not claimable.");
            }

            return View();
            //var avm = new ApplicationViewModel();
            //avm.Vin = vpevm.Vin;
            //avm.Plate = vpevm.Plate;
            ////avm.Email = vpevm.Email;
            //return RedirectToAction(nameof(Details), new { vin = vpevm.Vin, plate = vpevm.Plate });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(string vin)
        {
            var rad = _vinService.GetRetrofitApplicationDmvccddata(vin);
            //var vehicle = _dataService.GetVehicle(vin, plate, email);
            //if (vehicle == null)
            //    return View("Error");
            //ApplicationViewModel applicationViewModel = new ApplicationViewModel()
            //{
            //    VehicleId = vehicle.VehicleId,
            //    Vin = vehicle.Vin,
            //    Plate = vehicle.Plate,
            //    Year = vehicle.Year,
            //    Email = email,
            //    Enginemfg = vehicle.Enginemfg,
            //    Enginemodelyear = vehicle.Enginemodelyear,
            //    Gvwr = vehicle.Gvwr,
            //    Enginedisplacement = vehicle.Enginedisplacement,
            //    Enginefamilymember = vehicle.Enginefamilymember,
            //    Ownername  = vehicle.Ownername,
            //    Address = vehicle.Address,
            //    City = vehicle.City,
            //    State = vehicle.State,
            //    Zip = vehicle.Zip,
            //    Registrationexpirationdate = vehicle.Registrationexpirationdate,
            //    Publiclyowned = vehicle.Publiclyowned,
            //    Artfamilyname = vehicle.Artfamilyname,
            //    Retrofitprovider = vehicle.Retrofitprovider,
            //    Retrofittype = vehicle.Retrofittype,
            //    Installationdate = vehicle.Installationdate,
            //    Comment = vehicle.Comment,
            //    SubmissionStatus = vehicle.SubmissionStatus,
            //    LastUpdatedDate = vehicle.LastUpdatedDate,
            //    LastUpdatedBy = vehicle.LastUpdatedBy
            //};
            return View("Details", rad);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details([Bind("VehicleId,Vin,Email,Year,Enginemodelyear,Enginemfg,Gvwr,Enginedisplacement,Enginefamilymember,SubmissionStatus")] VehicleInfoViewModel avm)
        {
            var x = 2;
            return View("Error");
            //var vehicle = _dataService.UpdateVehicle(avm);
            //if (vehicle == null)
            //    return View("Error");
            //return View("Details", avm);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Details([Bind("Vin,Plate,Email,Year,Enginemodelyear,Enginemfg,Enginedisplacement,Enginefamilymember,Comment,SubmissionStatus")] ApplicationViewModel avm)
        //{
        //    var vehicle = _dataService.UpdateVehicle(avm);
        //    if (vehicle == null)
        //        return View("Error");
        //    return View("Details", avm);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Details([Bind("Vin,Plate,Email,Year,Make,Comment,SubmissionStatus")] ApplicationViewModel avm)
        //{
        //    var vehicle = _dataService.UpdateVehicle(avm);
        //    if (vehicle == null)
        //        return View("Error");
        //    return View("Details", avm);
        //}
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            //string vinIn = "CA51767";
            //var plateIn = "3VWRL7AJ7AM092282";
            //var emailIn = "quan.nga@deq.oregon.gov";

            //var vehicle = _dataService.GetEmailCode(plateIn, vinIn, emailIn);
            await _uploadService.UploadFile(files);
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult Search()
        {
            //ViewData["Title"] = "Search Page";
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignSecondaryOwner([Bind("Vin,Plate,Email,Code")] VinPlateEmailViewModel vpecvm)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            //ViewData["Title"] = "Search Page";
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AssignSecondaryOwner()
        {
            userId = _userManager.GetUserId(User);
            var emails = await _vinOwnershipService.GetSecondaryOwners(userId);
            ViewBag.Emails = emails.Select(x => new SelectListItem()
            {
                Text = x.Email,
                Value = x.SecondaryOwnerId

            }).ToList();
            return View();
        }


        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetVinsForAssignment(string secondaryOwnerId)
        {
            userId = _userManager.GetUserId(User);
            var x = await _vinOwnershipService.GetVinsForSecondaryOwnershipAssignment(userId, secondaryOwnerId);
            return (JsonResult)x ;
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}