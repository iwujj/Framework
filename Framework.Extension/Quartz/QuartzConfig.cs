using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Extensions
{
    /// <summary>
    /// 注册Quartz服务，待完善
    /// </summary>
    public static class QuartzConfig
    {
        public static void AddQuartz(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
        }
    }
}
