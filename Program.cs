namespace miniprojectSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Menu class
            Menusystem mainMenu = new Menusystem(new string[] { "Register hours", "New user", "New project", "Update user", "Update project", "Update hours", "Exit" });
            // Print the menu to the console
            mainMenu.PrintMenu();

            bool showMenu = true;

            // Loop until the showMenu variable is false
            while (showMenu)
            {
                // Get the selected index from the UseMenu method
                int index = mainMenu.UseMenu();
                switch (index)
                {
                    case 0:
                        Methods.RegisterHours();
                        break;
                    case 1:
                        Methods.CreateUser();
                        break;
                    case 2:
                        Methods.CreateProject();
                        break;
                    case 3:
                        Methods.ChangeUser();
                        break;
                    case 4:
                        Methods.ChangeProject();
                        break;
                    case 5:
                        Methods.ChangeRegisteredHours();
                        break;
                    case 6:
                        //Exits program
                        showMenu = false;
                        break;
                    // If the selected index is none of the above
                    default:
                        // Do nothing
                        break;
                }
            }
        }
    }
}