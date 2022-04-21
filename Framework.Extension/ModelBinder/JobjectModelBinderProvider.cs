namespace Framework.Extensions
{
    public class JobjectModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Metadata.ModelType == (typeof(JObject)))
            {
                return new JObjectModelBinder(context.Metadata.ModelType);
            }
            return null;
        }
    }
}
