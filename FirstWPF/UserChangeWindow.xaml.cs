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
using System.Windows.Shapes;
using System.Data.Entity;

namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для UserChangeWindow.xaml
    /// </summary>
    public partial class UserChangeWindow : Window
    {
        LibraryContext db;
        public UserChangeWindow()
        {
            InitializeComponent();
            db = new LibraryContext();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (db = new LibraryContext())
            {
                db.Roles.Load();
                cmbRole.ItemsSource = db.Roles.Local;
                //cmbRole.SelectedValuePath = cmbRole.ItemsSource["Id"];


                //foreach (Role role in db.Roles.Local )
                //{
                //    cmbRole.SelectedValuePath = role.Id.ToString();
                //    cmbRole.DisplayMemberPath = role.RoleName;

                //    cmbRole.Items.Add(new { role.RoleName, role.Id});
                //}
                //cmbRole.ItemsSource= db.Roles.Local;
                //cmbRole.DisplayMemberPath = db.Roles.Find
         
            }
            
            
    
        }

    }
}
