DROP DATABASE IF EXISTS Lajk;
CREATE DATABASE IF NOT EXISTS Lajk
    CHARSET utf8
    COLLATE utf8_hungarian_ci;
CREATE USER 'lajk'@'localhost' IDENTIFIED BY 'lajk';
GRANT ALL PRIVILEGES ON Lajk.* TO 'lajk'@'localhost';
FLUSH PRIVILEGES;
USE Lajk;

CREATE TABLE IF NOT EXISTS item (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(25) UNIQUE NOT NULL,
    img_url_1 VARCHAR(200) NOT NULL,
    img_url_2 VARCHAR(200) NOT NULL,
    img_url_3 VARCHAR(200) NOT NULL,
    price INT NOT NULL,
    emoji_price_increment INT NOT NULL DEFAULT 500);

INSERT INTO item (name, img_url_1, img_url_2, img_url_3, price)
VALUES
    ('Póló', '/media/termek_01.png', '/media/termek_01_feher.png', '/media/termek_01_fekete.png', '5000'),
    ('Tornazsák', '/media/termek_02.png', '/media/termek_02_feher.png', '/media/termek_02_fekete.png', '4600'),
    ('Zokni', '/media/termek_03.png', '/media/termek_03_feher.png', '/media/termek_03_fekete.png', '3700'),
    ('Poszter', '/media/termek_04.png', '/media/termek_04_feher.png', '/media/termek_04_fekete.png', '16400'),
    ('Terítő', '/media/termek_05.png', '/media/termek_05_feher.png', '/media/termek_05_fekete.png', '5900'),
    ('Párna', '/media/termek_06.png', '/media/termek_06_feher.png', '/media/termek_06_fekete.png', '11200'),
    ('Törölköző', '/media/termek_07.png', '/media/termek_07_feher.png', '/media/termek_07_fekete.png', '12000'),
    ('Bögre', '/media/termek_08.png', '/media/termek_08_feher.png', '/media/termek_08_fekete.png', '5900'),
    ('Ágyneműhuzat', '/media/termek_09.png', '/media/termek_09_feher.png', '/media/termek_09_fekete.png', '7500'),
    ('Nadrág', '/media/termek_10.png', '/media/termek_10_feher.png', '/media/termek_10_fekete.png', '8800'),
    ('Sapka', '/media/termek_11.png', '/media/termek_11_feher.png', '/media/termek_11_fekete.png', '4900'),
    ('Oldaltáska', '/media/termek_12.png', '/media/termek_12_feher.png', '/media/termek_12_fekete.png', '19300');
