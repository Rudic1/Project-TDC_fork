﻿using DataRepository;
using Microsoft.Data.SqlClient;
using System.Reflection;
using TDC.Backend.DataRepository.Config;
using TDC.Backend.DataRepository.Helper;

namespace TDC.Backend.DataRepository.Test
{
    public class TestStartUp
    {
        public static ConnectionFactory GetConnectionFactory()
        {
            var connectionStrings = GetConnectionStrings();
            UseEvolveMigration(connectionStrings);

            return new ConnectionFactory(connectionStrings);
        }

        public static ConnectionStrings GetConnectionStrings()
        {
            var connectionStrings = new ConnectionStrings();

            var config = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.dbtest.json")
                         .Build();
            config.Bind("ConnectionStrings", connectionStrings);

            return connectionStrings;
        }

        public static void CleanTables(IEnumerable<BaseRepository> repositories)
        {
            foreach (var repository in repositories)
            {
                repository.CleanTable();
            }
        }

        private static void UseEvolveMigration(ConnectionStrings connectionStrings)
        {
            var workingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var location = Path.Combine(workingDir, "Database", "migrations");

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var connectionString = env == "Development"
                                       ? connectionStrings.Sql_Local
                                       : connectionStrings.Sql;

            var connection = new SqlConnection(connectionString);

            var evolve = new EvolveDb.Evolve(connection, message => Console.WriteLine(message))
            {
                Locations = [location],
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
    }
}
