

using Microsoft.EntityFrameworkCore;

namespace Framework.Model
{
    public class CommonField :BaseModel
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
    }

}
