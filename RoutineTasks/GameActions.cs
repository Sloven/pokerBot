using Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowManipulations;
using WindowsInput;

namespace RoutineTasks
{
    public class GameActions
    {
        public static void Start()
        {
            string mainDir = ConfigResolver.GetSetting("MAIN_DIR");
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo = new ProcessStartInfo();
            startInfo.FileName = ConfigResolver.GetSetting("GAME_PATH");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = ConfigResolver.GetSetting("GAME_DIR");
            Process.Start(startInfo);
            Thread.Sleep(5000);

            AutoUpdate();
            Ops.WaitUntillWindowAppears("#32770", "PokerStars Lobby", 50000);
            Ops.WaitUntillWindowExists("#32770", "PokerStars Update", 50000);
            Ops.WaitUntillWindowExists("#32770", "Connecting ...", 50000);
            ClosePopup("News");

        }

        public static void Close() 
        {
            var pokerStars = Process.GetProcessesByName(ConfigResolver.GetSetting("GAME"))[0];
            if (pokerStars != null)
                pokerStars.Kill();
        }

        private static void ClosePopup(string popupCaption)
        {
            Ops.ClosePopup(popupCaption);
        }

        private static void AutoUpdate() 
        {
            var dlg = Ops.FindChild("PokerStars Update", "A newer version of the software is available on the server","Static");
            if(dlg != IntPtr.Zero)
            {
                Ops.SendWindowKey(dlg,VirtualKeyCode.RETURN);
            }
        }
    }
}
