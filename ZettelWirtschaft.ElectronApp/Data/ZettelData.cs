using System;
using System.ComponentModel.DataAnnotations;

namespace ZettelWirtschaft.ElectronApp.Data
{
    public class ZettelData
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(0)]
        public string Content { get; set; }
    }
}