
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;
using BNPL_Web.Common.Interface;

namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public interface IUnitOfWork
    {
        IRepository<ApplicationRole> AspNetRole { get; }
    }
}
