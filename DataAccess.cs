using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;

namespace miniprojectSQL
{
    internal class DataAccess
    {
        //Get data from dabe_person
        internal static List<PersonModel> LoadPerson()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>($"SELECT * FROM dabe_person", new DynamicParameters());
                return output.ToList();
            }
        }

        //Get data from dabe_project
        internal static List<ProjectModel> LoadProject()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>("SELECT * FROM dabe_project", new DynamicParameters());
                return output.ToList();
            }
        }

        //Get data from dabe_project_person
        internal static List<ProjectPersonModel> LoadProjectPerson()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectPersonModel>($"SELECT * FROM dabe_project_person", new DynamicParameters());
                return output.ToList();
            }
        }

        //Sends request to add new user
        internal static bool CreateUser(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute($"INSERT INTO dabe_person(person_name) VALUES ('{name.ToLower()}')", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    //Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }

        //Sends request to add new project
        internal static bool CreateProject(string projectName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute($"INSERT INTO dabe_project (project_name) VALUES ('{projectName.ToLower()}')", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    //Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }

        //Sends request to add new register.
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
                    cnn.Execute(@$"INSERT INTO dabe_project_person (project_id, person_id, hours) VALUES ('{name_id}','{project_id}','{hour}')", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }


        internal static bool UpdateProjectPerson(int day, int hours)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"UPDATE dabe_project_person SET hours='{hours}' WHERE id='{day}'", new DynamicParameters());
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;

            }
        }

        internal static bool ChangeUser(string oldName, string newName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"UPDATE dabe_person SET person_name='{newName.ToLower()}' WHERE person_name='{oldName.ToLower()}'", new DynamicParameters());
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
                    cnn.Query($"UPDATE dabe_project SET project_name='{newName.ToLower()}' WHERE project_name='{oldName.ToLower()}'", new DynamicParameters());
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
        /*
        internal static bool TryLoadProjectPerson(int day)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"SELECT id FROM dabe_project_person WHERE id='{day}'", new DynamicParameters());
                    return true;
                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
            }
        }
        
        internal static string TryLoadPerson(string person_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"SELECT person_name FROM dabe_person WHERE person_name='{person_name.ToLower()}'", new DynamicParameters());
                return output.ToString();
            }
        }

        internal static bool TryLoadProject(string project_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"SELECT * FROM dabe_project WHERE project_name='{project_name.ToLower()}'", new DynamicParameters());
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
                var output = cnn.Query<PersonModel>($"SELECT * FROM dabe_person WHERE person_name='{person_name.ToLower()}'", new DynamicParameters());
                return output.ToList();
            }
        }
        
        internal static List<ProjectModel> LoadProject(string project_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>($"SELECT * FROM dabe_project WHERE project_name='{project_name.ToLower()}'", new DynamicParameters());
                return output.ToList();
            }
        }*/


        /*internal static bool ChangeRegisteredHours(string person_name_old, string project_name_old, string person_name_new, string project_name_new)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Query($"UPDATE dabe_project SET project_name='{newName.ToLower()}' WHERE project_name='{oldName.ToLower()}'", new DynamicParameters());
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


