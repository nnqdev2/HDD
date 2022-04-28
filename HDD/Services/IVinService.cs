using HDD.Models;

namespace HDD.Services
{
    public interface IVinService
    {
        Task ClaimVin(string ownerId, string vin, string primaryOwner);
        bool IsPlateEligibleToClaim(string plate);

        bool IsVinEligibleToClaim(string vin);

        RetrofitApplicationDmvccddata GetRetrofitApplicationDmvccddata(string vin);
    }
}
