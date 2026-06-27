using System.Windows.Forms;

namespace Zombie_Shooter
{
    public partial class Starter : Form
    {
        public Starter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();

            using Form1 gameForm = new Form1();
            gameForm.ShowDialog(this);

            Close();
        }
    }
}
