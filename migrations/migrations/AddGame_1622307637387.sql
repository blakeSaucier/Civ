-- ---------- MIGRONDI:UP:1622307637387 --------------
CREATE TABLE games (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    code VARCHAR(10) NOT NULL,
    password VARCHAR (20) NOT NULL,
    created TIMESTAMP NOT NULL,
    updated TIMESTAMP NOT NULL
);

-- ---------- MIGRONDI:DOWN:1622307637387 --------------
DROP TABLE IF EXISTS "games";