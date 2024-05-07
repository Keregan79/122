using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGameKuksheva
{
    public partial class Form1 : Form
    {
        // объявляем переменную  как класс случайных чисел
        Random random = new Random();

        // обьявлем список элементов из символьных значений 16 штук
        List<string> icons = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // метки для 1 и 2 щелчка
        Label firstClicked = null;
        Label secondClicked = null;

        public Form1()
        {
            InitializeComponent();
            //метод подготовки первоначальной карты
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            // перебор 16 элементов
            foreach (Control control in tableLayoutPanel1.Controls)
            {

                Label iconLabel = control as Label;
                if (iconLabel != null)
                {// назначение элементам символа и закрашивание
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            // если таймер активен
            if (timer1.Enabled == true)
                return;
            // начинаем работать с метками
            Label clickedLabel = sender as Label;

            //если метка не пустая
            if (clickedLabel != null)
            {
               // если метка открыта
                if (clickedLabel.ForeColor == Color.Black)
                    return;


                // если пользователь открыл 1 метку
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // пользователь  2 метку
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // вызываем метод проверки ответов
                CheckForWinner();

                // если метки совпали
                if (firstClicked.Text == secondClicked.Text)
                {
                    /// оставляем открытыми
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                // активируем таймер 
                timer1.Start();


            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // таймер останавливаем
            timer1.Stop();

            // открытую 1  метку скрываем
            firstClicked.ForeColor = firstClicked.BackColor;
            // открытую 2 метку скрываем
            secondClicked.ForeColor = secondClicked.BackColor;
                    // значения меток сбрасываем для следующего выбора   
            firstClicked = null;
            secondClicked = null;
        }
        // метод для проверки открыты все метки
        private void CheckForWinner()
        {
            // перебираем все метки
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                // назначаем всем меткам управление
                Label iconLabel = control as Label;
                // если метка не пуста
                if (iconLabel != null)
                {
                    // если метка совпадает цвет шрифта с цветом фона 
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        // показываем карту
                        return;
                }
            }


            // образуется сообщение
            MessageBox.Show("Вы раскрыли все карты!", "Поздравляем!");
            Close();
        }
    }
}
