using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace MovingCircle
{
    public class Program: Form  
    {
        private System.Timers.Timer StepTimer;
        int CircleX = 20, CircleY = 20, CircleSize = 20, CircleSpeed = 5;
        bool UpKey = false, DownKey = false, LeftKey = false, RightKey = false;

        public static void Main() 
        {
            Application.Run(new Program());
        }

        public Program()
        {
            this.Paint += new PaintEventHandler(RenderScreen);
            this.Text = "Moving Circle";

            this.KeyDown += new KeyEventHandler(keypressed);
            this.KeyUp += new KeyEventHandler(keyreleased);

            this.initTimer();
        }

        private void initTimer()
        {
            StepTimer = new System.Timers.Timer(60);
            StepTimer.Elapsed += GameStep;
            StepTimer.AutoReset = true;
            StepTimer.Enabled = true;
        }

        private void GameStep(object o, ElapsedEventArgs e)
        {
            if (UpKey) CircleY -= CircleSpeed;
            if (DownKey) CircleY += CircleSpeed;
            if (LeftKey) CircleX -= CircleSpeed;
            if (RightKey) CircleX += CircleSpeed;

            this.Refresh();
        }

        private void RenderScreen(object o, PaintEventArgs e) 
        {
            Graphics g = e.Graphics; 

            g.FillEllipse( new SolidBrush(Color.Black), CircleX - (CircleSize/2), CircleY - (CircleSize/2), CircleSize, CircleSize);
        }

        private void keypressed(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) UpKey = true;
            if (e.KeyCode == Keys.Down) DownKey = true;
            if (e.KeyCode == Keys.Left) LeftKey = true;
            if (e.KeyCode == Keys.Right) RightKey = true;

            e.Handled = true;
        }   

        private void keyreleased(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) UpKey = false;
            if (e.KeyCode == Keys.Down) DownKey = false;
            if (e.KeyCode == Keys.Left) LeftKey = false;
            if (e.KeyCode == Keys.Right) RightKey = false;

            e.Handled = true;
        }
    }
}