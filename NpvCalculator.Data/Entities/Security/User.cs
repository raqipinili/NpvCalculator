using System;

namespace NpvCalculator.Data.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        // public virtual ICollection<UserNetPresentValue> UserNetPresentValues { get; set; }
    }
}
