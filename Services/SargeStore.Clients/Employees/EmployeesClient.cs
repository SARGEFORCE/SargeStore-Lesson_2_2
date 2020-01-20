using Microsoft.Extensions.Configuration;
using SargeStore.Clients.Base;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SargeStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, "api/employees") { }

        public void Add(EmployeeView Employee) => Post(_ServiceAddress, Employee);

        public bool Delete(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public EmployeeView Edit(int id, EmployeeView Employee)
        {
            var response = Put($"{_ServiceAddress}/{id}", Employee);
            return response.Content.ReadAsAsync<EmployeeView>().Result;
        }
 
        public IEnumerable<EmployeeView> GetAll() => Get<List<EmployeeView>>(_ServiceAddress);

        public EmployeeView GetById(int id) => Get<EmployeeView>($"{_ServiceAddress}/{id}");

        public void SaveChanges() { }
    }
}
