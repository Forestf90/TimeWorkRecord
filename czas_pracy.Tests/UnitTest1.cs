using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using czas_pracy;

namespace czas_pracy.Tests
{
    [TestClass]
    public class UnitTest1
    {
        SqlConnection con;
        [TestMethod]
        public void Polaczenie()
        {

        
             con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
             con.Open();
            bool tak= false;
            if (con.State == ConnectionState.Open) tak = true;
            Assert.AreEqual(true, tak);
        }

        [TestMethod]
        public void Update()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
            //con.Open();

            con.Open();
            string a = @"Update Pracownik Set DataZwolnienia = null";
            SqlCommand cmd = new SqlCommand(a, con);
           // cmd.ExecuteNonQuery();
            AssertFailedException.Equals(true, cmd.ExecuteNonQuery());
        }

        [TestMethod]
        public void Insert()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
            //con.Open();

            con.Open();
            string a = @"INSERT INTO Pracownik Values('11' ,'Paulina' , 'Stachnik', '2000.01.01' ,
                            null , '50' , 'o prace' , '8')";
            SqlCommand cmd = new SqlCommand(a, con);
            // cmd.ExecuteNonQuery();
            AssertFailedException.Equals(true, cmd.ExecuteNonQuery());
        }

        [TestMethod]
        public void Delete()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0PBRBDG;Initial Catalog=Dolars;Integrated Security=True");
            //con.Open();

            con.Open();
            string a = @"Delete from Pracownik where Nazwisko='Stachnik'";
            SqlCommand cmd = new SqlCommand(a, con);
            // cmd.ExecuteNonQuery();
            AssertFailedException.Equals(true, cmd.ExecuteNonQuery());
        }
    }
}
