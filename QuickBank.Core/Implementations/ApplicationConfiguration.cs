using Microsoft.Extensions.Configuration;
using QuickBank.Core.Interfaces;
using System.Text;

namespace QuickBank.Core.Implementations
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ValidAudience => _configuration.GetSection("JWT:ValidAudience").Value;

        public string ValidIssuer => _configuration.GetSection("JWT:ValidIssuer").Value;

        public byte[] Secret => Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value);

        public double TokenValidityInMinutes
            => Convert.ToDouble(_configuration.GetSection("JWT:TokenValidityInMinutes").Value);

        public double RefreshTokenValidityInMinutes
            => Convert.ToDouble(_configuration.GetSection("JWT:RefreshTokenValidityInMinutes").Value);
    }
}
