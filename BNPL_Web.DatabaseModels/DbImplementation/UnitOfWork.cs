

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;
using BNPL_Web.Common.Interface;

namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<ApplicationRole> AspNetRole => throw new NotImplementedException();
    }
}
