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
using System.Data;

namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibraryContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new LibraryContext();
            
            LoadLoginInfo();
            
        }



        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            using (db = new LibraryContext())
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
            using (db = new LibraryContext())
            {
                LoadLoginInfo();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LoadLoginInfo()
        {
            db.LoginInfos.Load();
            dgMain.ItemsSource = db.LoginInfos.Local.ToBindingList();
        }

        private void BtnAddLogin_Click(object sender, RoutedEventArgs e)
        {
            UserChangeWindow loginForm = new UserChangeWindow();
            db.Roles.Load();
            loginForm.cmbRole.ItemsSource = db.Roles.Local;
            loginForm.ShowDialog();
            if (loginForm.DialogResult==false)
                return;

            LoginInfo login = new LoginInfo();
            login.Login = loginForm.txtLogin.Text;
            login.Password = loginForm.txtPassword.Text;
            login.Role = (Role)loginForm.cmbRole.SelectedItem;

            db.LoginInfos.Add(login);
            db.SaveChanges();

            MessageBox.Show("Новый объект добавлен");
        }

        private void BtnChangeLogin_Click(object sender, RoutedEventArgs e)
        {
            if (dgMain.SelectedIndex > -1)
            {
                int index = dgMain.SelectedIndex;
                int id = 0;
                var firstSelectedCellContent = dgMain.Columns[0].GetCellContent(this.dgMain.SelectedItem);
                DataGridCell firstSelectedCell = firstSelectedCellContent != null ? firstSelectedCellContent.Parent as DataGridCell : null;
                TextBlock textBlock =(TextBlock)firstSelectedCell.Content;
                bool converted = Int32.TryParse(textBlock.Text, out id);
                if (converted == false)
                    return;

                LoginInfo login = db.LoginInfos.Find(id);
                

                UserChangeWindow loginForm = new UserChangeWindow();
                db.Roles.Load();
                loginForm.cmbRole.ItemsSource = db.Roles.Local;
                loginForm.txtLogin.Text = login.Login;
                loginForm.txtPassword.Text = login.Password;
                //loginForm.cmbRole.SelectedItem =loginForm.cmbRole.Items  login.Role;

                foreach (object role in loginForm.cmbRole.Items)
                {
                    if (role.Equals((object)login.Role))
                        loginForm.cmbRole.SelectedItem = role;
                }

                loginForm.ShowDialog();
                if (loginForm.DialogResult == false)
                    return;

                login.Login = loginForm.txtLogin.Text;
                login.Password = loginForm.txtPassword.Text;
                login.Role = db.Roles.Find(loginForm.cmbRole.SelectedValue);

                db.SaveChanges();
                //dataGridView1.Refresh(); // обновляем грид
                MessageBox.Show("Объект обновлен");

            }
        }

        private void BtnDeleteLogin_Click(object sender, RoutedEventArgs e)
        {
            if (dgMain.SelectedIndex > -1)
            {
                int index = dgMain.SelectedIndex;
                int id = 0;
                var firstSelectedCellContent = dgMain.Columns[0].GetCellContent(this.dgMain.SelectedItem);
                DataGridCell firstSelectedCell = firstSelectedCellContent != null ? firstSelectedCellContent.Parent as DataGridCell : null;
                TextBlock textBlock = (TextBlock)firstSelectedCell.Content;
                bool converted = Int32.TryParse(textBlock.Text, out id);
                if (converted == false)
                    return;

                UserProfile userProfile = db.UserProfiles.Find(id);
                db.UserProfiles.Remove(userProfile);
                db.SaveChanges();

                LoginInfo login = db.LoginInfos.Find(id);
                db.LoginInfos.Remove(login);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        private void DgMain_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int index = dgMain.SelectedIndex;
            int id = 0;
            var firstSelectedCellContent = dgMain.Columns[0].GetCellContent(this.dgMain.SelectedItem);
            DataGridCell firstSelectedCell = firstSelectedCellContent != null ? firstSelectedCellContent.Parent as DataGridCell : null;
            TextBlock textBlock = (TextBlock)firstSelectedCell.Content;
            bool converted = Int32.TryParse(textBlock.Text, out id);
            if (converted == false)
                return;
                        
            UserProfile userProfile = db.UserProfiles.Find(id);
            LoginInfo loginInfo = db.LoginInfos.Find(id);
            btnSaveProfileInfo.Tag = id;
            if (userProfile==null)
            {                
                tbSmallInfo.Text = "Пользователь с логином: "+loginInfo.Login+" не имеет анкеты";
                btnSaveProfileInfo.Content = "Добавить";
                txtName.Text = "";
                txtAge.Text = "";

            }
            else
            {
                tbSmallInfo.Text = "Анкета пользователя с логином: " + loginInfo.Login;
                txtName.Text = userProfile.Name;
                txtAge.Text = userProfile.Age.ToString();
                btnSaveProfileInfo.Content = "Сохранить";
            }
        }

        private void BtnSaveProfileInfo_Click(object sender, RoutedEventArgs e)
        {
            UserProfile profile;
            if (btnSaveProfileInfo.Content.ToString() == "Добавить")
            {
                profile = new UserProfile { Id = Convert.ToInt32(btnSaveProfileInfo.Tag), Age = Convert.ToInt32(txtAge.Text), Name = txtName.Text };
                db.UserProfiles.Add(profile);
                db.SaveChanges();
            }
            else
            {
                profile = db.UserProfiles.FirstOrDefault(p => p.LoginInfo.Id == Convert.ToInt32(btnSaveProfileInfo.Tag));
                
                if (profile != null)
                {
                    profile.Name = txtName.Text;
                    profile.Age = Convert.ToInt32(txtAge.Text);
                    db.Entry(profile).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            
        }

        private void BtnDeleteProfileInfo_Click(object sender, RoutedEventArgs e)
        {
            int index = dgMain.SelectedIndex;
            int id = 0;
            var firstSelectedCellContent = dgMain.Columns[0].GetCellContent(this.dgMain.SelectedItem);
            DataGridCell firstSelectedCell = firstSelectedCellContent != null ? firstSelectedCellContent.Parent as DataGridCell : null;
            TextBlock textBlock = (TextBlock)firstSelectedCell.Content;
            bool converted = Int32.TryParse(textBlock.Text, out id);
            if (converted == false)
                return;

            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
            db.SaveChanges();
        }
    }
}
