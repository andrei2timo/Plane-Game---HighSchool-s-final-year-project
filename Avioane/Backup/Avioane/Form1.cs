using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;




namespace Avioane
{
    //.Blue
    //.RoyalBlue 
    //.MediumBlue
    //System.Threading.Thread.Sleep(500) delay

    public partial class Form1 : Form
    {


        /// Variabile
        int w = 25, h = 25;
        int imgw = 30, imgh = 30;
        int PlayerPlaneNo = 0, RoboPlaneNo = 0;
        int padding = 1;
        int TopLeft = 30, TopUp = 30, marginx, marginy;
        int click = 0, poz = 0;
        int moves = 1, turn = 0; // 0 pt Player si 1 pt Robo
        int score = 0; // 0=joc in derulare, 1=Player a castoigat, 2=Robo a Castigat
        int[, ,] p = new int[4, 4, 4] { { { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 1, 0, 0 }, { 1, 1, 1, 0 } },
                                        { { 1, 0, 1, 0 }, { 1, 1, 1, 1 }, { 1, 0, 1, 0 }, { 0, 0, 0, 0 } },
                                        { { 1, 1, 1, 0 }, { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 1, 0, 0 } },
                                        { { 0, 1, 0, 1 }, { 1, 1, 1, 1 }, { 0, 1, 0, 1 }, { 0, 0, 0, 0 } } };
        Label[][] PlayerTable;
        Label[][] RoboTable;
        Label[][] Plane;
        Label message = new Label();
        Label PlayerLabel = new Label();
        Label RoboLabel = new Label();
        Label Legend = new Label();
        Label Hit = new Label();
        Label HitText = new Label();
        Label Air = new Label();
        Label AirText = new Label();
        Label title = new Label();
        Label myname = new Label();
        Label teachname = new Label();
        Label collegename = new Label();

        Button button1;
        Button start = new Button();
        Button newgame = new Button();
        Button exit = new Button();

        Random r;

        PictureBox[] img = new PictureBox[7];

        Color[] c = { Color.Blue, Color.MediumBlue, Color.RoyalBlue };


        public Form1()
        {
            InitializeComponent();
        }


        //Genereaza PlaterTable, RoboTable, Avionul cu motor si Butoanele
        public void GenerareObiecte()
        {
            int i, j;
            marginx = 4 * (w + padding);
            PlayerPlaneNo = 0; 
            RoboPlaneNo = 0;

            //Message
            message.Top = 10;
            message.Left = 10;
            message.Text = "Plaseaza 3 Avioane! Rotește avionul și apasa pe casuța în care vrei să fie cabina";
            message.Font = new System.Drawing.Font("Lucida Handwriting", 10F );
            message.AutoSize = true;
            message.BackColor = Color.Transparent;
            this.Controls.Add(message);

            //Player Table
            PlayerTable = new Label[10][];
            for (i = 0; i < 10; i++)
            {
                PlayerTable[i] = new Label[10];
                for (j = 0; j < 10; j++)
                {
                    PlayerTable[i][j] = new Label();
                    PlayerTable[i][j].Left = TopLeft + (w + padding) * j;
                    PlayerTable[i][j].Top = message.Top + TopUp + (h + padding) * i;
                    PlayerTable[i][j].Width = w;
                    PlayerTable[i][j].Height = h;
                    PlayerTable[i][j].BackColor = Color.Bisque;
                    PlayerTable[i][j].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.Controls.Add(PlayerTable[i][j]);
                    PlayerTable[i][j].Tag = i.ToString() + "/" + j.ToString(); //i si j sunt linia si coloana 
                    PlayerTable[i][j].TabIndex = 0; //0 adica AER
                    PlayerTable[i][j].Click += new EventHandler(ClickPlayerTable);

                }
            }
            //RoboTable
            RoboTable = new Label[10][];
            for (i = 0; i < 10; i++)
            {
                RoboTable[i] = new Label[10];
                for (j = 0; j < 10; j++)
                {
                    RoboTable[i][j] = new Label();
                    RoboTable[i][j].Left = TopLeft + (w + padding) * 10 + marginx + (w + padding) * j;
                    RoboTable[i][j].Top = message.Top + TopUp + (h + padding) * i;
                    RoboTable[i][j].Width = w;
                    RoboTable[i][j].Height = h;
                    RoboTable[i][j].BackColor = Color.Bisque;
                    RoboTable[i][j].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.Controls.Add(RoboTable[i][j]);
                    RoboTable[i][j].Tag = i.ToString() + "/" + j.ToString(); //  i = linia, j = coloana
                    RoboTable[i][j].TabIndex = 0; //0 adica AER 

                    //Adauga acest eveniment dupa ce incepe jocul !!!!!!!!! REMINDER
                    // RoboTable[i][j].Click += new EventHandler();
                }

            }

            //Plane Table
            marginy = 30;
            Plane = new Label[4][];
            for (i = 0; i < 4; i++)
            {
                Plane[i] = new Label[4];
                for (j = 0; j < 4; j++)
                {

                    Plane[i][j] = new Label();
                    Plane[i][j].Left = TopLeft + (w + padding) * (10 + j);
                    Plane[i][j].Top = TopUp + (h + padding) * (10 + i) + marginy;
                    Plane[i][j].Width = w;
                    Plane[i][j].Height = h;
                    if (p[0, i, j] == 1)
                    {
                        Plane[i][j].BackColor = Color.Blue;
                        Plane[i][j].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    }
                    else
                    {
                        Plane[i][j].BackColor = Color.Transparent;
                        Plane[i][j].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    }

                    this.Controls.Add(Plane[i][j]);
                }
            }
            //Buton
            button1 = new Button();
            button1.Top = TopUp + (h + padding) * 10 + marginy * 3 / 2;
            button1.Left = TopLeft + (w + padding) * 14 + marginy;
            button1.Text = "Rotește";
            button1.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            button1.AutoSize = true;
            button1.Click += new EventHandler(Click);
            this.Controls.Add(button1);

            //Apeleaza functia care creeaza 3 AVIOANE RANDOM PENTRU ROBO TABLE
            RoboPlanes();


        }


