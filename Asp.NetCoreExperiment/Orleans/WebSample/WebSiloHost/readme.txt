第一步
docker build -t websilohost .


第二步
docker run -it -p 11111:11111 -p 30000:30000 websilohost