using System;
using System.Drawing;

namespace MovingCircle
{
    public class Circle
    {
        public Program p;
        public Random rng;

        public double X, Y, MaxVel, Velocity, Direction, Size;

        public Circle( Program p, int x, int y, int size)
        {
            this.p = p;
            this.X = x;
            this.Y = y;
            this.Size = size;

            rng = new Random((int)( X * Y));
            rng = new Random(rng.Next( 10000000, 99999999));
            Direction = rng.NextDouble() * (2 * Math.PI);
            MaxVel = rng.NextDouble();
        }

        public void Step()
        {
            RandomChoice();

            ApplyMovement();
        }

        void RandomChoice()
        {
            int Choice = rng.Next(100);

            switch (Choice)
            {
                case 0:
                    Direction -= (MaxVel / Math.PI) * ( 1000 / p.StepTime);
                    break;
                
                case 1:
                    Direction += (MaxVel / Math.PI) * ( 1000 / p.StepTime);
                    break;

                case 2:
                    Velocity += MaxVel * ( 1000 / p.StepTime);
                    break;

                case 3:
                    Velocity -= MaxVel * ( 1000 / p.StepTime);
                    break;

                default:
                    break;
            }
        }

        public void ApplyMovement()
        {
            if (Direction > (2 * Math.PI))
                Direction -= (2 * Math.PI);
            if (Direction < 0)
                Direction += (2 * Math.PI);

            if (Velocity > MaxVel) Velocity = MaxVel;
            if (Velocity < MaxVel * -1) Velocity = MaxVel * -1;

            X += Velocity * Math.Cos( Direction);
            Y += Velocity * Math.Sin( Direction);
        }

        public void Draw( Graphics g)
        {
            Rectangle rec = new Rectangle((int)(X - (Size/2)), 
                                          (int)(Y - (Size/2)), 
                                          (int)(Size), 
                                          (int)(Size));

            g.DrawEllipse( new Pen(Color.Black, 2), rec);
            g.FillEllipse( new SolidBrush(Color.Red), rec);
        }
    }
}