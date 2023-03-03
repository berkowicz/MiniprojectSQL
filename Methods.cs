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
                            string? hourInput = Console.ReadLine();
                            //Validates input
                            if (hourInput.Equals("0"))
                            {
                                Console.WriteLine("Cant report 0 hours");
                                i = persons.Count; j = projects.Count;
                            }
                            //Input OK sends requst to DB
                            else if (int.TryParse(hourInput, out int hours) || hourInput.Equals(string.Empty) || hours > 24)
                            {
                                //Check if DB accepts request
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
            //Cant get TyCatch to work in getting single row in DataAccess.

            if (DataAccess.TryLoadPerson(personName).Equals(personName))
            {
                Console.Write("Input project: ");
                string? projectName = Console.ReadLine();
                if (DataAccess.TryLoadProject(projectName))
                {
                    Console.Write("Input hours: ");
                    string? hourInput = Console.ReadLine();
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
            //Cant get TyCatch to work in getting single row in DataAccess.

            Console.Write("Input project: ");
            string? projectName = Console.ReadLine();
            Console.Write("Input hours: ");
            string? hourInput = Console.ReadLine();
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

        //Create new user
        internal static void CreateUser()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Create New User!\n");
            Console.Write("Input name of user: ");
            string? input = Console.ReadLine();
            //Validates string
            if (StringInputValidator(input))
            {
                //Check if DB accepts request
                if (DataAccess.CreateUser(input))
                    Console.WriteLine("New user was added");
                else
                    Console.WriteLine("User allready axists");
            }
            else
                Console.WriteLine("Invalid input, try again!");
            EnterToContinue();
        }

        //Create new project
        internal static void CreateProject()
        {
            //Creates new project
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Create New Project!\n");
            Console.Write("Input name of Project: ");
            string? input = Console.ReadLine();
            //Validates string
            if (StringInputValidator(input))
            {
                //Check if DB accepts request
                if (DataAccess.CreateProject(input))
                    Console.WriteLine($"New project was added");
                else
                    Console.WriteLine("project allready exists");
            }
            else
                Console.WriteLine("Invalid input, try again!");
            EnterToContinue();
        }

        //Change username
        internal static void ChangeUser()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Change User!\n");
            Console.Write("Input old name of user: ");
            string? oldName = Console.ReadLine();
            List<PersonModel> persons = DataAccess.LoadPerson();
            //Loops throught every person
            for (int i = 0; i < persons.Count; i++)
            {
                //Looking for match in input and persons in list
                if (oldName.Equals(persons[i].person_name))
                {
                    Console.Write("Input new name of user: ");
                    string? newName = Console.ReadLine();
                    //Validates input
                    if (StringInputValidator(oldName) && StringInputValidator(newName))
                    {
                        //Check if DB accepts request
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

        //Change projectname
        internal static void ChangeProject()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Change Project!\n");
            Console.Write("Input old name of project: ");
            string? oldName = Console.ReadLine();
            List<ProjectModel> projects = DataAccess.LoadProject();
            //Loops throught every project
            for (int i = 0; i < projects.Count; i++)
            {
                // Looking for match in input and project in list
                if (oldName.Equals(projects[i].project_name))
                {
                    Console.Write("Input new name of project: ");
                    string? newName = Console.ReadLine();
                    //Validates input
                    if (StringInputValidator(oldName) && StringInputValidator(newName))
                    {
                        //Check if DB accepts request
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

        //Update reported hours
        internal static void ChangeRegisteredHours()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Change hour input!\n");
            Console.Write("Input the day(number) you want to update: ");
            //Validates input
            if (!int.TryParse(Console.ReadLine(), out int day))
            {
                Console.WriteLine("Invalid input, try again!");
                EnterToContinue();
                return;
            }
            List<ProjectPersonModel> projectPerson = DataAccess.LoadProjectPerson();
            //Loops throught every reported hour
            for (int i = 0; i < projectPerson.Count; i++)
            {
                // Looking for match in input and projectPerson in list
                if (day == projectPerson[i].id)
                {
                    Console.WriteLine($"You have {projectPerson[i].hours} hours registred on day {projectPerson[i].id}");
                    Console.Write("Input new value: ");
                    //Validates input
                    if (!int.TryParse(Console.ReadLine(), out int hours) || hours > 24)
                    {
                        Console.WriteLine("Invalid input, try again!");
                        EnterToContinue();
                        return;
                    }
                    //Check if DB accepts request
                    if (DataAccess.UpdateProjectPerson(day, hours))
                        Console.WriteLine($"Hours is updated to {hours} hours");
                    else
                        Console.WriteLine("Something went wrong");
                    i = projectPerson.Count;
                }
                else if (i == projectPerson.Count - 1)
                    Console.WriteLine("Registration not found, try again");
            }
            EnterToContinue();
        }

        //Validator for strings
        internal static bool StringInputValidator(string input)
        {
            if (int.TryParse(input, out int number) || string.IsNullOrWhiteSpace(input))
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
