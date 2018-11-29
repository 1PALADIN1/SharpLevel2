using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson1
{
    class Ship : BaseObject
    {
        private bool grow;

        public readonly static int maxSpeed = -5;
        public readonly static int minSpeed = -15;

        public readonly static int maxSize = 30;
        public readonly static int minSize = 5;

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            grow = false;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(pos.X, pos.Y, size.Width, size.Height));
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0) pos.X = Game.Width + size.Width;
            ChangeSize();
        }

        //динамически меняем размер
        private void ChangeSize()
        {
            if (grow)
            {
                size.Height++;
                size.Width++;
                if (size.Height > maxSize) grow = false;
            }
            else
            {
                size.Height--;
                size.Width--;
                if (size.Height < minSize) grow = true;
            }
        }
    }
}
