-- CREATE DATABASE PokemonCardStoreDB;
-- ALTER DATABASE PokemonCardStoreDB
-- SET AUTO_CLOSE OFF;

DROP TABLE IF EXISTS OrderSummary;
DROP TABLE IF EXISTS ShoppingCart;
DROP TABLE IF EXISTS PokemonCard;
DROP TABLE IF EXISTS Condition;
DROP TABLE IF EXISTS Foil;
DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS StoreFront;
DROP TABLE IF EXISTS Customer;

CREATE TABLE Customer (
	username varchar(20),
	email varchar(30),
	firstName varchar(20),
	lastName varchar(20),
	hashPassword varchar(80),
	PRIMARY KEY (username)
);

CREATE TABLE StoreFront (
	storeID INT IDENTITY(0000, 1),
	userName varchar(20), 
	city varchar(20),
	state varchar(2),
	PRIMARY KEY (storeID),
    FOREIGN KEY (userName) REFERENCES Customer(userName)
);

CREATE TABLE Inventory (
	storeID INT,
	cardID INT IDENTITY(0000, 1),
	quantity INT,
	PRIMARY KEY (cardID),
    FOREIGN KEY (storeID) REFERENCES storeFront(storeID)
);

CREATE TABLE Condition (
	conditionID INT PRIMARY KEY,
	conditionTitle varchar(20),
	conditionDesc varchar(500)
);

CREATE TABLE Foil (
	foilID INT PRIMARY KEY,
	foilTitle varchar(20),
	foilDesc varchar(400)
);

CREATE TABLE PokemonCard (
	cardID INT,
	cardName varchar(30),
	cardSet varchar(20),
	foilID INT NOT NULL,
	conditionID INT NOT NULL,
	price DECIMAL(10,2),
	PRIMARY KEY (cardID),
    FOREIGN KEY (cardID) REFERENCES Inventory(cardID),
    FOREIGN KEY (conditionID) REFERENCES Condition(conditionID),
    FOREIGN KEY (foilID) REFERENCES Foil(foilID)
);

CREATE TABLE ShoppingCart (
	username varchar(20),
	storeID INT,
	cardID INT,
	Quantity INT,
	IndividualPrice DECIMAL(10,2),
	PRIMARY KEY (username, storeID, cardID)
);

CREATE TABLE OrderSummary (
	individualOrderID INT IDENTITY(0000, 1),
	orderNumber INT,
	username varchar(20),
	storeID INT,
	cardID INT,
	quantity INT,
	orderDate DATE,
	totalPrice DECIMAL(10,2),
	PRIMARY KEY (individualOrderID)
);

----------------------------------------------------------------------------

ALTER TABLE ShoppingCart
ADD CONSTRAINT FK_ShoppingCart
FOREIGN KEY (userName)
REFERENCES Customer(userName),
FOREIGN KEY (storeID)
REFERENCES StoreFront(storeID),
FOREIGN KEY (cardID)
REFERENCES Inventory(cardID);

ALTER TABLE OrderSummary
ADD CONSTRAINT FK_OrderSummary
FOREIGN KEY (username)
REFERENCES Customer(username),
FOREIGN KEY (storeID)
REFERENCES StoreFront(storeID);

-----------------------------------------------------------------------------------

INSERT INTO Condition(conditionID, conditionTitle, conditionDesc)
VALUES (0, 'Mint', 'Cards in Mint (M) conditin came straight from the pack. The card was never played or shuffled.'),
	(1, 'Near Mint', 'Cards in Near Mint (NM) condition show minimal to no wear from shuffling, play or handling and can have a nearly unmarked surface, crisp corners and unblemished edges outside of a few minimal flaws. A Near Mint card may have a tiny edge nick or a tiny scratch or two, but overall look nearly unplayed with no major defects or flaws.'),
	(2, 'Lightly Played', 'Cards in Lightly Played (LP) condition may have minor border or corner wear or even just slight scuffs or scratches. There are no major defects such as liquid damage, bends or issues with the structural integrity of the card. Noticeable imperfections are okay, but none should be too severe or at too high a volume.'),
	(3, 'Moderately Played', 'Cards in Moderately Played (MP) condition can have border wear, corner wear, scratching or scuffing, creases or whitening or any combination of mild examples of these marks.'),
	(4, 'Heavily Played', 'Cards in Heavily Played (HP) condition show a severe amount of wear. Cards with less than 30% of the surface being liquid damaged are typically accepted but may be considered Damaged if especially detrimental. HP cards can have one small instance of missing ink (such as if something is stuck to a card, then removed and pulls away part of the card), along with major creasing, whitening and border wear if the card can still be sleeve playable.'),
	(5, 'Damaged', 'Cards in Damaged condition can exhibit a tear, bend or crease that may make the card illegal for tournament play, even in a sleeve. If more than 30% of the card is damaged by liquid, it''s considered Damaged. Cards in Damaged condition may have extreme border wear, extreme corner wear, heavy scratching or scuffing, folds, creases or tears or other damages that impacts the structural integrity of the card.');

