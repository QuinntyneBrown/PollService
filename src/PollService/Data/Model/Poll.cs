using System;
using System.Collections.Generic;
using PollService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Poll: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();

        public ICollection<PollResult> PollResults { get; set; } = new HashSet<PollResult>();
        
        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
