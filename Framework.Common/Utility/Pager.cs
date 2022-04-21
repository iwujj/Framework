using System.Linq.Expressions;

namespace Framework.Common
{
    public class Pager
    {
        public static IQueryable<T> SplitPage<T>(IQueryable<T> queryable, Pages pages)
        {
            // 如果分页信息为空，返回全部数据
            if (pages == null)
            {
                return queryable;
            }

            // 设置总记录数
            pages.RecordSum = queryable.Count();

            // 如果页码超出总页数，取最后一页
            if (pages.RecordSum > 0 && pages.PageNumber > pages.PageSum && pages.PageSum > 0)
            {
                //pages.PageNumber = pages.PageSum;
            }

            // 排序
            if (pages.SortFields.Count != 0)
            {
                bool firstOrder = true;
                pages.SortFields.Reverse();

                foreach (KeyValuePair<string, bool> sortField in pages.SortFields)
                {
                    queryable = OrderBy<T>(queryable, sortField, firstOrder);

                    firstOrder = false;
                }

                pages.SortFields.Reverse();
            }
            else if (typeof(T).GetProperty("OrderNo") != null)
            {
                // 用OrderNo排序
                string sortField = "OrderNo";
                queryable = OrderBy<T>(queryable, new KeyValuePair<string, bool>(sortField, true), true);
            }
            else if (typeof(T).GetProperty("Code") != null)
            {
                // 用Code排序
                string sortField = "Code";
                queryable = OrderBy<T>(queryable, new KeyValuePair<string, bool>(sortField, true), true);
            }
            else if (typeof(T).GetProperty("Name") != null)
            {
                // 用Name排序
                string sortField = "Name";
                queryable = OrderBy<T>(queryable, new KeyValuePair<string, bool>(sortField, true), true);
            }
            else
            {
                // 用主键属性进行排序
                string sortField = typeof(T).GetProperties().FirstOrDefault(x => x.GetCustomAttributes(true).Where(y => y.GetType().Name == "KeyAttribute").Count() != 0).Name;
                queryable = OrderBy<T>(queryable, new KeyValuePair<string, bool>(sortField, true), true);
            }

            // 返回当页的数据
            var result = queryable
                .Skip(pages.First - 1)
                .Take(pages.RecordPaginal);

            return result;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">查询对象类型</typeparam>
        /// <param name="queryable">需要排序的数据</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isFirstOrder">是否第一个排序</param>
        /// <returns>分页结果</returns>
        public static IQueryable<T> OrderBy<T>(IQueryable<T> queryable, KeyValuePair<string, bool> sortField, bool isFirstOrder)
        {
            string[] propertys = sortField.Key.Split('.');
            Type elementType = queryable.ElementType;

            // 参数
            ParameterExpression parameterExpression =
                Expression.Parameter(queryable.ElementType, "p");

            // 需要排序的属性
            MemberExpression memberExpression = null;

            for (int i = 0; i < propertys.Count(); i++)
            {
                string property = propertys[i];

                if (elementType.GetProperty(property) == null)
                {
                    throw new Exception("排序字段 " + property + " 不存在！");
                }

                if (i == 0)
                {
                    // 第一个属性
                    memberExpression = Expression.Property(parameterExpression, property);
                }
                else
                {
                    // 非第一个属性
                    memberExpression = Expression.Property(memberExpression, property);
                }

                elementType = elementType.GetProperty(property).PropertyType;
            }

            Expression expression = Expression.Lambda(memberExpression, parameterExpression);

            // 调用Queryable的静态方法进行排序
            MethodCallExpression methodCallExpression = Expression.Call(
                typeof(Queryable),
                sortField.Value ? (isFirstOrder ? "OrderBy" : "ThenBy") : (isFirstOrder ? "OrderByDescending" : "ThenByDescending"),
                new Type[] { queryable.ElementType, elementType },
                queryable.Expression,
                expression);

            // 返回当页的数据
            var result = queryable.Provider.CreateQuery<T>(methodCallExpression);

            return result;
        }
    }
}
