using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Shooter
{
    class Bullet
    {
        public string direction = "up";
        public int bulletLeft;
        public int bulletTop;

        private const int Speed = 10;
        private readonly PictureBox bullet = new PictureBox();
        private readonly System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();
        private Form? gameArea;
        private bool disposed;

        public void MakeBullet(Form form)
        {
            gameArea = form;
            bullet.BackColor = Color.Yellow;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;

            form.Controls.Add(bullet);
            bullet.BringToFront();
            form.FormClosed += (_, _) => DisposeBullet();

            bulletTimer.Interval = Speed;
            bulletTimer.Tick += BulletTimerEvent;
            bulletTimer.Start();
        }

        public void BulletTimerEvent(object? sender, EventArgs e)
        {
            if (bullet.IsDisposed)
            {
                DisposeBullet();
                return;
            }

            if (direction == "left")
            {
                bullet.Left -= Speed;
            }
            if (direction == "right")
            {
                bullet.Left += Speed;
            }
            if (direction == "up")
            {
                bullet.Top -= Speed;
            }
            if (direction == "down")
            {
                bullet.Top += Speed;
            }

            if (gameArea == null || bullet.Left < 0 || bullet.Left > gameArea.ClientSize.Width || bullet.Top < 0 || bullet.Top > gameArea.ClientSize.Height)
            {
                DisposeBullet();
            }
        }

        private void DisposeBullet()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            gameArea?.Controls.Remove(bullet);
            bulletTimer.Stop();
            bulletTimer.Dispose();

            if (!bullet.IsDisposed)
            {
                bullet.Dispose();
            }
        }
    }
}
