﻿using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class TagsModel
    {
        [Key]
        public Guid TagId { get; set; } = Guid.NewGuid();
        public string TagName { get; set; } = string.Empty;
        public Guid blogId { get; set; } = Guid.Empty;
    }
}
