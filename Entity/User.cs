using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum UserSate { First_Registered_NotBanned, First_NotRegistered };

    public class User
    {
        public string AccountsFile { get; set; }
        public int AccountIndex { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GameLogin { get; set; }
        public string GamePass { get; set; }
        public string Email { get; set; }
        public string EmailPass { get; set; }
        public bool IsBanned { get; set; }

        public User() {
            IsBanned = false;
        }

        public User(string AccFile, int AccIndex, UserSate state)
        {
            if (!String.IsNullOrEmpty(AccFile))
            {
                string[] accountString = (File.ReadAllLines(AccFile)[AccIndex]).Split(';');
                Email = accountString[0];
                EmailPass = accountString[1];
                if (state == UserSate.First_NotRegistered)
                {
                    FirstName = "FirstName";// accountString[2];
                    LastName = "LastName";  // accountString[3];
                    GameLogin = Email.Split('@')[0]; //accountString[4];
                    GamePass = GeneratePassword(EmailPass);
                }
                else if (state == UserSate.First_Registered_NotBanned)
                {
                    FirstName = accountString[2];
                    LastName = accountString[3];
                    GameLogin = accountString[4];
                    GamePass = accountString[5];
                }

                AccountsFile = AccFile;
                AccountIndex = AccIndex;
                IsBanned = false;
            }

        }

        private string GeneratePassword(string EPass)
        {
            if (Char.IsDigit(EPass[0]))
                return String.Concat("S", EPass);
            else
                return EPass;
        }

        public void UpdateAccountsFile()
        {
            // Read the old file.
            string[] lines = File.ReadAllLines(AccountsFile);
            string updatedLine = string.Join(";",Email,EmailPass,FirstName,LastName,GameLogin,GamePass,IsBanned);
            lines[AccountIndex] = updatedLine;

            File.Copy(AccountsFile, AccountsFile + ".bak");
            // Write the new file over the old file.
            File.Delete(AccountsFile);
            File.WriteAllLines(AccountsFile,lines);
            //delete bak if all good;
            File.Delete(AccountsFile + ".bak");
        }
    }
}
