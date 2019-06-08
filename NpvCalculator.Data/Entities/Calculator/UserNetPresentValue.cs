using Security.Data.Entities;
using System;

namespace NpvCalculator.Data.Entities
{
    public class UserNetPresentValue
    {
        public int UserNetPresentValueId { get; set; }
        public Guid UserId { get; set; }
        public int NetPresentValueId { get; set; }

        public virtual User User { get; set; }
    }
}
