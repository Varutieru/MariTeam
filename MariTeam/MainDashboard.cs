using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MariTeam
{
    [DesignerCategory("")]
    public partial class MainDashboard : Form
    {
        public MainDashboard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // FORM SETTINGS
            this.Text = "Mariteam - Main Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(30, 60, 70);

            // Get screen dimensions
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // BACKGROUND PANEL
            Panel backgroundPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(screenWidth, screenHeight),
                BackColor = Color.FromArgb(30, 60, 70)
            };
            backgroundPanel.BackgroundImage = Properties.Resources.bgDashboard;
            backgroundPanel.BackgroundImageLayout = ImageLayout.Stretch;

            // ===== HEADER PANEL =====
            Panel headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(screenWidth, 70),
                BackColor = Color.White
            };

            // Header Logo
            PictureBox headerLogo = new PictureBox
            {
                Location = new Point(20, 10),
                Size = new Size(250, 50),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            headerLogo.Image = Properties.Resources.logoHeader;

            // Notification Icon
            Label notificationIcon = new Label
            {
                Text = "🔔",
                Font = new Font("Segoe UI", 20),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(screenWidth - 140, 20),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };

            // User Info Panel
            Panel userInfoPanel = new Panel
            {
                Location = new Point(screenWidth - 400, 15),
                Size = new Size(240, 40),
                BackColor = Color.Transparent
            };

            Label userNameLabel = new Label
            {
                Text = EntryPage.HarborName,
                Font = new Font("Century Gothic", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(0, 0),
                Size = new Size(220, 20),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };

            Label userLocationLabel = new Label
            {
                Text = EntryPage.HarborLocation,
                Font = new Font("Century Gothic", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(0, 22),
                Size = new Size(220, 18),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };

            userInfoPanel.Controls.Add(userNameLabel);
            userInfoPanel.Controls.Add(userLocationLabel);

            headerPanel.Controls.Add(headerLogo);
            headerPanel.Controls.Add(userInfoPanel);
            headerPanel.Controls.Add(notificationIcon);

            // ===== LEFT SIDE PANEL =====
            Panel leftPanel = new Panel
            {
                Location = new Point(0, 70),
                Size = new Size(240, screenHeight - 70),
                BackColor = Color.FromArgb(100, 14, 57, 93)
            };

            // Navigation Buttons
            string[] navItems = { "Main Dashboard", "Ship Status", "Dock Dwelling Status", "Activity Log", "Security" };
            int[] navY = { 100, 140, 180, 220, 260 };

            for (int i = 0; i < navItems.Length; i++)
            {
                Button navButton = CreateNavButton(navItems[i], navY[i], i == 0);
                leftPanel.Controls.Add(navButton);
            }

            // Exit Software Button
            Button exitButton = new Button
            {
                Text = "Exit Software",
                Font = new Font("Century Gothic", 11, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, screenHeight - 130),
                Size = new Size(200, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft
            };
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += (s, e) => Application.Exit();
            leftPanel.Controls.Add(exitButton);

            // ===== MAIN CONTENT AREA =====
            Panel contentPanel = new Panel
            {
                Location = new Point(240, 70),
                Size = new Size(screenWidth - 240, screenHeight - 70),
                BackColor = Color.Transparent
            };

            Label dashboardTitle = new Label
            {
                Text = "Main Dashboard",
                Font = new Font("Century Gothic", 32, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(65, 40),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            int contentWidth = screenWidth - 240;
            int contentHeight = screenHeight - 70;

            int panelWidth1 = 470;
            int panelWidth2 = 650;
            int panelHeight = 310;
            int gap = 20;

            int totalPanelsWidth = panelWidth1 + gap + panelWidth2;
            int totalPanelsHeight = panelHeight + gap + panelHeight;

            int startX = (contentWidth - totalPanelsWidth) / 2;
            int startY = ((contentHeight - totalPanelsHeight) / 2) + 40;

            // ===== GLASSMORPHISM PANELS =====
            Panel shipStatusPanel = CreateGlassPanel("Ship Status", startX, startY, panelWidth1, panelHeight);
            Panel dockStatusPanel = CreateGlassPanel("Dock Dwelling Status", startX + panelWidth1 + gap, startY, panelWidth2, panelHeight);
            Panel activityLogPanel = CreateGlassPanel("Activity Log", startX, startY + panelHeight + gap, panelWidth1, panelHeight);
            Panel securityPanel = CreateGlassPanel("Security", startX + panelWidth1 + gap, startY + panelHeight + gap, panelWidth2, panelHeight);

            contentPanel.Controls.Add(dashboardTitle);
            contentPanel.Controls.Add(shipStatusPanel);
            contentPanel.Controls.Add(dockStatusPanel);
            contentPanel.Controls.Add(activityLogPanel);
            contentPanel.Controls.Add(securityPanel);

            // ADD ALL TO FORM - Background first, then everything on top
            this.Controls.Add(backgroundPanel);
            backgroundPanel.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(leftPanel);

            // BRING TO FRONT
            headerPanel.BringToFront();
            leftPanel.BringToFront();
        }

        private Button CreateNavButton(string text, int y, bool isActive)
        {
            Button btn = new Button
            {
                Text = text,
                Font = new Font("Century Gothic", 11, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = isActive ? Color.FromArgb(50, 255, 255, 255) : Color.Transparent,
                Location = new Point(20, y),
                Size = new Size(200, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(50, 255, 255, 255);
            btn.MouseLeave += (s, e) => btn.BackColor = isActive ? Color.FromArgb(50, 255, 255, 255) : Color.Transparent;

            return btn;
        }

        private Panel CreateGlassPanel(string title, int x, int y, int width, int height)
        {
            Panel glassPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.Transparent
            };

            glassPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(15, 255, 255, 255)))
                {
                    e.Graphics.FillRectangle(brush, 0, 50, glassPanel.Width, glassPanel.Height - 50);
                }
                using (Pen pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 50, glassPanel.Width - 1, glassPanel.Height - 51);
                }
            };

            Panel headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(width, 50),
                BackColor = Color.FromArgb(0, 179, 170)
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Century Gothic", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 12),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            LinkLabel viewDetailsLink = new LinkLabel
            {
                Text = "view details",
                Font = new Font("Century Gothic", 10),
                LinkColor = Color.White,
                ActiveLinkColor = Color.White,
                VisitedLinkColor = Color.White,
                Location = new Point(width - 100, 17),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            viewDetailsLink.LinkBehavior = LinkBehavior.HoverUnderline;

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(viewDetailsLink);

            Panel contentContainer = new Panel
            {
                Location = new Point(0, 50),
                Size = new Size(width, height - 50),
                BackColor = Color.Transparent,
                AutoScroll = true
            };

            glassPanel.Controls.Add(headerPanel);
            glassPanel.Controls.Add(contentContainer);

            return glassPanel;
        }
    }
}