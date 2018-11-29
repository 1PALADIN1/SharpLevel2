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

        public static BaseObject[] _objs;

        public static int Height { get; set; }
        public static int Width { get; set; }

        static Game()
        {

        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length; i++)
            {
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            }
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

            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        //отрисовка
        public static void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            buffer.Render();

            //отрисовка массива объект
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
    }
}
