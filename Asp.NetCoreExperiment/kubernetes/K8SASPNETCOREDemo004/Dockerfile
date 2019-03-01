FROM microsoft/dotnet:2.1.1-aspnetcore-runtime 

#设置工作目录
WORKDIR /app

#复制发布文件到/app下
COPY . /app

#设置端口
EXPOSE 4045

#使用dotnet LisAPI.dll来运行asp.net core项目，注意大小写
ENTRYPOINT ["dotnet", "K8SASPNETCOREDemo004.dll"]
