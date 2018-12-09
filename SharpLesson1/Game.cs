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
        public static readonly bool DEBUG_MODE = false; //режим отладки
        private static readonly int ITER_NUM = 20;
        private static int asteroidNumber = 10; //количество астероидов на карте при инициализации
        private static BufferedGraphicsContext _context;
        private static List<Bullet> bulletHitList; //пули
        private static List<Asteroid> asteroidHitList; //астероиды
        private static List<Chest> chestHitList; //аптечки
        private static int height;
        private static int width;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(10, 10), new Size(Ship.minSize, Ship.minSize)); //корабль-игрок
        private static Bullet _bullet;
        public static BufferedGraphics buffer;
        public static List<BaseObject> _objs;
        private static Timer _timer;
        public static List<BaseObject> garbage;
        public static Random Rnd = new Random();
        public static int HIT_COUNT; //количество очков
        private static int OBJECT_ARRAY_SIZE = 0;

        public static int Height
        {
            get => height;
            set
            {
                if (value > 1000 || value <= 0)
                    throw new ArgumentOutOfRangeException("Высота экрана не должна быть больше 1000 пикселей или отрицательной");
                height = value;
            }
        }
        public static int Width
        {
            get => width;
            set
            {
                if (value > 1000 || value <= 0)
                    throw new ArgumentOutOfRangeException("Ширина экрана не должна быть больше 1000 пикселей или отрицательной");
                width = value;
            }
        }

        static Game()
        {

        }

        #region инициализация игровых объектов
        /// <summary>
        /// Загрузка объектов
        /// </summary>
        private static void Load()
        {
            _objs = new List<BaseObject>();
            bulletHitList = new List<Bullet>();
            asteroidHitList = new List<Asteroid>();
            chestHitList = new List<Chest>();
            Random rnd = new Random();

            for (int i = 0; i < ITER_NUM; i++)
            {
                
                //звезды
                _objs.Add(new Star(new Point(Width, rnd.Next(0, Height + 1)),
                    new Point(rnd.Next(Star.minSpeed, Star.maxSpeed), 0),
                    new Size(Star.starSize, Star.starSize)));
            }

            //аптечки
            for (int i = 0; i < 5; i++)
            {
                Chest chest = new Chest(new Point(Width, rnd.Next(0, Height + 1)),
                    new Point(rnd.Next(Chest.minSpeed, Chest.maxSpeed), 0),
                    new Size(Chest.minSize, Chest.minSize));
                _objs.Add(chest);
                chestHitList.Add(chest);
            }

            //астероиды
            FillAsteroids();

            _objs.Add(_ship);
        }

        /// <summary>
        /// Заполнение игрового поля астероидами
        /// </summary>
        private static void FillAsteroids()
        {
            int size = 0;
            Random rnd = new Random();

            for (int i = 0; i < asteroidNumber; i++)
            {
                size = rnd.Next(Asteroid.minSize, Asteroid.maxSize + 1);
                Asteroid asteroid = new Asteroid(new Point(Width, rnd.Next(0, Height + 1)),
                    new Point(rnd.Next(Asteroid.minSpeed, Asteroid.maxSpeed), 0),
                    new Size(size, size));
                _objs.Add(asteroid);
                asteroidHitList.Add(asteroid);
            }
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
            garbage = new List<BaseObject>();

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Log.LogConsole(() => { return "Запуск корабля и начало игры!"; } );

            Load();

            _timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }
        #endregion

        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        /// <param name="sender">Объект, который вызвал метод</param>
        /// <param name="e">Параметры вызова</param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                _bullet = new Bullet(new Point(_ship.Position.X + _ship.ObjectSize.Width / 2, _ship.Position.Y + _ship.ObjectSize.Height / 4),
                    new Point(Bullet.maxSpeed, 0), new Size(Bullet.bulletSize, Bullet.bulletSize));
                _objs.Add(_bullet);
                bulletHitList.Add(_bullet);
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        /// <summary>
        /// Метод выполняющий обновление и отрисовку объектов каждый тик таймера
        /// </summary>
        /// <param name="sender">Объект, который вызвал метод</param>
        /// <param name="e">Параметры вызова</param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
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
                buffer.Graphics.DrawString("Array size:" + OBJECT_ARRAY_SIZE,
                                    SystemFonts.DefaultFont, Brushes.White, 0, 40);
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

            CollisionCheck();

            //повоторная инициализация массива астероидов (задание 1)
            if (asteroidHitList.Count == 0)
            {
                asteroidNumber++;
                FillAsteroids();
            }
        }

        #region проверка столкновений
        /// <summary>
        /// Метод обработки столкновений
        /// </summary>
        private static void CollisionCheck()
        {
            //столкновение пуль и астероидов
            foreach (var bullet in bulletHitList)
            {
                foreach (var asteroid in asteroidHitList)
                {
                    if (bullet.CheckHit(asteroid))
                    {
                        garbage.Add(bullet);
                        garbage.Add(asteroid);
                        //bullet.Hit();
                        asteroid.Hit();
                        HIT_COUNT++;

                        Log.LogConsole(() => { return "Астероид сбит"; });
                    }
                }
            }

            //столкновения корабля с объектами
            foreach (var item in _objs)
            {
                if (_ship.CheckHit(item))
                {
                    if (item is Chest)
                    {
                        (item as Chest).Hit();
                        _ship?.EnergyUp(Rnd.Next(1, 10));
                        Log.LogConsole(() => { return "Энергия увеличина"; });
                    }

                    if (item is Asteroid)
                    {
                        (item as Asteroid).Hit();
                        _ship?.EnergyLow(Rnd.Next(1, 10));
                        System.Media.SystemSounds.Asterisk.Play();
                        Log.LogConsole(() => { return "Попадание астероида по кораблю"; });

                        if (_ship.Energy <= 0)
                        {
                            _ship?.Die();
                            Log.LogConsole(() => { return "Корабль уничтожен..."; });
                        }
                    }
                }
            }

            //очистка пуль (пробный вариант)
            foreach (var garbageItem in garbage)
            {
                _objs.Remove(garbageItem);
                if (garbageItem is Bullet)
                {
                    Bullet bullet = garbageItem as Bullet;
                    bulletHitList.Remove(bullet);

                    bullet.Dispose();
                    bullet = null;
                }
                if (garbageItem is Asteroid)
                {
                    Asteroid asteroid = garbageItem as Asteroid;
                    asteroidHitList.Remove(asteroid);

                    asteroid.Dispose();
                    asteroid = null;
                }
            }

            //очистка массива с мусором
            if (garbage.Count > 20) garbage.Clear();

            OBJECT_ARRAY_SIZE = asteroidHitList.Count;
        }
        #endregion

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
