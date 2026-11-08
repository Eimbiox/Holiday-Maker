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
    `customers` ADD UNIQUE `customers_mail_unique`(`mail`);
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