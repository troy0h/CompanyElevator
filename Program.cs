// Created by Troy Hull - 2101507

namespace CompanyElevator
{
    internal static class Program
    {
        [STAThread]
        static void Main() // First thing to be ran in the program
        {
            Tables.AddColumns(); // Adds the columns to the table. Needed to be done early and only once.
            ApplicationConfiguration.Initialize();
            Application.Run(new ElevatorControl()); // Run the application
        }
    }
}