# IQueryable and IEnumerable

- IEnumerable is good when you are working with in memory data like array.
- IQueryable is good when you are working doing db interactions.

```csharp
EmployeeDbContext dc = new EmployeeDbContext();
IEnumerable<Employee> listE = dc.Employees.Where(p => p.Name.StartWith("H"));
// here IEnumerable pura data leke aayega db se and uske bd in-memory filter karega data ko.


IQueryable<Employee> listQ = dc.Employees.Where(p => p.Name.StartWith("H"));

// isme IQueryable kya krta hai to db me hi filter out kr deta hai data ko and final result leke aata hai.
```