        //Deseneaza avionul in casuta de jos  
        public void DrawPlane(int x) // x=pozitia avionului
        {
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (p[x, i, j] == 1)
                    {
                        Plane[i][j].BackColor = Color.Blue;
                        Plane[i][j].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    }
                    else
                    {
                        Plane[i][j].BackColor = Color.Transparent;
                        Plane[i][j].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    }

        }

        //Roteste avionul
        private void Click(object sender, EventArgs e)
        {
            click++;
            poz = click % 4;
            switch (click % 4)
            {
                case 0:
                    DrawPlane(0);
                    break;
                case 1:
                    DrawPlane(1);
                    break;
                case 2:
                    DrawPlane(2);
                    break;
                case 3:
                    DrawPlane(3);
                    break;
            }

        }


        //Create 3 random planes for Robo !
        private void RoboPlanes()
        {
            r = new Random();
            int RandPos, RandX, RandY;

            while (RoboPlaneNo < 3)
            {
                //det random pozitia {0, 1, 2, 3} si i,j {0, 1, ..., 9}
                int i, j, initx = 0, inity = 0, finx = 0, finy = 0, valid;
                RandPos = r.Next(4);
                RandX = r.Next(10);
                RandY = r.Next(10);


                valid = 1;

                ///draw plane
                if (RandPos == 0)
                {
                    initx = RandX;
                    inity = RandY - 1;
                    finx = initx + 3;
                    finy = inity + 2;
                }

                if (RandPos == 1)
                {
                    initx = RandX - 1;
                    inity = RandY - 3;
                    finx = initx + 2;
                    finy = inity + 3;
                }
                if (RandPos == 2)
                {
                    initx = RandX - 3;
                    inity = RandY - 1;
                    finx = initx + 3;
                    finy = inity + 2;
                }
                if (RandPos == 3)
                {
                    initx = RandX - 1;
                    inity = RandY;
                    finx = initx + 2;
                    finy = inity + 3;
                }
                if (initx >= 0 && inity >= 0 && finx <= 9 && finy <= 9) ///validare sa nu iasa din matrice
                {//validare sa nu se suprapuna peste alt avion
                    for (i = 0; i < 4; i++)
                        for (j = 0; j < 4 && valid == 1; j++)
                        {
                            if (p[RandPos, i, j] == 1 && RoboTable[initx + i][inity + j].TabIndex > 0)
                                valid = 0;
                        }

                    if (valid == 1)
                    {
                        for (i = 0; i < 4; i++)
                            for (j = 0; j < 4; j++)
                                if (p[RandPos, i, j] == 1)
                                {
                                    RoboTable[initx + i][inity + j].TabIndex = 1;
                                    RoboTable[initx + i][inity + j].TabIndex = 10 + RoboPlaneNo + 1;
                                    //RoboTable[initx + i][inity + j].BackColor = 10; + c[RoboPlaneNo];

                                }

                        RoboTable[RandX][RandY].TabIndex = 2; // 2 adica CAP
                        RoboTable[RandX][RandY].TabIndex = 20 + RoboPlaneNo + 1;
                        RoboPlaneNo++;
                    }
                }
            } //endof while
        }
        //end of funcion RoboPlanes


