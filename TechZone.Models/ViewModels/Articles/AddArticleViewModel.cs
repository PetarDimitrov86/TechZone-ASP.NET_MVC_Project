﻿namespace TechZone.Models.ViewModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    public class AddArticleViewModel
    {
        [Required]
        [MinLength(15, ErrorMessage = "Article title shouldd be at least 15 symbols long")]
        [MaxLength(100, ErrorMessage = "Article title shouldd be less than 100 characters long")]
        public string Title { get; set; }

        [Required]
        [MinLength(200, ErrorMessage = "Article content should be at least 200 characters long")]
        [MaxLength(5000, ErrorMessage = "Article content should be at least 200 characters long")]
        public string Content { get; set; }
    }
}
