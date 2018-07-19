1、在测试项目中的Nuget中引用coverlet.msbuild

2、从cmd中进入到测试项目源码目录

3、dotnet test /p:CollectCoverage=true
	生成测试结果的默认文件是json格式，如果使用opencover格式: 
	dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

4、在Nuget中引用ReportGenerator

5、从cmd中进入到测试项目源码目录

6、dotnet C:\Users\自己路径\.nuget\packages\reportgenerator\版本号\tools\netcoreapp2.0\ReportGenerator.dll -reports:.\coverage.opencover.xml -targetdir:.\Reports 

7、打开项目目录下的Report下的Index.html就可以查看详细的测试和覆盖率报告


注：
报告也可以用SonarCloud，它是一个云服务, 需要安装java环境,https://sonarcloud.io/