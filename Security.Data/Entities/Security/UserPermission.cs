using System;

namespace Security.Data.Entities
{
    public class UserPermission
    {
        public int UserPermissionId { get; set; }
        public Guid UserId { get; set; }
	    public int PermissionId { get; set; }

        public virtual User User { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
