using System;
using TaskManager.Service.Controllers;
using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Web.Http;

namespace TaskManager.Service.Tests.Controllers
{
    [TestFixture]
    public class TaskControllerTest
    {
        [Test, TestCase("{'Task_ID':'0','Parent_ID':'0','Task':'Task2','Priority':'5'}")]
        public void CreateTask(object data)
        {

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.CreateTask(data);

            Assert.AreEqual("OK", actionResult.StatusCode.ToString());


        }

        [Test, TestCase("{'Task_ID':'1','Parent_ID':'0','Task':'Task1','Priority':'5'}")
        , TestCase("{'Task_ID':'2','Parent_ID':'1','Task':'Task1','Priority':'5'}")]
        public void UpdateTask(object data)
        {

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.UpdateTask(data);

            Assert.AreEqual("OK", actionResult.StatusCode.ToString());
        }

        [TestCase(1)]
        public void EndTask(int id)
        {

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.EndTask(id);

            Assert.AreEqual("OK", actionResult.StatusCode.ToString());
        }

        [TestCase(2)]
        public void GetTaskById(int id)
        {

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.GetTaskById(id);

            Assert.AreEqual("OK", actionResult.StatusCode.ToString());
        }

        [TestCase()]
        public void ParentTask()
        {

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            var actionResult = controller.ParentTask();

            Assert.AreEqual("OK", actionResult.StatusCode.ToString());
        }



    }
}
