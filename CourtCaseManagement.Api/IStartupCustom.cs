using Microsoft.Extensions.DependencyInjection;

namespace CourtCaseManagement.Api
{
    public interface IStartupCustom
    {
        void AddSwaggerConfigure(IServiceCollection services);
        void AddDatabaseConfigure(IServiceCollection services);
        void AddInfrastructureClassDependencyInject(IServiceCollection services);
        void AddApplicationCoreClassDependencyInject(IServiceCollection services);
    }
}