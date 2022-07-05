using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model
{
    public class BaseModel
    {
        [Key]
        [DataMember]
        [Column("ID")]
        [Comment("主键")]
        public virtual Guid Id { get; set; }
        [DataMember]
        [Column("CreateUserID")]
        [Comment("创建人ID")]
        public virtual Guid CreateUserID { get; set; }
        [DataMember]
        [Column("CreateDate")]
        [Comment("创建时间")]
        public virtual DateTime CreateDate { get; set; }
        [DataMember]
        [Column("UpdateUserID")]
        [Comment("更新人ID")]
        public virtual Guid? UpdateUserID { get; set; }
        [DataMember]
        [Column("UpdateDate")]
        [Comment("更新时间")]
        public virtual DateTime? UpdateDate { get; set; }
        [DataMember]
        [Column("IsDeleted")]
        [Comment("是否删除")]
        public virtual bool IsDeleted { get; set; }
        [DataMember]
        [Column("IsEnabled")]
        [Comment("是否启用")]
        public virtual bool IsEnabled { get; set; }
    }
}
