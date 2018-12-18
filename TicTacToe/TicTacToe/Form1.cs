using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Socket socket;
        EndPoint endPointLocal, endPointRemote;
        public static string firstPlayerIP;
        public string secondPlayerIP;
        public string firstPlayerPort;
        public string secondPlayerPort;

        public Form1()
        {
            InitializeComponent();

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            firstPlayerIP = GetLocalIP();
        }


        bool turn = true;  // true = X, false = O 
        bool against_computer = false; 
        bool network = false;
        int turn_count = 0;


        private string GetLocalIP()
        {
            IPHostEntry ipHostEntry;
            ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in ipHostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        private void StartConnection()
        {
            try
            {
                endPointLocal = new IPEndPoint(IPAddress.Parse(firstPlayerIP), Convert.ToInt32(firstPlayerPort));
                socket.Bind(endPointLocal);
                endPointRemote = new IPEndPoint(IPAddress.Parse(secondPlayerIP), Convert.ToInt32(secondPlayerPort));
                socket.Connect(endPointRemote);

                byte[] buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointRemote, new AsyncCallback(MessageCallBack), buffer);

                MessageBox.Show("Bağlantı Kuruldu.");

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }

        private void MessageCallBack(IAsyncResult iAResult)
        {
            try
            {
                int size = socket.EndReceiveFrom(iAResult, ref endPointRemote);
                if (size > 0)
                {
                    byte[] incomingData = new byte[1464];
                    incomingData = (byte[])iAResult.AsyncState;

                    ASCIIEncoding asciiEncoding = new ASCIIEncoding();
                    string incomingMessage = asciiEncoding.GetString(incomingData);
                    string btnName = incomingMessage[0].ToString() + incomingMessage[1].ToString();

                    txtBtnName.Text = btnName;
                    Button btnControl;
                    btnControl = ControlOfButtonName(btnName);
                    btnControl.PerformClick();
                    txtBtnName.Text = "";
                }
                byte[] buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointRemote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }  

        public void SendMessage(string btnName)
        {
            try
            {
                string strBtnName = btnName;
                int turnCount = turn_count;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] btnNameMessage = new byte[1500];
                btnNameMessage = asciiEncoding.GetBytes(strBtnName);
                socket.Send(btnNameMessage);
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message);
            }
        }

        Button btn;
        private Button ControlOfButtonName(string name) //Control of Button Name
        {
            switch (name)
            {
                case "A1":
                    btn = A1;
                    break;
                case "A2":
                    btn = A2;
                    break; 
                case "A3":
                    btn = A3;
                    break;  
                case "B1":
                    btn = B1;
                    break; 
                case "B2":
                    btn = B2;
                    break; 
                case "B3":
                    btn = B3;
                    break; 
                case "C1":
                    btn = C1;
                    break; 
                case "C2":
                    btn = C2;
                    break; 
                case "C3":
                    btn = C3;
                    break;

                default:
                    btn = null;
                    break;
            }
            return btn;
        }


        Button btnIncoming;
        private void button_click(object sender, EventArgs e)
        {
            Button btnClicked = (Button)sender;
            if (network)
            {
                btnIncoming = null;
                if (txtBtnName.Text != "")
                {
                    btnIncoming = ControlOfButtonName(txtBtnName.Text);
                    txtBtnName.Text = "";
                }
                if (btnIncoming != null)
                {
                    if (secondPlayerPort == "20" && turn)
                    {
                        btnIncoming.Text = "X";
                        checkForTurn(btnClicked);
                    }
                    else if (secondPlayerPort == "21" && !turn)
                    {
                        btnIncoming.Text = "O";
                        checkForTurn(btnClicked);
                    }
                    else MessageBox.Show("Sıranızı Bekleyin!");
                }
                else if (btnClicked != null)
                {
                    if (firstPlayerPort == "20" && turn)
                    {
                        btnClicked.Text = "X";
                        SendMessage(btnClicked.Name);
                        checkForTurn(btnClicked);
                    }
                    else if (firstPlayerPort == "21" && !turn)
                    {
                        btnClicked.Text = "O";
                        SendMessage(btnClicked.Name);
                        checkForTurn(btnClicked);
                    }
                    else MessageBox.Show("Sıranızı Bekleyin!");
                }
            }
            else
            {
                if (turn)
                    btnClicked.Text = "X";
                else
                    btnClicked.Text = "O";

                checkForTurn(btnClicked);
            }

            if (!(turn) && against_computer)
            {
                computer_make_move();
            }
        } //Button Click

        private void checkForTurn(Button btn)
        {
            turn = !turn;
            btn.Enabled = false;
            turn_count++;
            progressBar1.Value = 50;

            lblDraw.Focus();
            if (!network)
                StartTimer();
            checkForWinner();
        } //Check For Turn

        private void checkForWinner()
        {
            bool there_is_a_winner = false;

            //horizontal control
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && !(A1.Enabled))
                there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && !(B1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && !(C1.Enabled))
                there_is_a_winner = true;

            //vertical control
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && !(A1.Enabled))
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && !(A2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && !(A3.Enabled))
                there_is_a_winner = true;

            //cross control
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && !(A1.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && !(C1.Enabled))
                there_is_a_winner = true;


            if (there_is_a_winner) //if there is a winner
            {
                string winner = "";
                if (turn)
                {
                    winner = "O";
                    o_winner_count.Text = (Int32.Parse(o_winner_count.Text) + 1).ToString();
                }
                else
                {
                    winner = "X";
                    x_winner_count.Text = (Int32.Parse(x_winner_count.Text) + 1).ToString();
                }

                disableButtons();
                timer1.Stop();
                MessageBox.Show(winner + " Kazandı!", "İşte Kazanan!");

                resetButtonTexts();
                
                turn_count = 0;
            }
            else // if there isn't a winner (Draw)
            {
                if (turn_count == 9)
                {
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();

                    timer1.Stop();
                    MessageBox.Show("Tekrar Dene!", "Berabere!");

                    resetButtonTexts();

                    turn_count = 0;
                }
            }

        } //Check For Winner

        private void disableButtons() 
        {
            foreach (Control control in Controls)
            {
                try
                {
                    Button btn = (Button)control;
                    btn.Enabled = false;
                }
                catch { }
            }

        } //Disable Buttons


        private void button_enter(object sender, EventArgs e) 
        {
            Button btn = (Button)sender;
            if (btn.Enabled)
            {
                if (turn)
                    btn.Text = "X";
                else
                    btn.Text = "O";
            }
        } //Button Enter  

        private void button_leave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Enabled)
                btn.Text = "";
        } //Button Leave


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } //Exit

        private void v1ToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            make_button_visibility_true();

            turn = true;
            against_computer = false;
            network = false;
            turn_count = 0;

            resetButtonTexts();

        } //1v1

        private void againstComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            make_button_visibility_true();

            against_computer = true;
            turn = true;
            network = false;
            turn_count = 0;

            resetButtonTexts();
        } //Against Computer

        private void networkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            network = true;
            turn = true;
            against_computer = false;
            turn_count = 0;

            MessageBox.Show("Network Modunda Zamanlayıcı Kapanacaktır!");

            Form connection = new ConnectionToNetwork(this);
            this.Hide();
            connection.ShowDialog();

            secondPlayerIP = ConnectionToNetwork.txtStaticFriendsIP;
            firstPlayerPort = ConnectionToNetwork.txtStaticLocalPort;
            secondPlayerPort = ConnectionToNetwork.txtStaticFriendsPort;

            make_button_visibility_true();
            resetButtonTexts();

            StartConnection();
        } //Network

        private void resetScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_winner_count.Text = "0";
            x_winner_count.Text = "0";
            draw_count.Text = "0";
        } //Reset Scores


        private void resetButtonTexts() 
        {
            foreach (Control control in Controls)
            {
                try
                {
                    Button btn = (Button)control;
                    btn.Enabled = true;
                    btn.Text = "";
                }
                catch { }
            }

            progressBar1.Value = 50; //Reset Time Bar
        } //Reset Button Texts

        private void make_button_visibility_true()
        {
            A1.Visible = true; A2.Visible = true; A3.Visible = true;
            B1.Visible = true; B2.Visible = true; B3.Visible = true;
            C1.Visible = true; C2.Visible = true; C3.Visible = true;
            lblX.Visible = true; lblDraw.Visible = true; lblO.Visible = true;
            x_winner_count.Visible = true; draw_count.Visible = true; o_winner_count.Visible = true;
            progressBar1.Visible = true;
        } //Make Button Visibility True

        private void make_button_visibility_false()
        {
            A1.Visible = false; A2.Visible = false; A3.Visible = false;
            B1.Visible = false; B2.Visible = false; B3.Visible = false;
            C1.Visible = false; C2.Visible = false; C3.Visible = false;
            lblX.Visible = false; lblDraw.Visible = false; lblO.Visible = false;
            x_winner_count.Visible = false; draw_count.Visible = false; o_winner_count.Visible = false;
            progressBar1.Visible = false;
        } //Make Button Visibility False


        private void computer_make_move()
        {
            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space

            Button move = null;

            //look for tic tac toe opportunities
            move = look_for_win_or_block("O"); //look for win  -  computer = O
            if (move == null)
            {
                move = look_for_win_or_block("X"); //look for block
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }
                }
            }

            try {
                move.PerformClick();
            }
            catch { }
        }  //Computer Make Move

        private Button look_for_open_space()
        {
            Button btn = null;
            foreach (Control control in Controls)
            {
                btn = control as Button;
                if (btn != null)
                {
                    if (btn.Text == "")
                        return btn;
                }
            }

            return null;
        }  //Look For Open Space

        private Button look_for_corner()
        {
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        } //Look For Corner

        private Button look_for_win_or_block(string mark)
        {
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //CROSS TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        } //Look For Win or Block


        private void StartTimer()
        {
            if(turn_count == 1)
            {
                timer1.Start();
            }
            else
                progressBar1.Value = 50;
        } //Start Timer

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (turn)
            {
                if (progressBar1.Value != 0)
                    progressBar1.Value -= 5;
                else
                {
                    timer1.Stop();
                    MessageBox.Show("O Kazandı!", "Süre Doldu!");

                    o_winner_count.Text = (Int32.Parse(o_winner_count.Text) + 1).ToString(); //o winner count
                    turn_count = 0;
                    resetButtonTexts();
                }
            }
            else
            {
                if (progressBar1.Value != 100)
                    progressBar1.Value += 5;
                else
                {
                    timer1.Stop();
                    MessageBox.Show("X Kazandı!", "Süre Doldu!");

                    x_winner_count.Text = (Int32.Parse(x_winner_count.Text) + 1).ToString(); //x winner count
                    turn_count = 0;
                    resetButtonTexts();
                    make_button_visibility_true();               
                }
            }

        } //Timer Tick


        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            make_button_visibility_false();

            CheckForIllegalCrossThreadCalls = false;  
        }
    }
}
