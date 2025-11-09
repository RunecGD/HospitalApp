using System;
using System.Collections.Generic;

namespace HospitalApp.DataAccessLayer;

public class InMemoryRepository<T> : IRepository<T> where T : class
{
    protected readonly Dictionary<Guid, T> storage = new();
    private readonly Func<T, Guid> idGetter;


    public InMemoryRepository(Func<T, Guid> idGetter)
    {
        this.idGetter = idGetter;
    }


    public void Add(T entity)
    {
        var id = idGetter(entity);
        storage[id] = entity;
    }


    public void Delete(Guid id) => storage.Remove(id);


    public T Get(Guid id) => storage.ContainsKey(id) ? storage[id] : null;


    public IEnumerable<T> GetAll() => storage.Values;


    public void Update(T entity)
    {
        var id = idGetter(entity);
        storage[id] = entity;
    }
}