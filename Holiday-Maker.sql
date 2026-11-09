CREATE DATABASE IF NOT EXISTS `holiday_maker`;
USE `holiday_maker`;
CREATE TABLE `customers`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `first_name` VARCHAR(255) NOT NULL,
    `last_name` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) NOT NULL,
    `phone_number` VARCHAR(15) NOT NULL
);
ALTER TABLE
    `customers` ADD UNIQUE `customers_email_unique`(`email`);
CREATE TABLE `accommodations`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(255) NOT NULL,
    `location` VARCHAR(255) NOT NULL,
    `pool` BOOLEAN NOT NULL,
    `restaurant` BOOLEAN NOT NULL,
    `rating` INT NOT NULL,
    `distance_to_beach` INT NOT NULL,
    `distance_to_city_center` INT NOT NULL
);
CREATE TABLE `rooms`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `room_number` VARCHAR(255) NOT NULL,
    `maximum_people` INT NOT NULL,
    `price_per_night` DECIMAL(8, 2) NOT NULL,
    `accommodation_id` INT UNSIGNED NOT NULL
);
CREATE TABLE `bookings`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `customer_id` INT UNSIGNED NOT NULL,
    `room_id` INT UNSIGNED NOT NULL,
    `check_in_date` DATE NOT NULL,
    `check_out_date` DATE NOT NULL,
    `number_of_people` INT NOT NULL
);
ALTER TABLE
    `bookings` ADD CONSTRAINT `bookings_room_id_foreign` FOREIGN KEY(`room_id`) REFERENCES `rooms`(`id`);
ALTER TABLE
    `rooms` ADD CONSTRAINT `rooms_accommodation_id_foreign` FOREIGN KEY(`accommodation_id`) REFERENCES `accommodations`(`id`);
ALTER TABLE
    `bookings` ADD CONSTRAINT `bookings_customer_id_foreign` FOREIGN KEY(`customer_id`) REFERENCES `customers`(`id`);


-- =====================================================================
-- Boenden: 6 st på 2 orter, med varierande faciliteter, omdöme och avstånd
-- =====================================================================
INSERT INTO accommodations (name, location, pool, restaurant, rating, distance_to_beach, distance_to_city_center) VALUES
    ('Hotell Strandbris',       'Malmö', TRUE,  TRUE,  5,  150, 2500),
    ('Pensionat Måsen',         'Malmö', FALSE, TRUE,  3,  600,  800),
    ('Stadshotellet Kvarnen',   'Malmö', FALSE, TRUE,  4, 3000,  100),
    ('Ringmursgården',          'Visby', FALSE, TRUE,  4,  900,  200),
    ('Gotlandsviken Resort',    'Visby', TRUE,  TRUE,  5,   50, 4000),
    ('Vandrarhemmet Sjöboden',  'Visby', FALSE, FALSE, 2,  300, 1500);

