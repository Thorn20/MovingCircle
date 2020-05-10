using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace MovingCircle
{
    public class Program: Form  
    {
        private System.Timers.Timer StepTimer;
        private DateTime StepStart, StepEnd;
        public double StepTime;
        public double StepsPerSec = 0.0;
        public double StepLimit = 100;

        private Size WinSize = new Size( 800, 600);

        public GUI Gui;
        public Gfx gfx;
        
        public Program() : base()
        {
            this.Text = "Moving Circle";
            this.Size = WinSize;
            this.Resize += new EventHandler(this.OnResize);
            this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true);

            Gui = new GUI(this);
            gfx = new Gfx(this);

            StepTimer = new System.Timers.Timer(1000 / StepLimit);
            StepTimer.Elapsed += GameStep;
            StepTimer.AutoReset = true;
            StepTimer.Enabled = true;
        }

        private void GameStep(object o, ElapsedEventArgs e)
        {
            StepTimer.Enabled = false;
            StepStart = DateTime.Now;

            Gui.Step();

            StepEnd = DateTime.Now;
            StepTime = StepEnd.Subtract(StepStart).TotalMilliseconds;

            if (StepTime < (1000 / StepLimit)) 
            {
                StepTimer.Interval = (1000 / StepLimit) - StepTime; 
                StepTime = (1000 / StepLimit);
            }
            else
                StepTimer.Interval = StepTime; 

            StepsPerSec = (1000 / StepTime);                       
            StepTimer.Enabled = true;          
        }

        protected override void OnPaint( PaintEventArgs e) 
        {
            gfx.Buffer.Render(e.Graphics);
        }

        private void OnResize(object o, EventArgs e)
        {
            this.Size = WinSize;
            gfx.InitGraphics();
        }

        public static void Main() 
        {
            Application.Run(new Program());
        }
    }
}