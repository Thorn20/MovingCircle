using System;
using System.Drawing;

namespace MovingCircle
{
    public class World
    {
        public Program p;
        public int Width, Height;

        Circle[] Circles;

        public World( Program p, int width, int height)
        {
            this.p = p;
            this.Width = width;
            this.Height = height;

            BuildCircles( 20 );
        }

        public void BuildCircles(int amount)
        {
            Random rng = new Random();

            Circles = new Circle[amount];

            for (int ii = 0; ii < Circles.Length; ii++)
            {
                int x = rng.Next( 0, this.Width), y = rng.Next( 0, this.Height);

                for (int jj = 0; jj < ii; jj++)
                {
                    double Distance = ( Math.Sqrt( Math.Abs( x - Circles[jj].X) + Math.Abs( y - Circles[jj].Y)));

                    if( Distance <= 40)
                    {
                        x = rng.Next( 0, this.Width); 
                        y = rng.Next( 0, this.Height);
                    }
                }

                Circles[ii] = new Circle( p, x, y, 20);
            }
        }

        public void Step()
        {
            foreach (Circle C in Circles) 
            {
                C.Step();

                if (C.X < 4 + (C.Size / 2)) C.X++;
                if (C.X > (Width - 4) - (C.Size / 2)) C.X--;
                if (C.Y < 4 + (C.Size / 2)) C.Y++;
                if (C.Y > (Height - 4) - (C.Size / 2)) C.Y--;
            }
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