namespace Zombie_Shooter
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        Random randNum = new Random();
        List<PictureBox> zombieslist = new List<PictureBox>();
        int score;


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimeEvent(object sender, EventArgs e)
        {
            if (gameOver)
            {
                return;
            }

            if (playerHealth > 0)
            {
                healthBar.Value = Math.Max(0, Math.Min(100, playerHealth));
            }
            else
            {
                EndGame();
                return;
            }


            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Kills: " + score;


            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }


            if (goRight == true && player.Right < this.ClientSize.Width)
            {
                player.Left += speed;
            }


            if (goUp == true && player.Top > 45)
            {
                player.Top -= speed;
            }


            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }

            foreach (Control x in this.Controls.Cast<Control>().ToList())
            {
                if (x is PictureBox && (string?)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }

                }

                if (x is PictureBox && (string?)x.Tag == "zombie")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    if (x.Left > player.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    if (x.Left < player.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }

                    if (x.Top < player.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }

                    if (x.Top > player.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                }

                foreach (Control j in this.Controls.Cast<Control>().ToList())
                {
                    if (j is PictureBox && (string?)j.Tag == "bullet" && x is PictureBox && (string?)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombieslist.Remove(((PictureBox)x));
                            MakeZombies();
                            break;
                        }

                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (gameOver == true)
            {
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true; // change go left to true
                facing = "left"; //change facing to left
                player.Image = Properties.Resources.left; // change the player image to LEFT image
            }
            // end of left key selection
            // if the right key is pressed then do the following
            if (e.KeyCode == Keys.Right)
            {
                goRight = true; // change go right to true
                facing = "right"; // change facing to right
                player.Image = Properties.Resources.right; // change the player image to right
            }
            // end of right key selection
            // if the up key is pressed then do the following
            if (e.KeyCode == Keys.Up)
            {
                facing = "up"; // change facing to up
                goUp = true; // change go up to true
                player.Image = Properties.Resources.up; // change the player image to up
            }
            // end of up key selection
            // if the down key is pressed then do the following
            if (e.KeyCode == Keys.Down)
            {
                facing = "down"; // change facing to down
                goDown = true; // change go down to true
                player.Image = Properties.Resources.down; //change the player image to down
            }
            // end of the down key selection
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;

            }

            if (e.KeyCode == Keys.Up)
            {
                facing = "up";
                goUp = false;
                player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                facing = "down";
                goDown = false;
                player.Image = Properties.Resources.down;
            }

            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                ShootBullet(facing);


                if (ammo < 1)
                {
                    DropAmmo();
                }



            }

            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }

        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + (player.Width / 2);
            shootBullet.bulletTop = player.Top + (player.Height / 2);
            shootBullet.MakeBullet(this);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombie.Left = randNum.Next(0, Math.Max(1, this.ClientSize.Width - zombie.Width));
            zombie.Top = randNum.Next(45, Math.Max(46, this.ClientSize.Height - zombie.Height));
            zombieslist.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront();
        }
        private void RestartGame()
        {
            GameTimer.Stop();
            player.Image = Properties.Resources.up;

            foreach (Control control in this.Controls.Cast<Control>().ToList())
            {
                if (control is PictureBox pictureBox && ((string?)pictureBox.Tag == "zombie" || (string?)pictureBox.Tag == "ammo" || (string?)pictureBox.Tag == "bullet"))
                {
                    this.Controls.Remove(pictureBox);
                    pictureBox.Dispose();
                }
            }

            foreach (PictureBox i in zombieslist)
            {
                this.Controls.Remove(i);
            }
            zombieslist.Clear();

            for (int i = 0; i < 3; i++)
            {
                MakeZombies();
            }

            goUp = false;
            goLeft = false;
            goDown = false;
            goRight = false;
            gameOver = false;
            playerHealth = 100;
            healthBar.Value = playerHealth;
            score = 0;
            ammo = 10;
            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Kills: " + score;

            GameTimer.Start();
        }
        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, Math.Max(11, this.ClientSize.Width - ammo.Width));
            ammo.Top = randNum.Next(60, Math.Max(61, this.ClientSize.Height - ammo.Height));
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        private void EndGame()
        {
            gameOver = true;
            GameTimer.Stop();
            player.Image = Properties.Resources.dead;

            using end_game endForm = new end_game(score);
            if (endForm.ShowDialog(this) == DialogResult.OK)
            {
                RestartGame();
            }
            else
            {
                Close();
            }
        }
    }
}
