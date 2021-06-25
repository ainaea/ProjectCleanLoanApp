using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.Entities
{
    public class AuditableEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
