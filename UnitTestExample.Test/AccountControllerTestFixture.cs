using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true),
            TestCase("0123456789",false)
            
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidateEmail(email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        //"A megadottt jelszó nem megfelelő!\n" +
         //           "A jelszó legalább 8 karakter hosszú kell legyen, csak az angol ABC betűiből és számokból állhat, 
        //és tartalmaznia kell legalább egy kisbetűt, egy nagybetűt és egy számot.");



        [
            Test,
            TestCase("AbcdefghLjhghgh@", false),
            TestCase("ABCDEFG1", false),
            TestCase("abcdefg1", false),
            TestCase("ac1A",false),
            TestCase("Abcdefg1", true),


        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidatePassword(password);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
