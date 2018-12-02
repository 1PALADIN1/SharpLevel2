using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SharpLesson1
{
    class Asteroid : BaseObject, IHitable
    {
        public readonly static int maxSpeed = -9;
        public readonly static int minSpeed = -25;
        public readonly static int maxSize = 30;
        public readonly static int minSize = 5;

        private Random rnd;

        /// <summary>
        /// Инициализация объекта астероида
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            rnd = new Random();
            image = Image.FromFile("res/asteroid.png");
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
        /// Проверка на столкновение объектов
        /// </summary>
        /// <param name="other">Объект типа BaseObject</param>
        /// <returns>Возвращает истину, если столкновение было обнаружено</returns>
        public bool CheckHit(BaseObject other)
        {
            if (other.Position.X <= (pos.X + size.Width) && other.Position.X >= (pos.X - size.Width)
                && other.Position.Y <= (pos.Y + size.Height) && other.Position.Y >= (pos.Y - size.Height))
                return true;
            return false;
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
