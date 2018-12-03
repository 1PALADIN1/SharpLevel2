using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson1
{
    /// <summary>
    /// Интерфейс для отслеживания столкновений
    /// </summary>
    interface IHitable
    {
        bool CheckHit(BaseObject other);
        void Hit();
    }
}
