using System;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    public partial class GetRandomGraphForm : MetroFramework.Forms.MetroForm
    {
        // Выбранная кнопка.
        private RadioButton selectedRb = new RadioButton();

        // Количество вершин для графа.
        internal int amount;

        // Флаг, который показывает, отменить генерацию, или нет.
        internal bool cancelGraph = true;
        internal GetRandomGraphForm()
        {
            InitializeComponent();
            selectedRb = null;
            DoingRadioButtonsUnchecked();
            var controls = new RadioButton[]
            {
                radioButton1, radioButton2, radioButton3,
                radioButton4, radioButton5, radioButton6, radioButton7, radioButton8,
                radioButton9, radioButton10, radioButton11, radioButton12, radioButton13,
                radioButton14, radioButton15, radioButton16, radioButton17, radioButton18,
                radioButton19, radioButton20
            };

            RadioButton[] radioButtons = 
            {
                radioButton1, radioButton2, radioButton3,
                radioButton4, radioButton5, radioButton6, radioButton7, radioButton8,
                radioButton9, radioButton10, radioButton11, radioButton12, radioButton13,
                radioButton14, radioButton15, radioButton16, radioButton17, radioButton18,
                radioButton19, radioButton20
            };

            groupBox1.Controls.AddRange(controls);

            foreach (var radioButton in radioButtons)
            {
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
            }
        }

        /// <summary>
        /// Запоминаем выбранную кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Выбранная кнопка.
            var resultRb = sender as RadioButton;

            if (resultRb.Checked)
                selectedRb = resultRb;
        }

        public (DialogResult, bool, int) MyShow()
        {
            var rg = new GetRandomGraphForm
            {
                cancelGraph = true
            };

            return (rg.ShowDialog(), rg.cancelGraph, rg.amount);
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
                var myMessageBox = new MyMessageBox();
                myMessageBox.ShowNothingChoose("Nothing selected");
                return;
            }

            Close();

            // Преобразуем текст кнопки в количество.
            amount = int.Parse(selectedRb.Text);
            selectedRb = null;
            DoingRadioButtonsUnchecked();
        }

        private void DoingRadioButtonsUnchecked()
        {
            RadioButton[] radioButtons =
            {
                radioButton1, radioButton2, radioButton3,
                radioButton4, radioButton5, radioButton6, radioButton7, radioButton8,
                radioButton9, radioButton10, radioButton11, radioButton12, radioButton13,
                radioButton14, radioButton15, radioButton16, radioButton17, radioButton18,
                radioButton19, radioButton20
            };

            foreach (var radioButton in radioButtons)
            {
                radioButton.Checked = false;
            }
        }

        /// <summary>
        /// Устанавливаем соответствующее значение флага и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancelGraph = true;

            DoingRadioButtonsUnchecked();

            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetRandomGraphForm_Load(object sender, EventArgs e)
        {
            Theme = MetroThemeStyle.Dark;
            ControlBox = false;
        }
    }
}