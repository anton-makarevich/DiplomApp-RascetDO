using CommonClasses;
using CommonClasses.Forms.Helpers;
using CommonClasses.Services.ApiClient;
using CommonClasses.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomApp.ViewModel
{
    public class MainPageViewModel:BaseViewModel
    {
        #region Fields
        private PavementModel _model;
        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            _model = new PavementModel
            {
                H1 = 3,
                H2 = 4,
                H3 = 6,
                E1 = 1000,
                E2 = 2000,
                E3 = 3000,
                E4 = 300
            };
        }
        #endregion

        #region Properties
        public double H1
        {
            get
            {
                return _model.H1;
            }
            set
            {
                _model.H1 = value;
                OnPropertyChanged();
            }
        }
        public double H2
        {
            get
            {
                return _model.H2;
            }
            set
            {
                _model.H2 = value;
                OnPropertyChanged();
            }
        }
        public double H3
        {
            get
            {
                return _model.H3;
            }
            set
            {
                _model.H3 = value;
                OnPropertyChanged();
            }
        }
        public double E1
        {
            get
            {
                return _model.E1;
            }
            set
            {
                _model.E1 = value;
                OnPropertyChanged();
            }
        }
        public double E2
        {
            get
            {
                return _model.E2;
            }
            set
            {
                _model.E2 = value;
                OnPropertyChanged();
            }
        }
        public double E3
        {
            get
            {
                return _model.E3;
            }
            set
            {
                _model.E3 = value;
                OnPropertyChanged();
            }
        }
        public double E4
        {
            get
            {
                return _model.E4;
            }
            set
            {
                _model.E4 = value;
                OnPropertyChanged();
            }
        }

        public double S1
        {
            get
            {
                return _model.Szim1;
            }
            private set
            {
                _model.Szim1 = value;
                OnPropertyChanged();
            }
        }
        public double S2
        {
            get
            {
                return _model.Szim2;
            }
            private set
            {
                _model.Szim2 = value;
                OnPropertyChanged();
            }
        }
        public double S3
        {
            get
            {
                return _model.Szim3;
            }
            private set
            {
                _model.Szim3 = value;
                OnPropertyChanged();
            }
        }

        public double N1
        {
            get
            {
                return _model.Sigma1;
            }
            private set
            {
                _model.Sigma1 = value;
                OnPropertyChanged();
            }
        }
        public double N2
        {
            get
            {
                return _model.Sigma2;
            }
            private set
            {
                _model.Sigma2 = value;
                OnPropertyChanged();
            }
        }
        public double N3
        {
            get
            {
                return _model.Sigma3;
            }
            private set
            {
                _model.Sigma3 = value;
                OnPropertyChanged();
            }
        }

        public double T1
        {
            get
            {
                return _model.Tau1;
            }
            private set
            {
                _model.Tau1 = value;
                OnPropertyChanged();
            }
        }
        public double T2
        {
            get
            {
                return _model.Tau2;
            }
            private set
            {
                _model.Tau2 = value;
                OnPropertyChanged();
            }
        }
        public double T3
        {
            get
            {
                return _model.Tau3;
            }
            private set
            {
                _model.Tau3 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public async Task<bool> Calculate()
        {
            await Task.Delay(700);
            DialogsHelper.UpdateLoading("Расчет модели на сервере");
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    await Task.Delay(700);
                    var response = await DiplomApiClient.Instance.CalculateAsync(_model);

                    DialogsHelper.UpdateLoading("Обработка результатов");
                    await Task.Delay(700);

                    if (response?.Result != null)
                    {
                        _model = response.Result;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                await DialogsHelper.ShowMessageAsync("Расчет не выполнен. Проверьте соединение с интернетом.");
            }
            return false;
        }
        #endregion
    }
}
