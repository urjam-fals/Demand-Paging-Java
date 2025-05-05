using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demand_Paging
{
    public partial class MainFrame : Form
    {
        // Constants
        private const int OS_KERNEL_SIZE = 10; // Memory size (MB)
        private const int FRAME_HEIGHT = 30; // Frame size in pixels

        //Variables
        private int memorySize;
        private int frameSize;
        private int numFrames;
        private int numJobs;
        private List<Job> jobs;
        private List<Frame> frames;
        private int currentJobIndex = 0;
        private int currentPageIndex = 0;
        private Random random;
        private Dictionary<int, Color> jobColors;
        private Queue<int> fifoQueue;

        public MainFrame()
        {
            InitializeComponent();
            random = new Random();
            jobColors = new Dictionary<int, Color>();
            fifoQueue = new Queue<int>();
            jobs = new List<Job>();
            frames = new List<Frame>();

        }

        // Error Trappings
        private bool ValidateInputs()
        {
            if (!int.TryParse(txtbMemorySize.Text, out int memSize) || memSize <= OS_KERNEL_SIZE)
            {
                MessageBox.Show("Please enter an integer (greater than 10MB)");
                return false;
            }

            if (!int.TryParse(txtbFrames.Text, out int frames) || frames <= 0)
            {
                MessageBox.Show("Please enter a valid number of frames");
                return false;
            }

            if (!int.TryParse(txtbJobs.Text, out int jobs) || jobs <= 0)
            {
                MessageBox.Show("Please enter a valid number of jobs");
                return false;
            }

            return true;
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //if (!ValidateInputs())
            //{
            //    MessageBox.Show("Failed!");
            //    return;
            //}
            if (!ValidateInputs()) return;

            // Parse inputs
            memorySize = int.Parse(txtbMemorySize.Text);
            numFrames = int.Parse(txtbFrames.Text);
            numJobs = int.Parse(txtbJobs.Text);

            // Calculate frame size (fixed calculation)
            frameSize = (memorySize - OS_KERNEL_SIZE) / numFrames;

            // Initialize frames
            frames = new List<Frame>();
            for (int i = 0; i < numFrames; i++)
            {
                frames.Add(new Frame
                {
                    FrameNumber = i,
                    IsOccupied = false,
                    JobNumber = -1,
                    PageNumber = -1
                });
            }

            // Initialize jobs
            jobs = new List<Job>();
            for (int i = 0; i < numJobs; i++)
            {
                jobs.Add(new Job
                {
                    JobNumber = i + 1,
                    Size = 0, // Will be set by user
                    Pages = new List<Page>(),
                    ArrivalTime = 0, //Will be set by user
                    PMTLocation = -1, // Will be set when first page is loaded
                });
            }

            SetupJobTable(numJobs);
            btnLoadPages.Visible = true;
            pnlJobTable.Visible = true;
            lblJobTable.Visible = true;
        }

        //Job Table
        private void SetupJobTable(int numJobs)
        {
            dgvJobTable.Rows.Clear();
            dgvJobTable.Columns.Clear();

            // Add columns if they don't exist
            if (dgvJobTable.Columns.Count == 0)
            {
                dgvJobTable.Columns.Add("JobNumber", "Job Number");
                dgvJobTable.Columns.Add("JobSize", "Job Size (MB)");
                dgvJobTable.Columns.Add("ArrivalTime", "Arrival Time (msec)");
                dgvJobTable.Columns.Add("PMTLocation", "PMT Location");
            }

            for (int i = 0; i < numJobs; i++)
            {
                dgvJobTable.Rows.Add(jobs[i].JobNumber, "", "");
            }
        }

        
        private void DrawMemory()
        {
            pnlMemory.Controls.Clear();//Panel Memory Size

            // Calculate total height needed
            int totalHeight = (numFrames + 1) * FRAME_HEIGHT; // +1 for OS Kernel
            pnlMemory.AutoScrollMinSize = new Size(0, totalHeight);


            // Draw OS Kernel
            Label lblKernel = new Label
            {
                Text = "0 MB -                                 OS Kernel (10MB)",
                BackColor = Color.LightGray,
                Size = new Size(350, FRAME_HEIGHT),
                Location = new Point(10, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlMemory.Controls.Add(lblKernel);

            // Draw frames
            for (int i = 0; i < numFrames; i++)
            {
                int yPos= (i + 1) * FRAME_HEIGHT;
                int memoryPosition = OS_KERNEL_SIZE + (i * frameSize);

                // Size label (left side)
                Label lblSize = new Label
                {
                    Text = $"{memoryPosition} MB",
                    Size = new Size(80, FRAME_HEIGHT),
                    Location = new Point(10, yPos),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                pnlMemory.Controls.Add(lblSize);

                // Frame content (middle)
                Frame frame = frames[i];
                if (frame.IsOccupied)
                {
                    Job job = jobs.First(j => j.JobNumber == frame.JobNumber);

                    // Assign random color for this job if not already assigned
                    if (!jobColors.ContainsKey(job.JobNumber))
                    {
                        jobColors[job.JobNumber] = Color.FromArgb(
                            random.Next(150, 255),
                            random.Next(150, 255),
                            random.Next(150, 255));
                    }

                    Label lblContent = new Label
                    {
                        Text = $"Job-{job.JobNumber}, Page-{frame.PageNumber}",
                        BackColor = jobColors[job.JobNumber],
                        Size = new Size(200, FRAME_HEIGHT),
                        Location = new Point(100, yPos), // x,y position
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    pnlMemory.Controls.Add(lblContent);
                }
                else
                {
                    Label lblEmpty = new Label
                    {
                        Text = "Free",
                        BackColor = Color.White,
                        Size = new Size(200, FRAME_HEIGHT),
                        Location = new Point(100, yPos),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    pnlMemory.Controls.Add(lblEmpty);
                }

                // Frame label (right side)
                Label lblFrame = new Label
                {
                    Text = $"Frame {i}",
                    Size = new Size(80, FRAME_HEIGHT),
                    Location = new Point(310, yPos),
                    TextAlign = ContentAlignment.MiddleRight
                };
                pnlMemory.Controls.Add(lblFrame);
            }
        }

        private void DisplayAllPageMapTables()
        {
            pnlPageMapTable.Controls.Clear();

            int yOffset = 0;
            foreach (var job in jobs)
            {
                Label title = new Label
                {
                    Text = $"Job-{job.JobNumber} Page Map Table",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(0, yOffset),
                    AutoSize = true
                };
                pnlPageMapTable.Controls.Add(title);
                yOffset += 20;

                DataGridView dgv = new DataGridView
                {
                    Location = new Point(0, yOffset),
                    Width = 700,
                    Height = 150,
                    BackgroundColor = Color.FromArgb(15, 10, 45),
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                };

                dgv.Columns.Add("PageNumber", "Page Number");
                dgv.Columns.Add("FrameNumber", "Frame Number");
                dgv.Columns.Add("StatusBit", "Status Bit");
                dgv.Columns.Add("ModifiedBit", "Modified Bit");
                dgv.Columns.Add("ReferenceBit", "Reference Bit");
                dgv.Columns.Add("SwappedOutBit", "Swapped Out Bit");
                dgv.Columns.Add("DiskLocation", "Disk Location");

                foreach (Page page in job.Pages)
                {
                    string diskLocation = "";
                    if (page.DiskLocation >= 0)
                    {
                        diskLocation = page.DiskLocation + " MB";
                    }
                    dgv.Rows.Add(
                        page.PageNumber,
                        page.FrameNumber >= 0 ? page.FrameNumber.ToString() : "-",
                        page.StatusBit,
                        page.ModifiedBit,
                        page.ReferenceBit,
                        page.SwappedOutBit,
                        diskLocation
                    );
                }

                pnlPageMapTable.Controls.Add(dgv);
                yOffset += 160; // space after each table
            }
        }

        private void btnLoadPages_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numJobs; i++)
            {
                // Get values from DataGridView
                string sizeStr = dgvJobTable.Rows[i].Cells["JobSize"].Value?.ToString();
                string arrivalStr = dgvJobTable.Rows[i].Cells["ArrivalTime"].Value?.ToString();

                // Parse Job Size
                if (!int.TryParse(sizeStr, out int jobSize) || jobSize <= 0)
                {
                    MessageBox.Show($"Please enter a valid size for Job {i + 1}");
                    return;
                }

                // Parse Arrival Time
                if (!int.TryParse(arrivalStr, out int arrivalTime) || arrivalTime < 0)
                {
                    MessageBox.Show($"Please enter a valid arrival time for Job {i + 1}");
                    return;
                }

                // Create and assign the Job object
                jobs[i] = new Job
                {
                    JobNumber = i + 1,
                    Size = jobSize,
                    Pages = new List<Page>(),
                    ArrivalTime = arrivalTime,
                    PMTLocation = -1 // Default until pages are loaded
                };
            }

            // Sort jobs by ArrivalTime in ascending order
            jobs = jobs.OrderBy(job => job.ArrivalTime).ToList();

            //dgvJobTable
            // Get current job
            Job currentJob = jobs[currentJobIndex];

            // If job size isn't set yet, get it from the table
            if (currentJob.Size == 0)
            {
                if (!int.TryParse(dgvJobTable.Rows[currentJobIndex].Cells["JobSize"].Value?.ToString(), out int jobSize) || jobSize <= 0)
                {
                    MessageBox.Show($"Please enter a valid size for Job {currentJob.JobNumber}");
                    return;
                }
                currentJob.Size = jobSize;

                // Calculate number of pages needed for this job
            }
            int numPages = (int)Math.Ceiling((double)currentJob.Size / frameSize);

            // Initialize pages for this job
            for (int i = 0; i < numPages; i++)
            {
                currentJob.Pages.Add(new Page
                {
                    PageNumber = i,
                    FrameNumber = -1,
                    StatusBit = 0,
                    ModifiedBit = 1,
                    ReferenceBit = 1,
                    DiskLocation = -1
                });
            }

            // Get current page
            Page currentPage = currentJob.Pages[currentPageIndex];

            // Find all empty frames and pick one randomly
            var emptyFrames = frames.Where(f => !f.IsOccupied).ToList();

            if (emptyFrames.Count > 0)
            {
                // Select a random empty frame
                Frame selectedFrame = emptyFrames[random.Next(emptyFrames.Count)];

                // Assign page to frame
                selectedFrame.IsOccupied = true;
                selectedFrame.JobNumber = currentJob.JobNumber;
                selectedFrame.PageNumber = currentPage.PageNumber;
                selectedFrame.TimeLoaded = DateTime.Now;
                fifoQueue.Enqueue(selectedFrame.FrameNumber);

                // Update page info
                currentPage.FrameNumber = selectedFrame.FrameNumber;
                currentPage.StatusBit = 1;
                currentPage.DiskLocation = OS_KERNEL_SIZE + (selectedFrame.FrameNumber * frameSize);


                // Set PMT location if this is the first page of the job
                if (currentJob.PMTLocation == -1)
                {
                    currentJob.PMTLocation = OS_KERNEL_SIZE + (selectedFrame.FrameNumber * frameSize);
                    dgvJobTable.Rows[currentJobIndex].Cells["PMTLocation"].Value = currentJob.PMTLocation + " MB";
                }

                // Update visualization
                DrawMemory();
                DisplayAllPageMapTables();

                // Move to next page
                currentPageIndex++;

                // If all pages loaded for this job, move to next job
                if (currentPageIndex >= currentJob.Pages.Count)
                {
                    currentJobIndex++;
                    currentPageIndex = 0;

                    // If all jobs processed, hide the load button
                    if (currentJobIndex >= jobs.Count)
                    {
                        btnLoadPages.Visible = false; //suggestion: unhide the load button
                        MessageBox.Show("All jobs loaded into memory!");
                        return;
                    }
                }
                // Display
                pnlMemory.Visible = true;
                lblMemory.Visible = true;
                dgvPageMapTable.Visible = true;
            }
            else
            {
                // No empty frames - need to replace
                btnReplacePage.Visible = true;
                btnLoadPages.Visible = false;
                MessageBox.Show("Memory full! Click 'Replace Page' to continue.");
                

            }

        }

        private void btnReplacePage_Click(object sender, EventArgs e)
        {
            if (fifoQueue.Count == 0)
            {
                MessageBox.Show("No pages to replace!");
                return;
            }

            // Get the frame to replace using FIFO
            int frameNumberToReplace = fifoQueue.Dequeue();
            Frame frameToReplace = frames.First(f => f.FrameNumber == frameNumberToReplace);

            // Get the job and page that currently occupies this frame
            Job oldJob = jobs.First(j => j.JobNumber == frameToReplace.JobNumber);
            Page oldPage = oldJob.Pages.First(p => p.PageNumber == frameToReplace.PageNumber);

            // Mark the old page as swapped out
            oldPage.StatusBit = 0;
            oldPage.SwappedOutBit = 1;
            oldPage.FrameNumber = -1;

            // Get current job and page to load
            Job currentJob = jobs[currentJobIndex];
            Page currentPage = currentJob.Pages[currentPageIndex];

            // Assign the new page to the frame
            frameToReplace.JobNumber = currentJob.JobNumber;
            frameToReplace.PageNumber = currentPage.PageNumber;
            frameToReplace.TimeLoaded = DateTime.Now;
            fifoQueue.Enqueue(frameToReplace.FrameNumber);

            // Update page info
            currentPage.FrameNumber = frameToReplace.FrameNumber;
            currentPage.StatusBit = 1;
            currentPage.SwappedOutBit = 0;
            currentPage.DiskLocation = OS_KERNEL_SIZE + (frameToReplace.FrameNumber * frameSize);

            // Set PMT location if this is the first page of the job
            if (currentJob.PMTLocation == -1)
            {
                currentJob.PMTLocation = OS_KERNEL_SIZE + (frameToReplace.FrameNumber * frameSize);
                dgvJobTable.Rows[currentJobIndex].Cells["PMTLocation"].Value = currentJob.PMTLocation + " MB";
            }

            // Update visualization
            DrawMemory();
            DisplayAllPageMapTables();

            // Move to next page
            currentPageIndex++;

            // If all pages loaded for this job, move to next job
            if (currentPageIndex >= currentJob.Pages.Count)
            {
                currentJobIndex++;
                currentPageIndex = 0;
            }

            // If all jobs processed, hide the buttons
            if (currentJobIndex >= jobs.Count)
            {
                btnReplacePage.Visible = false;
                btnLoadPages.Visible = false;
                MessageBox.Show("All jobs loaded into memory!");
            }
            else
            {
                btnReplacePage.Visible = false;
                btnLoadPages.Visible = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtbMemorySize.Clear();
            txtbFrames.Clear();
            txtbJobs.Clear();

            // Clear DataGridViews
            dgvJobTable.Rows.Clear();
            dgvJobTable.Columns.Clear();
            dgvPageMapTable.Rows.Clear();
            dgvPageMapTable.Columns.Clear();

            // Clear custom panel with multiple PMTs if used
            pnlPageMapTable.Controls.Clear();

            // Clear memory visualization
            pnlMemory.Controls.Clear();

            // Reset all internal variables
            memorySize = 0;
            frameSize = 0;
            numFrames = 0;
            currentJobIndex = 0;
            currentPageIndex = 0;
            jobs.Clear();
            frames.Clear();
            fifoQueue.Clear();
            jobColors.Clear();

            // Hide buttons
            btnLoadPages.Visible = false;
            btnReplacePage.Visible = false;

            // Hide Panels/Labels
            pnlJobTable.Visible = false;
            pnlMemory.Visible = false;
            lblJobTable.Visible = false;
            lblMemory.Visible = false;
        }
    }
}

