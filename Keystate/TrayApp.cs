namespace Keystate
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class TrayApp : Form
    {
        private static class IconProperties
        {
            public static Font Font = new Font("Verdana", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            public static Brush Brush = new SolidBrush(Color.LightGray);
            public static Point Position = new Point(0, -2);

            public static Bitmap CapsLockBitmap = new Bitmap(16, 16);
            public static Graphics CapsLockGraphics = Graphics.FromImage(CapsLockBitmap);
            public static string CapsLockTextOn = "A";
            public static string CapsLockTextOff = "a";
        };

        public static TrayApp Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TrayApp();
                }

                return instance;
            }
        }

        private static TrayApp instance;

        private NotifyIcon capsTreyIcon;

        protected TrayApp() : base()
        {
            capsTreyIcon = new NotifyIcon()
            {
                Visible = true,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Close", (object sender, EventArgs args) => Close()) }
                )
            };
        }

        public void UpdateCapsLockState(bool isLocked)
        {
            IconProperties.CapsLockGraphics.Clear(Color.Transparent);
            IconProperties.CapsLockGraphics.DrawString((isLocked) ? IconProperties.CapsLockTextOn : IconProperties.CapsLockTextOff,
                IconProperties.Font, IconProperties.Brush, IconProperties.Position);

            capsTreyIcon.Icon = Icon.FromHandle(IconProperties.CapsLockBitmap.GetHicon());
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            Visible = false;
            capsTreyIcon.Visible = false;
            capsTreyIcon.Icon.Dispose();
            capsTreyIcon.Dispose();

            base.OnClosed(e);
        }
    }
}
