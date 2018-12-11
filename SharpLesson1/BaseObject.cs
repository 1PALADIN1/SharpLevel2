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

        public delegate void Message();

        public Point Position
        {
            get => pos;
        }

        public Size ObjectSize
        {
            get => size;
        }

        /// <summary>
        /// Инициализация базового объекта
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
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
            if (image != null)
                Game.buffer.Graphics.DrawImage(image, pos.X, pos.Y, size.Width, size.Height);

            if (Game.DEBUG_MODE)
            {
                Game.buffer.Graphics.DrawLine(Pens.Red, pos.X + size.Width, pos.Y + size.Height, pos.X, pos.Y);
            }
        }

        public abstract void Update();

        /// <summary>
        /// Проверка на столкновение объектов
        /// </summary>
        /// <param name="other">Объект типа BaseObject</param>
        /// <returns>Возвращает истину, если столкновение было обнаружено</returns>
        public bool CheckHit(BaseObject other)
        {
            //if (other.Position.X <= (pos.X + size.Width) && other.Position.X >= pos.X
            //    && other.Position.Y <= (pos.Y + size.Height) && other.Position.Y >= pos.Y)
            //    return true;

            if ((pos.X + size.Width / 2) <= (other.Position.X + other.size.Width) && (pos.X + size.Width / 2) >= other.Position.X
                && (pos.Y + size.Height /2) <= (other.Position.Y + other.size.Height) && (pos.Y + size.Height / 2) >= other.Position.Y)
                return true;
            return false;
        }
    }
}
