using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string contact = txtContact.Text;

            SaveDataToCSV(name, email, contact);

            SqlConnection connection = new SqlConnection("Data Source=FAISAL-PC\\SQLEXPRESS;Initial Catalog=UserInformation;Integrated Security=True");
            connection.Open();

            string query = "IF NOT EXISTS (SELECT 1 FROM UserData WHERE Email = @Email) " +
                           "INSERT INTO UserData (Name, Email, Contact) VALUES (@Name, @Email, @Contact)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Contact", contact);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Error");
            }


            connection.Close();

        }

        private void SaveDataToCSV(string name, string email, string contact)
        {
            string filePath = "D:\\data.csv"; 

            bool fileExists = File.Exists(filePath);

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                
                if (!fileExists)
                {
                    sw.WriteLine("Name,Email,Contact"); 
                }

                sw.WriteLine(name + "," + email + "," + contact);
            }
        }

    }
}