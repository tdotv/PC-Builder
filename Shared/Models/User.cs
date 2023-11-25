﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_Designer.Shared
{
    public class User
    {
        // public User()
        // {
        //     ChatHistoryFromUsers = new HashSet<ChatHistory>();
        //     ChatHistoryToUsers = new HashSet<ChatHistory>();
        // }

        [Key]
        public long UserId { get; set; }
        
        [Required(ErrorMessage = "Email can't be null")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password can't be null")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Role can't be null")]
        public string Role { get; set; } = null!;
        public string? Source { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string AboutMe { get; set; } = string.Empty;
        public long Notifications { get; set; } = 0;
        public long DarkTheme { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // public virtual ICollection<ChatHistory> ChatHistoryFromUsers { get; set; }
        // public virtual ICollection<ChatHistory> ChatHistoryToUsers { get; set; }
    }
}