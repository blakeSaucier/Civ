-- ---------- MIGRONDI:UP:1622308463722 --------------
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
            REFERENCES civ.public.games(id)
);

-- ---------- MIGRONDI:DOWN:1622308463722 --------------
DROP TABLE IF EXISTS civilizations;