namespace Framework.Extensions
{
    public class JObjectModelBinder : IModelBinder
    {
        public JObjectModelBinder(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException("bindingContext");
            try
            {
                if (bindingContext.ModelType == typeof(JObject))
                {
                    string Content = new StreamReader(bindingContext.HttpContext.Request.Body).ReadToEndAsync().Result;
                    JObject obj = JsonConvert.DeserializeObject<JObject>(Content);
                    bindingContext.Result = (ModelBindingResult.Success(obj));
                }
            }
            catch (Exception exception)
            {
                if (!(exception is FormatException) && (exception.InnerException != null))
                {
                    exception = ExceptionDispatchInfo.Capture(exception.InnerException).SourceException;
                }
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, exception, bindingContext.ModelMetadata);
            }
            return Task.CompletedTask;
        }
    }
}
