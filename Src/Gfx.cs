using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;

namespace MovingCircle
{
    public class Gfx
    {
        Program p;

        public Timer FrameTimer;
        public DateTime FrameStart, FrameEnd;
        public double FrameTime;
        public double FrameLimit = 60;
        public double FramesPerSec = 0.0;

        public BufferedGraphicsContext Context;
        public BufferedGraphics Buffer;   

        public Rectangle WorldView; 
        public Point ViewPoint;     

        public Gfx( Program p)
        {
            this.p = p;

            WorldView = new Rectangle(10, 10, 500, 500);
            ViewPoint = new Point((p.GameWorld.Width / 2) - (WorldView.Width / 2), 
                                  (p.GameWorld.Height / 2) - (WorldView.Height / 2));

            InitGraphics();
        }

        public void InitGraphics()
        {
            Context = BufferedGraphicsManager.Current;

            Context.MaximumBuffer = new Size( p.Width + 1, p.Height + 1);

            if ( Buffer != null)
            {
                Buffer.Dispose();
                Buffer = null;
            }

            Buffer = Context.Allocate(p.CreateGraphics(), new Rectangle( 0, 0, p.Width, p.Height));

            p.Refresh();

            FrameTimer = new System.Timers.Timer(1000 / FrameLimit);
            FrameTimer.Elapsed += RenderFrame;
            FrameTimer.AutoReset = true;
            FrameTimer.Enabled = true;
        }

        public void RenderFrame(object o, ElapsedEventArgs e)
        {
            //FrameTimer.Enabled = false;
            FrameStart = DateTime.Now;

            Graphics g = Buffer.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, p.Width, p.Height);

            GraphicsContainer View = g.BeginContainer();
            Region ViewRegion = new Region(WorldView);
            g.SetClip(ViewRegion, CombineMode.Replace);
            g.TranslateTransform( WorldView.X - ViewPoint.X, WorldView.Y - ViewPoint.Y);
            p.GameWorld.Draw(g);
            g.EndContainer(View);

            Font WritingFont = new Font("Arial", 10);
            SolidBrush WritingBrush = new SolidBrush(Color.Black);

            g.DrawString( "Viewing: " + ViewPoint.X + ", " + ViewPoint.Y + " to "+ (ViewPoint.X + WorldView.Width) + ", " + (ViewPoint.Y + WorldView.Height),
                         WritingFont, WritingBrush, 10, 510);
            g.DrawString( "(Use Arrow Keys to move view around.)",
                         WritingFont, WritingBrush, 10, 530);

            g.DrawString( "FPS: " + FramesPerSec.ToString("F2"), WritingFont, WritingBrush, 510, 10);
            g.DrawString( "SPS: " + p.StepsPerSec.ToString("F2"), WritingFont, WritingBrush, 510, 30);

            g.DrawRectangle(new Pen(Color.Black), WorldView);

            Buffer.Render(Graphics.FromHwnd(p.Handle));

            FrameEnd = DateTime.Now;
            FrameTime = FrameEnd.Subtract(FrameStart).TotalMilliseconds;
            if (FrameTime < (1000/FrameLimit)) FrameTime = (1000/FrameLimit);
            FramesPerSec = 1000 / FrameTime;
            FrameTimer.Interval = FrameTime;
            //FrameTimer.Enabled = true;
        }
    }
}