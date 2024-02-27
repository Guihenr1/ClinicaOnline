using ClinicaOnline.Application.Models.Response.Base;

namespace ClinicaOnline.Application.Models.Response
{
    public class UserAuthenticateResponse : Validator
    {
        public string accessToken { get; set; }

        public string expiresIn { get; set; }

        public UserInfo userInfo { get; set; }


    }

    public class UserInfo
    {
        public string email { get; set; }

        public string name { get; set; }

        public string perfil { get; set; }
    }
}