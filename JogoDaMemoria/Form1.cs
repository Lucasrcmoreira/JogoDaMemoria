using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaMemoria
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            VirarCartas();
            
        }
        List<string> listaverificacao = new List<string>();

        int Tentativas,NmrTag, QtPares, QtCliques,timeLeft;
        int QTPARESRESULT;
        string pos1,pos2;

        Image[] GuardarImg = new Image[12];

        private void Aleatorio()
        {
            foreach (PictureBox itens in Controls.OfType<PictureBox>())
            {
                Random cmdAleatorio = new Random();

                
                int[] X = { 23, 120, 220, 320, 420, 520 };
                int[] Y = { 28, 160, 292, 424 };

                SortearOutraVez:
                
                var sorteioX = X[cmdAleatorio.Next(0, X.Length)];
                var sorteioY = Y[cmdAleatorio.Next(0, Y.Length)];

                itens.Location = new Point(sorteioX,sorteioY);

                string verificacao = sorteioX.ToString() + sorteioY.ToString();

                if (listaverificacao.Contains(verificacao))
                {
                    goto SortearOutraVez;
                }
                else
                {
                    itens.Location = new Point(sorteioX, sorteioY);
                    listaverificacao.Add(verificacao);
                }
            }
        }
        

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int[] NmrCliques = new int[2];

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            timer1.Interval = 1000;

            if (timeLeft >= 0)
            {
                timeLeft = timeLeft + 1;
                CRegressiva.Text = timeLeft + " seconds";
            }
            else
            {
                
                timer1.Stop();
            }

        }

        private void EncontrarCartas_Click(object Sender, EventArgs e)
        {
            bool ParesEncontrados = false;
            

            PictureBox img = (PictureBox)Sender;
            QtCliques++;

            NmrTag = int.Parse(String.Format("{0}", img.Tag));
            img.Image = GuardarImg[NmrTag];
            img.Refresh();
            

            if (QtCliques == 1)
            {
                pos1 = img.Location.ToString();
                NmrCliques[0] = int.Parse(String.Format("{0}", img.Tag));
            }
            else if(QtCliques == 2)
            {
                pos2 = img.Location.ToString();
                Tentativas++;
                label1.Text = "TENTATIVAS : " + Tentativas.ToString();
                NmrCliques[1] = int.Parse(String.Format("{0}", img.Tag));
               
                ParesEncontrados = QtParesEncontrados();
                Thread.Sleep(1000);
                DesvirarCartas(ParesEncontrados);
                
            }

            
           
        }
        private bool QtParesEncontrados()
        {
            QtCliques = 0;
            if (NmrCliques[0] == NmrCliques[1] && pos1.Trim() != pos2.Trim())
            {
                return true;
            }
            else {
                return false;
            }
        }

        private void VirarCartas()
        {
            foreach (PictureBox itens in Controls.OfType<PictureBox>())
            {
               
                    NmrTag = int.Parse(String.Format("{0}", itens.Tag));
                    GuardarImg[NmrTag] = itens.Image;
                    itens.Image = Properties.Resources.capa;
                    itens.Enabled = true;
                

            }

            timeLeft = 0;

            timer1.Start();
            Aleatorio();


        }
        private void DesvirarCartas(bool Verificacao) {
            foreach (PictureBox itens in Controls.OfType<PictureBox>())
            {
                if(int.Parse(String.Format("{0}", itens.Tag)) == NmrCliques[0] || int.Parse(String.Format("{0}", itens.Tag)) == NmrCliques[1])
                {
                    
                    
                    if (Verificacao == true) {
                        itens.Enabled = false;
                        QtPares++;
                        QTPARESRESULT = QtPares / 2;

                        label2.Text = "PARES ENCONTRADOS: " + QTPARESRESULT.ToString();
                         if(QTPARESRESULT == 12)
                         {
                            timer1.Stop();
                         }
                    }
                    else {
                        
                        itens.Image = Properties.Resources.capa;
                        itens.Refresh();
                    }
                }
            }
        }


    }
}
