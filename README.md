# NETCore.DapperKit

## Easy use [Dapper](https://github.com/StackExchange/Dapper) like ef with expression.


---

# Install with nuget

- To install NETCore.DapperKit, run the following command in the [Package Manager](https://docs.microsoft.com/zh-cn/nuget/tools/package-manager-console) Console

```
Install-Package NETCore.DapperKit -Pre
```

# Add Dapperkit service in starup.cs

- Using DapperKit extensions

```csharp
using NETCore.DapperKit.Extensions;
```

- Add Dapperkit service

```csharp
 public void ConfigureServices(IServiceCollection services)
      {
          // Add framework services.
          services.AddMvc();

          // Add DapperKit
          services.AddDapperKit(optionsBuilder =>
          {
              optionsBuilder.UseDapper(new DapperKitOptions()
              {
                  ConnectionString = "Data Source=.;User ID=sa;Password='123456';Initial Catalog=DapperDb;MultipleActiveResultSets=True;"
              });
          });
      }

```

# User DapperKit in asp.net core mvc controller

- Init DapperContext
```csharp
public class AccountController : Controller
{

    private readonly IDapperContext _DapperContext;

    public AccountController(IDapperContext context)
    {
        _DapperContext = context;
    }
    ...
}
```

- Use Dapper Like EF

```csharp
public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
{
    ...
    var users = await _DapperContext.DbSet<SysUser>()
                                    .Select<SysUserRole>((u, r) => new SysUser() { Id = u.Id, IsAdmin = u.IsAdmin, LoginName = u.LoginName, LoginPwd = u.LoginPwd, CreateTime = u.CreateTime, UserName = u.UserName, UserRoleNo = u.UserRoleNo, RoleName = r.Name })
                                    .Join<SysUserRole>((u, r) => u.UserRoleNo == r.No)
                                    .Where(m => m.LoginName == model.Account && m.LoginPwd == model.Password)
                                    .ToListAsync<SysUser>();
    ...                                 
}
```

#LICENSE

[MIT LICESE]


