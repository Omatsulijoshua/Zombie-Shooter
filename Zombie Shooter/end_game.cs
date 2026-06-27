using System.Windows.Forms;

namespace Zombie_Shooter
{
    public partial class end_game : Form
    {
        public end_game()
        {
            InitializeComponent();
        }

        public end_game(int kills)
        {
            InitializeComponent();
            label3.Text = kills.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void end_game_Load(object sender, EventArgs e)
        {
        }
    }
}
