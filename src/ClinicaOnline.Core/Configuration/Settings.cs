using System;

namespace ClinicaOnline.Core.Configuration
{
    public static class Settings
    {
        public static string Secret = "fedaf7d8863b48e197b9287d492b708e";
        public static DateTime TokenExpires = DateTime.UtcNow.AddHours(5);
    }
}