#!/usr/bin/env bash

docker run -it --rm -p 5432:5432 --name orleans-postgres -e POSTGRES_PASSWORD=powerumc postgres