using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HDD.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            //EmailCode = new HashSet<EmailCode>();
        }

        public int VehicleId { get; set; }
        public string Vin { get; set; }
        public string Plate { get; set; }
        public short? Year { get; set; }
        public string Enginemfg { get; set; }
        public short? Enginemodelyear { get; set; }
        public short? Gvwr { get; set; }
        public string Enginedisplacement { get; set; }
        public string Enginefamilymember { get; set; }
        public string Ownername { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? Registrationexpirationdate { get; set; }
        public string Publiclyowned { get; set; }
        public string Artfamilyname { get; set; }
        public string Retrofitprovider { get; set; }
        public string Retrofittype { get; set; }
        public DateTime? Installationdate { get; set; }
        public string Comment { get; set; }
        public string SubmissionStatus { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }

        public virtual ICollection<EmailCode> EmailCode { get; set; }
    }
}