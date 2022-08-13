CREATE TABLE games (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    code VARCHAR(10) NOT NULL,
    password VARCHAR (20) NOT NULL,
    created TIMESTAMP NOT NULL,
    updated TIMESTAMP NOT NULL
);

CREATE TABLE civilizations (
   id SERIAL PRIMARY KEY,
   name VARCHAR(50) NOT NULL,
   color VARCHAR(16) NOT NULL,
   notes TEXT,
   population BIGINT,
   game_id INTEGER NOT NULL,
   created timestamp NOT NULL,
   updated timestamp NOT NULL,
   CONSTRAINT fk_game
       FOREIGN KEY (game_id)
           REFERENCES games(id)
);

CREATE TABLE civilization_regions (
  id SERIAL PRIMARY KEY,
  region VARCHAR(8) NOT NULL,
  notes TEXT,
  civ_id INTEGER NOT NULL,
  created TIMESTAMP NOT NULL,
  updated TIMESTAMP NOT NULL,
  CONSTRAINT fk_civ
      FOREIGN KEY (civ_id)
          REFERENCES civilizations(id)
);

CREATE TABLE cities (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    notes TEXT NOT NULL,
    lat NUMERIC(10, 6) NOT NULL,
    long NUMERIC(10, 6) NOT NULL,
    civ_id INTEGER NOT NULL,
    civ_region_id INTEGER NOT NULL,
    created TIMESTAMP NOT NULL,
    updated TIMESTAMP NOT NULL,
    CONSTRAINT fk_civ_id
        FOREIGN KEY (civ_id)
            REFERENCES civilizations(id),
    CONSTRAINT fk_civ_region_id
        FOREIGN KEY (civ_region_id)
            REFERENCES civilization_regions(id)
);