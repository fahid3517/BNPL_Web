using Microsoft.Extensions.Logging;
using Project.Common.ViewModels;
using Project.Common.ViewModels.Common;

using Project.DatabaseModel.DbImplementation;
using Project.DatabaseModel.Models;
using Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer.Services
{

    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork unitofWork;

        public UnitService(IUnitOfWork unitofWork)
        {
            this.unitofWork = unitofWork;

        }
        
        public ResponseViewModel Activate(UnitViewModel value)
        {
            var response = new ResponseViewModel();
            try
            {
                var _dbvalue = unitofWork.UnitRepository.Get(p => p.Id == value.Id);
                if (_dbvalue == null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Not Exist";
                    response.Message = "Not Exist";
                    return response;
                }
                _dbvalue.ValidFlag = true;
                _dbvalue.UpdatedBy = value.ChangedBy;
                _dbvalue.UpdatedOn = value.ChangedOn;

                unitofWork.UnitRepository.Update(_dbvalue);
                unitofWork.UnitRepository.Commit();

                response.LogLevel = LogLevel.Information;
                response.Status = HttpStatusCode.OK;
                response.obj = "Updated";
                return response;

            }
            catch (Exception ex)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.GetBaseException().Message;
                response.Message = ex.GetBaseException().Message;
                response.LogLevel = LogLevel.Error;
                return response;
            }
        }

        public ResponseViewModel Add(UnitViewModel value)
        {
            var response = new ResponseViewModel();
            try
            {

                var DB_Value = unitofWork.UnitRepository.Get(p => p.Code == value.Code);

                if (DB_Value != null)
                {

                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = " Code Already Exist";
                    response.Message = "Code Already Exist";
                    return response;
                }
                var unit = new DbUnit();

              unit.SectionId = value.SectionId;
                unit.Code = value.Code;
                unit.Name = value.Name;
                unit.Abbrevation = value.Abbreviation;

                unit.ValidFlag = true;
                unit.CreatedOn = value.ChangedOn;
                unit.CreatedBy = value.ChangedBy;




                unitofWork.UnitRepository.Add(unit);
                unitofWork.UnitRepository.Commit();

                response.Status = HttpStatusCode.Created;
                response.obj = "Unit Added Successfully";
                response.Message = "Unit Added Successfully";
                response.LogLevel = LogLevel.Information;
                return response;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.GetBaseException().Message;
                response.Message = ex.GetBaseException().Message;
                response.LogLevel = LogLevel.Error;
                return response;
            }
        }
        public ResponseViewModel Deactivate(UnitViewModel value)
        {
            var response = new ResponseViewModel();
            try
            {
                var _dbvalue = unitofWork.UnitRepository.Get(p => p.Id == value.Id);
                if (_dbvalue == null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Not Exist";
                    response.Message = "Not Exist";
                    return response;
                }
                _dbvalue.ValidFlag = false;
                _dbvalue.UpdatedBy = value.ChangedBy;
                _dbvalue.UpdatedOn = value.ChangedOn;

                unitofWork.UnitRepository.Update(_dbvalue);
                unitofWork.UnitRepository.Commit();

                response.LogLevel = LogLevel.Information;
                response.Status = HttpStatusCode.OK;
                response.obj = "Updated";
                return response;

            }
            catch (Exception ex)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.GetBaseException().Message;
                response.Message = ex.GetBaseException().Message;
                response.LogLevel = LogLevel.Error;
                return response;
            }
        }


        public ResponseViewModel GetActiveUnits(int Id = 0)
        {
            var response = new ResponseViewModel();
            try
            {
                var units = unitofWork.UnitRepository.GetMany(p => p.ValidFlag == true && p.SectionId == Id)
                 .Select(p => new UnitViewModel()
                 {
                     Id = p.Id,
                     Name = p.Name
                 });

                response.Status = HttpStatusCode.OK;
                response.Message = "Success";
                response.obj = units;
                return response;

            }
            catch (Exception ex)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.GetBaseException().Message;
                response.Message = ex.GetBaseException().Message;
                response.LogLevel = LogLevel.Error;
                return response;
            }
        }

        public ResponseViewModel GetById(int id)
        {
            var response = new ResponseViewModel();
            try
            {
                var DB_Value = unitofWork.UnitRepository.Get(p => p.Id == id);

                if (DB_Value == null)
                {

                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Not Exist";
                    response.Message = "Not Exist";
                    return response;
                }

                var unit = new UnitViewModel();

                unit.Id = DB_Value.Id;
            unit.SectionId = DB_Value.SectionId;
                unit.Code = DB_Value.Code;
                unit.Name = DB_Value.Name;
                unit.Abbreviation = DB_Value.Abbrevation;
                response.LogLevel = LogLevel.Information;
                response.Status = HttpStatusCode.OK;
                response.obj = unit;
                return response;

            }
            catch (Exception ex)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.GetBaseException().Message;
                response.Message = ex.GetBaseException().Message;
                response.LogLevel = LogLevel.Error;
                return response;
            }
        }


        public PaginatedRecordModel<UnitViewModel> GetPaginatedRecords(PaginationSearchModel model)
        {
            int recordsFiltered;
            int totalRecords;
            int pageNo = model.PageStart / model.PageSize;

            //Get the Sorting column
            Func<UnitViewModel, Int32, string> getColName = (
                (t, iSortCol) =>
                  iSortCol == 0 ? t.Code :
                iSortCol == 1 ? t.Name :
                iSortCol == 2 ? t.SectionName :
                   iSortCol == 3 ? t.Abbreviation :
                iSortCol == 5 ? t.Id.ToString() :
                t.Id.ToString()
            );

            IEnumerable<UnitViewModel> data = unitofWork.UnitRepository.GetAll("Section").Select(p => new UnitViewModel()
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
               SectionName = p.Section.Name,
               Abbreviation = p.Abbrevation,
                ValidFlag = p.ValidFlag
            });

            totalRecords = data.Count();
            if (!string.IsNullOrEmpty(model.Search))
            {

                // In case of by default
                if (model.sorting == 4)
                {
                    data = data.Where(x => x.Code.ToUpper().Contains(model.Search.ToUpper())
                    || x.Name.ToUpper().Contains(model.Search.ToUpper())
                     || x.SectionName.ToUpper().Contains(model.Search.ToUpper())
                       || x.Abbreviation.ToUpper().Contains(model.Search.ToUpper())
                  );
                }
            }

            recordsFiltered = data.Count();
            if (model.direction.Contains("asc"))
            {
                data = data
                .OrderBy(x => getColName(x, model.sorting))
                .Skip(pageNo * model.PageSize)
                .Take(model.PageSize);
            }
            else
            {
                data = data
                .OrderByDescending(x => getColName(x, model.sorting))
                .Skip(pageNo * model.PageSize)
                .Take(model.PageSize);
            }

            var dataObject = new PaginatedRecordModel<UnitViewModel>();
            dataObject.draw = model.Draw;
            dataObject.data = data;
            dataObject.recordsTotal = totalRecords;
            dataObject.recordsFiltered = recordsFiltered;
            return dataObject;

        }



        public ResponseViewModel Update(UnitViewModel value)
        {
            var response = new ResponseViewModel();
            try
            {
                var DB_Value = unitofWork.UnitRepository.Get(p => p.Id == value.Id);

                if (DB_Value == null)
                {

                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Not Exist";
                    response.Message = "Not Exist";
                    return response;
                }


                DB_Value.Name = value.Name;
                DB_Value.SectionId = value.SectionId;
                DB_Value.Abbrevation = value.Abbreviation;
                DB_Value.UpdatedBy = value.ChangedBy;
                DB_Value.UpdatedOn = value.ChangedOn;


                unitofWork.UnitRepository.Update(DB_Value);
                unitofWork.UnitRepository.Commit();
                response.LogLevel = LogLevel.Information;
                response.Status = HttpStatusCode.OK;
                response.Message = "Unit Updated";

                return response;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.GetBaseException().Message;
                response.Message = ex.GetBaseException().Message;
                response.LogLevel = LogLevel.Error;

                return response;
            }
        }
    }
}
