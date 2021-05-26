using ClassLibrary;
using Terminal.Gui;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            string database = @"./../../../data.db";
            Application.Init();
            GoodRepository repo = new GoodRepository(database);
            Window win = new MainWindow(repo);
            Application.Top.Add(win);
            Application.Run();
        }
    }
}
