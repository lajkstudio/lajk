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
    ('Póló', '/media/termek_01.png', '', '', '5000'),
    ('Tornazsák', '/media/termek_02.png', '', '', '4600'),
    ('Zokni', '/media/termek_03.png', '', '', '3700'),
    ('Poszter', '/media/termek_04.png', '', '', '16400'),
    ('Terítő', '/media/termek_05.png', '', '', '5900'),
    ('Párna', '/media/termek_06.png', '', '', '11200'),
    ('Törölköző', '/media/termek_07.png', '', '', '12000'),
    ('Bögre', '/media/termek_08.png', '', '', '5900'),
    ('Ágyneműhuzat', '/media/termek_09.png', '', '', '7500'),
    ('Nadrág', '/media/termek_10.png', '', '', '8800'),
    ('Sapka', '/media/termek_11.png', '', '', '4900'),
    ('Oldaltáska', '/media/termek_12.png', '', '', '19300');
