﻿using System.ComponentModel.DataAnnotations;

#nullable disable

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
        public string EmailAddress { get; set; }

        // [Required(ErrorMessage = "Password can't be null")]
        public string Password { get; set; }

        public string Source { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePictureData { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public long? Notifications { get; set; }
        public long? DarkTheme { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // public virtual ICollection<ChatHistory> ChatHistoryFromUsers { get; set; }
        // public virtual ICollection<ChatHistory> ChatHistoryToUsers { get; set; }
    }
}