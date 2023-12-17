using System.ComponentModel;

namespace BmiCalculator
{
    public partial class MainPage : ContentPage
    {
        private BmiViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new BmiViewModel();
            BindingContext = _viewModel;
        }

        private void CalculateBMI_Pressed(object sender, EventArgs e)
        {
            var heightInMeter = heighSlider.Value / 100;
            var weightInKg = weightSlider.Value;
            _viewModel.Bmi = decimal.Round((decimal)(weightInKg / (heightInMeter * heightInMeter)), 2);
            (_viewModel.BmiStatus, _viewModel.StatusColor) = GetBmiStatus(_viewModel.Bmi);
            _viewModel.IsCalcualted = true;
        }

        private static (string, Color) GetBmiStatus(decimal bmi)
        {
            if (bmi < 18.5m)
            {
                return ("You are Underweight!", Colors.Red);
            }
            else if (bmi <= 24.9m)
            {
                return ("You have a Normal body weight.", Colors.Green);
            }
            else if (bmi <= 29.9m)
            {
                return ("You are Overweight!", Colors.Brown);
            }
            else
            {
                return ("You are Obese!", Colors.Red);
            }
        }

        private void ResetBmi_Pressed(Object sender, EventArgs e)
        {
            heighSlider.Value = 0;
            weightSlider.Value = 0;
            _viewModel.IsCalcualted = false;
        }
    }

    public class BmiViewModel : INotifyPropertyChanged
    {

        private decimal _bmi;

        public decimal Bmi
        {
            get => _bmi;
            set
            {
                _bmi = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bmi)));
            }
        }

        private string _bmiStatus;
        public string BmiStatus
        {
            get => _bmiStatus;
            set
            {
                _bmiStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BmiStatus)));
            }
        }

        private Color _statusColor;
        public Color StatusColor
        {
            get => _statusColor;
            set
            {
                _statusColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusColor)));
            }
        }

        private bool _isCalculated;
        public bool IsCalcualted
        {
            get => _isCalculated;
            set
            {
                _isCalculated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCalcualted)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotCalculated)));
            }
        }

        public bool IsNotCalculated => !IsCalcualted;

        public event PropertyChangedEventHandler? PropertyChanged;
    }

}
