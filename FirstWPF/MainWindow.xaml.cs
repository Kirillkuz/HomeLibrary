using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.Entity;

namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword1.Text == txtPassword2.Text && txtPassword1.Text != "" && txtLogin.Text != "")
            {
                using (LoginInfoContext db = new LoginInfoContext())
                {
                    tbSmallInfo.Text = "Создание начато";
                    LoginInfo newUser = new LoginInfo(txtLogin.Text, txtPassword1.Text);
                    db.LoginInfos.Add(newUser);
                    db.SaveChanges();
                    tbSmallInfo.Text = "Объекты сохранены";

                }
            }
            else
            {
                tbSmallInfo.Text = "Ошибка в данных";
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            using (LoginInfoContext db = new LoginInfoContext())
            {
                var loginInfos = db.LoginInfos;
                Console.WriteLine("Список объектов:");
                tbInfo.Text = "Id       Login       Password\n";
                foreach (LoginInfo l in loginInfos)
                {
                    tbInfo.Text +=$"Id:{l.Id}      Login:{l.Login}     Password:{l.Password}\n";
                }
            }
        }
    }
}
