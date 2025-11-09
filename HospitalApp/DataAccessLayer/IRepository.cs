using System;
using System.Collections.Generic;

namespace HospitalApp.DataAccessLayer;

public interface IRepository<T> where T : class
{
    T Get(Guid id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(Guid id);
}