using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQLDemo02
{

    public class Query
    {
        [UseDbContext(typeof(AdventureWorks2016Context))]
        [UseOffsetPaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts([ScopedService] AdventureWorks2016Context context)
        {
            return context.Products;
        }

        [UseDbContext(typeof(AdventureWorks2016Context))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Person> GetPersons([ScopedService] AdventureWorks2016Context context)
        {
            return context.People;
        }

        public TokenModel Login(string username, string password, [Service] PermissionRequirement requirement)
        {
            Console.WriteLine(username);
            var isValidated = username == "gsw" && password == "111111";
            if (!isValidated)
            {
                return new TokenModel()
                {
                    Result = false,
                    Message = "认证失败"
                };
            }
            else
            {
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(200000).ToString())
                };

                var token = JwtToken.BuildJwtToken(claims, requirement);
                return new TokenModel()
                {
                    Result = true,
                    Data = token.access_token
                };
            }
        }
    }
}
