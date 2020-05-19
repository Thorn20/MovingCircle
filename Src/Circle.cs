using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MovingCircle
{
    public class Circle
    {
        public Program p;
        public Random rng;
        public double X, Y, MaxVel, Velocity, Direction, Size, Energy;
        public Region Collider;

        public Circle( Program p, int x, int y, int size)
        {
            this.p = p;
            this.X = x;
            this.Y = y;
            this.Size = size;

            rng = new Random((int)( X * Y ));
            Direction = rng.NextDouble() * (2 * Math.PI);
            MaxVel = ( rng.NextDouble() * 10 ) + 10;
            Energy = 100;

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse( new RectangleF((float)(-(Size/2)), 
                                            (float)(-(Size/2)), 
                                            (float)(Size), 
                                            (float)(Size)));

            Collider = new Region( path);
        }

        public void Step()
        {
            Energy -= 0.1 + (Velocity * 0.1);

            RandomChoice();

            ApplyMovement();
        }

        void RandomChoice()
        {
            int Choice = rng.Next(4);

            if (Choice == 0) 
                Velocity += MaxVel * (p.StepTime / 1000);

            if (Choice == 1)
                Velocity -= MaxVel * (p.StepTime / 1000);

            if (Choice == 2) 
                Direction -= ((MaxVel) / Math.PI) * (p.StepTime / 1000);

            if (Choice == 3)
                Direction += ((MaxVel) / Math.PI) * (p.StepTime / 1000);

            //if (Choice == 4)
            //    Velocity = 0;            
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

        public void OnCollide( )
        {
            Velocity = 0;
        }

        public void Draw( Graphics g)
        {
            GraphicsContainer gc = g.BeginContainer();
            g.TranslateTransform( (float)X, (float)Y);
            g.RotateTransform( (float)( Direction * (360 / (2*Math.PI)) ));

            RectangleF rec = new RectangleF((float)(-(Size/2)), 
                                            (float)(-(Size/2)), 
                                            (float)(Size), 
                                            (float)(Size));

            g.DrawEllipse( new Pen(Color.Black, 2), rec);
            g.FillEllipse( new SolidBrush(Color.Red), rec);

            g.FillEllipse( new SolidBrush(Color.Black), 5f, -2.5f, 5f, 5f);
            g.EndContainer( gc);
        }
    }
}