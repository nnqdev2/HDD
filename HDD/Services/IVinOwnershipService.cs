using HDD.Models;

namespace HDD.Services
{
    public interface IVinOwnershipService
    {
        public void SendRequestPermissionEmail(string primaryEmail, string requestEmail, string vin);
        public void SendPermissionNotificationEmail(string requestEmail, string vin, bool isApproved);
        public bool IsIncomingSecondaryOwner(string email);
        Task<IEnumerable<VinOwnership>> GetPrimaryOwnershipVins(string ownerId);
        Task<IList<EmailInfo>> GetSecondaryOwners(string ownerId);

        Task<IList<string>> GetVinsForSecondaryOwnershipAssignment(string ownerId, string secondaryOwnerId);

    }
}