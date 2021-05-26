using ClassLibrary;
using Terminal.Gui;

namespace lab6
{
    class GoodViewWindow : Window
    {
        Good good;
        GoodRepository repository;

        TextField nameField;
        TextView descriptionField;
        TextField priceField;
        TextField creationDateField;
        Label availableLabel;
        public GoodViewWindow(long goodId, GoodRepository repository)
        {
            this.repository = repository;
            this.good = repository.GetById(goodId);
            Title = "Good";
            Initialize();
        }
        private void Initialize()
        {
            Label nameLabel = new Label("Name: ")
            {
                X = Pos.Percent(10),
                Y = Pos.Percent(10),
            };
            nameField = new TextField()
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Bottom(nameLabel) + 1,
                Width = Dim.Percent(80),
                ReadOnly = true
            };
            Label descriptionLabel = new Label("Description: ")
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Bottom(nameField) + 1,
            };
            Window inputWindow = new Window()
            {
                X = Pos.Left(descriptionLabel),
                Y = Pos.Bottom(descriptionLabel) + 1,
                Width = Dim.Percent(80),
                Height = Dim.Fill(17)
            };
            descriptionField = new TextView()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ReadOnly = true,
            };
            inputWindow.Add(descriptionField);
            Label priceLabel = new Label("Price: ")
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Bottom(inputWindow) + 1,
            };
            priceField = new TextField()
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Bottom(priceLabel) + 1,
                Width = Dim.Percent(80) - 4,
                ReadOnly = true
            };
            Label uahLabel = new Label("UAH")
            {
                X = Pos.Right(priceField) + 1,
                Y = Pos.Top(priceField)
            };
            Label creationDateLabel = new Label("Created at:")
            {
                X = Pos.Left(priceField),
                Y = Pos.Bottom(priceField) + 1,
            };
            creationDateField = new TextField()
            {
                X = Pos.Left(creationDateLabel),
                Y = Pos.Bottom(creationDateLabel) + 1,
                Width = 19,
                ReadOnly = true
            };
            availableLabel = new Label("1")
            {
                X = Pos.Left(creationDateField),
                Y = Pos.Bottom(creationDateField) + 1,
                Width = 30,
            };
            Button toMainWindow = new Button("To main window")
            {
                X = Pos.Center() - 20,
                Y = Pos.Bottom(availableLabel) + 2
            };
            Button edit = new Button("Edit")
            {
                X = Pos.Right(toMainWindow) + 3,
                Y = Pos.Top(toMainWindow)
            };
            Button delete = new Button("Delete")
            {
                X = Pos.Right(edit) + 3,
                Y = Pos.Top(edit)
            };
            UpdateInfo();

            toMainWindow.Clicked += OnToMainWindowClicked;
            edit.Clicked += OnEditClicked;
            delete.Clicked += OnDeleteClicked;

            this.Add(nameLabel, nameField,
                descriptionLabel, inputWindow,
                priceLabel, priceField, uahLabel,
                creationDateLabel, creationDateField,
                availableLabel,
                toMainWindow, edit, delete);
        }
        private void UpdateInfo()
        {
            nameField.Text = good.name;
            descriptionField.Text = good.description;
            priceField.Text = $"{good.price}";
            creationDateField.Text = good.createdAt.ToString();
            availableLabel.Text = $"Available: {good.isAvailable}";
        }
        private void OnDeleteClicked()
        {
            int result = MessageBox.Query("Info", "Are you sure, that you want to delete good?", "Yes", "No");
            if (result == 0)
            {
                repository.Delete(good.id);
                OnToMainWindowClicked();
            }
        }

        private void OnEditClicked()
        {
            Window editWindow = new GoodEditWindow(good.id, repository);
            Application.Run(editWindow);
            good = repository.GetById(good.id);
            UpdateInfo();
        }

        private void OnToMainWindowClicked()
        {
            Application.Top.RemoveAll();
            Window main = new MainWindow(repository);
            Application.Top.Add(main);
            Application.RequestStop();
            Application.Run();
        }
    }
}
