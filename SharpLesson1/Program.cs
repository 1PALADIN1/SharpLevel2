using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpLesson1 {
    class Program {
        static void Main(string[] args) {
            Form form = new Form {
                Width = 800,
                Height = 600
            };
            //Game
            form.Show();
            //Game
            Application.Run(form);
        }
    }
}
