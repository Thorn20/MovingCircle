using System;
using System.Windows.Forms;

namespace MovingCircle
{
    public class InputControl
    {
        public bool UpKey, DownKey, LeftKey, RightKey, RgtClk;

        public InputControl(Program p)
        {
            UpKey = DownKey = LeftKey = RightKey = RgtClk = false;

            p.KeyDown += new KeyEventHandler(this.keypressed);
            p.KeyUp += new KeyEventHandler(this.keyreleased);
            p.MouseDown += new MouseEventHandler(this.mousepressed);
            p.MouseUp += new MouseEventHandler(this.mousereleased);
        }

        public void keypressed(object o, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    UpKey = true;
                    break;

                case Keys.Down:
                    DownKey = true;
                    break;

                case Keys.Left:
                    LeftKey = true;
                    break;

                case Keys.Right:
                    RightKey = true;
                    break;

                default:
                    break;
            }

            e.Handled = true;
        }   

        public void keyreleased(object o, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    UpKey = false;
                    break;

                case Keys.Down:
                    DownKey = false;
                    break;

                case Keys.Left:
                    LeftKey = false;
                    break;

                case Keys.Right:
                    RightKey = false;
                    break;

                default:
                    break;
            }

            e.Handled = true;
        }

        public void mousepressed(object o, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    RgtClk = true;
                    break;

                default:
                    break;
            }
        }

        public void mousereleased(object o, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    RgtClk = false;
                    break;

                default:
                    break;
            }
        }

    }
}