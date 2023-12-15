using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ConnectDatabase
{
    public partial class FrmUpdateMember : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int Age;
        private string Program;
        private long StudentId;
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            clubRegistrationQuery.DisplayID(cmbStudentID);
        }

        public void Fill()
        {
            string ClubDBConnectionString = @"Data Source=Jericho-Garcia;Initial Catalog=ClubDB;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(ClubDBConnectionString);
            string getID = "SELECT * FROM ClubMembers";
            SqlCommand sqlCommand = new SqlCommand(getID, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                cmbStudentID.Items.Add(sqlDataReader["StudentId"]);
            }

            sqlConnection.Close();

        }
 
        public void TextFill()
        {
            txtFirstName.Text = clubRegistrationQuery._FirstName;
            txtLastName.Text = clubRegistrationQuery._LastName;
            txtMiddleName.Text = clubRegistrationQuery._MiddleName;
            txtAge.Text = clubRegistrationQuery._Age.ToString();
            cmbGender.Text = clubRegistrationQuery._Gender;
            cmbProgram.Text = clubRegistrationQuery._Program;
        }

        private void cmbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            clubRegistrationQuery.DisplayText(cmbStudentID.Text);
            TextFill();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            StudentId = Convert.ToInt64(cmbStudentID.Text);
            Age = Convert.ToInt32(txtAge.Text);
            Program = cmbProgram.Text;
            clubRegistrationQuery.UpdateStudent(StudentId, Age, Program);

            Clear();
            MessageBox.Show("Successfully Updated Member!", "Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Clear()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            
            txtAge.Clear();

        }
    }
}
