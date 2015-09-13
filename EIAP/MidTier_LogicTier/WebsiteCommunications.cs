/* Programmer: Matthew White
 * Date: 5/4/2015
 * File Name: ExternaIP.cs
 * 
 * Purpose:  Class exists to accept a HTTP address, browses to the website and returns an External IP Address.  
 * The HTTP Address must be to a website that displays external IP address.  This class will work on most routers that accept
 * browser authentication.
 */

using System;
using System.Net;

namespace EIAP
{
    class WebsiteCommunications
    {
        //-------------------- PROPERTIES --------------------------------
        private WebClient client { get; set; }
        internal string websiteText { get; set; }
        internal string httpAddress {get; set;}
        internal string userName { get; set; }
        internal string passWord { get; set; }
        //--------------------  END of PROPERTIES -----------------------

        // Constructor
        public WebsiteCommunications()
        {
            instantiateVariables();
        }

        // Download HTML of a website, accepts an httpAddress
        private string downloadWebsite(string httpAddress)
        {
            try
            {   // Saves HTML from a website to property
                websiteText = client.DownloadString(httpAddress);
            }
            catch (System.Net.WebException)
            {   // Return 404 to notify that HTTP address could not be found
                return "404";
            }
            catch (Exception)
            {
                // do nothing
            }

            // Return HTML from website
            return websiteText;
        }

        // Instantiate variables -- used when class loads
        private void instantiateVariables()
        {
                client = new WebClient();
                websiteText = "";
        }

        // Return HTML - uses private downloadWebsite function
        public void loadWebsite()
        {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                websiteText = downloadWebsite(httpAddress);
        }

        // Return HTML for websites that require authentication like a Router - uses private downloadWebsite function
        public void loadWebsiteWithAuthentication()
        {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Credentials = new NetworkCredential(userName, passWord);
                websiteText = downloadWebsite(httpAddress);
        }
    }
}
