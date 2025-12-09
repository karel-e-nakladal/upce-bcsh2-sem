using System.ComponentModel;
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
using CommunityToolkit.Mvvm.ComponentModel;
using SQLitePCL;
using WpfApp1.Database;
using WpfApp1.View;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public static MainWindow Instance;
    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
        MainFrame.Navigate(new WorldListView());
    }

}