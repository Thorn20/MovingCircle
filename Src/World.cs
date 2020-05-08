using System;
using System.Drawing;

namespace MovingCircle
{
    public class World
    {
        public int Width, Height;

        Circle[] Circles;

        public World( int width, int height)
        {
            this.Width = width;
            this.Height = height;

            BuildCircles( 20 );
        }

        public void BuildCircles(int amount)
        {
            Random rng = new Random();

            Circles = new Circle[amount];

            for (int ii = 0; ii < Circles.Length; ii++)
                Circles[ii] = new Circle( rng.Next( 0, this.Width), rng.Next( 0, this.Height), 20);
        }

        public void Step()
        {
            foreach (Circle C in Circles) 
            {
                C.Step();

                if (C.x < 4 + (C.size / 2)) C.x++;
                if (C.x > (Width - 4) - (C.size / 2)) C.x--;
                if (C.y < 4 + (C.size / 2)) C.y++;
                if (C.y > (Height - 4) - (C.size / 2)) C.y--;
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