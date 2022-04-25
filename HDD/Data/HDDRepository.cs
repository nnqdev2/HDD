using HDD.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HDD.Data
{
    public class HDDRepository : IHDDRepository
    {

        private HDDContext _context;
        private ILogger<HDDRepository> _logger;
        public HDDRepository(ILogger<HDDRepository> logger, HDDContext context)
        {
            _context = context;
            _logger = logger;
        }

        public Vehicle GetVehicle(int vehicleId)
        {
            IQueryable<Vehicle>? vehicle = _context.Vehicle.Where(a => a.VehicleId == vehicleId);
            //IQueryable<Vehicle> vehicle = from v in _context.Vehicle
            //                              join ec in _context.EmailCode on v.VehicleId equals ec.VehicleId
            //                              where v.Vin == vin || v.Plate == plate
            //                              select v;

            if (vehicle.Any())
                return vehicle.FirstOrDefault();
            return null;
        }
        public Vehicle GetVehicle(string vin, string plate)
        {
            IQueryable<Vehicle>? vehicle = _context.Vehicle.Where(a => a.Vin == vin || a.Plate == plate);
            //IQueryable<Vehicle> vehicle = from v in _context.Vehicle
            //                              join ec in _context.EmailCode on v.VehicleId equals ec.VehicleId
            //                              where v.Vin == vin || v.Plate == plate
            //                              select v;

            if (vehicle.Any())
                return vehicle.FirstOrDefault();
            return null;
        }
        public Vehicle GetVehicle(string vin, string plate, string email)
        {
            //throw new NotImplementedException();

            IQueryable<Vehicle>  vehicle =  from v in _context.Vehicle
                          join ec in _context.EmailCode on v.VehicleId equals ec.VehicleId
                          where v.Vin == vin || v.Plate == plate
                          select v;

         
            //var vehicle = from v in _context.Vehicle.ToList()
            //              where v.Vin == vin || v.Plate == plate
            //              select new Vehicle
            //              {
            //                  Vin = v.Vin,
            //                  Plate = v.Plate,
            //                  Year = v.Year,
            //                  Make = v.Make,
            //                  Comment = v.Comment,
            //                  SubmissionStatus = v.SubmissionStatus,
            //                  VehicleId = v.VehicleId,
            //                  LastUpdatedBy = v.LastUpdatedBy,
            //                  LastUpdatedDate = v.LastUpdatedDate,
            //                  EmailCode = v.EmailCode
            //              };
            //var vehicle = from v in _context.Vehicle.ToList()
            //              where v.Vin == vin || v.Plate == plate
            //              select new Vehicle
            //              {
            //                  Vin = v.Vin,
            //                  Plate = v.Plate,
            //                  Year = v.Year,
            //                  Make = v.Make,
            //                  Comment = v.Comment,
            //                  SubmissionStatus = v.SubmissionStatus,
            //                  VehicleId = v.VehicleId,
            //                  LastUpdatedBy = v.LastUpdatedBy,
            //                  LastUpdatedDate = v.LastUpdatedDate,
            //                  EmailCode = v.EmailCode
            //              };
            return vehicle.First();
        }

        public IEnumerable<EmailCode> GetEmailCode(string vin, string plate, string email)
        {
            //throw new NotImplementedException();

            var vehicle = from v in _context.Vehicle
                          join ec in _context.EmailCode on v.VehicleId equals ec.VehicleId
                          where v.Vin == vin || v.Plate == plate
                          select ec;


            //var vehicle = from v in _context.Vehicle.ToList()
            //              where v.Vin == vin || v.Plate == plate
            //              select new Vehicle
            //              {
            //                  Vin = v.Vin,
            //                  Plate = v.Plate,
            //                  Year = v.Year,
            //                  Make = v.Make,
            //                  Comment = v.Comment,
            //                  SubmissionStatus = v.SubmissionStatus,
            //                  VehicleId = v.VehicleId,
            //                  LastUpdatedBy = v.LastUpdatedBy,
            //                  LastUpdatedDate = v.LastUpdatedDate,
            //                  EmailCode = v.EmailCode
            //              };
            //var vehicle = from v in _context.Vehicle.ToList()
            //              where v.Vin == vin || v.Plate == plate
            //              select new Vehicle
            //              {
            //                  Vin = v.Vin,
            //                  Plate = v.Plate,
            //                  Year = v.Year,
            //                  Make = v.Make,
            //                  Comment = v.Comment,
            //                  SubmissionStatus = v.SubmissionStatus,
            //                  VehicleId = v.VehicleId,
            //                  LastUpdatedBy = v.LastUpdatedBy,
            //                  LastUpdatedDate = v.LastUpdatedDate,
            //                  EmailCode = v.EmailCode
            //              };
            return vehicle;
        }

        public Task<EmailCode> SendEmailCode(string vin, string plate, string email)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateVehicle(ApplicationViewModel avm)
        {
            var vehicle = new Vehicle();
            vehicle.VehicleId = 2;
            vehicle.Vin = avm.Vin;
            vehicle.Plate = avm.Plate;
            vehicle.LastUpdatedBy = avm.Email;
            vehicle.Comment = avm.Comment;
            vehicle.SubmissionStatus = avm.SubmissionStatus;
            vehicle.Year = avm.Year;
            vehicle.LastUpdatedDate = DateTime.Now;
            _context.Vehicle.Attach(vehicle);
            _context.Update(vehicle);
            await _context.SaveChangesAsync();
          

        }

        public bool IsIncomingPrimaryOwner(string vin, string plate, string registeredZip)
        {
            var count = _context.Dmvccddata.Where(a => a.Vin == vin && a.Plate == plate && a.Zip == registeredZip)
                .Count();
            return count > 0;
        }


        public bool IsIncomingSecondaryOwner(string email)
        {
            var efound = _context.SecondaryOwnersAssignment.Where(a => a.IncomingSecondaryOwnerEmail == email);

            return efound.ToList().Count > 0;
        }

        public async Task InsertOwnersVin(OwnersVin ownersVin)
        {
            await _context.AddAsync(ownersVin);
            await _context.SaveChangesAsync();
        }

        public  IEnumerable<OwnersVin> GetVins(string ownerId)
        {
            IEnumerable<OwnersVin> result = _context.OwnersVin.Where(a => a.OwnerId == ownerId && a.OwnerStatus == true);
            return result;
        }
        public IEnumerable<OwnersVin> GetPrimaryOwnersVins(string ownerId)
        {
            IEnumerable<OwnersVin> result = _context.OwnersVin.Where(a => a.OwnerId == ownerId && a.OwnerStatus == true && a.PrimaryOwner == "Y");
            return result;
        }
        public RetrofitApplicationDmvccddata GetRetrofitApplicationDmvccddata(string vin)
        {
            var retrofitApplicationDmvccddata = from ra in _context.RetrofitApplication
                    join d in _context.Dmvccddata on ra.Vin equals d.Vin
                    where ra.Vin == "vin1"
                    select new RetrofitApplicationDmvccddata
                    {
                        Vin = ra.Vin,
                        Plate = d.Plate,
                        Gvw = d.Gvw,
                        EngineYear = ra.EngineYear,
                        ModelYear = d.ModelYear,
                        EngineManufacturer = ra.EngineManufacturer,
                        EngineFamilyNumber = ra.EngineFamilyNumber,
                        EngineDisplacement = ra.EngineDisplacement,
                        ArtFamilyName = ra.ArtFamilyName,
                        ApplicationDate = ra.ApplicationDate,
                        RetrofitType = ra.RetrofitType,
                        RetrofitProvider = ra.RetrofitProvider,
                        Comments = ra.Comments,
                        RegistrationWeight = d.RegistrationWeight,
                        WeightRange = d.WeightRange,
                        OwnerName = d.OwnerName,
                        StreetAddress = d.StreetAddress,
                        City = d.City,
                        State = d.State,
                        Zip = d.Zip,
                        County = d.County,
                        PubliclyOwned = d.PubliclyOwned,
                        RegistrationExpiration = d.RegistrationExpiration,
                        RenewalAgency = d.RenewalAgency,
                        ChangedOwnership = d.ChangedOwnership
                    };
            return retrofitApplicationDmvccddata.FirstOrDefault();
        }

        public bool IsVinRegulated(string vin)
        {
            var efound = _context.RetrofitCertification.Where(a => a.Vin == vin && a.Vinstatus == "A");
            return efound.ToList().Count > 0;
        }
        public bool IsPlateRegulated(string plate)
        {
            var efound = (from ra in _context.RetrofitCertification
                          join d in _context.Dmvccddata on ra.Vin equals d.Vin
                          where d.Plate == plate && d.Vin == ra.Vin && ra.Vinstatus == "A"
                          select new { d.Plate, ra.Vin, ra.Vinstatus }
                          );
            return efound.ToList().Count > 0;
        }

        //public bool IsPlateEligibleToClaim(string plate)
        //{
        //    var vin = GetVin(plate);
        //    return IsVinEligibleToClaim(vin);
        //}

        //public bool IsVinEligibleToClaim(string vin)
        //{
        //    if (IsVinRegulated(vin) && !IsVinClaimed(vin))
        //        return true;
        //    return false;
        //}

        public string GetVin(string plate)
        {
            var vin = (from a in _context.Dmvccddata
                             where a.Plate == plate
                          select a.Vin.ToString()).FirstOrDefault();
            return vin;
        }
        public bool IsVinClaimed(string vin)
        {
            var efound = _context.OwnersVin.Where(a => a.Vin == vin && a.OwnerStatus == true);
            return efound.ToList().Count > 0;
        }

        public async Task<IList<OwnersVin>> GetSecondaryOwnerIds(string ownerId)
        {
            var p1 = new SqlParameter("@OwnerId", ownerId);
            var secondaryOwnersVins = await _context.OwnersVin.FromSqlInterpolated(
                $@"select ov2.*
                        from (select ov1.vin
                        from  dbo.OwnersVin ov1 where ov1.ownerid = {p1} and  ov1.PrimaryOwner like 'Y%' and ov1.OwnerStatus = 1) ov1
                        inner join dbo.OwnersVin ov2 on ov1.vin = ov2.vin and PrimaryOwner like 'N%' and OwnerStatus = 1")
                .AsNoTracking()
                .ToListAsync();

            //        var x = var parents = await context.Parents
            //.Select(x => new
            //{
            //    x.ParentId,
            //    x.Name,
            //    Children = x.Children.Select(c => new { c.ChildId, c.Name }).ToList(),
            //    ChildCount = x.Children.Count()
            //}).ToListAsync();

            return (IList<OwnersVin>)secondaryOwnersVins;
        }

        public async Task<IList<string>> GetVinsForSecondaryOwnershipAssignment(string ownerId, string secondaryOwnerId)
        {
            var p1 = new SqlParameter("@OwnerId", ownerId);
            var p2 = new SqlParameter("@SecondaryOwnerId", secondaryOwnerId);
            var vins = await _context.OwnersVin.FromSqlInterpolated(
                $@"
                    select vin from dbo.OwnersVIN ov
                    where ov.OwnerID = {p1}
                    and ov.vin not in (select vin from dbo.OwnersVIN where  OwnerID = {p2})")
                .AsNoTracking()
                .ToListAsync();

            //var secondaryOwnerVins = (from a in _context.OwnersVin
            //              where a.OwnerId == ownerId && a.PrimaryOwner == "N" && a.OwnerStatus == true 
            //              select new { a.Vin }).ToList();

            //var ownerVins = (from a in _context.OwnersVin
            //                         where a.OwnerId == ownerId && a.PrimaryOwner == "Y" && a.OwnerStatus == true
            //                         select new { a.Vin }).ToList();

            return (IList<string>)vins;
        }
        //IEnumerable<EmailCode> IHDDRepository.GetEmailCode(int vehicleId, string email)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<EmailCode> IHDDRepository.GetEmailCode(string vin, string plate, string email)
        //{
        //    throw new NotImplementedException();
        //}

        //Vehicle IHDDRepository.GetVehicle(string vin, string plate)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<EmailCode> IHDDRepository.SendEmailCode(string vin, string plate, string email)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
