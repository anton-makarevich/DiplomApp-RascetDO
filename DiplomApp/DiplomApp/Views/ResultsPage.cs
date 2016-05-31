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
    public class ResultsPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;
        public ResultsPage()
        {
            _viewModel=ViewModelProvider.GetViewModel<MainPageViewModel>();
            BindingContext = _viewModel;

            var l1Label = new Label
            {
                Text = "слой 1"
            };
            var l2Label = new Label
            {
                Text = "слой 2"
            };
            var l3Label = new Label
            {
                Text = "слой 3"
            };

            var dLabel = new Label
            {
                Text = "прогиб, мм"
            };


            var titleLabel = new Label
            {
                Text = "Максимальные напряжения, МПа",
                HorizontalOptions = LayoutOptions.Center
            };
            var sLabel = new Label
            {
                Text = "сжимающие"
            };
            var nLabel = new Label
            {
                Text = "нормальные"
            };
            var tLabel = new Label
            {
                Text = "касательные"
            };

            var rLabel = new Label
            {
                Text = "растягивающие"
            };

            var s1Label = new Label();
            s1Label.SetBinding(Label.TextProperty, nameof(_viewModel.S1));
            var s2Label = new Label();
            s2Label.SetBinding(Label.TextProperty, nameof(_viewModel.S2));
            var s3Label = new Label();
            s3Label.SetBinding(Label.TextProperty, nameof(_viewModel.S3));

            var n1Label = new Label();
            n1Label.SetBinding(Label.TextProperty, nameof(_viewModel.N1));
            var n2Label = new Label();
            n2Label.SetBinding(Label.TextProperty, nameof(_viewModel.N2));
            var n3Label = new Label();
            n3Label.SetBinding(Label.TextProperty, nameof(_viewModel.N3));

            var t1Label = new Label();
            t1Label.SetBinding(Label.TextProperty, nameof(_viewModel.T1));
            var t2Label = new Label();
            t2Label.SetBinding(Label.TextProperty, nameof(_viewModel.T2));
            var t3Label = new Label();
            t3Label.SetBinding(Label.TextProperty, nameof(_viewModel.T3));

            var r1Label = new Label();
            r1Label.SetBinding(Label.TextProperty, nameof(_viewModel.R1));
            var r2Label = new Label();
            r2Label.SetBinding(Label.TextProperty, nameof(_viewModel.R2));
            var r3Label = new Label();
            r3Label.SetBinding(Label.TextProperty, nameof(_viewModel.R3));

            var d1Label = new Label();
            d1Label.SetBinding(Label.TextProperty, nameof(_viewModel.D));

            var calcButton = new Button
            {
                Text = "Назад"
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
                    new RowDefinition(),
                    new RowDefinition()
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                Children =
                {
                    {titleLabel,1,5,0,1 },
                    {sLabel,1,1 },
                    {nLabel,2,1 },
                    {tLabel,3,1 },
                    {rLabel,4,1 },

                    {l1Label,0,2 },
                    {s1Label,1,2 },
                    {n1Label,2,2 },
                    {t1Label,3,2 },
                    {r1Label,4,2 },

                    {l2Label,0,3 },
                    {s2Label,1,3 },
                    {n2Label,2,3 },
                    {t2Label,3,3 },
                    {r2Label,4,3 },

                    {l3Label,0,4 },
                    {s3Label,1,4 },
                    {n3Label,2,4 },
                    {t3Label,3,4 },
                    {r3Label,4,4 },

                    {dLabel,0,5 },
                    {d1Label,1,5 },
                }
            };
            Content = new StackLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 5, 5), 10, 5),
                Children =
                {
                    new Label
                    {
                        Text = "Результаты расчета",
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
            await Navigation.PopModalAsync();
        }
    }
}
