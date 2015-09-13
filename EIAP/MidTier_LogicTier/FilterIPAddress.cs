// Programmer: Matthew White
// File: FilterIPAddress.cs
// Date: 5/4/2015
// The purpose of this class is to filter out an IP address from a string.

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EIAP
{
    class FilterIPAddress
    {
        public FilterIPAddress()
        {
            // Do nothing
        }
        
        // Accepts a string that holds information downloaded from a web page or other type of string
        // Uses Regex to filter out the IP address
        // Returns a string that holds the External IP Address
        public string filterExternalIPAddress(string aString)
        {
           // string tempExternalIP = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}").Match(aString).ToString();
            string tempExternalIP = new Regex(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b").Match(aString).ToString();
            return tempExternalIP;
        }

    }
}
