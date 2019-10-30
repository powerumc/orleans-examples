\c postgres;
SELECT 'CREATE DATABASE orleans' WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname='orleans')\gexec

CREATE USER powerumc WITH ENCRYPTED PASSWORD 'powerumc';
GRANT ALL ON DATABASE orleans to powerumc;
