using System.Reflection;

namespace VacationHire.AdministrativeApi.Mapper
{
    /// <summary>
    ///     Initialize auto mapper
    /// </summary>
    public static class AutoMapperInitialization
    {
        /// <summary>
        ///     Add AutoMapper to service collection
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="assembly">Assembly for the project</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services, Assembly assembly)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services), "A service collection is required.");

            services.AddAutoMapper((_, mapper) =>
            {
                mapper.AddProfile(new MappingProfile());
            }, assembly);

            return services;
        }
    }
}