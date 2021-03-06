﻿using System.ComponentModel.DataAnnotations;

namespace TechZone.Models.ViewModels.Reviews
{
    using System.Collections.Generic;

    public class ReviewDetailsViewModel
    {
        public ReviewDetailsViewModel()
        {
            this.ReviewComments = new HashSet<ReviewCommentViewModel>();
        }

        public int Id { get; set; }

        public string PublishDateString { get; set; }

        public string[] ContentParagraphs { get; set; }

        public string Title { get; set; }

        public string ReviewerUsername { get; set; }

        public string ReviewerImageData { get; set; }

        public int CountOfComments { get; set; }

        public int Useful { get; set; }

        public int Useless { get; set; }

        [MinLength(10, ErrorMessage = "Write at least 10 characters to our moderator.")]
        [MaxLength(300, ErrorMessage = "Your message shouldn't be longer than 300 characters")]
        [Required]
        public string Message { get; set; }

        public bool VisitorIsAlsoReviewPublisher { get; set; }

        public bool IsVisitorAllowedToPostComments { get; set; }

        public IEnumerable<ReviewCommentViewModel> ReviewComments { get; set; }
    }
}