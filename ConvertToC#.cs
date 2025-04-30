using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryJobScheduler
{
    public partial class MainWindow : Form
    {
        // Initialize form components
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // Form load logic (if needed)
        }

        // Method to initialize UI components
        private void InitializeComponent()
        {
            this.txtMemorySize = new System.Windows.Forms.TextBox();
            this.txtNoOfFrames = new System.Windows.Forms.TextBox();
            this.txtNoOfJobs = new System.Windows.Forms.TextBox();
            this.lblMemorySize = new System.Windows.Forms.Label();
            this.lblNoOfFrames = new System.Windows.Forms.Label();
            this.lblNoOfJobs = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.panelRAM = new System.Windows.Forms.Panel();
            this.scrollPane = new System.Windows.Forms.ScrollableControl();

            // Textboxes and Labels
            this.lblMemorySize.Text = "Enter Memory Size:";
            this.lblMemorySize.Location = new System.Drawing.Point(40, 40);
            this.txtMemorySize.Location = new System.Drawing.Point(180, 40);
            this.txtMemorySize.Size = new Size(100, 20);

            this.lblNoOfFrames.Text = "Enter No. of Frames:";
            this.lblNoOfFrames.Location = new System.Drawing.Point(40, 70);
            this.txtNoOfFrames.Location = new System.Drawing.Point(180, 70);
            this.txtNoOfFrames.Size = new Size(100, 20);

            this.lblNoOfJobs.Text = "Enter No. of Jobs:";
            this.lblNoOfJobs.Location = new System.Drawing.Point(40, 100);
            this.txtNoOfJobs.Location = new System.Drawing.Point(180, 100);
            this.txtNoOfJobs.Size = new Size(100, 20);

            // Button to generate panels and jobs
            this.btnContinue.Text = "Continue";
            this.btnContinue.Location = new System.Drawing.Point(180, 130);
            this.btnContinue.Click += new EventHandler(this.BtnContinue_Click);

            // Panel to hold RAM details
            this.panelRAM.Location = new System.Drawing.Point(40, 160);
            this.panelRAM.Size = new Size(300, 400);
            this.scrollPane.Controls.Add(panelRAM);
            this.scrollPane.Location = new Point(40, 160);
            this.scrollPane.Size = new Size(300, 450);

            // Main form setup
            this.ClientSize = new System.Drawing.Size(600, 650);
            this.Controls.Add(this.lblMemorySize);
            this.Controls.Add(this.txtMemorySize);
            this.Controls.Add(this.lblNoOfFrames);
            this.Controls.Add(this.txtNoOfFrames);
            this.Controls.Add(this.lblNoOfJobs);
            this.Controls.Add(this.txtNoOfJobs);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.scrollPane);
            this.Text = "Memory Job Scheduler";
        }

        private TextBox txtMemorySize;
        private TextBox txtNoOfFrames;
        private TextBox txtNoOfJobs;
        private Label lblMemorySize;
        private Label lblNoOfFrames;
        private Label lblNoOfJobs;
        private Button btnContinue;
        private Panel panelRAM;
        private ScrollableControl scrollPane;

        // Button Click event to generate panels and display memory layout
        private void BtnContinue_Click(object sender, EventArgs e)
        {
            GeneratePanels();
        }

        // Method to generate dynamic panels for RAM layout
        private void GeneratePanels()
        {
            panelRAM.Controls.Clear(); // Clear previous panels

            double osKernel = 10, finalMemorySize, gap;
            int memorySize, noOfFrames, noOfJobs;
            int[] noOfBoxes;

            try
            {
                // Get user inputs
                memorySize = int.Parse(txtMemorySize.Text);
                noOfFrames = int.Parse(txtNoOfFrames.Text);
                noOfJobs = int.Parse(txtNoOfJobs.Text);

                if (memorySize <= 0 || noOfFrames <= 0 || noOfJobs <= 0)
                    throw new Exception("Invalid input. All values must be positive.");

                // Calculate memory and gap
                finalMemorySize = memorySize - osKernel;
                gap = finalMemorySize / noOfFrames;

                // Create OS Kernel panel
                Panel os = new Panel();
                os.BorderStyle = BorderStyle.FixedSingle;
                os.Size = new Size(300, 30);
                os.Controls.Add(new Label() { Text = $"OS Kernel: {osKernel} MB", Location = new Point(10, 5) });
                panelRAM.Controls.Add(os);

                // Create panels for memory pages
                for (int i = 0; i < noOfFrames; i++)
                {
                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(300, 30);
                    newPanel.BorderStyle = BorderStyle.FixedSingle;
                    osKernel += gap;

                    // Add label for each page
                    newPanel.Controls.Add(new Label() { Text = $"{osKernel:0.0} MB | Page No: {i}", Location = new Point(10, 5) });
                    panelRAM.Controls.Add(newPanel);
                }

                // Update scrollable content
                scrollPane.VerticalScroll.Value = 0;
                panelRAM.VerticalScroll.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