        //Place 3 plane on PlayerTable !
        private void ClickPlayerTable(object sender, EventArgs e)
        {
            if (PlayerPlaneNo < 3)
            {
                int x, y, i, j, initx = 0, inity = 0, finx = 0, finy = 0, valid = 1;
                string s;
                char c1, c2;
                Label control = (Label)sender;
                s = control.Tag.ToString();
                c1 = s[0];
                c2 = s[2];
                x = (int)char.GetNumericValue(c1);
                y = (int)char.GetNumericValue(c2);
                ///draw plane
                if (poz == 0)
                {
                    initx = x;
                    inity = y - 1;
                    finx = initx + 3;
                    finy = inity + 2;
                }

                if (poz == 1)
                {
                    initx = x - 1;
                    inity = y - 3;
                    finx = initx + 2;
                    finy = inity + 3;
                }
                if (poz == 2)
                {
                    initx = x - 3;
                    inity = y - 1;
                    finx = initx + 3;
                    finy = inity + 2;
                }
                if (poz == 3)
                {
                    initx = x - 1;
                    inity = y;
                    finx = initx + 2;
                    finy = inity + 3;
                }
                if (initx >= 0 && inity >= 0 && finx <= 9 && finy <= 9)
                {//validare
                    for (i = 0; i < 4; i++)
                        for (j = 0; j < 4 && valid == 1; j++)
                        {
                            if (p[poz, i, j] == 1 && PlayerTable[initx + i][inity + j].TabIndex > 0)
                                valid = 0;
                        }

                    if (valid == 1)
                    {
                        for (i = 0; i < 4; i++)
                            for (j = 0; j < 4; j++)
                                if (p[poz, i, j] == 1)
                                {
                                    PlayerTable[initx + i][inity + j].BackColor = c[PlayerPlaneNo];
                                    PlayerTable[initx + i][inity + j].TabIndex = 1; //1 adica AVION
                                }
                        PlayerTable[x][y].TabIndex = 2; //2 adica CAP
                        PlayerPlaneNo++;
                        if (PlayerPlaneNo == 3)
                        {
                            MessageBox.Show("Incepe jocul!!!!!!!!!!!!!");
                            StartGame();
                        }

                    }
                    else
                        MessageBox.Show("Nu merge");

                }
                else
                    MessageBox.Show("Nu merge");

            }
        }
 
        //Fundal for GAME
        public void SetGame()
        {
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    this.Controls.Remove(Plane[i][j]);
                }
            }

            this.Controls.Remove(message);
            this.Controls.Remove(button1);

            //seteaza proprietati pentru label urile ce urmeaza
            PlayerLabel.Top = 10;
            PlayerLabel.Left = TopLeft + (w + padding) * 3;
            PlayerLabel.Text = "PLAYER";
            PlayerLabel.AutoSize = true;
            PlayerLabel.Font = new System.Drawing.Font("Lucida Handwriting", 12.0F, (System.Drawing.FontStyle.Bold));
            PlayerLabel.BackColor = Color.Transparent;

            RoboLabel.Left = PlayerLabel.Left + TopLeft + (w + padding) * 10 + marginx;
            RoboLabel.Top = 10;
            RoboLabel.Text = "ROBO";
            RoboLabel.AutoSize = true;
            RoboLabel.Font = new System.Drawing.Font("Lucida Handwriting", 12.0F, (System.Drawing.FontStyle.Bold));
            RoboLabel.BackColor = Color.Transparent;

