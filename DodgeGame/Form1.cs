using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgeGame
{
    public partial class Form1 : Form
    {
        Rectangle player = new Rectangle(10, 293, 15, 15);
        List<Rectangle> downRectangles = new List<Rectangle>();
        List<Rectangle> upRectangles = new List<Rectangle>();

        int playerSpeed = 10;
        int downRectangleSpeed = 5;
        int upRectangleSpeed = -5;
        int upRectangleCounter = 0;
        int downRectangleCounter = 0;

        bool rightDown = false;
        bool leftDown = false;
        bool upDown = false;
        bool downDown = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);


        public Form1()
        {
            InitializeComponent();
            Rectangle leftRectangles = new Rectangle(190, 0, 20, 60);
            downRectangles.Add(leftRectangles);
            Rectangle rightRectangles = new Rectangle(490, 540, 20, 60);
            upRectangles.Add(rightRectangles);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Move Player
            if (leftDown == true && player.X > 0)
            {
                player.X -= playerSpeed;
            }
            
            if (rightDown == true && player.X < this.Width - player.Width)
            {
                player.X += playerSpeed;
            }
            
            if (downDown == true && player.Y < this.Height - player.Height)
            {
                player.Y += playerSpeed;
            }

            if (upDown == true && player.Y > 0)
            {
                player.Y -= playerSpeed;
            }

            //Moving Rectangles
            for (int i = 0; i < downRectangles.Count(); i++)
            {
                int y = downRectangles[i].Y + downRectangleSpeed;
                downRectangles[i] = new Rectangle(downRectangles[i].X, y, 20, 60);
            }

            for (int i = 0; i < upRectangles.Count(); i++)
            {
                int y = upRectangles[i].Y + upRectangleSpeed;
                upRectangles[i] = new Rectangle(upRectangles[i].X, y, 20, 60);
            }

            //Adding Rectangles according to counter
            downRectangleCounter++;

            for (int i = 0; i < downRectangles.Count(); i++)
            {
                if (downRectangleCounter == 22)
                {
                    downRectangles.Add(new Rectangle(190, 0, 20, 60));
                    downRectangleCounter = 0;
                }
            }

            upRectangleCounter++;

            for (int i = 0; i < upRectangles.Count(); i++)
            {
                if (upRectangleCounter == 22)
                {
                    upRectangles.Add(new Rectangle(490, 540, 20, 60));
                    upRectangleCounter = 0;
                }
            }

            //Check to see if player touches rectangles or edge of form and stop game
            for (int i = 0; i < downRectangles.Count(); i++)
            {
                if (player.IntersectsWith(downRectangles[i]))
                {
                    gameTimer.Enabled = false;
                    outputLabel.Text = "You Lose!!";
                }
            }

            for (int i = 0; i < upRectangles.Count(); i++)
            { 
                if (player.IntersectsWith(upRectangles[i]))
                {
                    gameTimer.Enabled = false;
                    outputLabel.Text = "You Lose!!";
                }
            }

            if (player.X > this.Width - player.Width)            
            {
                gameTimer.Enabled = false;
                outputLabel.Text = "You Win!!";
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, player);
            for (int i = 0; i < downRectangles.Count(); i++)
            {
                e.Graphics.FillRectangle(blackBrush, downRectangles[i]);
                e.Graphics.FillRectangle(blackBrush, upRectangles[i]);
            }
             

        }
    }
}
