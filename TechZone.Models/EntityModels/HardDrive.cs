﻿namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using System.ComponentModel.DataAnnotations;

    [Table("HardDrives")]
    public class HardDrive : Product
    {
        [Range(80, 10000, ErrorMessage = "Hard drive capacity must be between 80 and 10000 Mbs")]
        public int Capacity { get; set; }

        public HardDriveBrandType DriveBrand { get; set; }

        public HardDriveType DriveType { get; set; }
    }
}