-- =====================================================================
-- Rum: 23 st med kapaciteterna 1, 2, 4 och 6 personer
-- =====================================================================
INSERT INTO rooms (room_number, maximum_people, price_per_night, accommodation_id)
SELECT r.room_number, r.maximum_people, r.price_per_night, a.id
FROM (
              SELECT 'Hotell Strandbris'      AS acc, '101'   AS room_number, 2 AS maximum_people, 1450.00 AS price_per_night
    UNION ALL SELECT 'Hotell Strandbris',             '102',                  2,                   1450.00
    UNION ALL SELECT 'Hotell Strandbris',             '201',                  4,                   2300.00
    UNION ALL SELECT 'Hotell Strandbris',             '202',                  4,                   2400.00
    UNION ALL SELECT 'Hotell Strandbris',             '301',                  6,                   3200.00

    UNION ALL SELECT 'Pensionat Måsen',               '1',                    1,                    550.00
    UNION ALL SELECT 'Pensionat Måsen',               '2',                    2,                    790.00
    UNION ALL SELECT 'Pensionat Måsen',               '3',                    2,                    820.00
    UNION ALL SELECT 'Pensionat Måsen',               '4',                    4,                   1200.00

    UNION ALL SELECT 'Stadshotellet Kvarnen',         '11',                   1,                    890.00
    UNION ALL SELECT 'Stadshotellet Kvarnen',         '12',                   2,                   1190.00
    UNION ALL SELECT 'Stadshotellet Kvarnen',         '21',                   2,                   1250.00
    UNION ALL SELECT 'Stadshotellet Kvarnen',         '31',                   4,                   1890.00

    UNION ALL SELECT 'Ringmursgården',                'A1',                   2,                   1050.00
    UNION ALL SELECT 'Ringmursgården',                'A2',                   2,                   1100.00
    UNION ALL SELECT 'Ringmursgården',                'B1',                   4,                   1650.00

    UNION ALL SELECT 'Gotlandsviken Resort',          'S1',                   2,                   1990.00
    UNION ALL SELECT 'Gotlandsviken Resort',          'S2',                   4,                   2890.00
    UNION ALL SELECT 'Gotlandsviken Resort',          'S3',                   4,                   2990.00
    UNION ALL SELECT 'Gotlandsviken Resort',          'Svit',                 6,                   4500.00

    UNION ALL SELECT 'Vandrarhemmet Sjöboden',        '1',                    1,                    390.00
    UNION ALL SELECT 'Vandrarhemmet Sjöboden',        '2',                    1,                    390.00
    UNION ALL SELECT 'Vandrarhemmet Sjöboden',        '3',                    2,                    590.00
) r
JOIN accommodations a ON a.name = r.acc;

-- =====================================================================
-- Kunder: 6 st fiktiva
-- =====================================================================
INSERT INTO customers (first_name, last_name, email, phone_number) VALUES
    ('Anna',   'Lind',      'anna.lind@exempel.se',      '0701112233'),
    ('Erik',   'Holm',      'erik.holm@exempel.se',      '0702223344'),
    ('Sara',   'Berg',      'sara.berg@exempel.se',      '0703334455'),
    ('Johan',  'Ek',        'johan.ek@exempel.se',       '0704445566'),
    ('Fatima', 'Nordström', 'fatima.nordstrom@exempel.se','0705556677'),
    ('Oskar',  'Sjöberg',   'oskar.sjoberg@exempel.se',  '0706667788');

