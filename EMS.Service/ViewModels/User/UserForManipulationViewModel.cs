﻿using System.ComponentModel.DataAnnotations;

namespace EMS.Service.ViewModels.User
{
    public abstract record UserForManipulationViewModel
    {
        [Required]
        public string? UserName { get; init; }

        [Required]
        [EmailAddress]
        public string? Email { get; init; }
    }
}
