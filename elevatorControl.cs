
// Created by Troy Hull - 2101507

namespace CompanyElevator
{
    public partial class ElevatorControl : Form
    {
        //
        // 
        // INITIALIZATION
        //
        //


        public int LiftPosition = 0; // Lift position needs to be used in many places
        public int LiftDestination = 0;
        
        public TextBox reqTextBox = new() // Add public text boxes, needs to be used in multiple places
        {
            AutoSize = false,
            Width = 435,
            Dock = DockStyle.None,
            Height = 40,
            ReadOnly = true,
            Font = new Font("Segoe UI", 18),
            Text = "",
            TextAlign = HorizontalAlignment.Center,
        };

        public TextBox conTextBox = new()
        {
            AutoSize = false,
            Width = 435,
            Dock = DockStyle.None,
            Height = 40,
            ReadOnly = true,
            Font = new Font("Segoe UI", 18),
            Text = "",
            TextAlign = HorizontalAlignment.Center,
        };

        public ElevatorControl()
        {
            FormClosing += FormClose;

            int topFloor = 10; // Initialising the window
            int bottomFloor = 0;
            int buttSize = 0;
            int floorDiff = topFloor - bottomFloor;
            
            if (floorDiff == 1)
                buttSize = 219;
            else if (floorDiff <= 7 )
                buttSize = 104;
            else
                buttSize = 50;

            this.BackgroundImage = Image.FromFile("metal.jpg");

            InitializeComponent();

            /*
             * Request Panel, on the left of the program
             */

            FlowLayoutPanel reqPanel = new() // Create the panel
            {
                Size = new Size(450, 500),
                BackColor = Color.Transparent,
                Location = new Point(0, 0)
            };

            this.Controls.Add(reqPanel);

            Label reqLabel = new() // Create a label for the request panel, and set its properties
            {
                AutoSize = false,
                Width = 435,
                TextAlign = (ContentAlignment)HorizontalAlignment.Center,
                Dock = DockStyle.None,
                Height = 40,
                Font = new Font("Segoe UI", 18),
                Text = "Request Panel"
            };
            reqPanel.Controls.Add(reqLabel);

            reqPanel.Controls.Add(reqTextBox); // Add the text boxes, made at the top to be public

            // Add the buttons
            for (int i = bottomFloor; i <= topFloor; i++)
            {
                Button reqButt = new();
                if (i == 0) // If i is 0, make the ground floor button
                {
                    reqButt.Text = "G";
                    reqButt.AccessibleName = "rG";
                } 
                else
                {
                    reqButt.Text = i.ToString();
                    reqButt.AccessibleName = "r" + i.ToString();
                }

                reqButt.Font = new Font("Segoe UI", 18);
                reqButt.Size = new Size(buttSize, buttSize);
                reqButt.Click += OnButtonClick;

                reqPanel.Controls.Add(reqButt);
            }

            /*
             * Control Panel, on the right of the program
             */

            FlowLayoutPanel conPanel = new() // Create the panel
            {
                Size = new Size(450, 500),
                BackColor = Color.Transparent,
                Location = new Point(550, 0)
            };

            this.Controls.Add(conPanel);

            Label conLabel = new() // Create a label for the control panel, and set its properties
            {
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                Width = 435,
                Dock = DockStyle.None,
                Height = 40,
                Font = new Font("Segoe UI", 18),
                Text = "Control Panel"
            };

            conPanel.Controls.Add(conLabel);

            conPanel.Controls.Add(conTextBox); // Add the text boxes, made at the top to be public

            for (int i = bottomFloor; i <= topFloor; i++)  // Add the buttons
            {
                Button conButt = new();
                if (i == 0) // If i is 0, make the ground floor button
                {
                    conButt.Text = "G";
                    conButt.AccessibleName = "cG";
                }
                else
                {
                    conButt.Text = i.ToString();
                    conButt.AccessibleName = "c" + i.ToString();
                }
                conButt.Font = new Font("Segoe UI", 18);
                conButt.Size = new Size(buttSize, buttSize);
                conButt.Click += OnButtonClick;
                conPanel.Controls.Add(conButt);
            }

            // Add logging button
            Button logButt = new()
            {
                AccessibleName = "log",
                Text = "Elevator Log",
                Font = new Font("Segoe UI", 18),
                Size = new Size(200, 50),
                Location = new Point(10, 540)
            };
            logButt.Click += OnButtonClick;
            this.Controls.Add(logButt);

            // Add button to add logs button
            Button addLogButt = new()
            {
                AccessibleName = "addLogs",
                Text = "Save Logs",
                Font = new Font("Segoe UI", 18),
                Size = new Size(200, 50),
                Location = new Point(220, 540)
            };
            addLogButt.Click += OnButtonClick;
            this.Controls.Add(addLogButt);
        }

