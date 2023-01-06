using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Pac_Man_Game
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight, isGameOver;

        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, BlueGhostX, BlueGhostY;


        public Form1()
        {
            InitializeComponent();

            ResetGame();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void Key_Down(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                ResetGame();
            }

        }

        private void mainGameTimer(object sender, EventArgs e)
        {

            Score.Text = "Score: " + score;

            if (goLeft == true)
            {
                PacMan.Left -= playerSpeed;
                PacMan.Image = Properties.Resources.left;
            }
            if (goRight == true)
            {
                PacMan.Left += playerSpeed;
                PacMan.Image = Properties.Resources.right;
            }
            if (goDown == true)
            {
                PacMan.Top += playerSpeed;
                PacMan.Image = Properties.Resources.down;
            }
            if (goUp == true)
            {
                PacMan.Top -= playerSpeed;
                PacMan.Image = Properties.Resources.Up;
            }
            // Place of PacMan if it reaches in any border
            if (PacMan.Left < -10)
            {
                PacMan.Left = 600;
            }
            if (PacMan.Left > 600)
            {
                PacMan.Left = -10;
            }

            if (PacMan.Top < -10)
            {
                PacMan.Top = 500;
            }
            if (PacMan.Top > 500)
            {
                PacMan.Top = 0;
            }
            // Intersections between PacMan and anything
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "Coin" && x.Visible == true)
                    {
                        if (PacMan.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "Wall")
                    {
                        if (PacMan.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("Ohhhh  You Lose!");
                        }


                        if (Blue.Bounds.IntersectsWith(x.Bounds))
                        {
                            BlueGhostX = -BlueGhostX;
                        }
                    }


                    if ((string)x.Tag == "ghost")
                    {
                        if (PacMan.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("Ohhhh  You Lose!");
                        }

                    }
                }
            }

             // to move the red and orange ghost

            Red.Left += redGhostSpeed;
            if (Red.Bounds.IntersectsWith(WallOne.Bounds)||  Red.Bounds.IntersectsWith(WallTwo.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }

            Orange.Left -= yellowGhostSpeed;

            if (Orange.Bounds.IntersectsWith(WallFour.Bounds) || Orange.Bounds.IntersectsWith(WallThree.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }


            Blue.Left -= BlueGhostX;
            Blue.Top -= BlueGhostY;

            //to move Blue ghost

            if (Blue.Top < 0 ||   Blue.Top > 500)
            {
                BlueGhostY = -BlueGhostY;
            }

            if (Blue.Left < 0 || Blue.Left > 600)
            {
                BlueGhostX = -BlueGhostX;
            }


            if (score == 34)
            {
                GameOver("You Win!");
            }


        }

        private void ResetGame()
        {

            Score.Text = "Score: 0";
            score = 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            BlueGhostX = 5;
            BlueGhostY = 5;
            playerSpeed = 10;

            isGameOver = false;

            // Location of the ghosts

            PacMan.Left = 12;
            PacMan.Top = 35;

            Red.Left = 156;
            Red.Top = 80;

            Orange.Left = 370;
            Orange.Top = 363;

            Blue.Left = 511;
            Blue.Top = 140;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }


            GameTimer.Start();
        }

        private void GameOver(string Message)
        {

            isGameOver = true;

            GameTimer.Stop();
            MessageBox.Show(Message);
        }

    }
}