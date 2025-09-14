using System;
using System.Globalization;
using Xamarin.Forms;

namespace EJERCICIO_1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnDetermineDataTypeClicked(object sender, EventArgs e)
        {
            try
            {
                string input = InputField.Text;

                if (string.IsNullOrWhiteSpace(input))
                {
                    ResultLabel.Text = "La entrada no puede estar vacía.";
                    ResultFrame.IsVisible = true;
                    return;
                }

                string resultText = DetermineDataType(input);
                ResultLabel.Text = resultText;
                ResultFrame.IsVisible = true;
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error: {ex.Message}";
                ResultFrame.IsVisible = true;
            }
        }

        private string DetermineDataType(string input)
        {
            // Intenta parsear como int
            if (int.TryParse(input, out _))
                return "El tipo de dato es:\nint";

            // Intenta parsear como double (usando InvariantCulture para '.' y CurrentCulture para ',')
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _) ||
                double.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out _))
                return "El tipo de dato es:\ndouble";

            // Intenta parsear como bool
            if (bool.TryParse(input, out _))
                return "El tipo de dato es:\nbool";

            // Intenta parsear como DateTime
            DateTime parsedDate;
            // Intenta formato específico y luego un parseo más general con la cultura actual
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) ||
                DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate))
            {
                // Día de la semana en el idioma de la cultura actual del dispositivo
                string dayOfWeek = parsedDate.ToString("dddd", CultureInfo.CurrentCulture);
                // Si prefieres siempre en inglés:
                // string dayOfWeek = parsedDate.ToString("dddd", CultureInfo.InvariantCulture);

                return $"El tipo de dato es:\nDateTime\n" +
                       $"Formato detectado: {parsedDate:yyyy-MM-dd}\n" +
                       $"Día de la semana:\n{dayOfWeek}\n" +
                       $"Mes: {parsedDate.Month}\n" +
                       $"Año: {parsedDate.Year}";
            }

            // Si nada de lo anterior funciona, es un string
            return $"El tipo de dato es:\nstring";
        }
    }
}