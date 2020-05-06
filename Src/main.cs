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
        private double StepTime;

        private BufferedGraphicsContext Context;
        public BufferedGraphics Grafx;
        private Size WinSize = new Size( 400, 300);

        InputControl InControl;

        Circle[] Circles;
        
        public Program() : base()
        {
            this.Text = "Moving Circle";
            this.Size = WinSize;
            this.Resize += new EventHandler(this.OnResize);
            
            this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true);

            InControl = new InputControl(this);

            StepTimer = new System.Timers.Timer(100);
            StepTimer.Elapsed += GameStep;
            StepTimer.AutoReset = true;
            StepTimer.Enabled = true;

            Context = BufferedGraphicsManager.Current;
            Context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            Grafx = Context.Allocate(this.CreateGraphics(), new Rectangle( 0, 0, this.Width, this.Height));

            this.BuildCircles( 20 );
        }

        public void BuildCircles(int amount)
        {
            Random rng = new Random();

            Circles = new Circle[amount];

            for (int ii = 0; ii < Circles.Length; ii++)
                Circles[ii] = new Circle( rng.Next( 0, this.Width), rng.Next( 0, this.Height), 20);
        }

        private void GameStep(object o, ElapsedEventArgs e)
        {
            StepStart = DateTime.Now;

            foreach (Circle C in Circles) C.Step(this);

            RenderFrame();

            StepEnd = DateTime.Now;

            StepTime = StepEnd.Subtract(StepStart).TotalMilliseconds;
            StepTimer.Interval = StepTime;
        }

        public void RenderFrame()
        {
            Grafx.Graphics.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);

            foreach (Circle C in Circles) C.Draw(Grafx.Graphics);

            Grafx.Render(Graphics.FromHwnd(this.Handle));
        }

        protected override void OnPaint( PaintEventArgs e) 
        {
            Grafx.Render(e.Graphics);
        }

        private void OnResize(object o, EventArgs e)
        {
            this.Size = WinSize;
        }

        public static void Main() 
        {
            Application.Run(new Program());
        }
    }
}