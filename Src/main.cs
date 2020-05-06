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
        private BufferedGraphics Grafx;
        private Size WinSize = new Size( 400, 300);

        PlayerControl PlayCont;

        int CircleX = 20, CircleY = 20, CircleSize = 20, CircleSpeed = 2;
        
        public Program() : base()
        {
            this.Text = "Moving Circle";
            this.Size = WinSize;
            this.Resize += new EventHandler(this.OnResize);
            
            this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true);

            PlayCont = new PlayerControl(this);

            StepTimer = new System.Timers.Timer(100);
            StepTimer.Elapsed += GameStep;
            StepTimer.AutoReset = true;
            StepTimer.Enabled = true;

            Context = BufferedGraphicsManager.Current;
            Context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            Grafx = Context.Allocate(this.CreateGraphics(), new Rectangle( 0, 0, this.Width, this.Height));
        }

        private void GameStep(object o, ElapsedEventArgs e)
        {
            StepStart = DateTime.Now;

            if (PlayCont.UpKey) CircleY -= CircleSpeed;
            if (PlayCont.DownKey) CircleY += CircleSpeed;
            if (PlayCont.LeftKey) CircleX -= CircleSpeed;
            if (PlayCont.RightKey) CircleX += CircleSpeed;

            RenderFrame();

            StepEnd = DateTime.Now;

            StepTime = StepEnd.Subtract(StepStart).TotalMilliseconds;
            StepTimer.Interval = StepTime;
        }

        public void RenderFrame()
        {
            Grafx.Graphics.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);

            Grafx.Graphics.FillEllipse( new SolidBrush(Color.Red), 
                                         CircleX - (CircleSize/2), 
                                         CircleY - (CircleSize/2), 
                                         CircleSize, 
                                         CircleSize);

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