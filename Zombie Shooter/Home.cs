using System.Drawing;
using System.Windows.Forms;
using Zombie_Shooter;

namespace Quiz_App
{
    public partial class Home : Form
    {
        private const int BaseWidth = 1920;
        private const int BaseHeight = 1080;

        public Home()
        {
            InitializeComponent();
        }

        public static void ScaleForm(Form form)
        {
            Rectangle workingArea = Screen.PrimaryScreen?.WorkingArea ?? Screen.GetWorkingArea(form);
            float scaleX = (float)workingArea.Width / BaseWidth;
            float scaleY = (float)workingArea.Height / BaseHeight;

            form.Scale(new SizeF(scaleX, scaleY));

            foreach (Control c in form.Controls)
            {
                c.Font = new Font(c.Font.FontFamily, c.Font.Size * Math.Min(scaleX, scaleY));
            }

            form.StartPosition = FormStartPosition.CenterScreen;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Width += 3;
            if (panel1.Width >= 850)
            {
                timer1.Stop();
                Hide();

                using Starter startForm = new Starter();
                startForm.ShowDialog(this);

                Close();
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ScaleForm(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
