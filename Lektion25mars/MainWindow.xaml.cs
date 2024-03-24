using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lektion25mars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _filtext;
        public string FilText
        {
            get { return _filtext; }
            set
            {
                if (_filtext != value)
                {
                    _filtext = value;
                    OnPropertyChanged(nameof(FilText));
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            FilText = "hej";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.DefaultExt = ".txt";


                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                FilText = fileContent;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(false);
        }
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(false);
        }
        private void SaveFile(bool saveAs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveAs || string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                if (saveFileDialog.ShowDialog() != true)
                {
                    return;
                }
            }

            string filePath = saveFileDialog.FileName;
            string fileContent = FilText;
            File.WriteAllText(filePath, fileContent);
        }
    }
}