docker-compose up -d --build

docker-compose logs -t -f producer
docker-compose logs -t -f consumer

docker-compose up -d --scale consumer=2 --scale producer=5
docker-compose up -d --scale consumer=1 --scale producer=5