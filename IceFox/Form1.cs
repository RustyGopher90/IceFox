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
            string html = GetWebContents(uRLInput);
            htmlComponents components = ParseHTML(html);
            string[] headLinks = htmlComponents.GetAllHeadLinks(components.Head);
            foreach (var link in headLinks)
            {
                System.Diagnostics.Debug.WriteLine(link);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string GetWebContents(string uRLInput)
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
                    string htmlOnly = "";
                    using (StreamReader dataStream = new StreamReader(data))
                    {
                        htmlOnly = dataStream.ReadToEnd();
                    }
                    return htmlOnly;
                }
                else
                {
                    MessageBox.Show("There was an Error getting the website. Try again");
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The url you provided is not good. Try another website. {ex.Message}");
                return "";
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

        private htmlComponents ParseHTML(string html)
        {
            htmlComponents components = new htmlComponents();
            try
            {
                string headChars = "</head>";
                string bodyChars = "</body>";
                int headIndex = html.IndexOf(headChars);
                string head = html.Substring(0, headIndex + headChars.Length);
                int bodyIndex = html.IndexOf(bodyChars);
                string body = html.Substring(headIndex + headChars.Length, bodyIndex - (headIndex + bodyChars.Length));
                string whatsLeft = html.Substring(bodyIndex - (headIndex + bodyChars.Length), html.Length - bodyIndex);
                body = body + whatsLeft;
                components.Head = head;
                components.Body = body;
                components.WhatsLeft = whatsLeft;
                return components;

            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem parsing the html...Thats my bad!");
                return components;                
            }


        }

    }
}