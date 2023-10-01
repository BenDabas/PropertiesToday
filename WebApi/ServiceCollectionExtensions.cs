using Application.Models;

namespace WebApi
{
    public static class ServiceCollectionExtensions
    {
        // Cache setting.
        public static CacheSettingsModel GetCacheSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettingConfigurations = configuration.GetSection("CacheSettings");

            services.Configure<CacheSettingsModel>(cacheSettingConfigurations);

            return cacheSettingConfigurations.Get<CacheSettingsModel>();
        }
    }
}
