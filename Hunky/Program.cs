using RoutineTasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hunky
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            if (args.Length == 0)
                args = new string[] { string.Empty };

            switch (args[0])
            {
                case "login":
                    PlayerActions.GetInWithAnyPlayer();
                    //report result
                    break;
                case "createnew": 
                    //get new proxy
                    //renew computer 
                    //set new mac 
                    //find new email
                    //start proxyfier
                    //start pokerstars
                    //create new user
                    //verify email
                    //report result
                    break;
                case "renewmac":
                    MACAddress.SetMAC();
                    break;
                case "help":
                    StringBuilder sb = new StringBuilder();
                    sb.Append("login:\r\n\t1)start proxyfier \r\n\t2)start pokerstars \r\n\t3)login \r\n\t4)report result \r\n");
                    sb.Append("createnew:\r\n\t1)get new proxy \r\n\t2)renew computer \r\n\t3)set new mac \r\n\t4)find new email \r\n\t5)start proxyfier \r\n\t6)start pokerstars \r\n\t7)create new user \r\n\t8)verify email \r\n\t9)report result\r\n");
                    sb.Append("generateemails:\r\n\t1)start mail generator \r\n\t2)save mails to file\r\n");
                    sb.Append("genereteproxies:\r\n\t1)start proxy generator \r\n\t2)save proxies to file\r\n");
                    sb.Append("renewcomputer:\r\n\t1)start stzBlaster \r\n\t2)set new mac \r\n\t3)restart\r\n");
                    sb.Append("renewproxy:\r\n\t1)loop throught proxies \r\n\t2)test each proxy untill \r\n\t3)proxy pass the test \r\n\t4)apply proxy\r\n");
                    Console.Write(sb.ToString());
                    //Console.ReadKey();
                    break;
                default:
                    Application.Run(new MainForm());
                    break;

            }
        }
    }
}
