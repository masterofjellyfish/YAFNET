namespace YAF.Data.MsSql
{
    using ServiceStack.OrmLite;
    using ServiceStack.OrmLite.SqlServer;

    using YAF.Classes;
    using YAF.Core.Data;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The YAF naming strategy base override.
    /// </summary>
    public class YafNamingStrategyBaseOverride : INamingStrategy
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get column name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetColumnName(string name)
        {
            return name;
        }

        /// <summary>
        /// Gets the name of the sequence.
        /// </summary>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns the sequence name</returns>
        public virtual string GetSequenceName(string modelName, string fieldName)
        {
            return string.Format("SEQ_{0}_{1}", modelName, fieldName);
        }

        /// <summary>
        /// Applies the name restrictions.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns the name</returns>
        public string ApplyNameRestrictions(string name)
        {
            return name;
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <param name="modelDef">The model definition.</param>
        /// <returns>Returns the name of the table.</returns>
        public string GetTableName(ModelDefinition modelDef)
        {
            return this.GetTableName(modelDef.ModelName);
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>Returns the name of the table.</returns>
        public string GetTableName(string name)
        {
            return string.Format("{0}{1}", Config.DatabaseObjectQualifier, name);
        }

        #endregion

        public string GetSchemaName(string name)
        {
            throw new System.NotImplementedException();
        }

        public string GetSchemaName(ModelDefinition modelDef)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// The YAF SQL Server ORM lite dialect provider.
    /// </summary>
    public class YafSqlServerOrmLiteDialectProvider : SqlServerOrmLiteDialectProvider
    {
        #region Static Fields

        /// <summary>
        /// The instance.
        /// </summary>
        public static YafSqlServerOrmLiteDialectProvider Instance = new YafSqlServerOrmLiteDialectProvider();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YafSqlServerOrmLiteDialectProvider"/> class.
        /// </summary>
        public YafSqlServerOrmLiteDialectProvider()
        {
            this.NamingStrategy = new YafNamingStrategyBaseOverride();
        }

        #endregion
    }

    /// <summary>
    /// The set MS SQL dialect event.
    /// </summary>
    [ExportService(ServiceLifetimeScope.InstancePerDependancy, new[] { typeof(IHandleEvent<InitDatabaseProviderEvent>) })]
    public class SetMsSqlDialectEvent : IHandleEvent<InitDatabaseProviderEvent>
    {
        #region Public Properties

        /// <summary>
        ///     Gets the order.
        /// </summary>
        public int Order
        {
            get
            {
                return 1000;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The event. 
        /// </param>
        public void Handle(InitDatabaseProviderEvent @event)
        {
            if (@event.ProviderName == MsSqlDbAccess.ProviderTypeName)
            {
                // set the OrmLite dialect provider...
                OrmLiteConfig.DialectProvider = YafSqlServerOrmLiteDialectProvider.Instance;
            }
        }

        #endregion
    }
}