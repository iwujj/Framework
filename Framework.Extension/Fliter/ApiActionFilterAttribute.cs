using Framework.Repository.UnitOfWork;

namespace Framework.Extensions
{
    public class ApiActionFilterAttribute:ActionFilterAttribute
    {
        IUnitOfWork _unitOfWork;
        public ApiActionFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        /// <summary>
        /// 请求开始
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            if (_unitOfWork.CurrentTransaction == null)
            {
                _unitOfWork.BeginTransaction();
            }
            base.OnActionExecuting(actionExecutingContext);
        }
        /// <summary>
        /// 请求结束
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (_unitOfWork.CurrentTransaction != null)
            {
                _unitOfWork.RollbackTransaction();
            }
            base.OnActionExecuted(actionExecutedContext);
        }


    }
}
