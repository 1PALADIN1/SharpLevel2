using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SharpLesson1
{
    class Asteroid : BaseObject
    {
        public readonly static int maxSpeed = -9;
        public readonly static int minSpeed = -25;

        private Random rnd;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            rnd = new Random();
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            //pos.Y = pos.Y + dir.Y;
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                ChangeSpeed();
            }
            //if (pos.Y < 0) pos.Y = Game.Height + size.Height;
        }

        //случайным образом меняем скорость атероида
        //после того как он залетел за экран
        private void ChangeSpeed()
        {
            dir.X = rnd.Next(minSpeed, maxSpeed);
        }
    }
}
