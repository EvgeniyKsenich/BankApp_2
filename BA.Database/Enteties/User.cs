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
        [MaxLength(60)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MaxLength(60)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
