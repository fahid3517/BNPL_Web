using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.DatabaseModel.DbImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer.Utilities
{
    public class CommonUtility
    {
        //public static int GetUserEmployeeId(string user)
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();
        //    var unitofWork  = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
        //    int id = 0;
        //             var employee = unitofWork.DbEmployeesRepository.Get(p => p.User.UserName.Equals(user));
        //    if (employee != null)
        //    {
        //        id = employee.Id;
        //    }
        //    return id;
        //}
        //public static FunctionResult  ValidateEmployee(EmployeeViewModel model)
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();
        //    var unitofWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
        //    var isExist= unitofWork.DbEmployeesRepository.Get(p => p.Code.Equals(model.Code));
        //    if (isExist != null)
        //    {
        //        return new FunctionResult { success = false, message = "Code Already Exist" };
        //    }
        //    if (model.LocationId ==0 )
        //    {
        //        return new FunctionResult { success = false, message = "Location Is Invalid!" };
        //    }
        //    if (model.BussinessunitId == 0)
        //    {
        //        return new FunctionResult { success = false, message = "BusinessUnit  Is Invalid!" };
        //    }
        //    if (model.DivisionId == 0)
        //    {
        //        return new FunctionResult { success = false, message = "Division Is Invalid!" };
        //    }
        //    if (model.DateofBirth == DateTime.MinValue)
        //    {
        //        return new FunctionResult { success = false, message = "DOB Is Invalid!" };
        //    }
        //    if (model.CNICIssueDate == DateTime.MinValue)
        //    {
        //        return new FunctionResult { success = false, message = "CNIC Issue Date Is Invalid!"};
        //    }
        //    if (model.CNICExpiryDate == DateTime.MinValue)
        //    {
        //        return new FunctionResult { success = false, message = "CNIC Expiry Date Is Invalid!" };
        //    }
        //    var CNIC = unitofWork.DbEmployeesRepository.Get(x => x.Cnic.Equals(model.CNIC));
        //    if (CNIC!=null)
        //    {
        //        return new FunctionResult { success = false, message = "CNIC Already Taken!" };
        //    }
        //    if (model.Code.Count()>10)
        //    {
        //        return new FunctionResult { success = false, message = "Code Should Not Grater Then 10 Characters!" };
        //    }

        //    return new FunctionResult { success = true, message = "Success!" };
        //}
    }
}
