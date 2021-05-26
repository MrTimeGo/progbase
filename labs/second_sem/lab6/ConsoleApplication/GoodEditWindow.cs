using ClassLibrary;
using Terminal.Gui;

namespace lab6
{
    class GoodEditWindow : Window
    {
        GoodRepository repository;
        Good good;

        TextField nameField;
        TextView descriptionField;
        TextField priceField;
        CheckBox availableCheckBox;
        public GoodEditWindow(long goodId, GoodRepository repository)
        {
            this.repository = repository;
            this.good = repository.GetById(goodId);
            Title = "Good edit";
            Initialize();
        }
        private void Initialize()
        {
            Label nameLabel = new Label("Name: ")
            {
                X = Pos.Percent(10),
                Y = Pos.Percent(10),
            };
            nameField = new TextField(good.name)
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
                Height = Dim.Fill(),
                Text = good.description
            };
            inputWindow.Add(descriptionField);
            Label priceLabel = new Label("Price: ")
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Bottom(inputWindow) + 1,
            };
            priceField = new TextField($"{good.price}")
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
                Checked = good.isAvailable
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

            good.name = name;
            good.description = description;
            good.price = price;
            good.isAvailable = availableCheckBox.Checked;
           
            repository.Edit(good);
            Application.RequestStop();
        }
        private void OnCancelClicked()
        {
            Application.RequestStop();
        }
    }
}
