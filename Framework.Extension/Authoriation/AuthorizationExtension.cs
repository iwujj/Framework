namespace Framework.Extensions
{
    public static class AuthorizationExtension
    {
        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var keyByteArray = Encoding.ASCII.GetBytes(AppSettings.JwtSettings.SecretKey);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = AppSettings.JwtSettings.Issuer;
            var Audience = AppSettings.JwtSettings.Audience;
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var permission = new List<PermissionItem>();
            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                Issuer,//发行人
                Audience,//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 60 * 24)//接口的过期时间
                );
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permission.User.ToString(),
                         policy => policy.Requirements.Add(permissionRequirement));
            });
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
        }
    }
}
