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

        /// <summary>
        /// Инициализация объекта космического корабля
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            grow = false;
            image = Image.FromFile("res/ship.png");
        }

        /// <summary>
        /// Обновление положения объекта в пространстве
        /// </summary>
        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0) pos.X = Game.Width + size.Width;
            ChangeSize();
        }

        /// <summary>
        /// Динамическое изменение размера корабля
        /// </summary>
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
