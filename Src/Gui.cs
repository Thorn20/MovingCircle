using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MovingCircle
{
    public class GUI
    {
        Program p;

        public InputControl InCtrl;

        public World GameWorld;
        public Rectangle WorldView; 
        public Point ViewPoint; 
        public Region ViewRegion;
        public bool MovingView;
        public Point MousePoint;

        public GUI(Program p)
        {
            this.p = p;

            InCtrl = new InputControl(p);

            GameWorld = new World(p, 1000, 1000);
            WorldView = new Rectangle(10, 10, 500, 500);
            ViewPoint = new Point((GameWorld.Width / 2) - (WorldView.Width / 2), 
                                  (GameWorld.Height / 2) - (WorldView.Height / 2));
            ViewRegion = new Region(WorldView);
            MovingView = false;
        }

        public void Step()
        {
            GameWorld.Step();

            if (InCtrl.RgtClk && ViewRegion.IsVisible(Control.MousePosition))
            {
                if (!MovingView)
                {
                    MovingView = true;
                    MousePoint = Control.MousePosition;
                }
                else
                {
                    Point MouseNow = Control.MousePosition;
                    Point Movement = new Point(MousePoint.X - MouseNow.X, MousePoint.Y - MouseNow.Y);

                    ViewPoint.X += Movement.X;
                    ViewPoint.Y += Movement.Y;

                    MousePoint = MouseNow;
                }
            } 
            else MovingView = false;

            if (ViewPoint.X < 0) ViewPoint.X = 0;
            if (ViewPoint.Y < 0) ViewPoint.Y = 0;
            if (ViewPoint.X > (GameWorld.Width - WorldView.Width)) ViewPoint.X = (GameWorld.Width - WorldView.Width);
            if (ViewPoint.Y > (GameWorld.Height - WorldView.Height)) ViewPoint.Y = (GameWorld.Height - WorldView.Height);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, p.Width, p.Height);

            GraphicsContainer View = g.BeginContainer();
            g.SetClip(ViewRegion, CombineMode.Replace);
            g.TranslateTransform( WorldView.X - ViewPoint.X, WorldView.Y - ViewPoint.Y);
            GameWorld.Draw(g);
            g.EndContainer(View);

            Font WritingFont = new Font("Arial", 10);
            SolidBrush WritingBrush = new SolidBrush(Color.Black);

            g.DrawString( "Viewing: " + ViewPoint.X + ", " + ViewPoint.Y + " to "+ (ViewPoint.X + WorldView.Width) + ", " + (ViewPoint.Y + WorldView.Height),
                         WritingFont, WritingBrush, 10, 510);
            g.DrawString( "(Right Click and drag to look around.)",
                         WritingFont, WritingBrush, 10, 530);

            double fps = p.gfx.FramesPerSec;
            double sps = p.StepsPerSec;
            g.DrawString( "FPS: " + fps.ToString("F2"), WritingFont, WritingBrush, 510, 10);
            
            g.DrawString( "SPS: " + sps.ToString("F2"), WritingFont, WritingBrush, 510, 30);

            g.DrawRectangle(new Pen(Color.Black), WorldView);
        }
    }
}