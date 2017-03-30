using System.Collections.Generic;
using PollService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using static PollService.Constants;

namespace PollService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Question: ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Poll")]
        public int? PollId { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        public QuestionType QuestionType { get; set; } = QuestionType.Default;

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public string Body { get; set; }

        public string Description { get; set; }

        public int? OrderIndex { get; set; }

        public ICollection<Option> Options { get; set; } = new HashSet<Option>();

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }
    
        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Poll Poll { get; set; }
    }
}
