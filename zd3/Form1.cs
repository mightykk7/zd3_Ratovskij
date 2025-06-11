using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace zd3
{
    public partial class Form1 : Form
    {
        private RoadWorkManager workManager = new RoadWorkManager();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            var results = workManager.SearchWorks(searchText);

            listBox1.Items.Clear();
            foreach (var work in results)
            {
                listBox1.Items.Add(work);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Введите название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var work = new EnhancedRoadWork(
                    width: (double)numericUpDown1.Value,
                    length: (double)numericUpDown2.Value,
                    weight: (double)numericUpDown3.Value,
                    workName: textBox1.Text,
                    startDate: DateTime.Now,
                    strengthCoefficient: 5,
                    contractor: "",
                    isCompleted: false
                );
                workManager.AddWork(work);
                textBox1.Text = "";
                UpdateListBox();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода данных: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                workManager.RemoveWork((RoadWork)listBox1.SelectedItem);
                UpdateListBox();
                UpdateStatistics();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Введите название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var work = new RoadWork(
                    width: (double)numericUpDown1.Value,
                    length: (double)numericUpDown2.Value,
                    weight: (double)numericUpDown3.Value,
                    workName: textBox1.Text,
                    startDate: DateTime.Now
                );
                workManager.AddWork(work);
                textBox1.Text = "";
                UpdateListBox();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода данных: {ex.Message}");
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            foreach (var work in workManager.GetAllWorks())
            {
                listBox1.Items.Add(work);
            }
        }

        private void UpdateStatistics()
        {
            var stats = workManager.GetStatistics();
            label1.Text = $"Всего работ: {stats.totalWorks}";
            label2.Text = $"Улучшенных работ: {stats.enhancedWorks}";

            if (stats.totalWorks > 0)
            {
                label3.Text = $"Качество: среднее {stats.avgQuality:F2}, макс {stats.maxQuality:F2}, мин {stats.minQuality:F2}";
                label4.Text = $"Суммарное качество: {stats.totalQuality:F2}";
            }
            else
            {
                label3.Text = "Качество: среднее 0, макс 0, мин 0";
                label4.Text = "Суммарное качество: 0";
            }
        }
    }
}
