namespace Framework.Extensions
{
    public class ApiActionFilterAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 请求开始
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            base.OnActionExecuting(actionExecutingContext);
        }
        /// <summary>
        /// 请求结束
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }


    }
}
