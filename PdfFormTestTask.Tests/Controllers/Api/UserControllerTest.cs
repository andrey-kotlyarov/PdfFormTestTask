using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFormTestTask.Client;
using PdfFormTestTask.Model;
using PdfFormTestTask.Service.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Tests.Controllers.Api
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Get_WrongPassword_NULL()
        {
            UserController controller = new UserController();
            //wrong password
            PfsResponse<PfsUser> resp = controller.Get("user1", "pass");
            Assert.IsNull(resp.Data);
            Assert.IsFalse(resp.IsOk);
        }

        [TestMethod]
        public void Get_WrongUsername_NULL()
        {
            UserController controller = new UserController();
            //wrong username
            PfsResponse<PfsUser> resp = controller.Get("user", "pass1");
            Assert.IsNull(resp.Data);
            Assert.IsFalse(resp.IsOk);
        }

        [TestMethod]
        public void Get_RightCredentials_NOT_NULL()
        {
            UserController controller = new UserController();
            //right credentials for user with id = 1
            PfsResponse<PfsUser> resp = controller.Get("user1", "pass1");
            Assert.IsNotNull(resp.Data);            
            Assert.AreEqual(resp.Data.Id, 1);
            Assert.IsTrue(resp.IsOk);
        }
    }
}