            Legend.Left = TopLeft + (w + padding) * 10;
            Legend.Top = TopUp + (h + padding) * 10 + marginy;
            Legend.Height = h;
            Legend.Text = "Legend";
            Legend.AutoSize = true;
            Legend.Font = new System.Drawing.Font("Lucida Handwriting", 12.0F, (System.Drawing.FontStyle.Bold));
            Legend.BackColor = Color.Transparent;


            Hit.Left = TopLeft + (w + padding) * 10;
            Hit.Top = TopUp + (h + padding) * 12 + marginy;
            Hit.Height = h;
            Hit.Width = w;
            Hit.BackColor = Color.Red;


            HitText.Left = Hit.Left + w + padding;
            HitText.Top = TopUp + (h + padding) * 12 + marginy;
            HitText.Height = h;
            HitText.Text = "HIT";
            HitText.AutoSize = true;
            HitText.BackColor = Color.Transparent;
            HitText.Font = new System.Drawing.Font("Lucida Handwriting", 12.0F, (System.Drawing.FontStyle.Bold));


            Air.Left = TopLeft + (w + padding) * 10;
            Air.Top = TopUp + (h + padding) * 11 + marginy;
            Air.Height = h;
            Air.Width = w;
            Air.BackColor = Color.White;

            AirText.Left = Air.Left + w + padding;
            AirText.Top = TopUp + (h + padding) * 11 + marginy;
            AirText.Height = h;
            AirText.Text = "AIR";
            AirText.AutoSize = true;
            AirText.Font = new System.Drawing.Font("Lucida Handwriting", 12.0F, (System.Drawing.FontStyle.Bold));
            AirText.BackColor = Color.Transparent;


            for (i = 1; i <= 3; i++)
            {
                img[i] = new PictureBox();
                img[i].Height = imgh;
                img[i].Width = imgw;
                img[i].Top = TopUp + (h + padding) * 10 + marginy;
                img[i].Left = TopLeft + 5 * (w + padding) - 3 * (imgw + padding) / 2 + (i - 1) * (imgw + padding);
                img[i].SizeMode = PictureBoxSizeMode.StretchImage;
                img[i].Image = Image.FromFile("plane.jpg");
                this.Controls.Add(img[i]);

            }
            for (i = 4; i <= 6; i++)
            {
                img[i] = new PictureBox();
                img[i].Height = imgh;
                img[i].Width = imgw;
                img[i].Top = TopUp + (h + padding) * 10 + marginy;
                img[i].Left = TopLeft + 15 * (w + padding) + marginx - 3 * (imgw + padding) / 2 + (i - 4) * (imgw + padding);
                img[i].SizeMode = PictureBoxSizeMode.StretchImage;
                img[i].Image = Image.FromFile("plane.jpg");
                this.Controls.Add(img[i]);

            }

