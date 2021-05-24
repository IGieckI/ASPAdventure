using System;
using System.IO;
using System.Windows;

namespace GeneratoreIATrainingData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAttacco_Click(object sender, RoutedEventArgs e)
        {
            WriteData("0");
            RandomValue();
        }

        private void btnMagiaAttacco_Click(object sender, RoutedEventArgs e)
        {
            WriteData("0");
            RandomValue();
        }

        private void btnMagiaCura_Click(object sender, RoutedEventArgs e)
        {
            WriteData("0");
            RandomValue();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RandomValue();
        }

        private void WriteData(string output)
        {
            using (StreamWriter sw = new StreamWriter(@"Training Data\Data.txt"))
            {
                sw.WriteLine(txtEnemyHp.Text + "," + txtEnemyMana.Text + "," + txtEnemyAttack.Text + "," + txtEnemyIntelligence.Text + "," + txtPlayerHp.Text + "," + txtPlayerMana.Text + "," + txtPlayerAttack.Text + "," + txtPlayerIntelligence.Text + "," + output);
            }
        }

        private void RandomValue()
        {
            Random rnd = new Random();
            txtPlayerHp.Text = rnd.Next(1, 30).ToString();
            txtPlayerMana.Text = rnd.Next(1, 30).ToString();
            txtPlayerAttack.Text = rnd.Next(1, 30).ToString();
            txtPlayerIntelligence.Text = rnd.Next(1, 30).ToString();

            txtEnemyHp.Text = rnd.Next(1, 30).ToString();
            txtEnemyMana.Text = rnd.Next(1, 30).ToString();
            txtEnemyAttack.Text = rnd.Next(1, 30).ToString();
            txtEnemyIntelligence.Text = rnd.Next(1, 30).ToString();
        }
    }
}
