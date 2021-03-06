﻿namespace TechZone.Models.EntityModels
{
    using Enums;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    [Table("Processor")]
    public class Processor : Product
    {
        public ProcessorSeriesType Series { get; set; }

        public ProcessorBrandType Brand { get; set; }

        public ProcessorCoresType Cores { get; set; }

        [Required]
        [Range(1, 32, ErrorMessage = "Cache should be between 1 and 32 Mb")]
        public int Cache { get; set; }

        [Required]
        [Range(1, 16, ErrorMessage = "Processor clock speed be between 1 and 16 Ghz")]
        public decimal ProcessorSpeed { get; set; }
    }
}