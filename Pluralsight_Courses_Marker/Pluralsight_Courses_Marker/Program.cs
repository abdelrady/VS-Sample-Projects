using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pluralsight_Courses_Marker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                if(Directory.Exists(arg))
                {
                    var myFolderIcon = new FolderIcon(arg);
                    var iconPath = ConfigurationManager.AppSettings["IconPath"];
                    myFolderIcon.CreateFolderIcon(iconPath, "This Means that this folder is done studying.");
                    MessageBox.Show("Done");

                }
            }
        }
    }
}
