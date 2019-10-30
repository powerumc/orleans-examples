#!/usr/bin/env bash

docker exec -i orleans-postgres sh -c "psql -U postgres" -t < init-postgresql.sql
docker exec -i orleans-postgres sh -c "psql -U powerumc -w -d orleans" -t < postgresql-main.sql
docker exec -i orleans-postgres sh -c "psql -U powerumc -w -d orleans" -t < postgresql-persistence.sql