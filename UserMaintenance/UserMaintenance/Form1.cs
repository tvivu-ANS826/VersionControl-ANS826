using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        
        public Form1()
        {

            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "Full Name";


            var u = new User();
            {
                /* LastName = txtLastName.Text;
                 Firstname = txtFirstName.Text; */
                FullName = txtFullName;
            }

            users.Add(u);


            
            
            InitializeComponent();
            /* label1LastName.Text = Resource1.LastName;
             label2FirstName.Text = Resource1.LastName; */
            label1FullName.Text = Resource1.Fullname;
            button1Add.Text = Resource1.Add;

        }
    }
}
