using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ConnectDatabase
{       
      
    class ClubRegistrationQuery
    {

        private SqlConnection sqlConnect;
        private SqlCommand SqlCommand;
        private SqlDataAdapter SqlAdapter;
        private SqlDataReader SqlReader;

        private string connectionString;

        public DataTable dataTable = new DataTable();
        public BindingSource bindingSource = new BindingSource();
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;

        public int _Age;

        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=Jericho-Garcia;Initial Catalog=ClubDB;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }
        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers ";
            SqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
            dataTable.Clear();
            SqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;

            return true;
        }

        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            SqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES (@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);

            SqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            SqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID;
            SqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = StudentID;
            SqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            SqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            SqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            SqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            SqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            SqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;

            sqlConnect.Open();
            SqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            return true;
        }

        public bool UpdateStudent(long StudentID, int Age, string Program)
        {
            SqlCommand = new SqlCommand("UPDATE ClubMembers SET Age = @Age, Program = @Program WHERE StudentId = @StudentId", sqlConnect);

            SqlCommand.Parameters.AddWithValue("@StudentId", StudentID);
            SqlCommand.Parameters.AddWithValue("@Age", Age);
            SqlCommand.Parameters.AddWithValue("@Program", Program);

            sqlConnect.Open();
            SqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            return true;
        }

        public void DisplayID(ComboBox comboBox)
        {
            string getID = "SELECT * FROM ClubMembers";
            SqlCommand = new SqlCommand(getID, sqlConnect);
            sqlConnect.Open();
            SqlReader = SqlCommand.ExecuteReader();

            while (SqlReader.Read())
            {
                comboBox.Items.Add(SqlReader["StudentId"]);
            }

            sqlConnect.Close();
        }

        public void DisplayText(string ID)
        {
            string getID = "SELECT * FROM ClubMembers WHERE StudentId = @Id";
            SqlCommand = new SqlCommand(getID, sqlConnect);
            SqlCommand.Parameters.AddWithValue("@Id", ID);
            sqlConnect.Open();
            SqlReader = SqlCommand.ExecuteReader();

            while (SqlReader.Read())
            {
                _FirstName = SqlReader.GetString(2);
                _MiddleName = SqlReader.GetString(3);
                _LastName = SqlReader.GetString(4);
                _Age = SqlReader.GetInt32(5);
                _Gender = SqlReader.GetString(6);
                _Program = SqlReader.GetString(7);

            }

            sqlConnect.Close();
        }
    }
}
