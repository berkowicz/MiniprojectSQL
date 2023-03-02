namespace miniprojectSQL
{
    internal class Methods
    {
        //Register hours to a specific user and project
        //Takes in personname and projectname and adds them together with number of hours
        //to add a new project_person with hour in DB
        internal static void RegisterHours()
        {
            //Register hours to a specific user and project
            Console.Clear();
            Console.CursorVisible = true;
            List<PersonModel> persons = DataAccess.LoadPerson();
            List<ProjectModel> projects = DataAccess.LoadProject();

            Console.WriteLine("Register Hours!");
            Console.Write("\nInput your name: ");
            string? personName = Console.ReadLine();
            //Loops throught every person
            for (int i = 0; i < persons.Count; i++)
            {
                //Looking for match in input and persons in list
                if (personName.Equals(persons[i].person_name))
                {
                    Console.Write("Input project: ");
                    string? projectName = Console.ReadLine();
                    //Loops throught every project
                    for (int j = 0; j < projects.Count; j++)
                    {
                        //Looking for match in input and peroject in list
                        if (projectName.Equals(projects[j].project_name))
                        {
                            Console.WriteLine("No input equals 8 hours");
                            Console.Write("Input hours: ");
                            string hourInput = Console.ReadLine();
                            //Validates input
                            if (hourInput.Equals("0"))
                            {
                                Console.WriteLine("Cant report 0 hours");
                                i = persons.Count; j = projects.Count;
                            }
                            //Input OK sends requst to DB
                            else if (int.TryParse(hourInput, out int hours) || hourInput.Equals(string.Empty))
                            {
                                if (DataAccess.ReportHours(persons[i].id, projects[j].id, hours))
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

            /*
            //Test 1 of only getting the person/project searched from the DB

            if (DataAccess.TryLoadPerson(personName).Equals(personName))
            {
                Console.Write("Input project: ");
                string? projectName = Console.ReadLine();
                if (DataAccess.TryLoadProject(projectName))
                {
                    Console.Write("Input hours: ");
                    string hourInput = Console.ReadLine();
                    if (hourInput.Equals("0"))
                    {
                        Console.WriteLine("Cant report 0 hours");
                    }
                    else if (int.TryParse(hourInput, out int hours) || hourInput.Equals(string.Empty))
                    {
                        List<PersonModel> persons = DataAccess.LoadPerson(personName);
                        List<ProjectModel> projects = DataAccess.LoadProject(projectName);
                        if (DataAccess.ReportHours(persons[0].id, projects[0].id, hours))
                            Console.WriteLine("Your hours are registered");
                        else
                            Console.WriteLine("Something went wrong");
                    }
                    else
                        Console.WriteLine("Not a number, try again");
                }
                else
                    Console.WriteLine("Project not found, try again");
            }
            else
                Console.WriteLine("Person not found, try again");
            */

            /*
            //Test 2 of only getting the person/project searched from the DB

            Console.Write("Input project: ");
            string? projectName = Console.ReadLine();
            Console.Write("Input hours: ");
            string hourInput = Console.ReadLine();
            if (hourInput.Equals("0"))
            {
                Console.WriteLine("Cant report 0 hours");
            }
            else if (int.TryParse(hourInput, out int hours) || hourInput.Equals(string.Empty))
            {
                List<PersonModel> person = DataAccess.LoadPerson(personName);
                List<ProjectModel> project = DataAccess.LoadProject(projectName);
                if (DataAccess.ReportHours(persons[0].id, projects[0].id, hours))
                    Console.WriteLine("Your hours are registered");
                else
                    Console.WriteLine("Something went wrong");
            }
            */

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
                if (DataAccess.CreateUser(input))
                    Console.WriteLine("New user was added");
                else
                    Console.WriteLine("User allready axists");
            }
            else
                Console.WriteLine("Invalid input, try again!");
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
                if (DataAccess.CreateProject(input))
                    Console.WriteLine($"New project was added");
                else
                    Console.WriteLine("project allready exists");
            }
            else
                Console.WriteLine("Invalid input, try again!");
            EnterToContinue();
        }

        internal static void ChangeUser()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Change User!\n");
            Console.Write("Input old name of user: ");
            string? oldName = Console.ReadLine();
            List<PersonModel> persons = DataAccess.LoadPerson();
            for (int i = 0; i < persons.Count; i++)
            {
                if (oldName.Equals(persons[i].person_name))
                {
                    Console.Write("Input new name of user: ");
                    string? newName = Console.ReadLine();
                    if (StringInputValidator(oldName) && StringInputValidator(newName))
                    {
                        if (DataAccess.ChangeUser(oldName, newName))
                            Console.WriteLine("Username is updated");
                        else
                            Console.WriteLine("Something went wrong");
                        i = persons.Count;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, try again!");
                        EnterToContinue();
                        return;
                    }
                }
                else if (i == persons.Count - 1)
                {
                    Console.WriteLine("Could not find person");
                }
            }
            EnterToContinue();
        }

        internal static void ChangeProject()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Change Project!\n");
            Console.Write("Input old name of project: ");
            string? oldName = Console.ReadLine();
            List<ProjectModel> projects = DataAccess.LoadProject();
            for (int i = 0; i < projects.Count - 1; i++)
            {
                if (!oldName.Equals(projects[i].project_name))
                {
                    Console.Write("Input new name of project: ");
                    string? newName = Console.ReadLine();
                    if (StringInputValidator(oldName) && StringInputValidator(newName))
                    {
                        if (DataAccess.ChangeProject(oldName, newName))
                            Console.WriteLine("Projectname is updated");
                        else
                            Console.WriteLine("Something went wrong");
                        i = projects.Count;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, try again!");
                        EnterToContinue();
                        return;
                    }
                }
                else if (i == projects.Count - 1)
                {
                    Console.WriteLine("Could not find project");
                }
            }
            EnterToContinue();
        }

        internal static void ChangeRegisteredHours()
        {

        }

        internal static bool StringInputValidator(string input)
        {
            if (input.Equals(string.Empty) || int.TryParse(input, out int number) || string.IsNullOrWhiteSpace(input))
            {
                return false;
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
