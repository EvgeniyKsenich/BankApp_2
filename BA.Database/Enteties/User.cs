using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BA.Database.Enteties
{
    public class User
    {
        public User()
        {
            this.Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
