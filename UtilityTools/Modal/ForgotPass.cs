using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace UtilityTools.Modal
{
    public partial class ForgotPass : Form
    {
        public ForgotPass()
        {
            InitializeComponent();
        }

        MySqlConnection objCon = new MySqlConnection("server=localhost; port=3309; User Id=root; database=utilitysi; password=usbw");


        private void ForgotPass_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            objCon.Open();

            string cpfcod = mtxtcpf.Text;
            string passcod = txtPassword.Text;

            try
            {

                MySqlCommand objCmd = new MySqlCommand("UPDATE `utilitysi`.`cadastros` SET `Senha` = '" + passcod + "' WHERE (`CPF` = '" + cpfcod + "');", objCon);


                objCmd.ExecuteNonQuery();

                objCon.Close();

            }
            catch (Exception a)
            {
                MessageBox.Show("Não Existe Usuário com Este CPF " + a.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objCon.Close();
            }


        }
    }
}
