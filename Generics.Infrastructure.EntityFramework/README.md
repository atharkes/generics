## Generics.Infrastructure.EntityFramework
Generic repository implementations for entity framework including the ```Generics.Specifications``` specification pattern.

### Sample: Dependency Injection
```c#
services.AddScoped(typeof(IRepository<>), typeof(GenericDbContextRepository<>));
services.AddScoped<IRepository, DbContextRepository>();
```

### Sample: Repository Usage
```c#
var employeeName = await _repository.SingleOrDefault(new EmployeeNameByIdSpecification(employeeId));
```

## Credits
* Icon: <a href="https://www.flaticon.com/free-icons/database" title="database icons">Database icons created by Smashicons - Flaticon</a>
