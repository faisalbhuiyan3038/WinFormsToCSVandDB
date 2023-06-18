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
            SqlCommand command = new SqlCommand("INSERT INTO UserData(Name,Email,Contact) values ('" + name + "','" + email + "','" + contact + "')", connection);

            int i = command.ExecuteNonQuery();
            if (i != 0)
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