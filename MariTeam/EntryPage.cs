using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace MariTeam
{
    [DesignerCategory("")]
    public partial class EntryPage : Form
    {
        public static string HarborName { get; set; }
        public static string HarborLocation { get; set; }

        public EntryPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // FORM SETTINGS
            this.Text = "Mariteam - Automated Harbour Logistic Software";
            this.Size = new Size(1920, 1080);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 60, 70);

            // BG
            Panel backgroundPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1920, 1080),
                BackColor = Color.FromArgb(30, 60, 70)
            };

            backgroundPanel.BackgroundImage = Properties.Resources.bgEntry;

            backgroundPanel.BackgroundImageLayout = ImageLayout.Stretch;

            // WELCOME TEXT
            Label welcomeLabel = new Label
            {
                Text = "Hello! Welcome to",
                Font = new Font("Century Gothic", 28, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(820, 320),
                BackColor = Color.Transparent
            };

            // LOGO
            PictureBox logoPictureBox = new PictureBox
            {
                Location = new Point(680, 340),
                Size = new Size(600, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            logoPictureBox.Image = Properties.Resources.logo;

            backgroundPanel.Controls.Add(welcomeLabel);
            backgroundPanel.Controls.Add(logoPictureBox);

            // LEFT PANEL - FORM
            Panel formPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(400, 1080),
                BackColor = Color.White
            };

            Label harborNameLabel = new Label
            {
                Text = "Harbor Name",
                Font = new Font("Century Gothic", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(40, 290),
                AutoSize = true
            };

            TextBox harborNameTextBox = new TextBox
            {
                Name = "txtHarborName",
                Font = new Font("Century Gothic", 12),
                Location = new Point(40, 325),
                Size = new Size(300, 30),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label locationLabel = new Label
            {
                Text = "Location",
                Font = new Font("Century Gothic", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(40, 390),
                AutoSize = true
            };

            TextBox locationTextBox = new TextBox
            {
                Name = "txtLocation",
                Font = new Font("Century Gothic", 12),
                Location = new Point(40, 425),
                Size = new Size(300, 30),
                BorderStyle = BorderStyle.FixedSingle
            };

            Button saveButton = new Button
            {
                Text = "Save Changes",
                Font = new Font("Century Gothic", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(115, 495),
                Size = new Size(150, 45),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            saveButton.FlatAppearance.MouseDownBackColor = Color.Transparent;

            // GRADIENT BUTTON PATH
            saveButton.Paint += (s, e) =>
            {
                Button btn = (Button)s;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int radius = 22;
                System.Drawing.Drawing2D.GraphicsPath buttonPath = new System.Drawing.Drawing2D.GraphicsPath();
                buttonPath.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                buttonPath.AddArc(btn.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
                buttonPath.AddArc(btn.Width - radius * 2, btn.Height - radius * 2, radius * 2, radius * 2, 0, 90);
                buttonPath.AddArc(0, btn.Height - radius * 2, radius * 2, radius * 2, 90, 90);
                buttonPath.CloseFigure();

                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    btn.ClientRectangle,
                    Color.FromArgb(0, 179, 170),
                    Color.FromArgb(0, 219, 161),
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                {
                    e.Graphics.FillPath(brush, buttonPath);
                }

                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font,
                    btn.ClientRectangle, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            saveButton.Click += (s, e) => SaveButton_Click(harborNameTextBox, locationTextBox);

            // HOVER
            saveButton.MouseEnter += (s, e) => saveButton.Invalidate();
            saveButton.MouseLeave += (s, e) => saveButton.Invalidate();

            // FORM CONTROL
            formPanel.Controls.Add(harborNameLabel);
            formPanel.Controls.Add(harborNameTextBox);
            formPanel.Controls.Add(locationLabel);
            formPanel.Controls.Add(locationTextBox);
            formPanel.Controls.Add(saveButton);

            this.Controls.Add(backgroundPanel);
            this.Controls.Add(formPanel);

            // PANEL Z-INDEX
            formPanel.BringToFront();
        }

        private void SaveButton_Click(TextBox harborNameTextBox, TextBox locationTextBox)
        {
            // VALIDATION
            if (string.IsNullOrWhiteSpace(harborNameTextBox.Text))
            {
                MessageBox.Show("Please enter a harbor name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                harborNameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                MessageBox.Show("Please enter a location.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                locationTextBox.Focus();
                return;
            }

            // STORE VALUES
            HarborName = harborNameTextBox.Text.Trim();
            HarborLocation = locationTextBox.Text.Trim();

            // TO MAIN DASHBOARD PAGE
            MainDashboard dashboard = new MainDashboard();
            dashboard.Show();
            this.Hide();
        }
    }
}