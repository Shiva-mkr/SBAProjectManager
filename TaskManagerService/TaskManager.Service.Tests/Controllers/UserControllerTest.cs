using System;
using TaskManager.Service.Controllers;
using TaskManger.DTOContext;
using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace TaskManager.Service.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        [Test, TestCase("{'UserID':'0','EmployeeID':'1','FirstName':'Pushparaj','LastName':'Pushpanathan'}")]
        public void CreateUser(object data)
        {
            
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            UserManagerContext obj = json_serializer.Deserialize<UserManagerContext>(data.ToString());
            var actionResult = controller.AddOrUpdateUsererList(obj);

            Assert.IsNotEmpty(actionResult);


        }

        [Test, TestCase("startdate")]
        public void GetUserList(string data)
        {

            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.GetUserList(data);

            Assert.IsNotNull(actionResult);


        }

        [Test, TestCase("1")]
        public void DeleteUser(string data)
        {

            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.DeleteUser(data);

            Assert.IsNotEmpty(actionResult);


        }

        [Test, TestCase("1")]
        public void DeleteProject(string data)
        {

            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.DeleteProject(data);

            Assert.IsNotEmpty(actionResult);


        }
        [Test, TestCase("{'Project_ID':'0','Project':'Project 2','Start_Date':'06/26/2018','End_Date':'07/16/2018','Priority':'50'}")]
        public void AddOrUpdateProject(object data)
        {

            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            ProjectrManagerContext obj = json_serializer.Deserialize<ProjectrManagerContext>(data.ToString());
            var actionResult = controller.AddOrUpdateProject(obj);

            Assert.IsNotEmpty(actionResult);


        }

        [Test, TestCase("startdate")]
        public void GetProjectList(string data)
        {

            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.GetProjectList(data);

            Assert.IsNotNull(actionResult);


        }

    }
}
