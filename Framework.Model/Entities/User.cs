

using System.Diagnostics.CodeAnalysis;

namespace Framework.Model.Entities
{
    [Table("Sys_User")]
    public class User : CommonField, IDeleteField, IEnabledField
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
        /// <summary>
        /// 是否删除
        /// </summary>

        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
