using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Lab4Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string formattedDate = DateTime.Now.ToString("dd/MM/yyyy");
            string url = "https://www.cbr.ru/scripts/XML_daily.asp?date_req=" + formattedDate; 
            
            string usd = null; //доллар
            string eur = null; //евро
            string tenge = null; //Казахстанских тенге
            string qar = null; //Катарский риал
            //я знаю, что можно сделать словарь, в который можно перегнать xml, чтобы можно было выбирать валюту, а не использовать предустановленные, но мне лень :) Мне был интересен сам факт получения данных ЦБ на дату вызова приложения
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(url).Result;
                XDocument request = XDocument.Parse(response);
                usd = (string)request.Descendants("Valute")
                                        .Where(e => (string)e.Attribute("ID") == "R01235")
                                        .Elements("Value")
                                        .FirstOrDefault();
                eur = (string)request.Descendants("Valute")
                                        .Where(e => (string)e.Attribute("ID") == "R01239")
                                        .Elements("Value")
                                        .FirstOrDefault();
                tenge = (string)request.Descendants("Valute")
                                        .Where(e => (string)e.Attribute("ID") == "R01335")
                                        .Elements("Value")
                                        .FirstOrDefault();
                qar = (string)request.Descendants("Valute")
                                        .Where(e => (string)e.Attribute("ID") == "R01355")
                                        .Elements("Value")
                                        .FirstOrDefault();
            }
            currentUSD.Content = "Курс ЦБ на " + formattedDate;
            exchangeUSD.Text = usd.Replace(',','.');
            currentEUR.Content = "Курс ЦБ на " + formattedDate;
            exchangeEUR.Text = eur.Replace(',', '.');
            currenttenge.Content = "Курс ЦБ на " + formattedDate;
            exchangetenge.Text = tenge.Replace(',', '.');
            currentqar.Content = "Курс ЦБ на " + formattedDate;
            exchangeqar.Text = qar.Replace(',', '.');
        }

        private void convertUSD_Click(object sender, RoutedEventArgs e)
        {
            exchangedUSD.Text = Convert.ToString(Convert.ToDouble(exchangeUSD.Text)* Convert.ToDouble(exchangableUSD.Text));
        }
        private void convertEUR_Click(object sender, RoutedEventArgs e)
        {
            exchangedEUR.Text = Convert.ToString(Convert.ToDouble(exchangeEUR.Text) * Convert.ToDouble(exchangableEUR.Text));
        }

        private void converttenge_Click(object sender, RoutedEventArgs e)
        {
            exchangedtenge.Text = Convert.ToString(Convert.ToDouble(exchangetenge.Text) * Convert.ToDouble(exchangabletenge.Text));
        }
        private void convertqar_Click(object sender, RoutedEventArgs e)
        {
            exchangedqar.Text = Convert.ToString(Convert.ToDouble(exchangeqar.Text) * Convert.ToDouble(exchangableqar.Text));
        }
        private void firstBoxConvert_Click(object sender, RoutedEventArgs e)
        {
            firstBoxReslut.Text = Convert.ToString(Convert.ToDouble(firstBox.Text) * 0.0254);
        }
        private void secondBoxConvert_Click(object sender, RoutedEventArgs e)
        {
            secondBoxReslut.Text = Convert.ToString(Convert.ToDouble(secondBox.Text) * 0.3048);
        }
        private void thirdBoxConvert_Click(object sender, RoutedEventArgs e)
        {
            thirdBoxReslut.Text = Convert.ToString(Convert.ToDouble(thirdBox.Text) * 1609.34);
        }
        private void fourthBoxConvert_Click(object sender, RoutedEventArgs e)
        {
            fourthBoxReslut.Text = Convert.ToString(Convert.ToDouble(fourthBox.Text) * 1066.8);
        }

    }


}

