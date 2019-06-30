using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;

namespace FinalDams
{
    public partial class NewUser : Form
    {
        public User LoggedInUser { get; set; }
        Context _context = new Context();
        public NewUser()
        {
            InitializeComponent();
        }
        private void NewUser_Load(object sender, EventArgs e)
        {
            foreach (var accessLvll in _context.Access)
            {
                comboBox1.Items.Add(accessLvll.usertype);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox3.Text == "" | textBox4.Text == "" | comboBox1.Text == "")
            {
                MessageBox.Show("Fil in all fields", "Validation");
                return;
            }
            User AddUser = new User();
            AddUser.Name = textBox3.Text;
            AddUser.Password = Crypto.Hash(textBox4.Text);
            AddUser.ACL = _context.Access.Where(x => x.usertype == comboBox1.SelectedItem).FirstOrDefault();
            _context.Users.Add(AddUser);
            _context.SaveChanges();
        }
        private void NewUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.LoggedInUser = LoggedInUser;
            this.Hide();
            this.Close();
            mainPage.ShowDialog();
        }
    }
}
