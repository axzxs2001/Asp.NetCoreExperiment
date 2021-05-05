taskkill /f /im 项目名称.exe
timeout 1 >NUL
@RD /S /Q "C:\项目名称\项目目录"
cd  C:\项目名称
git clone https://用户名:密码@github.com/用户名/项目名称.git
cd  C:\项目名称\项目目录
dotnet publish -o C:\项目名称\pub
timeout 1>NUL
cd C:\项目名称\pub
项目名称.exe