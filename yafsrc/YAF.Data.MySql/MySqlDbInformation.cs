/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2015 Ingo Herbote
 * http://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Data.MySql
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    using YAF.Classes;
    using YAF.Core.Data;
    using YAF.Types;
    using YAF.Types.Interfaces.Data;

    /// <summary>
    /// MS SQL DB Information
    /// </summary>
    public class MySqlDbInformation : IDbInformation
    {
        /// <summary>
        /// The DB parameters
        /// </summary>
        protected DbConnectionParam[] _dbParameters =
            {
                new DbConnectionParam(0, "Password", string.Empty),
                new DbConnectionParam(1, "Data Source", "(local)"),
                new DbConnectionParam(2, "Initial Catalog", string.Empty),
                new DbConnectionParam(11, "Use Integrated Security", "true")
            };

        /// <summary>
        /// The azure script list
        /// </summary>
        private static readonly string[] _AzureScriptList =
            {
                "mysql/install/azure/InstallCommon.sql",
                "mysql/install/azure/InstallMembership.sql",
                "mysql/install/azure/InstallProfile.sql",
                "mysql/install/azure/InstallRoles.sql"
            };

        /// <summary>
        /// The install script list
        /// </summary>
        private static readonly string[] _InstallScriptList =
            {
                "mysql/install/tables.sql", 
                "mysql/install/indexes.sql", 
                "mysql/install/views.sql",
                "mysql/install/constraints.sql", 
                "mysql/install/functions.sql", 
                "mysql/install/procedures.sql",
                "mysql/install/forum_ns.sql"
            };

        /// <summary>
        /// The upgrade script list
        /// </summary>
        private static readonly string[] _UpgradeScriptList =
            {
                "mysql/upgrade/tables.sql", 
                "mysql/upgrade/indexes.sql", 
                "mysql/upgrade/views.sql",
                "mysql/upgrade/constraints.sql", 
                "mysql/upgrade/triggers.sql",
                "mysql/upgrade/functions.sql", 
                "mysql/upgrade/procedures.sql",
                "mysql/upgrade/forum_ns.sql"
            };

        /// <summary>
        /// The YAF Provider script list
        /// </summary>
        private static readonly string[] _YAFProviderScriptList =
            {
                "mysql/install/providers/tables.sql",
                "mysql/install/providers/indexes.sql", 
                "mysql/install/providers/procedures.sql"
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDbInformation"/> class.
        /// </summary>
        public MySqlDbInformation()
        {
            this.ConnectionString = () => Config.ConnectionString;
            this.ProviderName = MySqlDbAccess.ProviderTypeName;
        }

        /// <summary>
        /// Gets or sets the DB Connection String
        /// </summary>
        public Func<string> ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the DB Provider Name
        /// </summary>
        public string ProviderName { get; protected set; }

        /// <summary>
        /// Gets Full Text Script.
        /// </summary>
        public string FullTextScript
        {
            get
            {
                return "mysql/fulltext.sql";
            }
        }

        /// <summary>
        /// Gets the Azure Script List.
        /// </summary>
        public IEnumerable<string> AzureScripts
        {
            get
            {
                return _AzureScriptList;
            }
        }

        /// <summary>
        /// Gets the Install Script List.
        /// </summary>
        public IEnumerable<string> InstallScripts
        {
            get
            {
                return _InstallScriptList;
            }
        }

        /// <summary>
        /// Gets the Upgrade Script List.
        /// </summary>
        public IEnumerable<string> UpgradeScripts
        {
            get
            {
                return _UpgradeScriptList;
            }
        }

        /// <summary>
        /// Gets the YAF Provider Script List.
        /// </summary>
        public IEnumerable<string> YAFProviderScripts
        {
            get
            {
                return _YAFProviderScriptList;
            }
        }

        /// <summary>
        /// Gets the DB Connection Parameters.
        /// </summary>
        public IDbConnectionParam[] DbConnectionParameters
        {
            get
            {
                return this._dbParameters.OfType<IDbConnectionParam>().ToArray();
            }
        }

        /// <summary>
        /// Builds a connection string.
        /// </summary>
        /// <param name="parameters">The Connection Parameters</param>
        /// <returns>Returns the Connection String</returns>
        public string BuildConnectionString([NotNull] IEnumerable<IDbConnectionParam> parameters)
        {
            CodeContracts.VerifyNotNull(parameters, "parameters");

            var connBuilder = new SqlConnectionStringBuilder();

            foreach (var param in parameters)
            {
                connBuilder[param.Name] = param.Value;
            }

            return connBuilder.ConnectionString;
        }
    }
}