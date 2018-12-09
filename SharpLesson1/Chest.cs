using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson1
{
    /// <summary>
    /// Класс аптечек
    /// </summary>
    class Chest : BaseObject
    {
        public readonly static int maxSpeed = -9;
        public readonly static int minSpeed = -25;
        public readonly static int maxSize = 15;
        public readonly static int minSize = 15;

        Random rnd;

        /// <summary>
        /// Инициализация объекта аптечки
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Chest(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            rnd = new Random();
            image = Image.FromFile("res/kit.png");
        }

        /// <summary>
        /// Обновление положения объекта в пространстве
        /// </summary>
        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0)
            {
                ChangeSpeed();
                ChangePosition();
            }
        }

        /// <summary>
        /// Случайным образом изменение скорости астероида
        /// </summary>
        private void ChangeSpeed()
        {
            dir.X = rnd.Next(minSpeed, maxSpeed);
        }

        /// <summary>
        /// Cлучайным образом изменение позиции астероида
        /// </summary>
        private void ChangePosition()
        {
            pos.X = Game.Width + size.Width;
            pos.Y = rnd.Next(0, Game.Height + 1);
        }

        /// <summary>
        /// Метод обрабатывающий столкновения объектов
        /// </summary>
        public void Hit()
        {
            ChangeSpeed();
            ChangePosition();
        }
    }
}
