-- ---------- MIGRONDI:UP:1622309680125 --------------
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
            REFERENCES civ.public.civilizations(id),
    CONSTRAINT fk_civ_region_id
        FOREIGN KEY (civ_region_id)
            REFERENCES civ.public.civilization_regions(id)
);

-- ---------- MIGRONDI:DOWN:1622309680125 --------------
DROP TABLE IF EXISTS cities;