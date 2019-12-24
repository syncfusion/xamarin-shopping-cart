using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ShoppingAPI.Database;

namespace ShoppingAPI.Repositories
{
    public class RepositoryDataAccessLayer<TObject> : IRepositoryDataAccessLayer<TObject> where TObject : class, new()
    {
        #region Fields

        /// <summary>
        /// Holds the database
        /// </summary>
        public ShoppingContext Context;

        #endregion

        #region Property

        public DbSet<TObject> DbSet => Context.Set<TObject>();

        #endregion

        #region How To use 

        //RepositoryDataAccessLayer<C_CT3_CT3User> repositorty;
        //public AdminView()
        //{
        //    InitializeComponent();
        //    this.DataContext = new AdminModel();
        //    repositorty = new RepositoryDataAccessLayer<C_CT3_CT3User>();
        //}
        //Click_Event
        //  {
        //    C_CT3_CT3User user = new C_CT3_CT3User();
        //    user.UserName = UserName.Text; here user.UserName is database column and UserName.Text is textbox text
        //    repositorty.Add(user);
        //  }

        #endregion

        #region Constructor

        public RepositoryDataAccessLayer()
        {
            Context = new ShoppingContext();
        }

        public RepositoryDataAccessLayer(ShoppingContext context)
        {
            Context = context;
        }

        #endregion

        #region Implementation

        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        public virtual IList<TObject> GetAll()
        {
            return DbSet.AsQueryable().ToList();
        }

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        public virtual IQueryable<TObject> Filter<Key>(Expression<Func<TObject, bool>> filter, out int total,
            int index = 0, int size = 50)
        {
            var skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual IQueryable<TObject> FindBy(Expression<Func<TObject, bool>> predicate)
        {
            var query = Context.Set<TObject>().Where(predicate);
            return query;
        }

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        public virtual TObject Add(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            Context.SaveChanges();
            return newEntry;
        }

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        public virtual int Count => DbSet.Count();

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>
        public virtual int Delete(TObject TObject)
        {
            DbSet.Remove(TObject);
            return Context.SaveChanges();
        }

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        public virtual int Update(TObject TObject)
        {
            var entry = Context.Entry(TObject);
            DbSet.Attach(TObject);
            entry.State = EntityState.Modified;
            return Context.SaveChanges();
        }

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            return Context.SaveChanges();
        }

        public virtual IQueryable<TObject> AllIncluding(params Expression<Func<TObject, object>>[] includeProperties)
        {
            IQueryable<TObject> query = Context.Set<TObject>();
            foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
            return query;
        }

        #endregion
    }
}