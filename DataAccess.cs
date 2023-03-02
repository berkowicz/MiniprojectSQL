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
                    cnn.Execute($"INSERT INTO dbe_person(person_name) VALUES ('{name.ToLower()}')", new DynamicParameters());
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
                    cnn.Execute($"INSERT INTO dbe_project (project_name) VALUES ('{projectName.ToLower()}')", new DynamicParameters());
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
                    //Console.WriteLine(e.MessageText);
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
        internal static bool ChangeUser(string oldName, string newName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"UPDATE dbe_person SET person_name='{newName.ToLower()}' WHERE person_name='{oldName.ToLower()}'", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }

        internal static bool ChangeProject(string oldName, string newName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"UPDATE dbe_project SET project_name='{newName.ToLower()}' WHERE project_name='{oldName.ToLower()}'", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }

        /* ////////////////////////TEST
        internal static string TryLoadPerson(string person_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"SELECT person_name FROM dbe_person WHERE person_name='{person_name.ToLower()}'", new DynamicParameters());
                return output.ToString();
            }
        }

        internal static bool TryLoadProject(string project_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"SELECT * FROM dbe_project WHERE project_name='{project_name.ToLower()}'", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                }
                return true;
            }
        }

        internal static List<PersonModel> LoadPerson(string person_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>($"SELECT * FROM dbe_person WHERE person_name='{person_name.ToLower()}'", new DynamicParameters());
                return output.ToList();
            }
        }
        
        internal static List<ProjectModel> LoadProject(string project_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>($"SELECT * FROM dbe_project WHERE project_name='{project_name.ToLower()}'", new DynamicParameters());
                return output.ToList();
            }
        }*/


        /*internal static bool ChangeRegisteredHours(string person_name_old, string project_name_old, string person_name_new, string project_name_new)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"UPDATE dbe_project SET project_name='{newName.ToLower()}' WHERE project_name='{oldName.ToLower()}'", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }*/

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}


