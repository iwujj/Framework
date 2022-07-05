

using System.Diagnostics.CodeAnalysis;

namespace Framework.Model.Entities
{
    [Table("Sys_User")]
    public class User : BaseModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// 名字
        /// </summary>

        public string? Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>

        public int? Age { get; set; }
        /// <summary>
        /// 账号
        /// </summary>

        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>

        public string Password { get; set; }
       
    }
}
