namespace YAF.Data.MySql
{
    using Autofac;

    using YAF.Types.Interfaces.Data;

    public class MySqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MySqlDbAccess>()
                .Named<IDbAccess>(MySqlDbAccess.ProviderTypeName)
                .InstancePerLifetimeScope();
        }
    }
}