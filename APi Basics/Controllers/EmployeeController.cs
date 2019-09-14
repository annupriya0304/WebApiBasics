using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace APi_Basics.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var ent = entities.Employees.FirstOrDefault(e => e.ID == id);
                if(ent != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ent);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, id.ToString() + "is not found");
                }
            }
            
        }

        public HttpResponseMessage Delete(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var ent = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (ent != null)
                {
                    entities.Employees.Remove(ent);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, ent);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, id.ToString() + "is not found");
                }
            }

        }

        public HttpResponseMessage Post([FromBody] Employee emp)
        {
            using (EmployeeDBEntities e = new EmployeeDBEntities())
            {
                try
                {
                    e.Employees.Add(emp);
                    e.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    message.Headers.Location = new Uri(Request.RequestUri + emp.ID.ToString());
                    return message;
                }

                catch (Exception Ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadGateway, Ex.ToString());
                }
            }
        }

        public HttpResponseMessage Put(int id,[FromBody] Employee emp)
        {
            using (EmployeeDBEntities e = new EmployeeDBEntities())
            {
                try
                {
                    var ent = e.Employees.FirstOrDefault(x => x.ID == id);
                    if (ent != null)
                    {
                        ent.FirstName = emp.FirstName;
                        ent.LastName = emp.LastName;
                        ent.Salary = emp.Salary;
                        ent.Gender = emp.Gender;
                        e.SaveChanges();

                        var message = Request.CreateResponse(HttpStatusCode.OK, emp);
                        return message;
                    }

                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadGateway, id.ToString() + "not found");
                    }
                }

                catch (Exception Ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadGateway, Ex.ToString());
                }
            }
        }
    }
}
