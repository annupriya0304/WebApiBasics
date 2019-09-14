using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace APi_Basics
{
    public class EmployeeSecurity
    {
        public static bool Login(string Id, string Password)
        {
            using (EmployeeDBEntities Entities = new EmployeeDBEntities())
            {
                return Entities.Users.Any(x => x.Username.Equals(Id, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(Password, StringComparison.OrdinalIgnoreCase));
            }
               
        }
    }
}