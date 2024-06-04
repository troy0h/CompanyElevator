using System.Data;

// Created by Troy Hull - 2101507

namespace CompanyElevator
{
    public class Tables
    {
        private static readonly DataTable insertTable = new(); // Create a table, used to insert data into the database

        public static void AddColumns() // Function to create columns needed
        {
            insertTable.Columns.Add("dateTime", typeof(string));
            insertTable.Columns.Add("startFloor", typeof(int));
            insertTable.Columns.Add("destFloor", typeof(int));
        }

        public static DataTable GetInsertTable() // Function to return the table
        {
            return insertTable;
        }
    }
}
