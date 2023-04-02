CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

ALTER TABLE accounts ADD COLUMN coverImg VARCHAR(500);

ALTER TABLE accounts
ALTER COLUMN coverImg
SET
    DEFAULT 'https://images.unsplash.com/photo-1457364887197-9150188c107b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80';

CREATE TABLE
    keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT NOT NULL DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    keeps(
        name,
        description,
        img,
        creatorId
    )
VALUES (
        'Baby Cows',
        'Here is a very cute baby cow',
        'https://images.unsplash.com/photo-1545468800-85cc9bc6ecf7?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=715&q=80',
        '63ebf351822e730e1e0b3616'
    );

CREATE TABLE
    vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    vaults(
        name,
        description,
        img,
        creatorId
    )
VALUES (
        'Farms',
        'This vault is for farms and farm animals',
        'https://images.unsplash.com/photo-1444858291040-58f756a3bdd6?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2048&q=80',
        '63ebf351822e730e1e0b3616'
    );

CREATE TABLE
    vaultKeeps(
        id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

SELECT k.*, v.*, vk.*, c.*
FROM vaultKeeps vk
    JOIN keeps k ON vk.keepId = k.id
    JOIN vaults v ON vk.vaultId = v.id
    JOIN accounts c ON vk.creatorId = c.id
WHERE vaultId = 9;

SELECT k.*
FROM keeps k
    JOIN accounts a ON k.creatorId = a.id
WHERE
    a.id = '63ebf351822e730e1e0b3616';

SELECT k.*, a.*
FROM keeps k
    JOIN accounts a ON k.creatorId = a.id
ORDER BY k.id;