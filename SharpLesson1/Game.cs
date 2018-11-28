using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SharpLesson1
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics buffer;

        public static int Height { get; set; }
        public static int Width { get; set; }

        static Game()
        {

        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;

            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
        }

        //отрисовка
        public static void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            buffer.Render();
        }
    }
}
