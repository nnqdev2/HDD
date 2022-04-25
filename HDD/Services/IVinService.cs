namespace HDD.Services
{
    public interface IVinService
    {
        Task ClaimVin(string ownerId, string vin, string primaryOwner);
        bool IsPlateEligibleToClaim(string plate);

        bool IsVinEligibleToClaim(string vin);
    }
}
