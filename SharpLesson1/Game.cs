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
        private static readonly int ITER_NUM = 20;
        private static BufferedGraphicsContext _context;
        private static List<Bullet> bulletHitList;
        private static List<Asteroid> asteroidHitList;
        private static int height;
        private static int width;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(Ship.minSize, Ship.minSize));
        private static Bullet _bullet;
        public static BufferedGraphics buffer;
        public static List<BaseObject> _objs;
        private static Timer _timer;
        public static Random Rnd = new Random();
        public static int HIT_COUNT; //количество очков

        public static int Height
        {
            get => height;
            set
            {
                if (value > 1000 || value <= 0) throw new ArgumentOutOfRangeException("Высота экрана не должна быть больше 1000 пикселей или отрицательной");
                height = value;
            }
        }
        public static int Width
        {
            get => width;
            set
            {
                if (value > 1000 || value <= 0) throw new ArgumentOutOfRangeException("Ширина экрана не должна быть больше 1000 пикселей или отрицательной");
                width = value;
            }
        }

        static Game()
        {

        }

        /// <summary>
        /// Загрузка объектов
        /// </summary>
        private static void Load()
        {
            _objs = new List<BaseObject>();
            bulletHitList = new List<Bullet>();
            asteroidHitList = new List<Asteroid>();
            Random rnd = new Random();
            int size = 0;

            for (int i = 0; i < ITER_NUM; i++)
            {
                size = rnd.Next(Asteroid.minSize, Asteroid.maxSize + 1);
                //астероиды
                Asteroid asteroid = new Asteroid(new Point(Width, rnd.Next(0, Height + 1)),
                    new Point(rnd.Next(Asteroid.minSpeed, Asteroid.maxSpeed), 0),
                    new Size(size, size));
                _objs.Add(asteroid);
                asteroidHitList.Add(asteroid);
                //звезды
                _objs.Add(new Star(new Point(Width, rnd.Next(0, Height + 1)), new Point(rnd.Next(Star.minSpeed, Star.maxSpeed), 0), new Size(Star.starSize, Star.starSize)));
                //корабли
                //size = rnd.Next(Ship.minSize, Ship.maxSize + 1);
                //_objs.Add(new Ship(new Point(Width, rnd.Next(0, Height + 1)), new Point(rnd.Next(Ship.minSpeed, Ship.maxSpeed), 0), new Size(size, size)));
                //пули
                //Bullet bullet = new Bullet(new Point(0, rnd.Next(0, Height + 1)),
                //    new Point(rnd.Next(Bullet.minSpeed, Bullet.maxSpeed), 0),
                //    new Size(Bullet.bulletSize, Bullet.bulletSize));
                //_objs.Add(bullet);
                //bulletHitList.Add(bullet);
            }

            //аптечки
            for (int i = 0; i < 5; i++)
            {
                _objs.Add(new Chest(new Point(Width, rnd.Next(0, Height + 1)), new Point(rnd.Next(Chest.minSpeed, Chest.maxSpeed), 0), new Size(Chest.minSize, Chest.minSize)));
            }

            _objs.Add(_ship);
        }

        /// <summary>
        /// Инициализация игровых настроек и запуск таймера обновления игровых переменных
        /// </summary>
        /// <param name="form">Форма, на которой будет отрисовываться игра</param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;

            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            HIT_COUNT = 0;

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            _timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                _objs.Add(new Bullet(new Point(_ship.Position.X + 10, _ship.Position.Y + 4), new Point(Bullet.maxSpeed, 0), new Size(Bullet.bulletSize, Bullet.bulletSize)));
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();

            //проверка на столкновения
            /*
            foreach (var bullet in bulletHitList)
            {
                foreach (var asteroid in asteroidHitList)
                {
                    if(bullet.CheckHit(asteroid))
                    {
                        bullet.Hit();
                        asteroid.Hit();
                    }
                }
            }
            */
        }

        /// <summary>
        /// Отрисовка объектов
        /// </summary>
        public static void Draw()
        {
            //buffer.Graphics.Clear(Color.Black);
            //buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //buffer.Render();

            //отрисовка массива объектов
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            //buffer.Render();

            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                buffer.Graphics.DrawString("Energy:" + _ship.Energy,
                                    SystemFonts.DefaultFont, Brushes.White, 0, 0);
                buffer.Graphics.DrawString("Points:" + HIT_COUNT,
                                    SystemFonts.DefaultFont, Brushes.White, 0, 20);
            }
            buffer.Render();
        }

        /// <summary>
        /// Обновление положения игровых объектов в пространстве
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();

            _bullet?.Update();
            for (var i = 0; i < asteroidHitList.Count; i++)
            {
                if (asteroidHitList[i] == null) continue;
                asteroidHitList[i].Update();
                if (_bullet != null && _bullet.CheckHit(asteroidHitList[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    asteroidHitList[i] = null;
                    _bullet = null;
                    HIT_COUNT++;
                    continue;
                }
                if (!_ship.CheckHit(asteroidHitList[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
        }

        /// <summary>
        /// Метод завершения игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            buffer.Render();
        }
    }
}