-- =====================================================================
-- Bokningar: 26 st i december 2026, 1–15 nätter
-- =====================================================================
INSERT INTO bookings (customer_id, room_id, check_in_date, check_out_date, number_of_people)
SELECT c.id, r.id, b.check_in_date, b.check_out_date, b.number_of_people
FROM (
              SELECT 'anna.lind@exempel.se' AS email, 'Hotell Strandbris' AS acc, '101' AS room, DATE '2026-12-01' AS check_in_date, DATE '2026-12-04' AS check_out_date, 2 AS number_of_people
    UNION ALL SELECT 'erik.holm@exempel.se',        'Hotell Strandbris',      '101',  DATE '2026-12-04', DATE '2026-12-08', 2   -- checkar in samma dag som föregående checkar ut
    UNION ALL SELECT 'sara.berg@exempel.se',        'Hotell Strandbris',      '102',  DATE '2026-12-10', DATE '2026-12-17', 1
    UNION ALL SELECT 'erik.holm@exempel.se',        'Hotell Strandbris',      '102',  DATE '2026-12-24', DATE '2026-12-26', 2
    UNION ALL SELECT 'johan.ek@exempel.se',         'Hotell Strandbris',      '201',  DATE '2026-12-20', DATE '2026-12-27', 4
    UNION ALL SELECT 'fatima.nordstrom@exempel.se', 'Hotell Strandbris',      '202',  DATE '2026-12-02', DATE '2026-12-05', 3
    UNION ALL SELECT 'oskar.sjoberg@exempel.se',    'Hotell Strandbris',      '301',  DATE '2026-12-05', DATE '2026-12-20', 6   -- lång bokning, 15 nätter

    UNION ALL SELECT 'anna.lind@exempel.se',        'Pensionat Måsen',        '1',    DATE '2026-12-10', DATE '2026-12-13', 1
    UNION ALL SELECT 'erik.holm@exempel.se',        'Pensionat Måsen',        '1',    DATE '2026-12-13', DATE '2026-12-16', 1   -- checkar in samma dag som föregående checkar ut
    UNION ALL SELECT 'sara.berg@exempel.se',        'Pensionat Måsen',        '2',    DATE '2026-12-01', DATE '2026-12-03', 2
    UNION ALL SELECT 'johan.ek@exempel.se',         'Pensionat Måsen',        '3',    DATE '2026-12-18', DATE '2026-12-24', 2
    UNION ALL SELECT 'fatima.nordstrom@exempel.se', 'Pensionat Måsen',        '4',    DATE '2026-12-26', DATE '2026-12-31', 4

    UNION ALL SELECT 'oskar.sjoberg@exempel.se',    'Stadshotellet Kvarnen',  '11',   DATE '2026-12-07', DATE '2026-12-09', 1
    UNION ALL SELECT 'anna.lind@exempel.se',        'Stadshotellet Kvarnen',  '12',   DATE '2026-12-14', DATE '2026-12-15', 1   -- en natt
    UNION ALL SELECT 'erik.holm@exempel.se',        'Stadshotellet Kvarnen',  '21',   DATE '2026-12-22', DATE '2026-12-29', 2
    UNION ALL SELECT 'sara.berg@exempel.se',        'Stadshotellet Kvarnen',  '31',   DATE '2026-12-03', DATE '2026-12-10', 3

    UNION ALL SELECT 'johan.ek@exempel.se',         'Ringmursgården',         'A1',   DATE '2026-12-05', DATE '2026-12-08', 2
    UNION ALL SELECT 'fatima.nordstrom@exempel.se', 'Ringmursgården',         'A2',   DATE '2026-12-11', DATE '2026-12-19', 2
    UNION ALL SELECT 'oskar.sjoberg@exempel.se',    'Ringmursgården',         'B1',   DATE '2026-12-20', DATE '2026-12-27', 4

    UNION ALL SELECT 'anna.lind@exempel.se',        'Gotlandsviken Resort',   'S1',   DATE '2026-12-01', DATE '2026-12-06', 2
    UNION ALL SELECT 'erik.holm@exempel.se',        'Gotlandsviken Resort',   'S2',   DATE '2026-12-20', DATE '2026-12-27', 4
    UNION ALL SELECT 'sara.berg@exempel.se',        'Gotlandsviken Resort',   'S3',   DATE '2026-12-19', DATE '2026-12-28', 3
    UNION ALL SELECT 'johan.ek@exempel.se',         'Gotlandsviken Resort',   'Svit', DATE '2026-12-21', DATE '2026-12-26', 5

    UNION ALL SELECT 'fatima.nordstrom@exempel.se', 'Vandrarhemmet Sjöboden', '1',    DATE '2026-12-12', DATE '2026-12-14', 1
    UNION ALL SELECT 'oskar.sjoberg@exempel.se',    'Vandrarhemmet Sjöboden', '2',    DATE '2026-12-12', DATE '2026-12-14', 1
    UNION ALL SELECT 'anna.lind@exempel.se',        'Vandrarhemmet Sjöboden', '3',    DATE '2026-12-28', DATE '2026-12-31', 2
) b
JOIN customers c      ON c.email = b.email
JOIN accommodations a ON a.name = b.acc
JOIN rooms r          ON r.accommodation_id = a.id AND r.room_number = b.room;
