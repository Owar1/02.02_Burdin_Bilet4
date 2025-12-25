using System;
using System.IO;
using System.Windows;

namespace _02._02_Burdin_Bilet4
{
    public partial class MainWindow : Window
    {
        private const double ALUMINUM_PRICE = 15.50;
        private const double PLASTIC_PRICE = 9.90;
        private double width = 0;
        private double height = 0;
        private double totalCost = 0;
        private string material = "Алюминий";
        private double pricePerM2 = 15.50;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка ввода ширины
                if (!double.TryParse(txtWidth.Text.Replace('.', ','), out width))
                {
                    MessageBox.Show("Ошибка: Введите корректное число для ширины", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка ввода высоты
                if (!double.TryParse(txtHeight.Text.Replace('.', ','), out height))
                {
                    MessageBox.Show("Ошибка: Введите корректное число для высоты", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка на положительные значения
                if (width <= 0 || height <= 0)
                {
                    MessageBox.Show("Ошибка: Размеры должны быть больше 0", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Определение материала и цены
                if (rbAluminum.IsChecked == true)
                {
                    material = "Алюминий";
                    pricePerM2 = ALUMINUM_PRICE;
                }
                else
                {
                    material = "Пластик";
                    pricePerM2 = PLASTIC_PRICE;
                }

                // Расчет стоимости
                double area = width * height;
                totalCost = Math.Round(area * pricePerM2, 2);

                // Вывод результата на форму 
                txtResult.Text = $"РАЗМЕР: {width:F2} м × {height:F2} м\n" +
                                $"МАТЕРИАЛ: {material}\n" +
                                $"СТОИМОСТЬ: {totalCost:F2} рублей\n\n" +
                                $"Детали:\n" +
                                $"• Площадь: {area:F2} м²\n" +
                                $"• Цена за м²: {pricePerM2:F2} руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчете: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnGenerateReceipt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка, был ли выполнен расчет
                if (totalCost == 0)
                {
                    MessageBox.Show("Сначала выполните расчет стоимости (нажмите 'Рассчитать')",
                        "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Генерация уникального номера чека (6 цифр)
                string receiptNumber = new Random().Next(100000, 999999).ToString();

                // Форматирование даты для имени файла
                string dateForFileName = DateTime.Now.ToString("ddMMyyyy");

                // Форматирование стоимости для имени файла 
                string costForFileName = totalCost.ToString("F0");

                // Создание имени файла по шаблону: Уникальный номер чека_дата_стоимость
                string fileName = $"{receiptNumber}_{dateForFileName}_{costForFileName}.txt";

                // Путь для сохранения 
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, fileName);

          
                string receiptText = GenerateReceiptText(receiptNumber);

                // Сохранение текстового документа
                File.WriteAllText(filePath, receiptText, System.Text.Encoding.UTF8);

                // Обновление информации на форме
                txtReceiptInfo.Text = $" Чек сохранен!\n" +
                                     $"Файл: {fileName}\n" +
                                     $"Путь: {desktopPath}\n" +
                                     $"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}";

                MessageBox.Show($"Квитанция успешно сохранена!\n\n{fileName}",
                    "Квитанция оформлена", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании квитанции:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateReceiptText(string receiptNumber)
        {
            double area = width * height;

            return $@"ООО ""Уютный Дом""

Добро пожаловать

ККМ 00075411     #3969
ИНН 1087746942040
ЭКЛЗ 3851495566
Чек №{receiptNumber}
{DateTime.Now:dd.MM.yy HH:mm} СИС.

=====================================
наименование товара: жалюзи
размер: {width:F2} x {height:F2} м
материал: {material}
площадь: {area:F2} м²
цена за м²: {pricePerM2:F2} руб.
=====================================
Итог: {totalCost:F2} руб.
Сдача: 0 руб.
Сумма итого: {totalCost:F2} руб.
=====================================

      00003751# 059705
";
        }

       

  
      

       
    
    }
}