        //
        // INITIALIZATION END
        //


        //
        // EVENTS
        //

        void FormClose(object sender, System.ComponentModel.CancelEventArgs e) // When the X is pressed on the application
        {
            if (sender is null)  // Sender should never be null
                throw new Exception("Something went wrong!");
            DialogResult res = MessageBox.Show("Are you sure you want to close the program? \nAll unsaved logs will be lost.", "Are you sure?", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information); // Open a message box that will allow for confirmation
            if (res == DialogResult.OK)
            {
                e.Cancel = false; // Continue closing
            }
            else if (res == DialogResult.Cancel)
            {
                e.Cancel = true; // Cancel closing
            }
        }

        void OnButtonClick(object? sender, EventArgs e) 
        {
            if (sender is null)  // Sender should never be null
                throw new Exception("Something went wrong!");

            else
            {
                Button clicked = (Button)sender;  // Finds button that was clicked, gets info from it
                String name = clicked.AccessibleName;
                String type = name[..1];
                name = name[1..];
                switch (type)
                {
                    case "r": // From request panel (Call to floor)
                        if (name == "G") // Don't want to call ground floor "floor 0"
                            {
                                reqTextBox.Text = "Called to ground floor";
                                name = "0";
                            }
                        else if (name != "G")
                            {
                                reqTextBox.Text = "Called to floor " + name;
                            }
                        else
                            throw new Exception("Something went wrong!");
                        AnimateElevator(LiftPosition, int.Parse(name));
                        break;

                    case "c": // From the control panel
                        if (name == "G") // Don't want to call ground floor "floor 0"
                        {
                            reqTextBox.Text = "Going to ground floor";
                            name = "0";
                        }
                        else if (name != "G")
                        {
                            reqTextBox.Text = "Going to floor " + name;
                        }
                        else
                            throw new Exception("Something went wrong!");
                        AnimateElevator(LiftPosition, int.Parse(name));
                        break;

                    case "l":
                        ElevatorLogViewer form = new(); // Show a new window
                        form.Show();
                        break;

                    case "a":
                        new Thread(() => // Run a thread in the background
                        {
                            Thread.CurrentThread.IsBackground = true;
                            SqlAccess.Log();
                        }).Start();
                        break;

                    default:
                        throw new Exception("Something went wrong!");
                }
            }
        }

        public void AnimateElevator(int liftPosition, int liftDestination)
        {
            String dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Date and time formatting
            Tables.GetInsertTable().Rows.Add(dateTime, liftPosition, liftDestination);
            Update(); // Forces the window to update
            if (liftPosition < liftDestination) // Position below destination, go up
            {
                while (liftPosition < liftDestination)
                {
                    Update(); // Update the window before sleeping to update the number
                    liftPosition++;
                    if (liftPosition == 0)
                        conTextBox.Text = "G"; // Avoid calling it "Floor 0" for better UX
                    else
                        conTextBox.Text = liftPosition.ToString();
                    LiftPosition = liftPosition; // Capital L is global, lowercase l is local
                    Thread.Sleep(500);
                }
                if (liftPosition == 0)
                    reqTextBox.Text = "Arrived at ground floor";
                else if (liftPosition != 0)
                    reqTextBox.Text = "Arrived at floor " + liftPosition.ToString();
            }

            else if (liftPosition > liftDestination) // Position above destination, go down
            {
                while (liftPosition > liftDestination)
                {
                    Update(); // Update the window before sleeping to update the number
                    liftPosition--;
                    if (liftPosition == 0)
                        conTextBox.Text = "G"; // Avoid calling it "Floor 0" for better UX
                    else
                        conTextBox.Text = liftPosition.ToString();
                    LiftPosition = liftPosition; // Capital L is global, lowercase l is local
                    Thread.Sleep(500);
                }
                if (liftPosition == 0)
                    reqTextBox.Text = "Arrived at ground floor";
                else
                    reqTextBox.Text = "Arrived at floor " + liftPosition.ToString();
            }
            else if (liftPosition == liftDestination) // Position already at destination, don't move
            {
                if (liftPosition == 0)
                    reqTextBox.Text = "Already on ground floor";
                else
                    reqTextBox.Text = "Already on floor " + liftPosition.ToString();
            }
            else // Some error happened
            {
                throw new Exception("Something went wrong!");
            }
        }
    }
}
