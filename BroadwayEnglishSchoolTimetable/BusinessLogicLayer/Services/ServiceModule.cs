using Autofac;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Infrastructure
{
    public class ServiceModule : Module
    {
        private string connectionString_;
        public ServiceModule(string connection)
        {
            connectionString_ = connection;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().WithParameter("connectionString", connectionString_); 
        }
    }
}
