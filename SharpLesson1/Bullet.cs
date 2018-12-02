using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson1
{
    class Bullet : BaseObject
    {
        public readonly static int maxSpeed = -9;
        public readonly static int minSpeed = -25;
        public readonly static int bulletSize = 12;

        private Random rnd;

        /// <summary>
        /// Инициализация объекта пули
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            rnd = new Random();
            image = Image.FromFile("res/bullet.png");
        }

        /// <summary>
        /// Обновление положения объекта в пространстве
        /// </summary>
        public override void Update()
        {
            pos.X = pos.X - dir.X;
            if (pos.X > Game.Width)
            {
                ChangeSpeed();
                ChangePosition();
            }
        }

        /// <summary>
        /// Случайным образом изменение скорости пули
        /// </summary>
        private void ChangeSpeed()
        {
            dir.X = rnd.Next(minSpeed, maxSpeed);
        }

        /// <summary>
        /// Cлучайным образом изменение позиции пули
        /// </summary>
        private void ChangePosition()
        {
            pos.X = -size.Width;
            pos.Y = rnd.Next(0, Game.Height + 1);
        }
    }
}
