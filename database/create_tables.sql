-- MySQL table creation script for the BMS API models.
-- Tables and columns are derived from the model classes in bmsmodel
-- and the table names used by the repositories in bmsrepository.

CREATE TABLE IF NOT EXISTS `Users` (
    `Id` BIGINT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(150) NOT NULL DEFAULT '',
    `Email` VARCHAR(255) NULL,
    `Mobile` VARCHAR(50) NOT NULL DEFAULT '',
    `Password` VARCHAR(255) NOT NULL DEFAULT '',
    `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (`Id`),
    KEY `IX_Users_IsActive` (`IsActive`),
    KEY `IX_Users_Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `FoodCategories` (
    `Id` BIGINT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(150) NOT NULL DEFAULT '',
    `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (`Id`),
    KEY `IX_FoodCategories_IsActive` (`IsActive`),
    KEY `IX_FoodCategories_Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `FoodTables` (
    `Id` BIGINT NOT NULL AUTO_INCREMENT,
    `TableStatus` INT NOT NULL DEFAULT 1 COMMENT 'FoodTableType: 1=Available, 2=Reserved, 3=Occupied, 4=Cleaning',
    `BookTime` DATETIME NOT NULL,
    `Name` VARCHAR(150) NOT NULL DEFAULT '',
    `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (`Id`),
    KEY `IX_FoodTables_IsActive` (`IsActive`),
    KEY `IX_FoodTables_TableStatus` (`TableStatus`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Foods` (
    `Id` BIGINT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(150) NOT NULL DEFAULT '',
    `Description` TEXT NULL,
    `Price` DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    `FoodCategoryId` BIGINT NOT NULL,
    `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (`Id`),
    KEY `IX_Foods_IsActive` (`IsActive`),
    KEY `IX_Foods_Name` (`Name`),
    KEY `IX_Foods_FoodCategoryId` (`FoodCategoryId`),
    CONSTRAINT `FK_Foods_FoodCategories_FoodCategoryId`
        FOREIGN KEY (`FoodCategoryId`) REFERENCES `FoodCategories` (`Id`)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Orders` (
    `Id` BIGINT NOT NULL AUTO_INCREMENT,
    `UserId` BIGINT NOT NULL,
    `OrderStatus` INT NOT NULL DEFAULT 1 COMMENT 'OrderStatus: 1=Pending, 2=Accepted, 3=Preparing, 4=Ready, 5=Served, 6=Completed, 7=Cancelled',
    `OrderType` INT NOT NULL DEFAULT 1 COMMENT 'OrderType: 1=DineIn, 2=TakeAway, 3=Delivery',
    `OrderDate` DATETIME NOT NULL,
    `Notes` TEXT NULL,
    `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (`Id`),
    KEY `IX_Orders_IsActive` (`IsActive`),
    KEY `IX_Orders_UserId` (`UserId`),
    KEY `IX_Orders_OrderStatus` (`OrderStatus`),
    KEY `IX_Orders_OrderType` (`OrderType`),
    CONSTRAINT `FK_Orders_Users_UserId`
        FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OrderItems` (
    `Id` BIGINT NOT NULL AUTO_INCREMENT,
    `Quantity` INT NOT NULL DEFAULT 0,
    `FoodId` BIGINT NOT NULL,
    `Notes` TEXT NULL,
    `OrderId` BIGINT NOT NULL,
    `OrderStatus` VARCHAR(50) NOT NULL DEFAULT '',
    `FoodTableId` BIGINT NOT NULL,
    `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (`Id`),
    KEY `IX_OrderItems_IsActive` (`IsActive`),
    KEY `IX_OrderItems_FoodId` (`FoodId`),
    KEY `IX_OrderItems_OrderId` (`OrderId`),
    KEY `IX_OrderItems_FoodTableId` (`FoodTableId`),
    CONSTRAINT `FK_OrderItems_Foods_FoodId`
        FOREIGN KEY (`FoodId`) REFERENCES `Foods` (`Id`)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `FK_OrderItems_Orders_OrderId`
        FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`Id`)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `FK_OrderItems_FoodTables_FoodTableId`
        FOREIGN KEY (`FoodTableId`) REFERENCES `FoodTables` (`Id`)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
