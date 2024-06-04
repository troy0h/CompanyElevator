// Created by Troy Hull - 2101507

namespace CompanyElevator
{
    public partial class ElevatorLogViewer : Form
    {
        public ElevatorLogViewer()
        {
            DataGridView dataGridView = new() // Create a new DataGridView, looks like a table
            {
                Dock = DockStyle.Fill, // Fills the whole panel
                Size = new Size(500, 250), 
                Font = new Font("Segoe UI", 18),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize, // 3 things to auto size the rows and columns
                DataSource = SqlAccess.PopulateDataTable(), // Sets the data source, this is what's outputted in the DataGridView
            };
            Controls.Add(dataGridView); // Adds the new DataGridView to the screen

            InitializeComponent();
        }
    }
}
