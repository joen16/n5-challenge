docker build -t n5-challenge -f Dockerfile .

docker run -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 -e KAFKA_LISTENERS=PLAINTEXT://broker:29092,PLAINTEXT_HOST://192.168.22.187:9092  -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://broker:29092,PLAINTEXT_HOST://192.168.22.187:9092 --name n5-challenge-app n5-challenge 