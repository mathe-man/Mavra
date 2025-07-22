using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;

namespace MavraGUI.Controls
{
    public partial class ScientificFloatInput : UserControl
    {
        public static readonly StyledProperty<string> MantissaProperty =
            AvaloniaProperty.Register<ScientificFloatInput, string>(
                nameof(Mantissa), 
                "0.0",
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<int> ExponentProperty =
            AvaloniaProperty.Register<ScientificFloatInput, int>(
                nameof(Exponent), 
                0,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<float> ValueProperty =
            AvaloniaProperty.Register<ScientificFloatInput, float>(
                nameof(Value), 
                0.0f,
                defaultBindingMode: BindingMode.TwoWay);
        
        public static readonly StyledProperty<string> TextProperty =
	        AvaloniaProperty.Register<ScientificFloatInput, string>(
		        nameof(Text), 
		        "Scientific float",
		        defaultBindingMode: BindingMode.TwoWay);

        public string Mantissa
        {
            get => GetValue(MantissaProperty);
            set => SetValue(MantissaProperty, value);
        }

        public int Exponent
        {
            get => GetValue(ExponentProperty);
            set => SetValue(ExponentProperty, value);
        }

        public float Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public string Text
        {
	        get => GetValue(TextProperty);
	        set => SetValue(TextProperty, value);
        }

        public ScientificFloatInput()
        {
            InitializeComponent();
        }

        private void UpdateValue()
        {
            if (string.IsNullOrEmpty(Mantissa))
	            Mantissa = "0";

            if (float.TryParse(Mantissa, NumberStyles.Float, CultureInfo.InvariantCulture, out float mantissa))
	            Value = mantissa * (float)Math.Pow(10, Exponent);
            
            else
            {
                Mantissa = "0";
                Exponent = 0;
                Value = 0;
            }
        }

        private void MantissaTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
			=> UpdateValue();

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == MantissaProperty || change.Property == ExponentProperty)
	            UpdateValue();
        }

        private bool IsValidKey(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.OemPeriod:
                case Key.Decimal:
                case Key.OemMinus:
                case Key.Subtract:
                    return true;

                case Key.Back:
                case Key.Delete:
                case Key.Left:
                case Key.Right:
                case Key.Home:
                case Key.End:
                    return true;

                default:
                    return false;
            }
        }
    }
}