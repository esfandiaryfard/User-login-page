//********************************************************************//
//     This is checking Ip form of final application,                //
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\anahita\Desktop\user\user\user.mdf;Integrated Security=True;User Instance=True");//Connect to the database 
        string localIp = "?"; //declare a variable for get local Ip
        private void enter_Click(object sender, EventArgs e)
        {
            con.Open(); //open the connection
            IPHostEntry host; //declare a variable for finding Ip
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = ip.ToString(); //get Ip and put into the localIp variable
                }
            }
            //MessageBox.Show(localIp);
            //connect to the database to get Ip and compare with localIp value 
            SqlCommand cmd = new SqlCommand("select Ip from autocomp where IP='" + localIp + "'", con); 
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count += 1;
            }
            if (count == 1)//if local Ip exist go to next
            {
                dr.Close();
                Next();
            }
            else
            {
                MessageBox.Show("شما مجاز نیستید");
                this.Close();
            }
            con.Close();
        }
        private void Next()//check if the Ip is authorized or not
        {
            SqlCommand cmd1 = new SqlCommand("select autoIp from autocomp where autoIp='" + localIp + "'", con);
            SqlDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            int count = 0;
            while (dr1.Read())
            {
                count += 1;
            }
            if (count == 1)//If its true get username for enter the programme
            {
                getuser();
            }
            else
            {
                MessageBox.Show("شما مجاز نیستید");
                this.Close();
            }
        }
        private void getuser()//open a new form for enter username and password
        {
            new Form2().Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        }
    }

//*********************************************************//
//code and designed by Anahita Esfandiaryfard February 2015//
//********************************************************//