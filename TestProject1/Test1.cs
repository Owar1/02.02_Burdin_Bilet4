using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using _02._02_Burdin_Bilet4;

namespace TestProject1
{
    [TestClass]
    public sealed class BlindsCalculatorTests
    {
        
        private (bool isValid, string error) ValidateInput(string widthText, string heightText)
        {
      
            if (!double.TryParse(widthText?.Replace('.', ','), out double width))
                return (false, "Ошибка: Введите корректное число для ширины");

            if (!double.TryParse(heightText?.Replace('.', ','), out double height))
                return (false, "Ошибка: Введите корректное число для высоты");

            if (width <= 0 || height <= 0)
                return (false, "Ошибка: Размеры должны быть больше 0");

            return (true, "");
        }

        // Большие числа
        [TestMethod]
        public void Test1_LargeNumbersInput()
        {
            // Arrange 
            string largeNumber = "999999999999999"; // 15 цифр

            // Act
            var result = ValidateInput(largeNumber, "2.5");

            // Assert
            Assert.IsTrue(result.isValid, "Большие числа должны быть валидны");
        }

        // Отрицательные числа  
        [TestMethod]
        public void Test2_NegativeNumbersInput()
        {
            // Act & Assert
            var result1 = ValidateInput("-5.5", "3.0");
            Assert.IsFalse(result1.isValid, "Отрицательная ширина должна быть невалидна");

            var result2 = ValidateInput("2.5", "-3.0");
            Assert.IsFalse(result2.isValid, "Отрицательная высота должна быть невалидна");

            var result3 = ValidateInput("-2.0", "-3.0");
            Assert.IsFalse(result3.isValid, "Оба отрицательных должны быть невалидны");
        }

        // Пустые поля
        [TestMethod]
        public void Test3_EmptyInputFields()
        {
            // Act & Assert
            var result1 = ValidateInput("", "2.5");
            Assert.IsFalse(result1.isValid, "Пустая ширина должна быть невалидна");

            var result2 = ValidateInput("3.0", "");
            Assert.IsFalse(result2.isValid, "Пустая высота должна быть невалидна");

            var result3 = ValidateInput("", "");
            Assert.IsFalse(result3.isValid, "Оба пустых должны быть невалидны");

            var result4 = ValidateInput("abc", "2.5");
            Assert.IsFalse(result4.isValid, "Текст должен быть невалиден");
        }
    }
}