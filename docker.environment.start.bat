docker network create --driver=bridge --subnet=127.0.0.0/16 clientsvh
SET COMPOSE_CONVERT_WINDOWS_PATHS=1
docker-compose -f svh.docker-compose.yml up

pause