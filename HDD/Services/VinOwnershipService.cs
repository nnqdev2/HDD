using HDD.Areas.Identity.Data;
using HDD.Data;
using HDD.Email;
using HDD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;

namespace HDD.Services
{
    public class VinOwnershipService : IVinOwnershipService
    {
        private IEmailService _emailService;
        private ILogger<VinOwnershipService> _logger;
        private readonly EmailOptions _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHDDRepository _dataService;
        public VinOwnershipService(ILogger<VinOwnershipService> logger, IEmailService emailService, IOptions<EmailOptions> options
            , UserManager<ApplicationUser> userManager, IHDDRepository dataService)
        {
            _logger = logger;
            _emailService = emailService;
            _options = options.Value;
            _userManager = userManager;
            _dataService = dataService;
        }

        public void SendPermissionNotificationEmail(string requestEmail, string vin, bool isApproved)
        {
            StringBuilder sb = new StringBuilder();
            DateTime now = DateTime.Now;
            sb.Append(now.ToLongDateString() + " " + now.ToLongTimeString() + "</br>");
            sb.Append("click here to approve https://localhost:7137/op/1/999/876  </br>");
            sb.Append("click here to deny    https://localhost:7137/op/0/999/876  </br>");
            throw new NotImplementedException();
        }

        public void SendRequestPermissionEmail(string primaryEmail, string requestEmail, string vin)
        {
            StringBuilder sb = new StringBuilder();
            DateTime now = DateTime.Now;
            sb.Append(now.ToLongDateString() + " " + now.ToLongTimeString() + "\r\n\r\n");
            sb.Append("click here to approve https://localhost:7137/op/1/999/876  \r\n\r\n");
            sb.Append("click here to deny    https://localhost:7137/op/0/999/876  \r\n\r\n");
            _emailService.SendEmail(_options.AdminEmail, "ngaquan4@gmail.com", "request secondary permission emal", sb.ToString(), false);
        }

        public bool IsIncomingSecondaryOwner(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VinOwnership>> GetPrimaryOwnershipVins(string ownerId)
        {
            var ownersVins = _dataService.GetPrimaryOwnersVins(ownerId);
            IList<VinOwnership> vinOwnerships = new List<VinOwnership>();
            foreach (OwnersVin ownerVin in ownersVins)
            {
                var user = await _userManager.FindByIdAsync(ownerVin.OwnerId);
                var vinOwnership = new VinOwnership
                {
                    OwnerId = ownerVin.OwnerId,
                    Vin = ownerVin.Vin,
                    PrimaryOwner = ownerVin.PrimaryOwner,
                    OwnerStatus = ownerVin.OwnerStatus,
                    UpdateDateTime = ownerVin.UpdateDateTime,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                vinOwnerships.Add(vinOwnership);

            }

            return vinOwnerships;
        }

        public async Task<IList<EmailInfo>> GetSecondaryOwners(string ownerId)
        {
            var secondaryOwnersVins = await _dataService.GetSecondaryOwnerIds("aaf7efd0-ae13-48e9-9d72-83e713ef8100");
            //var secondaryOwnerIds = secondaryOwnersVins.GroupBy(o => o.OwnerId).Select(s => s.First());
            var distinctSecondaryOwnersVins = secondaryOwnersVins.GroupBy(o => new { o.OwnerId }).Select(g => g.First());
            IList<EmailInfo> emailInfos = new List<EmailInfo>();
            foreach (var secondaryOwnersVin in distinctSecondaryOwnersVins)
            {
                var user = await _userManager.FindByIdAsync(secondaryOwnersVin.OwnerId);
                var emailInfo = new EmailInfo
                {
                    //OwnerId = ownerId,
                    SecondaryOwnerId = secondaryOwnersVin.OwnerId,
                    Email = user.Email,
                    //FirstName = user.FirstName,
                    //LastName = user.LastName
                };
                emailInfos.Add(emailInfo);
            }
            return emailInfos;
        }

        public async Task<IList<string>> GetVinsForSecondaryOwnershipAssignment(string ownerId, string secondaryOwnerId)
        {
            return await _dataService.GetVinsForSecondaryOwnershipAssignment(ownerId, secondaryOwnerId);
        }
    }
}
