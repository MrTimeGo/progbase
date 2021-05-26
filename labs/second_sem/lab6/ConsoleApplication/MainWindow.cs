using ClassLibrary;
using System.Collections.Generic;
using Terminal.Gui;

namespace lab6
{
    class MainWindow : Window
    {
        GoodRepository repository;
        int totalPages;
        int currentPage;

        ListView list;
        Button prevPage;
        TextField bottomPageCounter;
        Button nextPage;
        public MainWindow(GoodRepository repository)
        {
            this.repository = repository;
            this.totalPages = repository.GetTotalPages();
            this.currentPage = 1;
            Y = Pos.Percent(0) + 1;
            Initialize();
        }
        private void Initialize()
        {
            MenuBar menu = new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem ("_File", new MenuItem[]
                {
                    new MenuItem ("_New...", "Add new good", OnNewGoodClicked),
                    new MenuItem ("_Quit", "Help text", OnQuit)
                }),
                new MenuBarItem ("_Help", new MenuItem[]
                {
                    new MenuItem ("_About", "Information", OnAboutClicked)
                })
            });

            Application.Top.Add(menu);

            Button newGood = new Button("Add new good")
            {
                X = Pos.Center(),
                Y = Pos.Percent(10),
            };
            list = new ListView()
            {
                X = Pos.Percent(10),
                Y = Pos.Center(),
                Width = Dim.Percent(80),
                Height = 10
            };
            prevPage = new Button("<-")
            {
                X = Pos.Right(list) - 20,
                Y = Pos.Bottom(list) + 2,
            };
            bottomPageCounter = new TextField()
            {
                X = Pos.Right(prevPage) + 1,
                Y = Pos.Top(prevPage),
                Width = 3,
                Height = Dim.Height(prevPage)
            };
            Label bottomAllPage = new Label($"/{totalPages}")
            {
                X = Pos.Right(bottomPageCounter) + 1,
                Y = Pos.Top(bottomPageCounter),
                Height = Dim.Height(bottomPageCounter)
            };
            nextPage = new Button("->")
            {
                X = Pos.Right(bottomAllPage) + 1,
                Y = Pos.Top(bottomAllPage),
            };

            Label notFoundLabel = new Label("Goods not found")
            {
                X = Pos.Center(),
                Y = Pos.Center()
            };

            UpdateInfo();

            newGood.Clicked += OnNewGoodClicked;
            list.OpenSelectedItem += OnGoodClicked;

            prevPage.Clicked += OnPrevPageClicked;
            nextPage.Clicked += OnNextPageClicked;
            bottomPageCounter.KeyDown += OnCounterPressed;

            if (totalPages != 0)
            {
                this.Add(newGood, list, prevPage, bottomPageCounter, bottomAllPage, nextPage);
            }
            else
            {
                this.Add(newGood, notFoundLabel);
            }
        }

        private void OnGoodClicked(ListViewItemEventArgs obj)
        {
            Good good = (Good)obj.Value;
            Window viewWindow = new GoodViewWindow(good.id, repository);
            Application.Top.RemoveAll();
            Application.Top.Add(viewWindow);
            Application.RequestStop();
            Application.Run();
        }

        private void OnNewGoodClicked()
        {
            Window newGood = new GoodCreationWindow(repository);
            Application.Top.RemoveAll();
            Application.Top.Add(newGood);
            Application.RequestStop();
            Application.Run();
        }

        private void OnCounterPressed(KeyEventEventArgs obj)
        {
            if (obj.KeyEvent.Key == Key.Enter)
            {
                if (!int.TryParse(bottomPageCounter.Text.ToString(), out int number))
                {
                    bottomPageCounter.Text = string.Empty;
                    return;
                }
                if (number > totalPages || number < 1)
                {
                    bottomPageCounter.Text = string.Empty;
                    return;
                }
                currentPage = number;
                UpdateInfo();
            }
        }

        private void OnNextPageClicked()
        {
            currentPage++;
            UpdateInfo();
        }
        private void OnPrevPageClicked()
        {
            currentPage--;
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (totalPages == 0)
            {
                bottomPageCounter.Text = "0";
                prevPage.Visible = false;
                nextPage.Visible = false;
                return;
            }

            prevPage.Visible = currentPage != 1;
            nextPage.Visible = currentPage != totalPages;
            bottomPageCounter.Text = $"{currentPage}";
            Application.Refresh();

            List<Good> source = repository.GetPage(currentPage);
            list.SetSource(source);
        }
        private void OnQuit()
        {
            Application.RequestStop();
        }
        private void OnAboutClicked()
        {
            MessageBox.Query("Info", "Lab6 by Artem Petselia", "Ok");
        }
    }
}
