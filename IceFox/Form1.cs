using System.Net;

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
            string uRLInput = searchTextbox.Text;
            GetWebContents(uRLInput);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GetWebContents(string uRLInput)
        {
            string sanitizedURL = FormatURLInput(uRLInput);
            if (sanitizedURL == "")
            {
                MessageBox.Show($"There was an error parsing the url you provided. " +
                    $"{uRLInput} is not a valid url. Please provide a url format like this" +
                    "https://www.somewebsite.com \nwww.somewebsite.com \nsomewebsite.com"
                    );
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create($"{sanitizedURL}");
                request.AllowAutoRedirect = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    
                        Stream data = response.GetResponseStream();
                        string html = String.Empty;
                        using (StreamReader sr = new StreamReader(data))
                        {
                            html = sr.ReadToEnd();
                        }
                        MessageBox.Show(html);
                    
                }
                else
                {
                    MessageBox.Show("There was an Error getting the website. Try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The url you provided is not good. Try another website. {ex.Message}");
            }      

     
  
        }

        private string FormatURLInput(string url)
        {
            if ((url.Contains("https://") || url.Contains("http://")) && url.Contains("www."))
            {
                return url;
            }else if (url.Contains("www.") && (!url.Contains("https://") || !url.Contains("http://")))
            {
                return $"https://{url}";
            }else if (!url.Contains("www.") && (!url.Contains("https://") || !url.Contains("http://")))
            {
                return $"https://www.{url}";
            }else
            {
                return "";
            }
        }
    }
}