INSERT INTO Foil(foilID, foilTitle, foilDesc)
VALUES (0, 'Normal Print', 'Cards with no "shiny" holographic effects anywhere.'),
	(1, 'Rare Holo', 'Cards have a black star and a “shiny” (foil) illustration. In many English sets, for every rare holo card there is another card at a lower rarity that is identical in terms of gameplay, but has a different collector card number.'),
	(2, 'Reverse Holo', 'Cards are foil on every part of the card except the illustration. This only changes the physical appearance of the card and does not change its rarity or collector card number.'),
	(3, 'Ultra Rare', 'Cards are foil and feature a specific game mechanic and/or appearance that distinguishes them from Rare Holo cards.'),
	(4, 'Secret Rare', 'Cards have a collector number higher than the advertised number of cards in the set. They are usually foil with a unique appearance. Like Rare Holo cards, in many sets for each Secret Rare card there is another card at a lower rarity that is identical in terms of gameplay but has a different collector card number.');

INSERT INTO Customer (username, email, firstName, lastName, hashPassword)
VALUES ('Mr. Duck', 'quacker@gmail.com', 'Steve', 'Duck', '$2a$11$6e.l2KFb2r3tX8doOLnFQu1b3I57eS/FauU/B2CZ2cEIu0IZuVpQO'),
	('crkobel', 'crkobel@verizon.net', 'Connor', 'Kobel', '$2a$11$6e.l2KFb2r3tX8doOLnFQu1b3I57eS/FauU/B2CZ2cEIu0IZuVpQO'),
	('Spiderman', 'webslinger@gmail.com', 'Peter', 'Parker', '$2a$11$6e.l2KFb2r3tX8doOLnFQu1b3I57eS/FauU/B2CZ2cEIu0IZuVpQO'),
	('iLoveFire', 'notahorse@gmail.com', 'Solara', 'Driftleaf', '$2a$11$6e.l2KFb2r3tX8doOLnFQu1b3I57eS/FauU/B2CZ2cEIu0IZuVpQO');

INSERT INTO StoreFront (username, city, state)
VALUES ('crkobel', 'Huntington Beach', 'CA'),
	('Spiderman', 'Queens', 'NY');

INSERT INTO Inventory (storeID, quantity)
VALUES (0, 2),
	(0, 1),
	(0, 1),
	(0, 5),
	(0, 1),
	(0, 1),
	(0, 2),
	(1, 4),
	(1, 2),
	(1, 6),
	(1, 2),
	(1, 1),
	(1, 3),
	(1, 1);

INSERT INTO PokemonCard (cardID, cardName, cardSet, conditionID, foilID, price)
VALUES (0, 'Articuno', 'Fossil', 1, 1, 19),
	(1, 'Rhydon', 'Base Set 2', 3, 0, 3.95),
	(2, 'Lickitung', 'Furious Fists', 2, 0, .99),
	(3, 'Gengar', 'Generations', 1, 1, 12),
	(4, 'Vulpix', 'Evolutions', 0, 1, 1.25),
	(5, 'Growlithe', 'Base Set Shadowless', 4, 0, 3.95),
	(6, 'Seel', 'Base Set', 5, 0, 3.69),
	(7, 'Articuno', 'Fossil', 1, 1, 19),
	(8, 'Metapod', 'Base Set Shadowless', 1, 1, 30.10),
	(9, 'Pikachu', 'Sword and Shield', 0, 0, 5.98),
	(10, 'Scyther', 'Platinum Base Set', 3, 2, 38.10),
	(11, 'Blastoise', 'Base Set', 3, 1, 130),
	(12, 'Charizard', 'Base Set 1st Edition', 0, 1, 9050.00),
	(13, 'Venusaur', 'Legendary Collection', 1, 2, 247.69);
--------------------------------------------------------------------------------------------------
SELECT pc.cardID, cardName, cardSet, f.foilTitle, c.conditionTitle, price, storeID, i.quantity FROM PokemonCard pc
INNER JOIN Inventory i ON i.cardID = pc.cardID
INNER JOIN Condition c ON c.conditionID = pc.conditionID
INNER JOIN FOIL f ON f.foilID = pc.foilID
ORDER BY storeID, cardName;

SELECT * FROM StoreFront;

SELECT * FROM OrderSummary;

SELECT os.orderNumber, os.storeID, pc.cardName, pc.cardSet, conditionTitle, foilTitle, os.quantity, orderDate, totalPrice FROM OrderSummary os
INNER JOIN StoreFront sf ON sf.storeID = os.storeID
INNER JOIN Inventory i ON i.cardID = os.cardID
INNER JOIN PokemonCard pc ON pc.cardID = i.cardID
INNER JOIN Condition c ON c.conditionID = pc.conditionID
INNER JOIN Foil f ON f.foilID = pc.foilID
WHERE os.username = 'crkobel';

SELECT * FROM PokemonCard 
INNER JOIN Inventory ON Inventory.CardID = PokemonCard.CardID 
INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID 
INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID 
INNER JOIN StoreFront ON StoreFront.storeID = Inventory.storeID 
WHERE username = 'crkobel' ORDER BY cardName;

