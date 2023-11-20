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
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Password can't be null")]
        public string Password { get; set; } = null!;
        public string? Source { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public byte[]? DateOfBirth { get; set; }
        public string? AboutMe { get; set; }
        public long? Notifications { get; set; }
        public long? DarkTheme { get; set; }
        public byte[]? CreatedDate { get; set; }

        // public virtual ICollection<ChatHistory> ChatHistoryFromUsers { get; set; }
        // public virtual ICollection<ChatHistory> ChatHistoryToUsers { get; set; }
    }
}