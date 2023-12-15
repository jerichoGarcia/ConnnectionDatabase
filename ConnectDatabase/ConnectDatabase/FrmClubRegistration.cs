namespace ConnectDatabase
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age;
        private int count = 0;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;

        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();
            StudentId = Convert.ToInt64(txtStudentID.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Age = Convert.ToInt32(txtAge.Text);
            Gender = cmbGender.Text;
            Program = cmbProgram.Text;
            clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);

            Clear();
            MessageBox.Show("Successfully Registered Member!", "Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            new FrmUpdateMember().ShowDialog();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }
        public int RegistrationID()
        {
            return  count++;
        }
        public void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dgvClubMembers.DataSource = clubRegistrationQuery.bindingSource;
        }

        public void Clear()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtStudentID.Clear();
            txtAge.Clear();
           
        }
    }
}