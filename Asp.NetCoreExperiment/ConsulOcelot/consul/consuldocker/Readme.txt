
build consul镜像
docker build -t gliderlabs/consul-server:$('1.0.2') .

运行consul镜像
docker run -p 8500:8500 --name hisconsul -d gliderlabs/consul-server:1.0.2


记问
http://localhost:8500/