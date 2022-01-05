using Project.Common.Interfaces;
using Project.DatabaseModel.Models;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.DatabaseModel.DbImplementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private bool disposed = false;
        private DbService databaseService;

       public IRepository<DbDevPrivilege> PrivilegeRepository { get; }
       public IRepository<DbRolePrivilege> RolePrivilegeRepository { get; }
       public IRepository<AspNetUser> AspNetUserRepository { get; }
        public IRepository <DbBank> DbBanksRepository { get;  }
        public IRepository <DbBusinessType> DbBusinessTypesRepository { get; }
        public IRepository <DbCity> DbCitiesRepository { get;  }
        public IRepository <DbCountry> DbCountriesRepository { get;  }
        public IRepository <DbDevPerformanceIndicatorType> DbDevPerformanceIndicatorTypesRepository { get; }
        public IRepository <DbDevPrivilege> DbDevPrivilegesRepository { get;  }
        public IRepository <DbDevProbationAssesmentType> DbDevProbationAssesmentTypesRepository { get;  }
      
        public IRepository <DbFixedAssetType> DbFixedAssetTypesRepository { get;  }
        public IRepository <DbGrade> DbGradesRepository { get;  }
        public IRepository <DbLeaveType> DbLeaveTypesRepository { get; }
        public IRepository <DbLevel> DbLevelsRepository { get;  }
        public IRepository <DbLocation> DbLocationsRepository { get;  }
        public IRepository <DbPayElement> DbPayElementsRepository { get;  }
        public IRepository <DbPerformanceEvaluationPeriod> DbPerformanceEvaluationPeriodsRepository { get; }
        public IRepository <DbPerformanceIndicator> DbPerformanceIndicatorsRepository { get; }
        public IRepository <DbProbationAssesmentArea> DbProbationAssesmentAreasRepository { get; }
        public IRepository <DbProbationAssesmentCharacteristic> DbProbationAssesmentCharacteristicsRepository { get; }
        public IRepository <DbProvidentFundTerm> DbProvidentFundTermsRepository { get; }
        public IRepository <DbRolePrivilege> DbRolePrivilegesRepository { get; }
        public IRepository <DbTrainingCourse> DbTrainingCoursesRepository { get; }
        public IRepository <DbTrainingCourseType> DbTrainingCourseTypesRepository { get; }

        public IRepository<AspNetRole> RoleRepository { get; }

        public IRepository<DbEmployee> DbEmployeesRepository { get; }

        public IRepository<DbDepartment> DbDepartmentsRepository { get; }

        public IRepository<DbSubDepartment> DbSubDepartmentsRepository { get; }

        public IRepository<DbDesignation> DbDesignationsRepository { get; }

        public IRepository<DbDevEducationType> DevEducationTypeRepository { get; }

        public IRepository<DbQualification> QualificationRepository { get; }

        public IRepository<DbBusinessUnit> BusinessUnitsRepository { get; }

        public IRepository<DbUnit> UnitRepository { get; }
        public IRepository<DbDevEmploymentType> EmploymentTypeRepository { get; }

        public IRepository<DbSection> SectionRepository { get; }

        public IRepository<DbEmployeeAddress> EmployeeAddressRepository { get; }

        public IRepository<DbInstitute> InstituteRepository { get; }

        public IRepository<DbEmployeeQualification> EmployeeQualificationRepository { get; }

        public IRepository<DbAttendance> EmployeeAttendanceRepository { get; }

        public IRepository<DbCategory> CategoryRepository { get; }

        public IRepository<DbDivision> DivisionRepository { get; }

        public IRepository<DbEmployeeExperience> EmployeeExperienceRepository { get; }

        public IRepository<DbEmployeeBank> EmployeeBankRepository { get; }

        public IRepository<DbEmployeeNominee> EmployeeNomineeRepository { get; }

        public IRepository<DbEmployeeManager> EmployeeManagerRepository { get; }

        public IRepository<DbDocument> DocumentRepository { get; }

        public IRepository<DbEmployeeDocument> EmployeeDocumentRepository { get; }

        public IRepository<DbAttendancePolicy> AttendancePolicyRepository { get; }

        public IRepository<DbDevCalendar>  CelenderRepository { get; }
        public IRepository<DbAttendanceConfiguration> AttendancePolicyConfigurationRepository { get; }

        public IRepository<DbDevSalaryFrequency> DevSalaryFrequencyRepository { get; }

        public IRepository<DbDevSalarySetting> DevSalarySettingRepository { get; }

        public IRepository<DbEmployeeSalary> EmployeeSalaryRepository { get; }
        public IRepository<DbEmployeeSalaryHead> EmployeeSalaryHeadsRepository { get; }

        public IRepository<DbAttendanceSetting> AttendanceSettingRepository { get; }

       public IRepository<DbDevAttendanceSettingApplicable> DevAttendanceSettingRepository { get; }
        public IRepository<DbIncomeTaxSlab> IncomeTaxRepository { get; }
        public IRepository<DbDevFiscalYear> FiscalYearRepository { get; }
        public IRepository<DbCompany> CompanyRepository { get; }
        public UnitOfWork(
         DbService databaseService,
        
         IRepository<DbDocument> DocumentRepository,
         IRepository<DbEmployeeDocument> EmployeeDocumentRepository,
         IRepository<DbEmployeeManager> EmployeeManagerRepository,
         IRepository<DbEmployeeNominee> EmployeeNomineeRepository,
         IRepository<DbEmployeeBank> EmployeeBankRepository,
         IRepository<DbEmployeeExperience> EmployeeExperienceRepository,
         IRepository<DbCategory> CategoryRepository,
         IRepository<DbDivision> DivisionRepository,
         IRepository<DbAttendance> EmployeeAttendanceRepository,
         IRepository<DbInstitute> InstituteRepository,
         IRepository<DbEmployeeQualification> EmployeeQualificationRepository,
         IRepository<DbSection> SectionRepository,
         IRepository<DbEmployeeAddress> EmployeeAddressRepository,
         IRepository<DbRolePrivilege> RolePrivilegeRepository,
         IRepository<DbDevPrivilege> PrivilegeRepository,
         IRepository<AspNetUser> AspNetUserRepository,
         IRepository<DbBank> DbBanksRepository,
         IRepository<DbBusinessType> DbBusinessTypesRepository,
         IRepository<DbCity> DbCitiesRepository,
         IRepository<DbCountry> DbCountriesRepository,
         IRepository<DbDevPerformanceIndicatorType> DbDevPerformanceIndicatorTypesRepository,
         IRepository<DbDevPrivilege> DbDevPrivilegesRepository,
         IRepository<DbDevProbationAssesmentType> DbDevProbationAssesmentTypesRepository,

         IRepository<DbFixedAssetType> DbFixedAssetTypesRepository,
         IRepository<DbGrade> DbGradesRepository,
         IRepository<DbLeaveType> DbLeaveTypesRepository,
         IRepository<DbLevel> DbLevelsRepository,
         IRepository<DbLocation> DbLocationsRepository,
         IRepository<DbPayElement> DbPayElementsRepository,
         IRepository<DbPerformanceEvaluationPeriod> DbPerformanceEvaluationPeriodsRepository,
         IRepository<DbPerformanceIndicator> DbPerformanceIndicatorsRepository,
         IRepository<DbProbationAssesmentArea> DbProbationAssesmentAreasRepository,
         IRepository<DbProbationAssesmentCharacteristic> DbProbationAssesmentCharacteristicsRepository,
         IRepository<DbProvidentFundTerm> DbProvidentFundTermsRepository,
         IRepository<DbRolePrivilege> DbRolePrivilegesRepository,
         IRepository<DbTrainingCourse> DbTrainingCoursesRepository,
         IRepository<DbTrainingCourseType> DbTrainingCourseTypesRepository,
         IRepository<AspNetRole> RoleRepository,
         IRepository<DbEmployee> DbEmployeesRepository,

         IRepository<DbDepartment> DbDepartmentsRepository,
         IRepository<DbBusinessUnit> BusinessUnitsRepository,
         IRepository<DbUnit> UnitsRepository,
         IRepository<DbDevEducationType> EducationTypeRepository,
         IRepository<DbQualification> QualificationRepository,

         IRepository<DbSubDepartment> DbSubDepartmentsRepository,

         IRepository<DbDesignation> DbDesignationsRepository,
         IRepository<DbDevEmploymentType> EmploymentTypeRepository,
         IRepository<DbAttendancePolicy> AttendancePolicyRepository,
         IRepository<DbDevCalendar> CalenderRepository,
         IRepository<DbAttendanceConfiguration> AttendancePolicyConfigurationRepository,
         IRepository<DbDevSalaryFrequency> DevSalaryFrequencyRepository,
        IRepository<DbDevSalarySetting> DevSalarySettingRepository,
        IRepository<DbEmployeeSalary>EmployeeSalaryRepository,
        IRepository<DbEmployeeSalaryHead> EmployeeSalaryHeadRepository,
        IRepository<DbAttendanceSetting> AttendanceSettingRepository,
        IRepository<DbDevAttendanceSettingApplicable> DevAttendanceSettingRepository,
         IRepository<DbIncomeTaxSlab> IncomeTaxRepository,
           IRepository<DbDevFiscalYear> FiscalYearRepository,
           IRepository<DbCompany> CompanyRepository
            )
        {
            
            this.DocumentRepository =DocumentRepository;
            this.EmployeeDocumentRepository = EmployeeDocumentRepository;
            this.EmployeeManagerRepository = EmployeeManagerRepository;
            this.EmployeeNomineeRepository = EmployeeNomineeRepository;
            this.EmployeeBankRepository = EmployeeBankRepository;
            this.EmployeeExperienceRepository = EmployeeExperienceRepository;
            this.DbLevelsRepository = DbLevelsRepository;
            this.UnitRepository = UnitsRepository;
            this.CategoryRepository = CategoryRepository;
            this.DivisionRepository = DivisionRepository;
            this.EmployeeAttendanceRepository = EmployeeAttendanceRepository;
            this.InstituteRepository = InstituteRepository;
            this.EmployeeQualificationRepository = EmployeeQualificationRepository;
            this.EmployeeAddressRepository = EmployeeAddressRepository;
            this.BusinessUnitsRepository = BusinessUnitsRepository;
            this.QualificationRepository = QualificationRepository;
            this.DevEducationTypeRepository = EducationTypeRepository;
            this.DbDesignationsRepository = DbDesignationsRepository;
            this.DbDepartmentsRepository = DbDepartmentsRepository;
            this.DbSubDepartmentsRepository = DbSubDepartmentsRepository;
            this.DbDepartmentsRepository = DbDepartmentsRepository;
            this.DbEmployeesRepository = DbEmployeesRepository;
            this.databaseService = databaseService;
            this.RolePrivilegeRepository = RolePrivilegeRepository;
            this.AspNetUserRepository = AspNetUserRepository;
            this.PrivilegeRepository = PrivilegeRepository;
            this.DbBanksRepository = DbBanksRepository;
            this.DbBusinessTypesRepository = DbBusinessTypesRepository;
            this.DbCitiesRepository = DbCitiesRepository;
            this.DbCountriesRepository = DbCountriesRepository;
            this.DbDevPerformanceIndicatorTypesRepository = DbDevPerformanceIndicatorTypesRepository;
            this.DbFixedAssetTypesRepository = DbFixedAssetTypesRepository;
            this.DbGradesRepository = DbGradesRepository;
            this.DbLeaveTypesRepository = DbLeaveTypesRepository;
            this.DbLocationsRepository = DbLocationsRepository;
            this.DbPayElementsRepository = DbPayElementsRepository;
            this.DbPerformanceEvaluationPeriodsRepository = DbPerformanceEvaluationPeriodsRepository;
            this.DbPerformanceIndicatorsRepository = DbPerformanceIndicatorsRepository;
            this.DbProbationAssesmentAreasRepository = DbProbationAssesmentAreasRepository;
            this.DbProbationAssesmentCharacteristicsRepository = DbProbationAssesmentCharacteristicsRepository;
            this.DbProvidentFundTermsRepository = DbProvidentFundTermsRepository;
            this.DbTrainingCoursesRepository = DbTrainingCoursesRepository;
            this.DbTrainingCourseTypesRepository = DbTrainingCourseTypesRepository;
            this.RoleRepository = RoleRepository;
            this.DbDesignationsRepository = DbDesignationsRepository;
            this.EmploymentTypeRepository = EmploymentTypeRepository;
            this.SectionRepository = SectionRepository;
            this.AttendancePolicyRepository = AttendancePolicyRepository;
            this.CelenderRepository = CalenderRepository;
            this.AttendancePolicyConfigurationRepository = AttendancePolicyConfigurationRepository;
            this.DevSalaryFrequencyRepository = DevSalaryFrequencyRepository;
            this.DevSalarySettingRepository = DevSalarySettingRepository;
            this.EmployeeSalaryRepository = EmployeeSalaryRepository;
            this.EmployeeSalaryHeadsRepository = EmployeeSalaryHeadsRepository;
            this.AttendanceSettingRepository = AttendanceSettingRepository;
            this.DevAttendanceSettingRepository = DevAttendanceSettingRepository;
            this.CompanyRepository = CompanyRepository;
            this.IncomeTaxRepository = IncomeTaxRepository;
            this.FiscalYearRepository = FiscalYearRepository;
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    databaseService.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
