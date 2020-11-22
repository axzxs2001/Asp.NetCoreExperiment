using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQLDemo02
{

    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [UseProjection]
        public List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { StuNo="N0001", Name="张三", Age=21, Sex=true },
                new Student { StuNo="N0002", Name="李四", Age=22, Sex=false  },
                new Student { StuNo="N0003", Name="王五", Age=23, Sex=true }
            };
        }

        public Token Login(string username, string password, [Service] PermissionRequirement requirement)
        {
            Console.WriteLine(username);
            var isValidated = username == "gsw" && password == "111111";
            if (!isValidated)
            {
                return new Token()
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
                return new Token()
                {
                    Result = true,
                    Data = token.access_token
                };
        }

    }
}
public class Token
{
    public bool Result { get; set; }
    public string Data { get; set; }
    public string Message { get; set; }
}

[Authorize(Policy = "Permission")]
public class Student
{
    public string StuNo { get; set; }
    public string Name { get; set; }

    public int Age { get; set; }

    public bool Sex { get; set; }
}
}