            //Adauga labeluri
            this.Controls.Add(PlayerLabel);
            this.Controls.Add(RoboLabel);
            this.Controls.Add(Legend);
            this.Controls.Add(Hit);
            this.Controls.Add(Air);
            this.Controls.Add(HitText);
            this.Controls.Add(AirText);


        }

        ///Start Game
        public void StartGame()
        {
            SetGame();
            int i, j;
            for (i = 0; i < 10; i++)
                for (j = 0; j < 10; j++)
                    RoboTable[i][j].Click += new EventHandler(ClickRoboTable);
            
            //Comenzi
            NextTurn();

        }

       


        //Randul Robotului 
        public void RoboTurn()
        {

            //Functie ce determina random o casuta
            r = new Random();
            int x = 0, y = 0, i, j;
            int fincell = 0;
            Color cul = new Color();
            while (fincell == 0)
            {
                x = r.Next(10);
                y = r.Next(10);
                if (PlayerTable[x][y].TabIndex < 4)
                {

                    fincell = 1;


                    if (PlayerTable[x][y].TabIndex == 0)
                        PlayerTable[x][y].BackColor = Color.White;
                    if (PlayerTable[x][y].TabIndex == 1)
                        PlayerTable[x][y].BackColor = Color.Red;
                    if (PlayerTable[x][y].TabIndex == 2)
                    { //draw whole plane
                        cul = PlayerTable[x][y].BackColor;
                        for (i = 0; i < 10; i++)
                            for (j = 0; j < 10; j++)
                                if (PlayerTable[i][j].BackColor == cul)
                                {
                                    PlayerTable[i][j].BackColor = Color.Red;
                                    PlayerTable[i][j].TabIndex = 4;
                                }
                        this.Controls.Remove(img[PlayerPlaneNo]);
                        PlayerPlaneNo--;
                    }
                    PlayerTable[x][y].TabIndex = 4;
                }


            }

            NextTurn();

        }

        //Event Click on RoboTable = Randul jucatorului
        private void ClickRoboTable(object sender, EventArgs e)
        {
            int x, y, i, j;
            string s;
            char c1, c2;
            Label control = (Label)sender;
            int nr;
            s = control.Tag.ToString();
            c1 = s[0];
            c2 = s[2];
            x = (int)char.GetNumericValue(c1);
            y = (int)char.GetNumericValue(c2);
            //Doar daca e randul Robotului


            if (turn == 0 && RoboTable[x][y].TabIndex / 10 < 4)
            {
                if (RoboTable[x][y].TabIndex / 10 == 0)
                    RoboTable[x][y].BackColor = Color.White;
                if (RoboTable[x][y].TabIndex / 10 == 1)
                    RoboTable[x][y].BackColor = Color.Red;
                if (RoboTable[x][y].TabIndex / 10 == 2)
                //draw whole plane
                {
                    nr = RoboTable[x][y].TabIndex % 10;
                    for (i = 0; i < 10; i++)
                        for (j = 0; j < 10; j++)
                            if (RoboTable[i][j].TabIndex % 10 == nr)
                            {
                                RoboTable[i][j].BackColor = Color.Red;
                                RoboTable[i][j].TabIndex = 40 + nr;
                            }
                    this.Controls.Remove(img[RoboPlaneNo + 3]);
                    RoboPlaneNo--;
                }
                RoboTable[x][y].TabIndex = 40;
                NextTurn();
            }

        }

        

        //Next turn
        private void NextTurn()
        {
            moves++;
            if (PlayerPlaneNo > 0 && RoboPlaneNo > 0)
            {
                turn = moves % 2;
                if (turn == 1)
                    RoboTurn();
            }
            else
            {
                //if (PlayerPlaneNo == 0)
                {
                    if (RoboPlaneNo == 0)
                        title.Text = "Ai castigat!";
                    else
                        title.Text = "Rob a castigat!";

                    turn = 1;
                    MessageBox.Show("GAME OVER!");
                  
                    ResetScreen();
                }   
            }
        }


        //Reseteaza ecranului dupa finalul jocului cu desemnare castigatr, new game button si exit button
        public void ResetScreen()
        {
            //Sterge
            int i, j;
            for(i = 0; i < 10; i++)
                for(j = 0; j < 10; j++)
                { 
                    this.Controls.Remove(PlayerTable[i][j]); 
                    this.Controls.Remove(RoboTable[i][j]);
                }
            for(i = 1; i < 6; i++)
                    this.Controls.Remove(img[i]); 
            
            this.Controls.Remove(PlayerLabel);
            this.Controls.Remove(RoboLabel);
            this.Controls.Remove(Legend);
            this.Controls.Remove(Hit);
            this.Controls.Remove(Air);
            this.Controls.Remove(HitText);
            this.Controls.Remove(AirText);
            

            //Adauga
            newgame.Text = "New Game";
            newgame.Font = new System.Drawing.Font("Bradley Hand ITC", 20F, (System.Drawing.FontStyle.Bold));
            newgame.Top=this.Height *2 /3;
            newgame.Left=100;
            newgame.AutoSize = true;
            newgame.Click += new EventHandler(ClickNewGame);
            this.Controls.Add(newgame);

            exit.Text = "Exit";
            exit.Font = new System.Drawing.Font("Bradley Hand ITC", 20F, (System.Drawing.FontStyle.Bold));
            exit.Top = this.Height * 2 / 3;
            exit.Left = 500;
            exit.AutoSize = true;
            exit.Click += new EventHandler(ClickExit);
            this.Controls.Add(exit);
            
            this.Controls.Add(title);
        
        }

