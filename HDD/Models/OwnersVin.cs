﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HDD.Models
{
    public partial class OwnersVin
    {
        public string OwnerId { get; set; }
        public string Vin { get; set; }
        public string PrimaryOwner { get; set; }
        public bool OwnerStatus { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual Dmvccddata VinNavigation { get; set; }
    }
}