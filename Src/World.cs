using System;
using System.Drawing;
using System.Collections.Generic;

namespace MovingCircle
{
    public class World
    {
        public Program p;
        public int Width, Height;

        public List<Circle> Circles;

        public World( Program p, int width, int height)
        {
            this.p = p;
            this.Width = width;
            this.Height = height;

            BuildCircles( 5 );
        }

        public void BuildCircles(int amount)
        {
            Random rng = new Random();
            Circles = new List<Circle>();
            int x = 0, y = 0;

            for (int ii = 0; ii < amount; ii++)
            {
                for (int jj = 0; jj < ii; jj++)
                {        
                    double Distance = 0;            
                    while( Distance <= 20)
                    {
                        x = rng.Next( 0, this.Width); 
                        y = rng.Next( 0, this.Height);

                        Distance = Math.Sqrt( Math.Abs( x - Circles[jj].X) + Math.Abs( y - Circles[jj].Y));
                    }
                }

                Circles.Add( new Circle( p, x, y, 20));
            }
        }

        public void Step()
        {
            foreach (Circle C in Circles) 
            {
                C.Step();

                if (C.X < (C.Size / 2)) 
                { 
                    C.X = (C.Size / 2); 
                    C.Velocity = 0;
                }
                if (C.X > Width - (C.Size / 2)) 
                {
                    C.X = Width - (C.Size / 2);
                    C.Velocity = 0;
                }
                if (C.Y < (C.Size / 2)) 
                {
                    C.Y = (C.Size / 2);
                    C.Velocity = 0;
                }
                if (C.Y > Height - (C.Size / 2)) 
                {
                    C.Y = Height - (C.Size / 2);
                    C.Velocity = 0;
                }

                
            }
            /*
            for (int ii = 0; ii < Circles.Count; ii++)
                if (Circles[ii].Energy <= 0) Circles.Remove(Circles[ii]);
            */
        }

        public void Draw( Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.DarkGreen), 0, 0, Width, Height);
            Pen DrawPen = new Pen(Color.DarkGray, 4);
            g.DrawRectangle(DrawPen, 0, 0, Width, Height);
            g.DrawLine(DrawPen, (Width / 2), (Height / 2) - 10, (Width / 2), (Height / 2) + 10);
            g.DrawLine(DrawPen, (Width / 2) - 10, (Height / 2), (Width / 2) + 10, (Height / 2));

            foreach (Circle C in Circles) C.Draw(g);
        }
    }
}