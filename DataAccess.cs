using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;

namespace miniprojectSQL
{
    internal class DataAccess
    {
        internal static bool CreateUser(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute($"INSERT INTO public.dbe_person(person_name) VALUES ('{name}')", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    //Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }
        internal static bool CreateProject(string projectName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute($"INSERT INTO dbe_project (project_name) VALUES ('{projectName}')", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    //Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }


        internal static bool ReportHours(int name_id, int project_id, int hour)
        {
            if (hour == 0)
            {
                hour = 8;
            }
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute(@$"INSERT INTO dbe_project_person (project_id, person_id, hours) VALUES ('{name_id}','{project_id}','{hour}')", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }

        internal static List<PersonModel> LoadPerson()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>($"SELECT * FROM dbe_person", new DynamicParameters());
                return output.ToList();
            }
        }

        internal static List<ProjectModel> LoadProject()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>("SELECT * FROM dbe_project", new DynamicParameters());
                return output.ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}


