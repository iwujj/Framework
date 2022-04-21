using Framework.Model;

namespace Framework.Common
{
    public  class SqlHelper 
    {
        /// <summary>
        /// 权限验证写在这里
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static IQueryable<T> Allow<T>(IQueryable<T> queryable)
        {
            return queryable;
        }

    }
}
