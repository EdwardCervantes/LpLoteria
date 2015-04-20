using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LenguajeProgramacion
{
    /// <summary>
    /// Lógica de interacción para Loteria.xaml
    /// </summary>
    public partial class Loteria : Window
    {
        public Loteria()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        List<List<Resultado>> resultados;

        private void btnLotoGol_Click(object sender, RoutedEventArgs e)
        {
            //resultados = new List<List<Resultado>>();
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArchivoLotoGol.txt";
            string[] lines = System.IO.File.ReadAllLines(ruta);
            string resultadotexto = "";
            //List<string> resultado = new List<string>();
            double preciototal = 0;
            List<string> r = new List<string>();
            for (int i = 0; i < lines.Count(); i++)
            {
                var line=lines[i];
                var nros = line.Split(' ');
                if (i < lines.Count() - 1)
                {
                    switch (nros[0])
                    {
                        case "1": preciototal += 0.50; break;
                        case "2": preciototal += 1.00; break;
                        case "4": preciototal += 2.00; break;
                    }
                }
                else
                {
                    var wr = line.Split(' ');
                    for (int j = 0; j < wr.Count(); j += 2)                    
                        r.Add(wr[j] + wr[j+1]);
                }
            }
            resultadotexto += "Total acumulado: " + preciototal.ToString() + "\n";
            double premios = (double)Decimal.Round(Convert.ToDecimal(((preciototal * 28) / 104.4)), 2);
            resultadotexto += "Total premios: " + premios.ToString() + "\n";

            int aciertos5 = 0;
            int aciertos4 = 0;
            int aciertos3 = 0;
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                if (i < lines.Count() - 1)
                {
                    List<string> _r = new List<string>();
                    var nros = line.Split(' ');
                    string[] n = new string[]{};
                    for (int j = 1; j < nros.Count(); j += 2)
                        _r.Add(nros[j] + nros[j+1]);

                    var f = _r.Intersect(r);

                    switch (f.Count())
                    {
                        case 5: aciertos5 += 1; break;
                        case 4: aciertos4 += 1; break;
                        case 3: aciertos3 += 1; break;
                    }
                    //resultados.Add(_r);
                }
            }
            double premio5 = (double)Decimal.Round((Convert.ToDecimal((premios * 40) / 100) / aciertos5), 2);
            double premio4 = (double)Decimal.Round((Convert.ToDecimal((premios * 30) / 100) / aciertos4), 2);
            double premio3 = (double)Decimal.Round((Convert.ToDecimal((premios * 30) / 100) / aciertos3), 2);
            resultadotexto += "1ro " + aciertos5.ToString() + " 5 aciertos " + premio5.ToString() + "\n";
            resultadotexto += "2do " + aciertos4.ToString() + " 4 aciertos " + premio4.ToString() + "\n";
            resultadotexto += "3ro " + aciertos3.ToString() + " 3 aciertos " + premio3.ToString() + "\n";
            txtResultadosLotoGol.Text = resultadotexto;
        }

        private void btnQuina_Click(object sender, RoutedEventArgs e)
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArchivoQuina.txt";
            string[] lines = System.IO.File.ReadAllLines(ruta);
            string resultado = "";
            double preciototal = 0;
            string[] r = new string[] { };
            for(int i=0;i<lines.Count();i++)
            {
                var line=lines[i];
                var nros = line.Split(' ');
                if (i < lines.Count() - 1)
                {
                    switch (nros.Count())
                    {
                        case 5: preciototal += 0.75; break;
                        case 6: preciototal += 3.00; break;
                        case 7: preciototal += 7.50; break;
                    }
                }
                else
                    r = line.Split(' ');                    
            }
            
            resultado += "Total acumulado: "+preciototal.ToString() + "\n";
            double premios = (double)Decimal.Round(Convert.ToDecimal(((preciototal * 32.20) / 104.5)), 2);
            resultado += "Total premios: "+premios.ToString() + "\n";
            int aciertos5 = 0;
            int aciertos4 = 0;
            int aciertos3 = 0;
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                if (i < lines.Count() - 1)
                {
                    var nros = line.Split(' ');
                    var f = nros.Intersect(r);
                    switch (f.Count())
                    {
                        case 5: aciertos5 += 1; break;
                        case 4: aciertos4 += 1; break;
                        case 3: aciertos3 += 1; break;
                    }
                }
            }
            double premio5 = (double)Decimal.Round((Convert.ToDecimal((premios * 35) / 100) / aciertos5), 2);
            double premio4 = (double)Decimal.Round((Convert.ToDecimal((premios * 25) / 100) / aciertos4), 2);
            double premio3 = (double)Decimal.Round((Convert.ToDecimal((premios * 25) / 100) / aciertos3), 2);
            resultado += "Quina " + aciertos5.ToString()+" 5 aciertos "+premio5.ToString() + "\n";
            resultado += "Cuadra " + aciertos4.ToString() + " 4 aciertos "+premio4.ToString() + "\n";
            resultado += "Terno " + aciertos3.ToString() + " 3 aciertos "+premio3.ToString() + "\n";
            txtResultadosQuina.Text = resultado;
        }
    }
}
