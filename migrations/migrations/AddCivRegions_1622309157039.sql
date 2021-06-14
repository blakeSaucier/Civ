-- ---------- MIGRONDI:UP:1622309157039 --------------
CREATE TABLE civilization_regions (
    id SERIAL PRIMARY KEY,
    region VARCHAR(8) NOT NULL,
    notes TEXT,
    civ_id INTEGER NOT NULL,
    created TIMESTAMP NOT NULL,
    updated TIMESTAMP NOT NULL,
    CONSTRAINT fk_civ
        FOREIGN KEY (civ_id)
            REFERENCES civ.public.civilizations(id)
);

-- ---------- MIGRONDI:DOWN:1622309157039 --------------
DROP TABLE IF EXISTS civilization_regions;