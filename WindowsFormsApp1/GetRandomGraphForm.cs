using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GetRandomGraphForm : Form
    {
        // Выбранная кнопка.
        RadioButton selectedRb = new RadioButton();

        // Количество вершин для графа.
        internal int amount;

        // Флаг, который показывает, отменить генерацию, или нет.
        internal bool cancelGraph = true;
        internal GetRandomGraphForm()
        {
            InitializeComponent();

            groupBox1.Controls.AddRange(new[]
            {
                radioButton1, radioButton2, radioButton3,
                radioButton4, radioButton5, radioButton6, radioButton7, radioButton8,
                radioButton9, radioButton10, radioButton11, radioButton12, radioButton13,
                radioButton14, radioButton15, radioButton16, radioButton17, radioButton18,
                radioButton19, radioButton20});

            radioButton1.CheckedChanged += radioButton_CheckedChanged;
            radioButton2.CheckedChanged += radioButton_CheckedChanged;
            radioButton3.CheckedChanged += radioButton_CheckedChanged;
            radioButton4.CheckedChanged += radioButton_CheckedChanged;
            radioButton5.CheckedChanged += radioButton_CheckedChanged;

            radioButton6.CheckedChanged += radioButton_CheckedChanged;
            radioButton7.CheckedChanged += radioButton_CheckedChanged;
            radioButton8.CheckedChanged += radioButton_CheckedChanged;
            radioButton9.CheckedChanged += radioButton_CheckedChanged;
            radioButton10.CheckedChanged += radioButton_CheckedChanged;

            radioButton11.CheckedChanged += radioButton_CheckedChanged;
            radioButton12.CheckedChanged += radioButton_CheckedChanged;
            radioButton13.CheckedChanged += radioButton_CheckedChanged;
            radioButton14.CheckedChanged += radioButton_CheckedChanged;
            radioButton15.CheckedChanged += radioButton_CheckedChanged;

            radioButton16.CheckedChanged += radioButton_CheckedChanged;
            radioButton17.CheckedChanged += radioButton_CheckedChanged;
            radioButton18.CheckedChanged += radioButton_CheckedChanged;
            radioButton19.CheckedChanged += radioButton_CheckedChanged;
            radioButton20.CheckedChanged += radioButton_CheckedChanged;
        }

        /// <summary>
        /// Запоминаем выбранную кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Выбранная кнопка.
            RadioButton resultRb = sender as RadioButton;

            if (resultRb.Checked)
                selectedRb = resultRb;
        }

        /// <summary>
        /// Возвращаем количество вершин для графа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            amount = 0;
            cancelGraph = false;

            // Случай, когда ничего не выбрано.
            if (selectedRb is null)
            {
                MessageBox.Show("Выберите что-нибудь");
                return;
            }

            Close();

            // Преобразуем текст кнопки в количество.
            amount = int.Parse(selectedRb.Text);

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;

            radioButton6.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
            radioButton9.Checked = false;
            radioButton10.Checked = false;

            radioButton11.Checked = false;
            radioButton12.Checked = false;
            radioButton13.Checked = false;
            radioButton14.Checked = false;
            radioButton15.Checked = false;

            radioButton16.Checked = false;
            radioButton17.Checked = false;
            radioButton18.Checked = false;
            radioButton19.Checked = false;
            radioButton20.Checked = false;
        }

        /// <summary>
        /// Устанавливаем соответствующее значение флага и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancelGraph = true;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;

            radioButton6.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
            radioButton9.Checked = false;
            radioButton10.Checked = false;

            radioButton11.Checked = false;
            radioButton12.Checked = false;
            radioButton13.Checked = false;
            radioButton14.Checked = false;
            radioButton15.Checked = false;

            radioButton16.Checked = false;
            radioButton17.Checked = false;
            radioButton18.Checked = false;
            radioButton19.Checked = false;
            radioButton20.Checked = false;

            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetRandomGraphForm_Load(object sender, EventArgs e) =>
            ControlBox = false;
    }
}