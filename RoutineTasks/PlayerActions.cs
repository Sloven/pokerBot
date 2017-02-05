using Config;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowManipulations;
using WindowsInput;

namespace RoutineTasks
{
    public class PlayerActions
    {
        public static void RegisterNewPlayer(User player)
        {
            try{
                MouseSimulator.MoveLClick(659 + 25, 527 + 25);
                MouseSimulator.MoveLClick(294 + 5, 320 + 5);
                Thread.Sleep(5000);
                MouseSimulator.MoveLClick(386 + 5, 210 + 5);
                InputSimulator.SimulateTextEntry(player.GameLogin);
                
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateTextEntry(player.GamePass);
                
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateTextEntry(player.GamePass);
                
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateTextEntry(player.Email);
                
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateTextEntry(player.Email);
                
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_F);

                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.SPACE);

                //create account btn pressed
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.SPACE);
                
                //Not checked
                Thread.Sleep(3000);
                MouseSimulator.MoveLClick(247 + 5, 800 + 5);

                player.UpdateAccountsFile();

            }
            finally
            {
                GameActions.Close();
            }
        }

        public static void LogInExistedPlayer(User player)
        {
                Ops.ClickChild("PokerStars Lobby", "Log In", 1000, "PokerStarsButtonClass");
                Thread.Sleep(3000);
                Ops.ClickWindow("Log In",xDelta:10, yDelta:40);
                
                InputSimulator.SimulateTextEntry(player.GameLogin);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                InputSimulator.SimulateTextEntry(player.GamePass);

                Ops.ClickChild("Log In", "Log In", 0, "PokerStarsButtonClass");
                Thread.Sleep(10000);
                Ops.ClosePopup("My News");
            
        }

        public static void GetInWithAnyPlayer()
        {
            User p = GetPlayer(UserSate.First_Registered_NotBanned);
            if (p == null)
            {
                p = GetPlayer(UserSate.First_NotRegistered);
                if(p != null)
                    RegisterNewPlayer(p);
                else
                    throw new Exception("Player list exhousted");
            }

            LogInExistedPlayer(p);
        }

        private static User GetPlayer(UserSate state)
        {
            User p = new User();
            string maindir = ConfigResolver.GetSetting("MAIN_DIR");
            string filePath = String.Concat(maindir, ConfigResolver.GetSetting("ACCOUNTS_FILE_DIR"));
            string[] accounts = File.ReadAllLines(filePath);

            for (int i = 0; i < accounts.Length; i++)
            {
                string[] split = accounts[i].Split(';');
                if (split.Length == 7)
                {
                    if (split[6] == "false" && state == UserSate.First_Registered_NotBanned)
                        return new User(filePath, i, state);
                }
                else if (split.Length == 2 && state == UserSate.First_NotRegistered)
                    return new User(filePath, i,state);
            }

            return null;
        }
    }
}
