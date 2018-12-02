using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SharpLesson1
{
    abstract class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        protected Image image;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
            image = Image.FromFile("res/asteroid.png");
        }

        /// <summary>
        /// Метод отрисовки объектов
        /// </summary>
        public virtual void Draw()
        {
            //Game.buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y, size.Width, size.Height);
            Game.buffer.Graphics.DrawImage(image, pos.X, pos.Y, size.Width, size.Height);
        }

        public abstract void Update();
    }
}
