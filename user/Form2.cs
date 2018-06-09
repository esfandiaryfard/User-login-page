//********************************************************************//
//     This is checking login form of final application,             //
//     This version is coded and designed by Anahita Esfandiaryfard //
//     For your information please read comments,                  //
//     and for more detail and any question conatact me,          //
//     anita.kntu@gmail.com                                      //
//**************************************************************//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;//for connect to the sql  
using System.Data.SqlClient;//FOR connect to the sql
using System.Net;//for connect to the sql

namespace user
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            button2.Hide();
            button3.Hide();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        int wctr = 0;
        //connecting to database
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\anahita\Desktop\user\user\user.mdf;Integrated Security=True;User Instance=True");
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();//connect to the databse
            SqlCommand cmd = new SqlCommand("select username from login where username='" + textBox1.Text + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            //check if the username is correct
            while (dr.Read())
            {
                count += 1;
            }
            if (count == 1)//if username is correct go to get password
            {
                dr.Close();
                Password();

            }
            else //alert code raise
            {
                MessageBox.Show("کد اخطار را وارد کنید");
                button3.Show();
            }
            dr.Close();
            con.Close();
        }

        private void pass()
        {
           
            SqlCommand cmd3 = new SqlCommand("select password from login where alertcode='" + textBox4.Text + "'and password='" + textBox2.Text + "'", con);
            SqlDataReader dr3;
            dr3 = cmd3.ExecuteReader();
            int count = 0;
            while (dr3.Read())
            {
                count += 1;
            }
            if (count == 1)
            {
                dr3.Close();
                SqlCommand cmd14 = new SqlCommand("select username from login alertcode='" + textBox4.Text + "'and username='" + textBox1.Text + "'", con);
                SqlDataReader dr14;
                dr14 = cmd14.ExecuteReader();
                int count1 = 0;
                while (dr14.Read())
                {
                    count1 += 1;
                }
                if (count1 == 1)
                {
                    int ctr = 0;
                    ctr = ctr + 50;
                    if (ctr >= 1000)
                    {
                        MessageBox.Show("!!!!!!برنامه تخریب شد");
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    SqlCommand cmd5 = new SqlCommand("INSERT INTO login (username, password, alertcode) VALUES (@username, @password, @alertcode)");
                    cmd5.CommandType = CommandType.Text;
                    cmd5.Connection = con;
                    cmd5.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd5.Parameters.AddWithValue("@password", textBox2.Text);
                    cmd5.Parameters.AddWithValue("@alertcode", textBox3.Text);
                    con.Open();
                    cmd5.ExecuteNonQuery();
                }

            }
            else
            {
                this.Close();
            }
        }
        private void Password()
        {
            //check for password
            SqlCommand cmd2 = new SqlCommand("select password from login where username='" + textBox1.Text + "'and password='" + textBox2.Text + "'", con);
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            int count2 = 0;
            while (dr2.Read())
            {
                count2 += 1;
            }
            if (count2 == 1 && wctr == 0)//if password is true open the form
            {
                dr2.Close();
                open();
            }
            else //lock the programme
            {
                wctr++;
                if (wctr <= 2)
                {
                    MessageBox.Show("!!!!برنامه قفل شد");
                    dr2.Close();
                }
                else //after two temprory lock ask security question
                {
                    dr2.Close();
                    next();
                }
            }
        }
        private void next()
        {

            if (wctr == 1)
            {
                askquestion();
            }
            else
            {
                next1();
            }
        }
        private void next1()
        {
            if (wctr == 2)
            {
                askquestion();
            }
            else
            {
                next2();
            }
        }
        private void next2()
        {
            if (wctr == 3)
            {
                askquestion();
            }
            else
            {
                next3();
            }
        }
        private void next3()
        {
            if (wctr == 4)
            {
                MessageBox.Show("!!!قفل دائم");
                this.Close();
            }
        }
        private void askquestion()
        {
            MessageBox.Show("به سوال پاسخ دهید");
            Random random = new Random();
            int rnd = random.Next(1, 3);
            SqlCommand cmd3 = new SqlCommand("select * from question where ID='" + rnd + "'", con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                textBox5.Text = dr3["question"].ToString();
            }
            dr3.Close();
            button2.Show();
        }
        private void open()
        {

            new Form3().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //check if answer is true
            con.Open();
            SqlCommand cmd12 = new SqlCommand("select answer from question where question='" + textBox5.Text + "'and answer='" + textBox4.Text + "'", con);
            SqlDataReader dr12 = cmd12.ExecuteReader();
            int count = 0;
            while (dr12.Read())
            {
                count += 1;
            }
            if (count == 1)
            {
                dr12.Close();
                wctr--;
                Help();
            }
            else
            {
                dr12.Close();
                if (wctr == 1)
                {
                    wctr++;
                    next1();
                }
                if (wctr == 2)
                {
                    wctr++;
                    next2();
                }
                if (wctr == 3)
                {
                    wctr++;
                    next3();
                }
            }
            dr12.Close();
            con.Close();
        }
        private void Help()
        {
            if (wctr == 0)
            {
                open();
            }
            if (wctr == 1 || wctr == 2)
            {
                askquestion();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd13 = new SqlCommand("select alertcode from login where alertcode='" + textBox3.Text + "'", con);
            SqlDataReader dr13;
            dr13 = cmd13.ExecuteReader();
            int count1 = 0;
            while (dr13.Read())
            {
                count1 += 1;
            }
            if (count1 == 1)
            {
                dr13.Close();
                pass();

            }
            else
            {
                this.Close();
            }
        }
        }
     
        }

//*********************************************************//
//code and designed by Anahita Esfandiaryfard February 2015//
//********************************************************//