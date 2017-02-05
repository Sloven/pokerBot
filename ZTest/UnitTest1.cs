using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoutineTasks;
using Entity;
using Config;
using System.Runtime.InteropServices;
using System.Text;
using DataAccess;

namespace ZTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Classifier c = new Classifier();
            //c.main();
        }
        [TestMethod]
        public void CreateNewAccount()
        {
            var p = new User(ConfigResolver.GetSetting("ACCOUNTS_FILE"), 0, UserSate.First_NotRegistered);
            PlayerActions.RegisterNewPlayer(p);
        }

        [TestMethod]
        public void Login()
        {
            var p = new User(ConfigResolver.GetSetting("ACCOUNTS_FILE"), 0, UserSate.First_Registered_NotBanned);
            PlayerActions.LogInExistedPlayer(p);
        }

        [TestMethod]
        public void Mac()
        {
            MACAddress.SetMAC();
            MACAddress.GetMAC();
        }


        [TestMethod]
        public void BigTry()
        {
            MACAddress.SetMAC();

            var p = new User(ConfigResolver.GetSetting("ACCOUNTS_FILE"), 0, UserSate.First_Registered_NotBanned);
            PlayerActions.LogInExistedPlayer(p);

        }

        [TestMethod]
        public void CreateDBContext()
        {
            Context cnt = new Context();
        }

    }
}
