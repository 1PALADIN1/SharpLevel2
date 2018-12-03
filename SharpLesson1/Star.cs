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
        public readonly static int starSize = 12;

        /// <summary>
        /// Инициализация объекта звезды
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Image.FromFile("res/star.png");
        }

        /// <summary>
        /// Обновление положения объекта в пространстве
        /// </summary>
        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0) pos.X = Game.Width + size.Width;
        }
    }
}
