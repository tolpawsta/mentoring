using IntroductionCore;
using System;
using System.Windows.Forms;

namespace IntroductionWinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.txtUserName.TextChanged += TxtUserName_TextChanged;
        }

        private void TxtUserName_TextChanged(object sender, EventArgs e)
        {
            string userName = ((TextBox)sender).Text;
            lblHelloUser.Text =userName==String.Empty?"Welcom":MessageSender.SendHelloToUser(userName);
        }
    }
}
