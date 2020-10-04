using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        
        public Form1()
        {
            button1 = new Button(Resource1);
            button1.Text = "Fájlba írás";


            private void Felhasznalok (string mezo)
            { 

                using (StreamWriter iras = new StreamWriter(mezo, true))
                {
                    foreach (var item in users)
                    {
                        iras.WriteLine(item.Fullname);
                    }
                }


           /* string fajlnev = @"C:\Users\tvivu\Desktop\Suli\Corvinus\5.félév\Információs rendszerek fejlesztése\3-HW\Új mappa (2)\UserMaintenance\UserMaintenance";
            StreamWriter iras = new StreamWriter(fajlnev,false,Encoding.Default);
            iras.WriteLine("ID+FullName"); */






            }

            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "Full Name";


            private void button1_Click (object sender, System.EventArgs e)
            { 

             var u = new User();
            {
                LastName = txtLastName.Text;
                Firstname = txtFirstName.Text;
            }

            users.Add(u);
            }

            private void button2_Click (object sender, System.EventArgs e)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "csv FILES (*.csv)|*.csv";

                var mezo = sfd.ShowDialog();
                var fajlnev = sfd.Fajlnev;

                if (!string.IsNullOrEmpty(fajlnev))
                    Felhasznalok(sfd.FileName);


            }


            private void button3_Click(object sender, System.EventArgs e)
            {
                var sorszam = listBox1.SelectedIndex;
                if (sorszam != -1)
                    users.RemoveAt(sorszam);
            }

















            InitializeComponent();
            label1LastName.Text = Resource1.LastName;
            label2FirstName.Text = Resource1.LastName;
            button1Add.Text = Resource1.Add;

        }
    }
}
