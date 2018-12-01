using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson1
{
    class Star : BaseObject
    {
        public readonly static int maxSpeed = -5;
        public readonly static int minSpeed = -15;

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Image.FromFile("res/star.png");
        }

        public override void Draw()
        {
            base.Draw();
            //Game.buffer.Graphics.DrawLine(Pens.White, pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Height);
            //Game.buffer.Graphics.DrawLine(Pens.White, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Height);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0) pos.X = Game.Width + size.Width;
        }
    }
}
