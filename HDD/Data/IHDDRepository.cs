using HDD.Models;

namespace HDD.Data
{
    public interface IHDDRepository
    {
        //Vehicle GetVehicle(string vin, string plate);
        //IEnumerable<EmailCode> GetEmailCode(int vehicleId, string email);
        //Task<EmailCode> GetEmailCode(string vin, string plate, string email);
        Vehicle GetVehicle(string vin, string plate, string email);
        Vehicle GetVehicle(string vin, string plate);
        Vehicle GetVehicle(int vehicleId);
        Task UpdateVehicle(ApplicationViewModel avm);
        IEnumerable<EmailCode> GetEmailCode(string vin, string plate, string email);
        //Task<EmailCode> SendEmailCode(string vin, string plate, string email);

        bool IsIncomingPrimaryOwner(string vin, string plate, string registeredZip);
        bool IsIncomingSecondaryOwner(string email);
        Task InsertOwnersVin(OwnersVin ownersVin);
        IEnumerable<OwnersVin> GetVins(string ownerId);
        IEnumerable<OwnersVin> GetPrimaryOwnersVins(string ownerId);
        Task<IList<OwnersVin>> GetSecondaryOwnerIds(string ownerId);
        RetrofitApplicationDmvccddata GetRetrofitApplicationDmvccddata(string vin);
        IEnumerable<VehicleDocuments> GetVehicleDocuments(string vin);
        bool IsVinRegulated(string vin);
        bool IsPlateRegulated(string plate);

        string GetVin(string plate);

        bool IsVinClaimed(string vin);
        Task<IList<string>> GetVinsForSecondaryOwnershipAssignment(string ownerId, string secondaryOwnerId);
    }
}
