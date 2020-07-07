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
        LoginInfoContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new LoginInfoContext();
            db.LoginInfos.Load();
            dgMain.ItemsSource=db.LoginInfos.Local.ToBindingList();
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
      
            if (PasBox1.Password == PasBox2.Password && PasBox1.Password != "" && txtLogin.Text != "")
            {
                using (db = new LoginInfoContext())
                {
                    tbSmallInfo.Text = "Создание начато";
                    LoginInfo newUser = new LoginInfo(txtLogin.Text, PasBox1.Password);
                    db.LoginInfos.Add(newUser);
                    db.SaveChanges();
                    tbSmallInfo.Text = "Объекты сохранены";

                }
            }
            else
            {
                tbSmallInfo.Text = "Ошибка в данных";
            }

            #region Попытка прикрутить удаленную БД
            //if (PasBox1.Password == PasBox2.Password && PasBox1.Password != "" && txtLogin.Text != "")
            //{
            //    using (ApplicationContext db = new ApplicationContext())
            //    {
            //        tbSmallInfo.Text = "Создание начато";
            //        LoginInfo newUser = new LoginInfo(txtLogin.Text, PasBox1.Password);

            //        db.LoginInfos.Add(newUser);
            //        db.SaveChanges();
            //        tbSmallInfo.Text = "Объекты сохранены";

            //    }
            //}
            #endregion
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            using (db = new LoginInfoContext())
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

        private void BtnLoadInfo_Click(object sender, RoutedEventArgs e)
        {
            using (db = new LoginInfoContext())
            {
                var loginInfos = db.LoginInfos;
                Console.WriteLine("Список объектов:");
                tbInfo.Text = "Id       Login       Password\n";
                foreach (LoginInfo l in loginInfos)
                {
                    tbInfo.Text += $"Id:{l.Id}      Login:{l.Login}     Password:{l.Password}\n";
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
