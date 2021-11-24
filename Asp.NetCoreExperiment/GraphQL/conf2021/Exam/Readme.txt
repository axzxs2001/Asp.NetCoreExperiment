1、安装doetnet-ef

dotnet tool install --global dotnet-ef

2、引用nuget包  Microsoft.EntityFrameworkCore.Design，Microsoft.EntityFrameworkCore.SqlServer


3、从数据库生成实体类
dotnet ef dbcontext scaffold "server=.;database=Exam;uid=sa;pwd=sa;" Microsoft.EntityFrameworkCore.SqlServer -o Models

删除数据库，来重建迁移

4、生成迁移 dotnet ef migrations add InitialCreate

5、执行迁移到数据库  dotnet ef database update

6、以后，每次更新表结构，都要生成迁移，然后更新数据库
修改数据表字段等信息，再次生成迁移  dotnet ef migrations add ModifyTableColumn,然后更新dotnet ef database update