namespace HeroesWebApiDemo.Routes.V1;

public static class ApiRoutes
{
    private const string Base = "api/v1";
        
    public static class Heroes
    {
        public const string GetAll = Base + "/heroes";
        public const string GetById = Base + "/heroes/{id}";
        public const string Create = Base + "/heroes";
        public const string Update = Base + "/heroes/{id}";
        public const string Delete = Base + "/heroes/{id}";
    }

    public static class Identity
    {
        public const string Register = Base + "/identity/register";
        public const string Login = Base + "/identity/login";
    }
}