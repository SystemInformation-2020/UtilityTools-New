using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Algorithmia;

using MySql.Data.MySqlClient;

namespace UtilityTools.Modal
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }


        MySqlConnection con = new MySqlConnection();

        MySqlConnection objCon = new MySqlConnection("server=localhost; port=3309; User Id=root; database=utilitysi; password=usbw");

        public double numb1, numb2, total, enter = 0;


        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void rbtnLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnLocal.Checked)
            {
                btnArquivoUp.Enabled = true;

                btnCarregarCarro.Enabled = true;
            }
            else
            {
                btnArquivoUp.Enabled = false;
            }
        }

        private void btnArquivoUp_Click(object sender, EventArgs e)
        {
            OpenFileDialog arquivoup = new OpenFileDialog();

            if (arquivoup.ShowDialog() == DialogResult.OK)
            {
                // StreamReader readarq = new StreamReader(arquivoup.FileName, Encoding.Default);
                // readarq.Close();

                txtEnderecoCarro.Text = arquivoup.FileName;

            }


        }

        private void btnCarregarCarro_Click(object sender, EventArgs e)
        {
            var input = txtEnderecoCarro.Text;
            var client = new Client("simGui+0ItIawILfndtvMe8Bbsd1");
            var algorithm = client.algo("LgoBE/CarMakeandModelRecognition/0.4.7");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipe<object>(input);

            var resultado = algorithm.pipe<object>(input);

            Console.WriteLine(response.result);

            Console.WriteLine("----------");

            

            //txtOutCarro.Text = response.result.ToString();


            txtOutCarro.Text = resultado.result.ToString();

            pbCarroOut.ImageLocation = txtEnderecoCarro.Text;


            pbCarroOut.BackgroundImageLayout = ImageLayout.Stretch;


            txtOutCarro.Text = txtOutCarro.Text.Replace("[", "");
            txtOutCarro.Text = txtOutCarro.Text.Replace("]", "");

            //txtOutCarro.Text = txtOutCarro.Text.Replace("{", "");//NAO CONTEM*

            txtOutCarro.Text = txtOutCarro.Text.Replace("},", "\n \n ----------");

            txtOutCarro.Text = txtOutCarro.Text.Replace("\"", "");

            txtOutCarro.Text = txtOutCarro.Text.Replace("{", "");

            //PALAVRAS

            txtOutCarro.Text = txtOutCarro.Text.Replace("body_style:", "Estilo de Corpo:");

            txtOutCarro.Text = txtOutCarro.Text.Replace("confidence:", "Chance (%*100):");

            txtOutCarro.Text = txtOutCarro.Text.Replace("make:", "Marca:");

            txtOutCarro.Text = txtOutCarro.Text.Replace("model:", "Modelo:");

            txtOutCarro.Text = txtOutCarro.Text.Replace("model_year:", "Ano:");

            txtOutCarro.Text = txtOutCarro.Text.Replace("}", "");


        }

        private void rbtnLink_CheckedChanged(object sender, EventArgs e)
        {
            if(this.rbtnLink.Checked)
            {
                btnCarregarCarro.Enabled = true;
            }
        }

        private void btnGoImageColor_Click(object sender, EventArgs e)
        {
            pboxImagemColor.BackgroundImage = null;
            var input = txtEnderecoImagemColor.Text ;


            var client = new Client("simGui+0ItIawILfndtvMe8Bbsd1");
            var algorithm = client.algo("deeplearning/ColorfulImageColorization/1.1.14");

            var nlp_directory = client.dir("data://PinguinZip/Colorizer");

            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(input);

            if (!nlp_directory.exists())
            {
                nlp_directory.create();
            }

            nlp_directory.updatePermissions(ReadDataAcl.PUBLIC);

            if (nlp_directory.getPermissions() == ReadDataAcl.PUBLIC)
            {
                Console.WriteLine("Directory updated to PUBLIC");
            }


            var resultado = algorithm.pipe<object>(input);

            Console.WriteLine(response.result);// RESULTADO

            btnFullSc.Enabled = true;

            txtUrlColorizador.Text = resultado.result.ToString();

            txtUrlColorizador.Text = txtUrlColorizador.Text.Replace("data://.algo/deeplearning/ColorfulImageColorization/temp/", "");

            txtUrlColorizador.Text = txtUrlColorizador.Text.Replace("{\"output\":", "");

            txtUrlColorizador.Text = txtUrlColorizador.Text.Replace("\"}", "");

        }

        private void rbtnLinkColor_CheckedChanged(object sender, EventArgs e)
        {
            if(this.rbtnLinkColor.Checked)
            {
                btnGoImageColor.Enabled = true;
            }
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(4);
        }

        private void btnQRGerar_Click(object sender, EventArgs e)
        {
            if (txtTextoQR.Text == string.Empty || txtLarguraQR.Text == string.Empty && txtAlturaQR.Text == string.Empty)
            {
                MessageBox.Show("Informações inválidas. Complete as informações para gerar o QRCode...");
                txtTextoQR.Focus();
                return;
            }

            try
            {
                int largura = Convert.ToInt32(txtLarguraQR.Text);
                int altura = Convert.ToInt32(txtAlturaQR.Text);
                picQRCode.Image = GerarQRCode(largura, altura, txtTextoQR.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions()
                {
                    Width = width,
                    Height = height,
                    Margin = 0
                };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        private void txtTextoQR_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTextoQR.Text))
            {
                btnQRGerar.Enabled = true;
            }
        }


        private void btn0_Click(object sender, EventArgs e)
        {
            if(enter == 1)
            {
                txtResultado.Text += "0";
            }
            else if(enter == 0)
            {
                txtResultado.Text += txtConta.Text + "0";
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "1";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "1";
            }
            
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "2";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "2";
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "3";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "3";
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "4";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "4";
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "5";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "5";
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "6";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "6";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "7";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "7";
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "8";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "8";
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += "9";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + "9";
            }
        }

        private void btnVirgula_Click(object sender, EventArgs e)
        {
            if (enter == 1)
            {
                txtResultado.Text += ".";
            }
            else if (enter == 0)
            {
                txtResultado.Text += txtConta.Text + ".";
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {

            numb2 = Convert.ToDouble(txtResultado.Text);

            //lblConta.Text = lblConta.Text + btnResult.Text;
            //numb2 = Convert.ToDouble(txtValor.Text);

            //txtConta.Text += "/ " + txtResultado.Text;

            //txtValor.Text = "";

            total = numb1 / numb2;
            txtConta.Text = total.ToString();
            total = numb1 % numb2;

            txtResultado.Text = "Resto: " + total.ToString();

            btn0.Enabled = false;
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
            btnVirgula.Enabled = false;
            btnDiv.Enabled = false;
            
        }

        private void btnDia_Click(object sender, EventArgs e)
        {

            Random randNum = new Random();
           int number = randNum.Next(1,10);

            //int number = Convert.ToInt32(randNum);

            if (number == 1)
            {
                MessageBox.Show("Você é Incrivel!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 2)
            {
                MessageBox.Show("O Que Importa é Se Você Está Feliz!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 3)
            {
                MessageBox.Show("Nunca Esquecer Daquilo Que Nós Deixou Feliz Um Dia", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 4)
            {
                MessageBox.Show("Super-Poderes? Você Quer? Seja Super-Feliz!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 5)
            {
                MessageBox.Show("É Verdade Que Nós Choramos As Vezes. \n Mas É Verdade Que Sempre Quis Deixar Um Sorriso No Rosto.", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 6)
            {
                MessageBox.Show("Aquilo Que Me Abalou Só Me Motivou a Continuar.", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 7)
            {
                MessageBox.Show("Não Chore ;-; \n a Tristeza Não Prevalece \n Mas a Felicidade Sim! ", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 8)
            {
                MessageBox.Show("Nunca Esquecer Daquilo Que Nós Deixou Feliz Um Dia •ᴗ•", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (number == 9)
            {
                MessageBox.Show("O Seu Amor Leal é  ETERNO \n Salmos  100 : 5  ✞", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //var input = "{" + "  \"image\": \""+textBox2.Text+ "}";

            var input2 = txtimgurl.Text;

            var client = new Client("simGui+0ItIawILfndtvMe8Bbsd1");
            var algorithm = client.algo("deeplearning/ColorfulImageColorization/1.1.14");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(input2);

            Console.WriteLine("");
            Console.WriteLine("-------------------");
            Console.WriteLine(response.result);
            Console.WriteLine("-------------------");
            Console.WriteLine("");

            var nlp_directory = client.dir("data://PinguinZip/deeplearning/ColorfulImageColorization/temp/");

            

            //var acl = nlp_directory.getPermissions();

            //if (acl == ReadDataAcl.PUBLIC)
            //{
            //    Console.WriteLine("acl is the default permissions type MY_ALGOS");
            //}

            // Update permissions to private


            // Create your data collection if it does not exist


            //if (acl == ReadDataAcl.MY_ALGOS)
            //{
            //    Console.WriteLine("acl is the default permissions type MY_ALGOS");
            //}

            //var text_file = "data://PinguinZip/nlp_directory/jack_london.txt";

            //nlp_directory.updatePermissions(ReadDataAcl.PUBLIC);

            var text_file = (response.result.ToString());

            string colorimagesub = response.result.ToString();

            string colorimagenext = response.result.ToString();

            //colorimagesub = colorimagesub.Replace("data://.algo/deeplearning/ColorfulImageColorization/temp/", "");

            colorimagesub = colorimagesub.Replace("data://.algo/deeplearning/ColorfulImageColorization/temp/", "");

            if (client.file(text_file).exists())
            {
                var localfile = client.file(text_file).getFile();

                MessageBox.Show(localfile.ToString());

               
            }
            else
            {
                Console.WriteLine("The file does not exist");

                MessageBox.Show("Arquivo Não Encontrado" + text_file);
            }


            //pictureBox1.Load("https://algorithmia.com/v1/data/.algo/deeplearning/ColorfulImageColorization/temp/d7be234e-f770-4fd7-a80e-47940c6bd650.png");

            //pictureBox1.Load("https://algorithmia.com/v1/data/.algo/deeplearning/ColorfulImageColorization/temp/" + colorimagesub);


    }

        private void button2_Click(object sender, EventArgs e)
        {
            lblimg.Visible = true;
            txtimgurl.Visible = true;
            btnimgload.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Modal.FormFullScreen formfs = new Modal.FormFullScreen();
            formfs.ShowDialog();

            using (var ffs = new FormFullScreen())
            {
                ffs.ShowDialog();
            }

        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if(enter == 0)
            {


            numb1 = Convert.ToDouble(txtResultado.Text);
            //lblConta.Text = Convert.ToString(numb1) + "/";

            txtConta.Text += txtResultado.Text;

            txtResultado.Clear();

                enter = 1;

            }
            else
            {

            }

        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            enter = 0;
            numb1 = 0;
            numb2 = 0;
            total = 0;
            txtConta.Text = "";
            lblConta.Text = "";
            txtResultado.Text = "";

            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            btnVirgula.Enabled = true;
            btnDiv.Enabled = true;
        }
    }
}
