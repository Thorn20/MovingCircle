using System;
using System.Drawing;

namespace MovingCircle
{
    public class Circle
    {
        public int x, y, size, direction;
        public Random rng;

        public Circle( int x, int y, int size)
        {
            this.x = x;
            this.y = y;
            this.size = size;

            rng = new Random(x*y);
            direction = rng.Next(4);
        }

        public void Step(Program p)
        {
            int Choice = rng.Next(100);

            switch (Choice)
            {
                case 0:
                    if (direction == 0) 
                        direction = 3;
                    else direction--;
                    break;
                
                case 1:
                    if (direction == 3) 
                        direction = 0;
                    else direction++;
                    break;

                default:
                    break;
            }
                
            switch (direction)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;
            }

            if (x < 0) x = p.Width;
            if (x > p.Width) x = 0;
            if (y < 0) y = p.Height;
            if (y > p.Height) y = 0;
        }

        public void Draw( Graphics g)
        {
            Rectangle rec = new Rectangle(x - (size/2), 
                                          y - (size/2), 
                                          size, 
                                          size);

            g.DrawEllipse( new Pen(Color.Black), rec);
            g.FillEllipse( new SolidBrush(Color.Red), rec);
        }
    }
}