using SqlManager.Models;

namespace SqlManager.Logic
{
    public static class Connection
    {
        public static AuthorizationModel AuthorizationModel { get; set; }

        public static string ConnectionString =>
            $"Server={AuthorizationModel.Ip},{AuthorizationModel.Port};" +
            $"User Id={AuthorizationModel.Login};Password={AuthorizationModel.Password}";
    }
}