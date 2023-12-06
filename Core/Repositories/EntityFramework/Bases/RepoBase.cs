﻿using Core.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Core.Repositories.EntityFramework.Bases
{
    /// <summary>
    /// TR: Repository Pattern tasarım desenini kullanan, kolay ve merkezi olarak CRUD (Create, Read, Update ve Delete)
    /// işlemlerinin yapılmasını sağlayan tüm entity'ler üzerinden veritabanı işlemleri için kullanılacak soyut sınıf.
    /// EN: An abstract class that uses the Repository Design Pattern to provide a convenient and central way 
    /// to perform CRUD (Create, Read, Update, and Delete) operations on all entities for database operations.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : class, new()
    {
        #region DbContext object constructor injection
        protected readonly DbContext _db;

        protected RepoBase(DbContext db)
        {
            _db = db;
        }
        #endregion

        public virtual IQueryable<TEntity> Query()
        {
            return _db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query(bool isNoTracking = false)
        {
            if (isNoTracking)
                return _db.Set<TEntity>().AsNoTracking();
            return _db.Set<TEntity>();
        }

        public virtual void Add(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
                Save();
        }

        public virtual void Update(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
                Save();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false)
        {
            return Query(isNoTracking).Any(predicate);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = Query().Where(predicate).ToList();
            _db.Set<TEntity>().RemoveRange(entities);
            if (save)
                Save();
        }

        public virtual int Save()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception exc)
            {
                #region Exception handling
                string message = "Exception: " + exc.Message;
                if (exc.InnerException is not null)
                    message += " | Inner Exception: " + exc.InnerException.Message;
                Debug.WriteLine(message);
                #endregion

                throw;
            }
        }

        /// <summary>
        /// TR: Garbage Collector'a işimizin bittiğini söylüyoruz ki objeyi en kısa sürede hafızadan temizlesin.
        /// EN: We tell the Garbage Collector that we are done with the object so that it can clean it up 
        /// from memory as soon as possible.
        /// </summary>
        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }

		public virtual IQueryable<TRelationalEntity> Query<TRelationalEntity>() where TRelationalEntity : class, new()
		{
			return _db.Set<TRelationalEntity>();
		}



		public virtual void Delete<TRelationalEntity>(Expression<Func<TRelationalEntity, bool>> predicate, bool save = false) where TRelationalEntity : class, new()
		{
			var relationalEntities = Query<TRelationalEntity>().Where(predicate).ToList(); // yukarıdaki ilişkili entity'ler için
																						   // oluşturduğumuz Query methodundan
																						   // listeyi çekiyoruz.

			_db.Set<TRelationalEntity>().RemoveRange(relationalEntities); // daha sonra ilişkili entity DbSet'inden çektiğimiz
																				 // ilişkili entity listesini siliyoruz.

			if (save)
				Save();
		}
	}
}
