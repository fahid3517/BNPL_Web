using BNPL_Web.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;


namespace Project.DatabaseModel.DbImplementation
{
    public class Repository<T> /*: IRepository<T>, IDisposable where T : class*/
    {
        //internal DbService _context;
        //internal DbSet<T> dbSet;
        //private DateTime currentTimeStamp;
        //private IHttpContextAccessor httpContextAccessor;
        //public Repository(DbService _context, IHttpContextAccessor httpContextAccessor)
        //{
        //    this._context = _context;
        //    dbSet = _context.Set<T>();
        //    currentTimeStamp = DateTime.Now;
        //    this.httpContextAccessor = httpContextAccessor;
        //}
        //public IEnumerable<T> GetAll()
        //{
        //    return dbSet.ToList();
        //}
        //public T GetById(object id)
        //{
        //    return dbSet.Find(id);
        //}

        //public void Update(T obj)
        //{
        //    dbSet.Attach(obj);
        //    _context.Entry(obj).State = EntityState.Modified;
        //}
        //public void Delete(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entity);
        //    }
        //    dbSet.Remove(entity);
        //}

        //public T Get(Expression<Func<T, bool>> where, params string[] paths)
        //{
        //    IQueryable<T> obj = dbSet.Where(where);
        //    obj = Includes(obj, paths);
        //    return obj.FirstOrDefault<T>();
        //}
        //private IQueryable<T> Includes(IQueryable<T> obj, params string[] paths)
        //{
        //    try
        //    {
        //        foreach (var path in paths)
        //            obj = obj.Include(path);

        //        obj.Load();
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return obj;
        //}
        //public virtual void Delete(Expression<Func<T, bool>> where)
        //{
        //    IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
        //    foreach (T obj in objects)
        //        dbSet.Remove(obj);
        //}

        //public virtual IEnumerable<T> GetAll(params string[] paths)
        //{
        //    IQueryable<T> obj = _context.Set<T>();
        //    obj = Includes(obj, paths);
        //    return dbSet.ToList();
        //}
        //public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params string[] paths)
        //{
        //    IQueryable<T> obj = dbSet.Where(where);
        //    obj = Includes(obj, paths);
        //    return obj;
        //}

        //public void Add(T obj)
        //{
        //    dbSet.Add(obj);
        //}
        //public void Dispose()
        //{
        //    _context.Dispose();

        //}
        //public virtual void Commit()
        //{
        //    try
        //    {
        //        SaveChanges();
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            //foreach (var validationError in validationErrors.ValidationErrors)
        //            //{
        //            //    Logger.Instance.Write(EnumLogLevel.Error, "Class: " + validationErrors.Entry.Entity.GetType().FullName +
        //            //        ", Property: " + validationError.PropertyName + ", Error: " + validationError.ErrorMessage);
        //            //}
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Logger.Instance.Write(EnumLogLevel.Error, ex.InnerException.Message, ex);
        //        throw;
        //    }

        //}
        //private int SaveChanges()
        //{
        //    _context.ChangeTracker.DetectChanges();

        //    // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
        //    foreach (var ent in _context.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified).ToList())
        //    {
        //        // For each changed record, get the audit record entries and add them
        //        foreach (AuditLog audit in GetAuditRecordsForChange(ent))
        //        {
        //            _context.AuditLogs.Add(audit);
        //        }
        //    }

        //    ////Log newly added many-to-many relations
        //    //foreach (AuditLog relationshipAuditLog in GetAddedRelationshipAuditLogs())
        //    //{
        //    //    _context.AuditLogs.Add(relationshipAuditLog);
        //    //}

        //    ////Log deleted many-to-many relations
        //    //foreach (AuditLog relationshipAuditLog in GetDeletedRelationshipAuditLogs())
        //    //{
        //    //    _context.AuditLogs.Add(relationshipAuditLog);
        //    //}

        //    // Call the original SaveChanges(), which will save both the changes made and the audit records
        //    return _context.SaveChanges();
        //}

        //private List<AuditLog> GetAuditRecordsForChange(EntityEntry dbEntry)
        //{
        //    List<AuditLog> result = new List<AuditLog>();

        //    string tableName = dbEntry.Entity.GetType().Name;

