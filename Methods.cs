namespace miniprojectSQL
{
    internal class Methods
    {
        internal static void RegisterHours()
        {
            //Register hours to a specific user and project
            //Enter name and project then enter hours
            Console.Clear();
            Console.CursorVisible = true;
            List<PersonModel> persons = DataAccess.LoadPerson();
            List<ProjectModel> projects = DataAccess.LoadProject();
            Console.WriteLine("Register Hours!");
            Console.Write("\nInput your name: ");
            string? name = Console.ReadLine();
            for (int i = 0; i < persons.Count; i++)
            {
                if (name.Equals(persons[i].person_name))
                {
                    Console.Write("Input project: ");
                    string? project = Console.ReadLine();
                    for (int j = 0; j < projects.Count; j++)
                    {
                        if (project.Equals(projects[j].project_name))
                        {
                            Console.WriteLine("No input equals 8 hours");
                            Console.Write("Input hours: ");
                            string hourInput = Console.ReadLine();
                            if (hourInput.Equals("0"))
                            {
                                Console.WriteLine("Cant report 0 hours");
                                i = persons.Count; j = projects.Count;
                            }
                            else if (int.TryParse(hourInput, out int hours) || hourInput.Equals(string.Empty))
                            {
                                bool success = DataAccess.ReportHours(persons[i].id, projects[j].id, hours);
                                if (success)
                                    Console.WriteLine("Your hours are registered");
                                else
                                    Console.WriteLine("Something went wrong");
                                i = persons.Count; j = projects.Count;
                            }
                            else
                            {
                                Console.WriteLine("Not a number, try again");
                                i = persons.Count; j = projects.Count;
                            }
                        }
                        else if (j == projects.Count - 1)
                        {
                            Console.WriteLine("Project not found, try again");
                            i = persons.Count;
                        }
                    }
                }
                else if (i == persons.Count - 1)
                {
                    Console.WriteLine("Person not found, try again");
                }
            }
            EnterToContinue();
        }

        internal static void CreateUser()
        {
            //Creates new user
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Create New User!\n");
            Console.Write("Input name of user: ");
            string? input = Console.ReadLine();
            if (StringInputValidator(input))
            {
                bool success = DataAccess.CreateUser(input);
                if (success)
                    Console.WriteLine("New user was added");
                else
                    Console.WriteLine("Something went wrong");
            }
            EnterToContinue();
        }

        internal static void CreateProject()
        {
            //Creates new project
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Create New Project!\n");
            Console.Write("Input name of Project: ");
            string? input = Console.ReadLine();
            if (StringInputValidator(input))
            {
                bool success = DataAccess.CreateProject(input);
                if (success)
                    Console.WriteLine($"New project was added");
                else
                    Console.WriteLine("Something went wrong");
            }
            EnterToContinue();
        }

        internal static bool StringInputValidator(string input)
        {
            if (input.Equals(string.Empty))
            {
                Console.WriteLine("You did not input any, try again!");
                return false;
            }
            else if (int.TryParse(input, out int number) || input.Contains(" "))
            {
                Console.WriteLine("Invalid input, try again!");
                return false;
            }
            return true;
        }

        internal static bool IntInputValidator(string input)
        {
            if (!int.TryParse(input, out int number))
            {
                Console.WriteLine("You did not enter a number, try again!");
                return false;
            }
            else if (input.Equals("0"))
            {
                Console.WriteLine("You entered 0 do you want to ");
            }
            return true;
        }

        internal static void EnterToContinue()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
