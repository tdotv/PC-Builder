﻿using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PC_Designer.Shared
{
    public class User
    {
        [Key]
        public long UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Source { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePictureData { get; set; }
        public string AboutMe { get; set; }
        public long? Notifications { get; set; }
        public long? DarkTheme { get; set; }
        public string Role { get; set; }
    }
}