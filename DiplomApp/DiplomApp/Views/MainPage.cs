using Acr.UserDialogs;
using CommonClasses.Forms.Helpers;
using CommonClasses.ViewModels;
using DiplomApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace DiplomApp.Views
{
    public class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;
        public MainPage()
        {
            _viewModel=ViewModelProvider.GetViewModel<MainPageViewModel>();
            BindingContext = _viewModel;

            var l1Label = new Label
            {
                Text = "слой 1",
                VerticalOptions = LayoutOptions.Center
            };
            var l2Label = new Label
            {
                Text = "слой 2",
                VerticalOptions = LayoutOptions.Center
            };
            var l3Label = new Label
            {
                Text = "слой 3",
                VerticalOptions = LayoutOptions.Center
            };
            var l4Label = new Label
            {
                Text = "основание",
                VerticalOptions = LayoutOptions.Center
            };

            var hLabel = new Label
            {
                Text = "Толщина, см",
                VerticalOptions = LayoutOptions.Center
            };
            var eLabel = new Label
            {
                Text = "Модуль упругости, МПа",
                VerticalOptions = LayoutOptions.Center
            };

            var h1Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            h1Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.H1));
            var h2Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            h2Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.H2));
            var h3Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            h3Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.H3));

            var e1Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            e1Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.E1));
            var e2Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            e2Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.E2));
            var e3Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            e3Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.E3));
            var e4Entry = new Entry()
            {
                Keyboard = Keyboard.Numeric
            };
            e4Entry.SetBinding(Entry.TextProperty, nameof(_viewModel.E4));

            var calcButton = new Button
            {
                Text = "Расчет"
            };
            calcButton.Clicked += CalcButton_Clicked;

            var dataGrid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                Children =
                {
                    {hLabel,1,0 },
                    {eLabel,2,0 },

                    {l1Label,0,1 },
                    {h1Entry,1,1 },
                    {e1Entry,2,1 },

                    {l2Label,0,2 },
                    {h2Entry,1,2 },
                    {e2Entry,2,2 },

                    {l3Label,0,3 },
                    {h3Entry,1,3 },
                    {e3Entry,2,3 },

                    {l4Label,0,4 },
                    {e4Entry,2,4 }
                }
            };
            Content = new StackLayout
            {
                Padding=new Thickness(10,Device.OnPlatform(20,5,5),10,5),
                Children =
                {
                    new Label
                    {
                        Text = "Расчет дорожной одежды",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    dataGrid,
                    calcButton
                }
            };
        }

        private async void CalcButton_Clicked(object sender, EventArgs e)
        {
            DialogsHelper.ShowLoading("Проверка соединения...");
            var res = await _viewModel.Calculate();
            DialogsHelper.HideLoading();
            if (res)
            {
                await Navigation.PushModalAsync(new ResultsPage());
            } 
        }
    }
}
