using HDD.Data;
using HDD.Email;
using HDD.Models;
using Microsoft.Extensions.Options;

namespace HDD.Services
{
    public class VinService : IVinService
    {
        private IEmailService _emailService;
        private ILogger<VinService> _logger;
        private readonly EmailOptions _options;
        private readonly IHDDRepository _dataService;
        public VinService(ILogger<VinService> logger, IEmailService emailService, IOptions<EmailOptions> options
            , IHDDRepository dataService)
        {
            _logger = logger;
            _emailService = emailService;
            _options = options.Value;
            _dataService = dataService;
        }
        public async Task ClaimVin(string ownerId, string vin, string primaryOwner)
        {
            var ownersVin = new OwnersVin();
            ownersVin.OwnerId = ownerId;
            ownersVin.Vin = vin;
            ownersVin.UpdateDateTime = DateTime.Now;
            ownersVin.PrimaryOwner = primaryOwner;
            ownersVin.OwnerStatus = true;
            ownersVin.UpdateDateTime = DateTime.Now;
            await _dataService.InsertOwnersVin(ownersVin);
        }

        public bool IsPlateEligibleToClaim(string plate)
        {
            var vin = _dataService.GetVin(plate);
            return IsVinEligibleToClaim(vin);
        }

        public bool IsVinEligibleToClaim(string vin)
        {
            if (_dataService.IsVinRegulated(vin) && !_dataService.IsVinClaimed(vin))
                return true;
            return false;
        }

        public RetrofitApplicationDmvccddata GetRetrofitApplicationDmvccddata(string vin)
        {
            var rad = _dataService.GetRetrofitApplicationDmvccddata(vin);
            return rad;
        }
    }
}
