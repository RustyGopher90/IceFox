namespace IceFox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string input = searchTextbox.Text;
            MessageBox.Show(input);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}