//New Game Button
        public void ClickNewGame(object sender, EventArgs e)
        {
            this.Controls.Remove(title);
            this.Controls.Remove(newgame);
            this.Controls.Remove(exit);
            GenerareObiecte();
        }

//Exit Button
        public void ClickExit(object sender, EventArgs e)
        {
            this.Close();
        }

        //Imagine initiala
        public void InitFormSettings() 
        {
            //dimensiune
            Width = 2 * (TopLeft + (w + padding) * 10) + marginx + 100;
            Height = TopUp + (h + padding) * 14 + marginy + 100;
            this.Text = "AVIOANELE";
            this.BackgroundImage = Image.FromFile("funnyplane2.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;


            //Mesaje 
            title.Text="Lucrare de atestat la informatica" + "\n" + "Mini-Game AVIOANELE";
            title.AutoSize = false;
            //title.Font = new System.Drawing.Font("Monotype Corsiva", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title.Font = new System.Drawing.Font("Lucida Handwriting", 20F, (System.Drawing.FontStyle.Bold));
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Dock = DockStyle.None;
            title.Height = 100;
            title.Top = this.Height / 3;
            title.Left = 0;
            title.Width = this.Width;
            title.BackColor = Color.Transparent;
            this.Controls.Add(title);
            
            //College name
            collegename.Text = "Colegiul National “Mihail Kogalniceanu”";
            collegename.AutoSize = false;
            collegename.Font = new System.Drawing.Font("Lucida Handwriting", 18F, (System.Drawing.FontStyle.Bold));
            collegename.TextAlign = ContentAlignment.MiddleCenter;
            collegename.Dock = DockStyle.None;
            collegename.Height = 100;
            collegename.Top = 10;
            collegename.Left = 0;
            collegename.Width = this.Width;
            collegename.BackColor = Color.Transparent;
            this.Controls.Add(collegename);
            
            //My name
            myname.Text = "Elev" + "\n" + "Nazare Daniela-Andreea" + "\n" + "Clasa a XII-a D";
            myname.AutoSize = false;
            myname.Font = new System.Drawing.Font("Lucida Handwriting", 12.5F, (System.Drawing.FontStyle.Bold));
            myname.TextAlign = ContentAlignment.TopLeft;
            myname.Dock = DockStyle.None;
            myname.Height = 100;
            myname.Top = this.Height *2 /3;
            myname.Left = this.Width /2 + 40;
            myname.Width = this.Width /2;
            myname.BackColor = Color.Transparent;
            this.Controls.Add(myname);

            //Teacher name
            teachname.Text = "Profesor coordonator" + "\n" + "Neagu Violeta";
            teachname.AutoSize = false;
            teachname.Font = new System.Drawing.Font("Lucida Handwriting", 12.5F, (System.Drawing.FontStyle.Bold));
            teachname.TextAlign = ContentAlignment.TopLeft;
            teachname.Dock = DockStyle.None;
            teachname.Height = 100;
            teachname.Top = this.Height *2 / 3;
            teachname.Left = 30;
            teachname.Width = this.Width /3;
            teachname.BackColor = Color.Transparent;
            this.Controls.Add(teachname);
            
            //Buton de start
            start.Top = this.Height *2 /3;
            start.Left = this.Width / 2 - start.Width -10;
            start.Text = "START";
            start.Font = new System.Drawing.Font("Bradley Hand ITC", 20F, (System.Drawing.FontStyle.Bold));
            start.AutoSize = true;
            
            start.Click += new EventHandler(StartClick);
            this.Controls.Add(start);


        }
       
        //Click on START Button
        public void StartClick(object sender, EventArgs e) 
        {   
            //this.BackgroundImage = null;
            this.Controls.Remove(start);
            this.Controls.Remove(title);
            this.Controls.Remove(collegename);
            this.Controls.Remove(myname);
            this.Controls.Remove(teachname);
            
            GenerareObiecte();
        }


        //What makes this real
        private void Form1_Load(object sender, EventArgs e)
        {
            InitFormSettings();
        }
    }
}