        //    var keyName = dbEntry.Metadata.FindPrimaryKey().Properties.Select(x => x.Name).Single();
        //    string entityId = dbEntry.Entity.GetType().GetProperty(keyName).GetValue(dbEntry.Entity, null).ToString();

        //    string createdBy = httpContextAccessor.HttpContext.User.Identity.Name;
           
        //    if (dbEntry.State == EntityState.Added)
        //    {
        //        result.Add(new AuditLog()
        //        {
        //            Id = Guid.NewGuid(),
        //            CreatedBy = createdBy,
        //            CreatedOn = currentTimeStamp,
        //            EventType = "A", // Added
        //            TableName = tableName,
        //            EntityId = entityId,
        //            ColumnName = "*ALL",
        //            NewValue = dbEntry.CurrentValues.ToObject().ToString()
        //        });
        //    }
        //    else if (dbEntry.State == EntityState.Deleted)
        //    {
        //        result.Add(new AuditLog()
        //        {
        //            Id = Guid.NewGuid(),
        //            CreatedBy = createdBy,
        //            CreatedOn = currentTimeStamp,
        //            EventType = "D", // Deleted
        //            TableName = tableName,
        //            EntityId = entityId,
        //            ColumnName = "*ALL",
        //            OriginalValue = dbEntry.OriginalValues.ToObject().ToString()
        //        });
        //    }
        //    else if (dbEntry.State == EntityState.Modified)
        //    {
        //        foreach (var v in dbEntry.Properties)
        //        {
        //            // For updates, we only want to capture the columns that actually changed
        //            if (!object.Equals(v.OriginalValue, v.CurrentValue))
        //            {
        //                result.Add(new AuditLog()
        //                {
        //                    Id = Guid.NewGuid(),
        //                    CreatedBy = createdBy,
        //                    CreatedOn = currentTimeStamp,
        //                    EventType = "M",    // Modified
        //                    TableName = tableName,
        //                    EntityId = entityId,
        //                    ColumnName = v.Metadata.Name,
        //                    OriginalValue = v.OriginalValue == null ? "" : v.OriginalValue.ToString(),
        //                    NewValue = v.CurrentValue == null ? "" : v.CurrentValue.ToString()
        //                });
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public IEnumerable<AuditLog> GetAddedRelationshipAuditLogs()
        //{
        //    _context.ChangeTracker.DetectChanges();
        //    var objectContext = ((IObjectContextAdapter)_context).ObjectContext;

        //    IEnumerable<AuditLog> audits = objectContext
        //        .ObjectStateManager
        //        .GetObjectStateEntries((System.Data.Entity.EntityState)EntityState.Added)
        //        .Where(e => e.IsRelationship)
        //        .Select(
        //            e => new AuditLog()
        //            {
        //                Id = Guid.NewGuid(),
        //                CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name,
        //                CreatedOn = currentTimeStamp,
        //                EventType = "A",
        //                TableName = e.EntitySet.Name,
        //                EntityId = "",
        //                ColumnName = "",
        //                NewValue = "",
        //                RelationalColumnName = ((EntityKey)e.CurrentValues[1]).EntityKeyValues[0].Key,
        //                RelationalColumnValue = ((EntityKey)e.CurrentValues[1]).EntityKeyValues[0].Value.ToString()
        //            }); ;

        //    return audits;
        //}

        //public IEnumerable<AuditLog> GetDeletedRelationshipAuditLogs()
        //{
        //    _context.ChangeTracker.DetectChanges();
        //    var objectContext = ((IObjectContextAdapter)_context).ObjectContext;

        //    IEnumerable<AuditLog> audits = objectContext
        //        .ObjectStateManager
        //        .GetObjectStateEntries((System.Data.Entity.EntityState)EntityState.Added)
        //        .Where(e => e.IsRelationship)
        //        .Select(
        //            e => new AuditLog()
        //            {
        //                Id = Guid.NewGuid(),
        //                CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name,
        //                CreatedOn = currentTimeStamp,
        //                EventType = "D",
        //                TableName = e.EntitySet.Name,
        //                EntityId = "",
        //                ColumnName = "",
        //                OriginalValue = "",
        //                RelationalColumnName = ((EntityKey)e.CurrentValues[1]).EntityKeyValues[0].Key,
        //                RelationalColumnValue = ((EntityKey)e.CurrentValues[1]).EntityKeyValues[0].Value.ToString()
        //            });

        //    return audits;
        //}
    }
}
