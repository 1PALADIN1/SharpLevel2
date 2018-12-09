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
        private static readonly int MAX_ENERGY = 100;
        private int _energy = MAX_ENERGY;
        public int Energy => _energy;

        private bool grow;

        public readonly static int maxSpeed = -5;
        public readonly static int minSpeed = -15;
        public readonly static int maxSize = 30;
        public readonly static int minSize = 20;

        public static event Message MessageDie;

        /// <summary>
        /// Метод отнимающий энергию корабля
        /// </summary>
        /// <param name="n">Количество отнимаемых единиц энергии за тик таймера</param>
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        /// <summary>
        /// Метод добавляющий энергию корабля
        /// </summary>
        /// <param name="n">Количество добавляемых единиц энергии за тик таймера</param>
        public void EnergyUp(int n)
        {
            if ((_energy + n) > MAX_ENERGY) _energy = MAX_ENERGY;
            else
                _energy += n;
        }

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
            //pos.X = pos.X + dir.X;
            //if (pos.X < 0) pos.X = Game.Width + size.Width;
            //ChangeSize();
        }

        /// <summary>
        /// Смещение корабля вверх
        /// </summary>
        public void Up()
        {
            if (pos.Y > 0) pos.Y -= dir.Y;
        }

        /// <summary>
        /// Смещение корабля вниз
        /// </summary>
        public void Down()
        {
            if (pos.Y < Game.Height) pos.Y += dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
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
