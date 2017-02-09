using System;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Results;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using EmployeeMS.Controllers;
using EmployeeMS.Models;

namespace EmployeesMS.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        List<Employee> _employees = new List<Employee>(){new Employee(){FirstName="Peter", LastName="Worlin", Grade=5, ID=1, IsManager=true},
                                new Employee(){FirstName="Clare", LastName="Worlin", Grade=3, ID=2, IsManager=true},
                                new Employee{FirstName="Lauren", LastName="Worlin", Grade=2, ID=3, IsManager=false},
                                new Employee{FirstName="Sophie", LastName="Worlin", Grade=1, ID=4, IsManager=false}};

        EmployeeController ec;

        [TestInitialize]
        public void Init()
        {
            //Arrange
            ec = new EmployeeController(_employees);
            ec.Request = new System.Net.Http.HttpRequestMessage();
            ec.Configuration = new System.Web.Http.HttpConfiguration();
        }

        [TestMethod]
        public void TestGetEmployees()
        {
            var response = ec.GetEmployees();

            Assert.IsNotNull(response);

            //ReadAsAsync is an extension method on System.Threading.Tasks - hence need a using stmt
            var emps = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            Assert.AreEqual(4, emps.Count());

        }

        [TestMethod]
        public void TestGetManagers()
        {
            var response = ec.GetManagers();

            Assert.IsNotNull(response);

            var mgrs = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            Assert.AreEqual(2, mgrs.Count());
        }

       

    }
}
