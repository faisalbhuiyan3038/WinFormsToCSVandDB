using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email format is invalid");
                return;
            }

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string email = txtEnterEmail.Text;

            SqlConnection connection = new SqlConnection("Data Source=FAISAL-PC\\SQLEXPRESS;Initial Catalog=UserInformation;Integrated Security=True");
            connection.Open();

            string query = "SELECT * FROM UserData WHERE Email = @Email";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = command.ExecuteReader();

            StringBuilder messageText = new StringBuilder();

            if (reader.HasRows)
            {
                messageText.Clear();
                messageText.Append("Email Found");
            }
            else
            {
                messageText.Clear();
                messageText.Append("Email Does Not Exist");
            }

            if (IsValidEmail(email))
            {
                messageText.Append(" and Email is Valid");
                MessageBox.Show(messageText.ToString());
            }
            else
            {
                messageText.Append(" and Email is Invalid");
                MessageBox.Show(messageText.ToString());
            }

            reader.Close();
            connection.Close();
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}