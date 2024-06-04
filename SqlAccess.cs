using System.Data;
using System.Data.OleDb;

// Created by Troy Hull - 2101507

namespace CompanyElevator
{
    class SqlAccess
    {        
        public static OleDbConnection GetConn()
        {
            // Return the connection string
            return new("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb;");
        }

        public static void Log()
        {
            try
            {   
                using OleDbConnection conn = GetConn(); // Get the connection string
                conn.Open(); // Open the connection
                OleDbDataAdapter dataAdapter = new() // Create a new data adapter
                {
                    InsertCommand = new OleDbCommand("INSERT INTO log ([dateTime], startFloor, destFloor)" +
                    "VALUES (?, ?, ?)", conn) // Create the insert command, insert datetime, startfloor and destination floor
                };

                dataAdapter.InsertCommand.Parameters.Add("dateTime", OleDbType.VarChar, 20, "dateTime");
                dataAdapter.InsertCommand.Parameters.Add("startFloor", OleDbType.Integer, 2, "startFloor");
                dataAdapter.InsertCommand.Parameters.Add("destFloor", OleDbType.Integer, 2, "destFloor"); // Add needed data as parameters

                dataAdapter.Update(Tables.GetInsertTable()); // Add values from the table into the database
                conn.Close(); // Close the connection
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString()); // If there's an exception, show it in a message box
            }
        }

        public static DataTable PopulateDataTable()
        {
            try
            {
                using OleDbConnection conn = GetConn(); // Get the connection
                conn.Open(); // Open the connection
                OleDbDataAdapter dataAdapter = new() // Create a new data adapter
                {
                    SelectCommand = new OleDbCommand("SELECT * FROM log", conn) // Create a select command, get everything from the database
                };
                DataTable dt = new(); // Create a new data table
                dataAdapter.Fill(dt); // Fill a table with database info
                return dt; // Return the filled data table
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return new DataTable();
            }
        }
    }
}
