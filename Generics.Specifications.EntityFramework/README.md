## Generics.Specifications.EntityFramework
Apply the specification pattern in an entity framework repository.

### Sample: Repository Implementation
The specification can be applied by using the ```Apply<T>(this DbSet<T> dbSet, ISpecification<T> specification)``` extension method.
```c#
public async Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    => await _dbSet.Apply(specification).ToListAsync(cancellationToken);
```

## Credits
* Icon: <a href="https://www.flaticon.com/free-icons/specification" title="specification icons">Specification icons created by Freepik - Flaticon</a>
