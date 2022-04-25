﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HDD.Models
{
    public partial class Dmvccddata
    {
        public Dmvccddata()
        {
            OwnersVin = new HashSet<OwnersVin>();
            SecondaryOwnersAssignment = new HashSet<SecondaryOwnersAssignment>();
        }

        public string Vin { get; set; }
        public string Plate { get; set; }
        public int? ModelYear { get; set; }
        public int? Gvw { get; set; }
        public int? RegistrationWeight { get; set; }
        public string WeightRange { get; set; }
        public string OwnerName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string PubliclyOwned { get; set; }
        public string RegistrationExpiration { get; set; }
        public string RenewalAgency { get; set; }
        public string ChangedOwnership { get; set; }
        public DateTime? RunDate { get; set; }
        public DateTime? EntryDateTime { get; set; }

        public virtual RetrofitApplication RetrofitApplication { get; set; }
        public virtual RetrofitCertification RetrofitCertification { get; set; }
        public virtual ICollection<OwnersVin> OwnersVin { get; set; }
        public virtual ICollection<SecondaryOwnersAssignment> SecondaryOwnersAssignment { get; set; }
    }
}