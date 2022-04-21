namespace Framework.Common
{
    /// <summary>
    /// 分页信息
    /// </summary>
    [Serializable]
    public class Pages
    {
        /// <summary>
        /// 默认每页记录数
        /// </summary>
        private static int recordsPaginalDefault = 20;

        /// <summary>
        /// 排序
        /// </summary>
        private List<KeyValuePair<string, bool>> sortFields = new List<KeyValuePair<string, bool>>();

        /// <summary>
        /// 页码
        /// </summary>
        private int pageNumber = 1;

        /// <summary>
        /// 每页记录数
        /// </summary>
        private int recordPaginal = recordsPaginalDefault;

        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordSum;

        /// <summary>
        /// 创建 Pages 对象实例
        /// </summary>
        /// <param name="orderBy">排序</param>
        public Pages(string orderBy) : this(1, int.MaxValue, orderBy)
        {
        }

        /// <summary>
        /// 创建 Pages 对象实例
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="recordPaginal">每页数量</param>
        /// <param name="orderBy">排序</param>
        public Pages(int pageNumber, int recordPaginal, string orderBy)
        {
            this.PageNumber = pageNumber;
            this.RecordPaginal = recordPaginal;
            if (!string.IsNullOrEmpty(orderBy))
            {
                foreach (string item in orderBy.Split(',').Reverse())
                {
                    if (item.Substring(0, 1) == "+")
                    {
                        this.AddSortField(item.Substring(1, item.Length - 1));
                    }
                    else
                    {
                        this.AddConverseField(item.Substring(1, item.Length - 1));
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置是否当前触发
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 获取或设置页码
        /// </summary>
        public int PageNumber
        {
            get { return this.pageNumber; }
            set { this.pageNumber = value; }
        }

        /// <summary>
        /// 获取或设置每页记录数
        /// </summary>
        public int RecordPaginal
        {
            get { return this.recordPaginal; }
            set { this.recordPaginal = value; }
        }

        /// <summary>
        /// 获取或设置获取或设置总记录数
        /// </summary>
        public int RecordSum
        {
            get { return this.recordSum; }
            set { this.recordSum = value; }
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        public int PageSum
        {
            get
            {
                if (this.recordSum % this.recordPaginal == 0)
                {
                    return this.recordSum / this.recordPaginal;
                }
                else
                {
                    return (this.recordSum / this.recordPaginal) + 1;
                }
            }
        }

        /// <summary>
        /// 获取本页记录数
        /// </summary>
        public int RecordThisPage
        {
            get
            {
                if (this.pageNumber == this.PageSum)
                {
                    // 最后一页
                    return this.RecordSum - (this.RecordPaginal * (this.pageNumber - 1));
                }
                else
                {
                    return this.RecordPaginal;
                }
            }
        }

        /// <summary>
        /// 获取起始记录号
        /// </summary>
        public int First
        {
            get
            {
                return ((this.pageNumber - 1) * this.recordPaginal) + 1;
            }
        }

        /// <summary>
        /// 获取终止记录号
        /// </summary>
        public int Last
        {
            get
            {
                if (this.recordSum == 0)
                {
                    // 无记录
                    return 0;
                }
                else if (this.pageNumber == this.PageSum)
                {
                    // 最后一页
                    return this.RecordSum;
                }
                else
                {
                    return this.RecordPaginal * this.pageNumber;
                }
            }
        }

        /// <summary>
        /// 获取上一页码
        /// </summary>
        public int PrevPage
        {
            get
            {
                return this.PageNumber == 1 ? 1 : this.PageNumber - 1;
            }
        }

        /// <summary>
        /// 获取下一页码
        /// </summary>
        public int NextPage
        {
            get
            {
                return this.PageNumber == this.PageSum ? this.PageSum : this.PageNumber + 1;
            }
        }

        /// <summary>
        /// 获取或设置排序字段
        /// </summary>
        public List<KeyValuePair<string, bool>> SortFields
        {
            get { return this.sortFields; }
            set { this.sortFields = value; }
        }

        /// <summary>
        /// 添加排序属性
        /// </summary>
        /// <param name="field">排序字段</param>
        public void AddSortField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return;
            }

            if (this.sortFields.Count == 0)
            {
                // 还未有排序规则，直接添加
                this.sortFields.Add(new KeyValuePair<string, bool>(field, true));
                return;
            }

            // 该排序字段为第一个，翻转
            if (this.sortFields.Last().Key == field)
            {
                this.sortFields[this.sortFields.Count - 1] = new KeyValuePair<string, bool>(field, !this.sortFields[this.sortFields.Count - 1].Value);
                return;
            }

            foreach (KeyValuePair<string, bool> item in this.sortFields)
            {
                // 已存在该排序字段
                if (item.Key == field)
                {
                    this.sortFields.Remove(item);
                    break;
                }
            }

            // 不存在该排序字段
            this.sortFields.Add(new KeyValuePair<string, bool>(field, true));
        }

        /// <summary>
        /// 添加逆序属性
        /// </summary>
        /// <param name="field">排序字段</param>
        public void AddConverseField(string field)
        {
            this.AddSortField(field);
            this.AddSortField(field);
        }

        /// <summary>
        /// 添加排序属性
        /// </summary>
        /// <param name="field">排序字段</param>
        /// <param name="order">是否按顺序</param>
        public void AddSortField(string field, bool order)
        {
            this.AddSortField(field);
            if (!order)
            {
                this.AddSortField(field);
            }
        }
    }
}
