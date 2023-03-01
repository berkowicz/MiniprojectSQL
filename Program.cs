namespace miniprojectSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Menu class with the options "Sign In", "Create New User", "Exit"
            Menusystem mainMenu = new Menusystem(new string[] { "Register hours", "New user", "New project", "Exit" });
            // Print the menu to the console
            mainMenu.PrintMenu();

            // Declare a variable to keep track of whether to show the menu or not
            bool showMenu = true;

            // Loop until the showMenu variable is false
            while (showMenu)
            {
                // Get the selected index from the UseMenu method
                int index = mainMenu.UseMenu();
                // Check the selected index
                switch (index)
                {
                    // If the selected index is 0 (Sign In)
                    case 0:
                        Methods.RegisterHours();
                        break;
                    // If the selected index is 1 (Create New User)
                    case 1:
                        Methods.CreateUser();
                        break;
                    case 2:
                        Methods.CreateProject();
                        break;
                    // If the selected index is 2 (Exit)
                    case 3:
                        // Set the showMenu variable to false to exit the loop
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