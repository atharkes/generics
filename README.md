# Generics
Generic solutions to common problems.


## Generics.Infrastructure.EntityFramework
Generic repository implementations for entity framework including the ```Generics.Specifications``` specification pattern.

### Sample: Depdenency Injection
```c#
services.AddScoped(typeof(IRepository<>), typeof(GenericDbContextRepository<>));
services.AddScoped<IRepository, DbContextRepository>();
```

#### Sample: Repository Usage
```c#
var employeeName = await _repository.SingleOrDefault(new EmployeeNameByIdSpecification(employeeId));
```


## Generics.Specifications
Powerful and immutable specification pattern.

### Sample: Specification Creation
Specifications can be made by inheriting from the ```Specification<T>``` or ```Specification<TBase, TResult>``` class.
```c#
public class EmployeeNameByIdSpecification : Specification<Employee, string>
{
    public uint EmployeeId { get; }

    public EmployeeNameByIdSpecification(uint employeeId) : base(query => query
        .Where(employee => employee.Id == employeeId)
        .Select(employee => employee.Name)
    ) => EmployeeId = employeeId;
}
```


## Generics.Specifications.EntityFramework
Apply the specification pattern in an entity framework repository.

### Sample: Repository Implementation
The specification can be applied by using the ```Apply<T>(this DbSet<T> dbSet, ISpecification<T> specification)``` extension method.
```c#
public async Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    => await _dbSet.Apply(specification).ToListAsync(cancellationToken);
```

## Credits
* Generics.Infrastructure icon: <a href="https://www.flaticon.com/free-icons/database" title="database icons">Database icons created by Smashicons - Flaticon</a>
* Generics.Specifications icon: <a href="https://www.flaticon.com/free-icons/specification" title="specification icons">Specification icons created by Freepik - Flaticon</a>
