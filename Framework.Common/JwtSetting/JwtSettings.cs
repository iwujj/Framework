namespace Framework.Common
{
    public class JwtSettings
    {
        // token颁发者
        public string Issuer { get; set; } = "owner";
        // token使用的客户端
        public string Audience { get; set; } = "wujj";
        // 加密Key
        public string SecretKey { get; set; } = "j3bvie0zuke8duwhwzh3zz7bv";
    }
}
