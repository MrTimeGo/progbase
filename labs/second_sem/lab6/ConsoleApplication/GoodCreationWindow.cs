using System;
using ClassLibrary;
using Terminal.Gui;

namespace lab6
{
    class GoodCreationWindow : Window
    {
        GoodRepository repository;

        TextField nameField;
        TextView descriptionField;
        TextField priceField;
        CheckBox availableCheckBox;
        public GoodCreationWindow(GoodRepository repository)
        {
            this.repository = repository;

            Title = "New good";

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
                Width = Dim.Percent(80)
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
                Height = Dim.Fill(10)
            };
            descriptionField = new TextView()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill()
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
                Width = Dim.Percent(80) - 4
            };
            Label uahLabel = new Label("UAH")
            {
                X = Pos.Right(priceField) + 1,
                Y = Pos.Top(priceField)
            };
            availableCheckBox = new CheckBox("Available")
            {
                X = Pos.Left(priceField),
                Y = Pos.Bottom(priceField) + 1,
            };
            Button confirm = new Button("Confirm")
            {
                X = Pos.Center() - 10,
                Y = Pos.Bottom(availableCheckBox) + 2
            };
            Button cancel = new Button("Cancel")
            {
                X = Pos.Right(confirm) + 3,
                Y = Pos.Top(confirm),
            };

            confirm.Clicked += OnConfirmClicked;
            cancel.Clicked += OnCancelClicked;

            this.Add(nameLabel, nameField,
                descriptionLabel, inputWindow,
                priceLabel, priceField, uahLabel,
                availableCheckBox,
                confirm, cancel);
        }

        private void OnCancelClicked()
        {
            Application.Top.RemoveAll();
            Window main = new MainWindow(repository);
            Application.Top.Add(main);
            Application.RequestStop();
            Application.Run();
        }

        private void OnConfirmClicked()
        {
            string name = nameField.Text.ToString();
            string description = descriptionField.Text.ToString();
            string priceString = priceField.Text.ToString();

            if (!double.TryParse(priceString, out double price))
            {
                MessageBox.ErrorQuery("Error", "Price should be number", "Ok");
                return;
            }
            if (name == "")
            {
                MessageBox.ErrorQuery("Error", "Name should be provided", "Ok");
                return;
            }
            if (description == "")
            {
                MessageBox.ErrorQuery("Error", "Description should be provided", "Ok");
                return;
            }

            Good good = new Good()
            {
                name = name,
                description = description,
                price = price,
                isAvailable = availableCheckBox.Checked,
                createdAt = DateTime.Now
            };

            good.id = repository.Insert(good);

            Application.Top.RemoveAll();
            Window viewWindow = new GoodViewWindow(good.id, repository);
            Application.Top.Add(viewWindow);
            Application.RequestStop();
            Application.Run();
        }
    }
}
