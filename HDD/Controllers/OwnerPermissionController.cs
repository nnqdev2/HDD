
using HDD.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HDD.Controllers
{
    [Route("op")]
    [ApiController]
    public class OwnerPermissionController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVinOwnershipService _ownerPermissionService;
        public OwnerPermissionController(ILogger<HomeController> logger, IVinOwnershipService ownerPermissionService)
        {
            _logger = logger;
            _ownerPermissionService = ownerPermissionService;
        }

        [HttpGet("{isApproved}/{ownerId}/{vin}")]
        [Produces("text/html")]
        public IActionResult OwnerType(int isApproved, int ownerId, string vin)
        {
            //update database 
            //send email
            StringBuilder sb = new StringBuilder();
            DateTime now = DateTime.Now;
            sb.Append(now.ToLongDateString() + " " + now.ToLongTimeString() + "\r\n\r\n");
            var z = isApproved;
            var x = ownerId;
            var y = vin;
            string html = @" 
            <title>Success title</title>
            <style type='text/css'>
            button{
                color: green;
            }
            </style>
            <h1> Successfully updated the permission </h1>
            <p>Successfully updated the permission </p>
            <p style='color:blue;'>I am blue</p>
            ";
            string html2 = $" {html}" + $"<p style='color:blue;'> {now.ToLongTimeString()} </p>";
     
            return Content(html2, "text/html", Encoding.UTF8);
         }

        [HttpGet("sendemail")]
        public IActionResult SendEmail()
        {
            //update database 
            //send email
            _ownerPermissionService.SendRequestPermissionEmail("nga.quan@deq.oregon.gov", "ngaquan4@gmail.com", "vinnumberxxxx");
            return NoContent();
        }

    }
}
