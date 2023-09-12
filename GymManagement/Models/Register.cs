using System;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.Models
{
    public class Register
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; } 
        public string ContactNumber { get; set; } 
        public string Role { get; set; } 
        public bool IsGymStaff { get; set; }
    }

}