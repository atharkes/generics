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

## Credits
* Icon: <a href="https://www.flaticon.com/free-icons/specification" title="specification icons">Specification icons created by Freepik - Flaticon</a>
