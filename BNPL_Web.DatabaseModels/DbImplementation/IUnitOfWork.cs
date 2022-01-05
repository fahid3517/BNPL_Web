using Project.Common.Interfaces;
using Project.DatabaseModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.DatabaseModel.DbImplementation
{
    public interface IUnitOfWork
    {

        IRepository<DbRolePrivilege> RolePrivilegeRepository { get; }
        IRepository<AspNetUser> AspNetUserRepository { get; }
        IRepository<DbDevPrivilege> PrivilegeRepository { get; }
        IRepository<AspNetRole> RoleRepository { get; }
        IRepository<DbBank> DbBanksRepository { get; }
        IRepository<DbBusinessType> DbBusinessTypesRepository { get;  }
        IRepository<DbCity> DbCitiesRepository { get; }
        IRepository<DbCountry> DbCountriesRepository { get; }
        IRepository<DbDevPerformanceIndicatorType> DbDevPerformanceIndicatorTypesRepository { get; }
        IRepository<DbDevPrivilege> DbDevPrivilegesRepository { get; }
        IRepository<DbDevProbationAssesmentType> DbDevProbationAssesmentTypesRepository { get; }
      
        IRepository<DbFixedAssetType> DbFixedAssetTypesRepository { get; }
        IRepository<DbGrade> DbGradesRepository { get; }
        IRepository<DbLeaveType> DbLeaveTypesRepository { get; }
        IRepository<DbLevel> DbLevelsRepository { get; }
        IRepository<DbLocation> DbLocationsRepository { get; }
        IRepository<DbPayElement> DbPayElementsRepository { get; }
        IRepository<DbPerformanceEvaluationPeriod> DbPerformanceEvaluationPeriodsRepository { get; }
        IRepository<DbPerformanceIndicator> DbPerformanceIndicatorsRepository { get; }
        IRepository<DbProbationAssesmentArea> DbProbationAssesmentAreasRepository { get; }
        IRepository<DbProbationAssesmentCharacteristic> DbProbationAssesmentCharacteristicsRepository { get; }
        IRepository<DbProvidentFundTerm> DbProvidentFundTermsRepository { get; }
        IRepository<DbRolePrivilege> DbRolePrivilegesRepository { get; }
        IRepository<DbTrainingCourse> DbTrainingCoursesRepository { get; }
        IRepository<DbTrainingCourseType> DbTrainingCourseTypesRepository { get; }
        IRepository<DbEmployee> DbEmployeesRepository { get; }
        IRepository<DbDepartment> DbDepartmentsRepository { get; }
        IRepository<DbSubDepartment> DbSubDepartmentsRepository { get; }
        IRepository<DbDesignation> DbDesignationsRepository { get; }
        IRepository<DbDevEducationType> DevEducationTypeRepository { get; }
        IRepository<DbQualification> QualificationRepository { get; }
        IRepository<DbBusinessUnit> BusinessUnitsRepository { get; }
        IRepository<DbUnit> UnitRepository { get; }
        IRepository<DbDevEmploymentType> EmploymentTypeRepository { get; }
        IRepository<DbSection> SectionRepository { get; }
        IRepository<DbEmployeeAddress> EmployeeAddressRepository { get; }
        IRepository<DbInstitute> InstituteRepository { get; }
        IRepository<DbEmployeeQualification> EmployeeQualificationRepository { get; }
        IRepository<DbAttendance> EmployeeAttendanceRepository { get; }
        IRepository<DbCategory> CategoryRepository { get; }
        IRepository<DbDivision> DivisionRepository { get; }
        IRepository<DbEmployeeExperience> EmployeeExperienceRepository { get; }
        IRepository<DbEmployeeBank> EmployeeBankRepository { get; }
        IRepository<DbEmployeeNominee> EmployeeNomineeRepository { get; }
        IRepository<DbEmployeeManager> EmployeeManagerRepository { get; }
        IRepository<DbDocument> DocumentRepository { get; }
        IRepository<DbEmployeeDocument> EmployeeDocumentRepository  { get; }
        IRepository<DbAttendancePolicy> AttendancePolicyRepository { get; }
        IRepository<DbAttendanceConfiguration> AttendancePolicyConfigurationRepository { get; }
        IRepository<DbDevCalendar> CelenderRepository { get; }

        IRepository<DbDevSalaryFrequency> DevSalaryFrequencyRepository { get; }
        IRepository<DbDevSalarySetting> DevSalarySettingRepository { get; }
        IRepository<DbAttendanceSetting> AttendanceSettingRepository { get; }
        IRepository<DbEmployeeSalary> EmployeeSalaryRepository { get; }
        IRepository<DbEmployeeSalaryHead> EmployeeSalaryHeadsRepository { get; }

        IRepository<DbIncomeTaxSlab> IncomeTaxRepository { get; }
        IRepository<DbDevFiscalYear> FiscalYearRepository { get; }
        IRepository<DbDevAttendanceSettingApplicable> DevAttendanceSettingRepository { get; }
        IRepository<DbCompany> CompanyRepository { get; }
        void Dispose();
    }
}
