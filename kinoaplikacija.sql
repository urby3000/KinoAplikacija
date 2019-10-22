-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Gostitelj: 127.0.0.1
-- Čas nastanka: 30. jun 2019 ob 10.44
-- Različica strežnika: 10.1.37-MariaDB
-- Različica PHP: 7.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Zbirka podatkov: `kinoaplikacija`
--

-- --------------------------------------------------------

--
-- Struktura tabele `bills`
--

CREATE TABLE `bills` (
  `ID` int(11) NOT NULL,
  `OrderDate` datetime NOT NULL,
  `PayDate` datetime DEFAULT NULL,
  `Paid` tinyint(1) DEFAULT NULL,
  `Price` decimal(19,5) DEFAULT NULL,
  `FullPrice` decimal(19,5) DEFAULT NULL,
  `FK_Discount_Id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `bills`
--

INSERT INTO `bills` (`ID`, `OrderDate`, `PayDate`, `Paid`, `Price`, `FullPrice`, `FK_Discount_Id`) VALUES
(1, '2019-05-07 15:00:00', '2019-05-08 16:00:00', 1, '150.00000', '30.00000', 1),
(4, '2019-05-30 00:00:00', '2019-05-30 12:15:20', 1, '126.00000', '0.00000', 19),
(6, '2019-05-09 16:48:13', '2019-05-09 17:35:01', 1, '12.00000', '11.40000', 7),
(7, '2019-05-09 16:49:09', '2019-05-09 17:48:50', 1, '6.00000', '1.20000', 1),
(8, '2019-05-09 17:27:38', '2019-05-09 17:48:58', 1, '96.00000', '19.20000', 1),
(9, '2019-05-09 17:28:21', '2019-05-09 17:49:34', 0, '48.00000', '9.60000', 1),
(10, '2019-05-09 17:28:32', '2019-05-09 17:49:58', 1, '24.00000', '4.80000', 1),
(11, '2019-05-09 17:51:38', '2019-05-09 17:55:23', 1, '36.00000', '34.20000', 7),
(12, '2019-05-09 17:51:51', '2019-05-09 17:55:26', 1, '90.00000', '85.50000', 7),
(13, '2019-05-09 17:52:18', '2019-05-09 17:55:29', 1, '24.00000', '22.80000', 7),
(14, '2019-05-09 18:26:49', '2019-05-09 18:27:03', 1, '6.00000', '5.70000', 7),
(15, '2019-05-11 10:56:32', NULL, 0, '180.00000', '171.00000', 7),
(16, '2019-05-12 16:56:09', '2019-05-12 16:56:51', 0, '60.00000', '57.00000', 7),
(17, '2019-05-12 18:30:45', NULL, 0, '24.00000', '22.80000', 7),
(19, '2019-05-13 20:05:04', NULL, 0, '48.00000', '45.60000', 7),
(20, '2019-05-14 15:35:06', '2019-05-14 15:35:16', 1, '118.00000', '112.10000', 7),
(21, '2019-05-06 01:00:00', NULL, 0, '40.00000', '40.00000', NULL),
(22, '2019-05-06 01:00:00', NULL, 0, '5.00000', '5.00000', NULL),
(23, '2019-05-23 12:41:41', '2019-05-23 12:41:59', 1, '49.00000', '46.55000', 7),
(24, '2019-06-06 08:32:45', '2019-06-06 08:32:58', 1, '24.00000', '22.56000', 8),
(25, '2019-06-06 10:22:25', '2019-06-06 10:28:56', 1, '52.00000', '48.88000', 8);

-- --------------------------------------------------------

--
-- Struktura tabele `countries`
--

CREATE TABLE `countries` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Abbreviation` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `countries`
--

INSERT INTO `countries` (`ID`, `Name`, `Abbreviation`) VALUES
(1, 'Slovenija', 'SVN'),
(2, 'Italia', 'ITA'),
(17, 'Hrvatska', 'CRO');

-- --------------------------------------------------------

--
-- Struktura tabele `discounts`
--

CREATE TABLE `discounts` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Percent` decimal(19,5) NOT NULL,
  `FromDate` datetime DEFAULT NULL,
  `ToDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `discounts`
--

INSERT INTO `discounts` (`ID`, `Name`, `Percent`, `FromDate`, `ToDate`) VALUES
(1, 'Birthday Discount', '80.00000', NULL, NULL),
(3, 'January Discount', '1.00000', '2019-01-01 00:00:00', '2019-01-31 00:00:00'),
(4, 'February Discount', '2.00000', '2019-02-01 00:00:00', '2019-02-28 00:00:00'),
(5, 'March Discount', '3.00000', '2019-03-01 00:00:00', '2019-03-31 00:00:00'),
(6, 'April Discount', '4.00000', '2019-04-01 00:00:00', '2019-04-30 00:00:00'),
(7, 'May Discount', '5.00000', '2019-05-01 00:00:00', '2019-05-31 00:00:00'),
(8, 'June Discount', '6.00000', '2019-06-01 00:00:00', '2019-06-30 00:00:00'),
(9, 'July Discount', '7.00000', '2019-07-01 00:00:00', '2019-07-31 00:00:00'),
(10, 'August Discount', '8.00000', '2019-08-01 00:00:00', '2019-08-31 00:00:00'),
(11, 'September Discount', '9.00000', '2019-09-01 00:00:00', '2019-09-30 00:00:00'),
(12, 'October Discount', '10.00000', '2019-10-01 00:00:00', '2019-10-31 00:00:00'),
(13, 'November Discount', '11.00000', '2019-11-01 00:00:00', '2019-11-30 00:00:00'),
(14, 'December Discount', '12.00000', '2019-12-01 00:00:00', '2019-12-31 00:00:00'),
(19, 'Free Discount', '100.00000', NULL, NULL);

-- --------------------------------------------------------

--
-- Struktura tabele `events`
--

CREATE TABLE `events` (
  `ID` int(11) NOT NULL,
  `Date` datetime NOT NULL,
  `Price` decimal(19,5) NOT NULL,
  `FK_Movie_Id` int(11) NOT NULL,
  `FK_Room_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `events`
--

INSERT INTO `events` (`ID`, `Date`, `Price`, `FK_Movie_Id`, `FK_Room_Id`) VALUES
(3, '2019-07-01 14:00:00', '12.00000', 1, 7),
(4, '2019-09-09 19:00:00', '6.00000', 2, 2),
(5, '2019-05-14 19:00:00', '3.00000', 7, 8),
(6, '2019-05-31 20:40:00', '7.00000', 9, 4),
(7, '2019-11-21 20:00:00', '5.00000', 1, 8);

-- --------------------------------------------------------

--
-- Struktura tabele `genres`
--

CREATE TABLE `genres` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `genres`
--

INSERT INTO `genres` (`ID`, `Name`) VALUES
(1, 'Comedy'),
(3, 'Action'),
(4, 'Drama'),
(5, 'Crime'),
(6, 'Biography'),
(7, 'Mystery'),
(8, 'Thriller'),
(9, 'War'),
(10, 'Fantasy'),
(11, 'Romance');

-- --------------------------------------------------------

--
-- Struktura tabele `moviegenre`
--

CREATE TABLE `moviegenre` (
  `ID` int(11) NOT NULL,
  `FK_Movie_Id` int(11) NOT NULL,
  `FK_Genre_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `moviegenre`
--

INSERT INTO `moviegenre` (`ID`, `FK_Movie_Id`, `FK_Genre_Id`) VALUES
(1, 1, 5),
(2, 1, 4),
(3, 3, 6),
(4, 3, 5),
(5, 3, 4),
(6, 2, 5),
(7, 2, 4),
(8, 11, 4),
(9, 11, 5),
(10, 11, 7),
(11, 12, 4),
(12, 12, 5),
(13, 12, 7),
(20, 5, 3),
(21, 5, 4),
(22, 5, 5),
(23, 6, 4),
(24, 7, 4),
(26, 8, 4),
(27, 9, 4),
(28, 9, 5),
(29, 10, 3),
(30, 10, 4),
(31, 10, 5);

-- --------------------------------------------------------

--
-- Struktura tabele `movies`
--

CREATE TABLE `movies` (
  `ID` int(11) NOT NULL,
  `Title` text NOT NULL,
  `Description` text NOT NULL,
  `ImageSource` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `movies`
--

INSERT INTO `movies` (`ID`, `Title`, `Description`, `ImageSource`) VALUES
(1, 'Taxi Driver', '1976', 'https://m.media-amazon.com/images/M/MV5BM2M1MmVhNDgtNmI0YS00ZDNmLTkyNjctNTJiYTQ2N2NmYzc2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SY1000_CR0,0,658,1000_AL_.jpg'),
(2, 'Scarface', '1983', 'https://m.media-amazon.com/images/M/MV5BNjdjNGQ4NDEtNTEwYS00MTgxLTliYzQtYzE2ZDRiZjFhZmNlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SY1000_CR0,0,666,1000_AL_.jpg'),
(3, 'Goodfellas ', '1990', 'https://m.media-amazon.com/images/M/MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX667_CR0,0,667,999_AL_.jpg'),
(4, 'The Usual Suspects', '1995', 'https://m.media-amazon.com/images/M/MV5BYTViNjMyNmUtNDFkNC00ZDRlLThmMDUtZDU2YWE4NGI2ZjVmXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SY1000_CR0,0,670,1000_AL_.jpg'),
(5, 'Léon', '1994', 'https://m.media-amazon.com/images/M/MV5BZDAwYTlhMDEtNTg0OS00NDY2LWJjOWItNWY3YTZkM2UxYzUzXkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_SY1000_CR0,0,710,1000_AL_.jpg'),
(6, 'American History X', '1998', 'https://m.media-amazon.com/images/M/MV5BZjA0MTM4MTQtNzY5MC00NzY3LWI1ZTgtYzcxMjkyMzU4MDZiXkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_.jpg'),
(7, 'Forrest Gump', '1994', 'https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg'),
(8, 'Fight Club', '1999', 'https://m.media-amazon.com/images/M/MV5BMjJmYTNkNmItYjYyZC00MGUxLWJhNWMtZDY4Nzc1MDAwMzU5XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SY1000_CR0,0,676,1000_AL_.jpg'),
(9, 'The Godfather', '1972', 'https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SY1000_CR0,0,704,1000_AL_.jpg'),
(10, 'The Dark Knight', '2008', 'https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_SY1000_CR0,0,675,1000_AL_.jpg'),
(11, 'Zodiac', '2007', 'https://m.media-amazon.com/images/M/MV5BN2UwNDc5NmEtNjVjZS00OTI5LWE5YjctMWM3ZjBiZGYwMGI2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SY1000_CR0,0,677,1000_AL_.jpg'),
(12, 'Prisoners', '2013', 'https://m.media-amazon.com/images/M/MV5BMTg0NTIzMjQ1NV5BMl5BanBnXkFtZTcwNDc3MzM5OQ@@._V1_SY1000_CR0,0,675,1000_AL_.jpg');

-- --------------------------------------------------------

--
-- Struktura tabele `passwordreset`
--

CREATE TABLE `passwordreset` (
  `ID` int(11) NOT NULL,
  `SecurityCode` text NOT NULL,
  `ResetDate` datetime DEFAULT NULL,
  `Reset` tinyint(1) DEFAULT NULL,
  `FK_User_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `passwordreset`
--

INSERT INTO `passwordreset` (`ID`, `SecurityCode`, `ResetDate`, `Reset`, `FK_User_Id`) VALUES
(29, 'WDCU8', '2019-05-12 19:12:12', 1, 7),
(30, 'D16IR', '2019-05-12 19:20:46', 1, 7),
(31, 'J5T0Z', '2019-05-12 19:22:08', 1, 7),
(32, 'L8CD3', '2019-05-12 19:23:13', 1, 7),
(33, 'X64S8', '2019-05-13 08:57:20', 1, 7),
(34, 'RS5V0', '2019-05-13 09:00:04', 1, 7),
(35, 'S8L42', '2019-05-13 09:25:38', 1, 7),
(36, 'F3FGZ', '2019-05-13 09:29:04', 1, 7),
(37, '331SO', '2019-05-13 09:31:02', 1, 7),
(38, 'TKW9R', '2019-05-13 10:39:38', 1, 7),
(39, 'U0F4D', '2019-05-13 10:45:06', 1, 7),
(40, 'M0QTO', '2019-05-13 10:52:38', 1, 7),
(41, 'YROAE', '2019-05-13 11:16:33', 1, 7),
(42, 'IMW4F', '2019-05-13 11:22:47', 1, 7),
(43, 'I4Y8G', '2019-05-13 11:29:41', 1, 7),
(44, 'JRN1B', '2019-05-13 13:30:59', 1, 7),
(45, 'QW6IQ', '2019-05-13 20:01:33', 1, 7),
(46, 'WL3ZR', '2019-05-13 20:03:56', 1, 7),
(47, 'ZGFGX', '2019-05-13 20:11:42', 1, 7),
(48, 'BU9H1', '2019-05-14 12:01:33', 1, 7),
(49, '0UC07', '2019-05-14 12:03:12', 1, 7),
(50, '2A499', '2019-05-14 12:04:25', 1, 7),
(51, 'WNTO5', '2019-05-14 12:18:11', 1, 7),
(52, 'OBI7G', '2019-05-14 12:18:41', 1, 7),
(53, '4ZTQX', '2019-05-17 22:11:22', 1, 7),
(54, '11MO7', '2019-05-18 10:06:23', 1, 7),
(55, '9DJGX', '2019-05-18 10:08:05', 1, 7),
(56, 'Q7RL7', '2019-05-18 10:17:22', 1, 7),
(57, 'YWP1W', '2019-05-18 11:10:36', 1, 7),
(58, 'QHWZO', '2019-05-18 11:11:37', 1, 7),
(59, 'QIEBU', '2019-05-23 12:38:01', 1, 7),
(60, 'HBFQB', '2019-05-23 12:38:51', 1, 7),
(61, '1BJNH', '2019-06-06 08:47:02', 1, 7),
(62, 'KT0QA', '2019-06-06 10:04:34', 1, 7);

-- --------------------------------------------------------

--
-- Struktura tabele `places`
--

CREATE TABLE `places` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `PostalCode` text NOT NULL,
  `FK_Country_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `places`
--

INSERT INTO `places` (`ID`, `Name`, `PostalCode`, `FK_Country_Id`) VALUES
(34, 'Žalec', '3310', 1),
(35, 'Celje', '3000', 1),
(38, 'Ljubljana', '1000', 1),
(39, 'Zagreb', '10000', 17),
(40, 'Umag', '52470', 17),
(41, 'Genova', '16121', 2),
(42, 'Milano', '20100', 2),
(43, 'Venezia', '30100', 2),
(44, 'Velenje', '3320', 1);

-- --------------------------------------------------------

--
-- Struktura tabele `reservations`
--

CREATE TABLE `reservations` (
  `ID` int(11) NOT NULL,
  `SeatNumber` int(11) NOT NULL,
  `FK_User_Id` int(11) NOT NULL,
  `FK_Event_Id` int(11) NOT NULL,
  `FK_Bill_Id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `reservations`
--

INSERT INTO `reservations` (`ID`, `SeatNumber`, `FK_User_Id`, `FK_Event_Id`, `FK_Bill_Id`) VALUES
(1, 10, 7, 4, 4),
(2, 2, 10, 4, 1),
(3, 138, 10, 3, 1),
(4, 1, 10, 3, 1),
(5, 2, 10, 3, 1),
(6, 3, 10, 3, 1),
(7, 4, 10, 3, 1),
(8, 5, 10, 3, 1),
(9, 6, 10, 3, 1),
(10, 7, 10, 3, 1),
(11, 8, 10, 3, 1),
(12, 9, 10, 3, 1),
(13, 10, 10, 3, 1),
(14, 11, 10, 3, 1),
(18, 12, 7, 3, 4),
(19, 13, 7, 3, 4),
(20, 14, 7, 3, 4),
(21, 15, 7, 3, 4),
(22, 16, 7, 3, 4),
(23, 17, 7, 3, 4),
(24, 18, 7, 3, 4),
(25, 19, 7, 3, 4),
(26, 20, 7, 3, 4),
(27, 21, 7, 3, 4),
(29, 22, 7, 3, 6),
(30, 3, 7, 4, 7),
(31, 23, 7, 3, 8),
(32, 24, 7, 3, 8),
(33, 25, 7, 3, 8),
(34, 26, 7, 3, 8),
(35, 27, 7, 3, 8),
(36, 4, 7, 4, 8),
(37, 5, 7, 4, 8),
(38, 6, 7, 4, 8),
(39, 7, 7, 4, 8),
(40, 8, 7, 4, 8),
(41, 9, 7, 4, 8),
(42, 28, 7, 3, 9),
(43, 29, 7, 3, 9),
(44, 35, 7, 3, 9),
(45, 36, 7, 3, 9),
(46, 45, 7, 3, 10),
(47, 44, 7, 3, 10),
(48, 81, 7, 4, 11),
(49, 80, 7, 4, 11),
(50, 52, 7, 3, 11),
(51, 51, 7, 3, 11),
(52, 139, 7, 3, 12),
(53, 140, 7, 3, 12),
(54, 131, 7, 3, 12),
(55, 130, 7, 3, 12),
(56, 137, 7, 3, 12),
(57, 72, 7, 4, 12),
(58, 63, 7, 4, 12),
(59, 61, 7, 4, 12),
(60, 96, 7, 4, 12),
(61, 97, 7, 4, 12),
(62, 43, 7, 3, 13),
(63, 42, 7, 3, 13),
(64, 57, 7, 4, 14),
(65, 50, 7, 3, 15),
(66, 49, 7, 3, 15),
(67, 48, 7, 3, 15),
(68, 47, 7, 3, 15),
(69, 38, 7, 3, 15),
(70, 39, 7, 3, 15),
(71, 41, 7, 3, 15),
(72, 40, 7, 3, 15),
(73, 54, 7, 4, 15),
(74, 45, 7, 4, 15),
(75, 44, 7, 4, 15),
(76, 53, 7, 4, 15),
(77, 52, 7, 4, 15),
(78, 43, 7, 4, 15),
(79, 51, 7, 4, 15),
(80, 42, 7, 4, 15),
(81, 41, 7, 4, 15),
(82, 33, 7, 4, 15),
(83, 50, 7, 4, 15),
(84, 32, 7, 4, 15),
(85, 40, 7, 4, 15),
(86, 49, 7, 4, 15),
(87, 104, 7, 3, 16),
(88, 105, 7, 3, 16),
(89, 106, 7, 3, 16),
(90, 107, 7, 3, 16),
(91, 103, 7, 3, 16),
(92, 31, 7, 3, 17),
(93, 33, 7, 3, 17),
(94, 4, 7, 5, 19),
(95, 5, 7, 5, 19),
(96, 6, 7, 5, 19),
(97, 7, 7, 5, 19),
(98, 30, 7, 3, 19),
(99, 32, 7, 3, 19),
(100, 34, 7, 3, 19),
(101, 3, 7, 6, 20),
(102, 4, 7, 6, 20),
(103, 5, 7, 6, 20),
(104, 6, 7, 6, 20),
(105, 58, 7, 3, 20),
(106, 59, 7, 3, 20),
(107, 60, 7, 3, 20),
(108, 61, 7, 3, 20),
(109, 53, 7, 3, 20),
(110, 76, 7, 4, 20),
(111, 77, 7, 4, 20),
(112, 68, 7, 4, 20),
(113, 78, 7, 4, 20),
(114, 69, 7, 4, 20),
(115, 1, 7, 7, 21),
(116, 2, 7, 7, 21),
(117, 3, 7, 7, 21),
(118, 4, 7, 7, 21),
(119, 5, 7, 7, 21),
(120, 6, 7, 7, 21),
(121, 7, 7, 7, 21),
(122, 8, 7, 7, 21),
(123, 9, 7, 7, 22),
(124, 25, 7, 5, 23),
(125, 34, 7, 5, 23),
(126, 33, 7, 5, 23),
(127, 69, 7, 6, 23),
(128, 78, 7, 6, 23),
(129, 79, 7, 6, 23),
(130, 70, 7, 6, 23),
(131, 38, 7, 4, 23),
(132, 39, 7, 4, 23),
(133, 46, 11, 3, 24),
(134, 37, 11, 3, 24),
(135, 19, 7, 6, 25),
(136, 20, 7, 6, 25),
(137, 21, 7, 6, 25),
(138, 22, 7, 6, 25),
(139, 20, 7, 4, 25),
(140, 22, 7, 4, 25),
(141, 29, 7, 4, 25),
(142, 31, 7, 4, 25);

-- --------------------------------------------------------

--
-- Struktura tabele `rooms`
--

CREATE TABLE `rooms` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `NumberOfSeats` int(11) NOT NULL,
  `FK_Theater_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `rooms`
--

INSERT INTO `rooms` (`ID`, `Name`, `NumberOfSeats`, `FK_Theater_Id`) VALUES
(1, 'Room 1', 100, 1),
(2, 'Room 2', 100, 1),
(3, 'Room 1', 120, 2),
(4, 'Room 2', 120, 2),
(5, 'Room 3', 120, 2),
(6, 'Room 4', 120, 2),
(7, 'Room 1', 140, 5),
(8, 'Room 1', 150, 6),
(9, 'Room 2', 150, 6);

-- --------------------------------------------------------

--
-- Struktura tabele `theaters`
--

CREATE TABLE `theaters` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Address` text NOT NULL,
  `FK_Place_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `theaters`
--

INSERT INTO `theaters` (`ID`, `Name`, `Address`, `FK_Place_Id`) VALUES
(1, 'Kino Name 1', 'Kino Address 1', 35),
(2, 'Kino Name 2', 'Kino Address 2', 38),
(3, 'Kino Name 3', 'Kino Address 3', 34),
(5, 'Kino Name 4', 'Kino Address 4', 44),
(6, 'Kino Name 5', 'Kino Address 5', 38);

-- --------------------------------------------------------

--
-- Struktura tabele `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `Name` text CHARACTER SET utf8 COLLATE utf8_slovenian_ci NOT NULL,
  `Surname` text CHARACTER SET utf8 COLLATE utf8_slovenian_ci NOT NULL,
  `Email` text NOT NULL,
  `TelephoneNumber` text NOT NULL,
  `RightsLevel` text NOT NULL,
  `PasswordHash` text NOT NULL,
  `JoinDate` datetime NOT NULL,
  `Birthday` datetime DEFAULT NULL,
  `Address` text,
  `FK_Place_Id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Odloži podatke za tabelo `users`
--

INSERT INTO `users` (`ID`, `Name`, `Surname`, `Email`, `TelephoneNumber`, `RightsLevel`, `PasswordHash`, `JoinDate`, `Birthday`, `Address`, `FK_Place_Id`) VALUES
(1, 'admin', 'admin', 'admin@admin.com', '123', '1', '202cb962ac59075b964b07152d234b70', '2019-04-29 10:21:49', '1996-09-23 00:00:00', '', NULL),
(7, 'Urban', 'Božic', 'urbanbozic1996@gmail.com', '1234', '5', '202cb962ac59075b964b07152d234b70', '2019-04-30 10:21:49', '2019-04-30 14:49:06', '', NULL),
(10, 'John', 'Doe', 'normalguy2@email.com', '12345', '5', '202cb962ac59075b964b07152d234b70', '2019-04-30 10:21:49', '1972-02-02 00:00:00', 'indirizzo di casa 10', 42),
(11, 'aa', 'aaa', 'aaa@gmail.com', '123321', '5', '202cb962ac59075b964b07152d234b70', '2019-06-06 08:25:37', '2019-06-06 00:00:00', 'Naslov 1', 35);

--
-- Indeksi zavrženih tabel
--

--
-- Indeksi tabele `bills`
--
ALTER TABLE `bills`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Discount_Id` (`FK_Discount_Id`);

--
-- Indeksi tabele `countries`
--
ALTER TABLE `countries`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksi tabele `discounts`
--
ALTER TABLE `discounts`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksi tabele `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Movie_Id` (`FK_Movie_Id`),
  ADD KEY `FK_Room_Id` (`FK_Room_Id`);

--
-- Indeksi tabele `genres`
--
ALTER TABLE `genres`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksi tabele `moviegenre`
--
ALTER TABLE `moviegenre`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Movie_Id` (`FK_Movie_Id`),
  ADD KEY `FK_Genre_Id` (`FK_Genre_Id`);

--
-- Indeksi tabele `movies`
--
ALTER TABLE `movies`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksi tabele `passwordreset`
--
ALTER TABLE `passwordreset`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_User_Id` (`FK_User_Id`);

--
-- Indeksi tabele `places`
--
ALTER TABLE `places`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Country_Id` (`FK_Country_Id`);

--
-- Indeksi tabele `reservations`
--
ALTER TABLE `reservations`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_User_Id` (`FK_User_Id`),
  ADD KEY `FK_Event_Id` (`FK_Event_Id`),
  ADD KEY `FK_Bill_Id` (`FK_Bill_Id`);

--
-- Indeksi tabele `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Theater_Id` (`FK_Theater_Id`);

--
-- Indeksi tabele `theaters`
--
ALTER TABLE `theaters`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Place_Id` (`FK_Place_Id`);

--
-- Indeksi tabele `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Place_Id` (`FK_Place_Id`);

--
-- AUTO_INCREMENT zavrženih tabel
--

--
-- AUTO_INCREMENT tabele `bills`
--
ALTER TABLE `bills`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT tabele `countries`
--
ALTER TABLE `countries`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT tabele `discounts`
--
ALTER TABLE `discounts`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT tabele `events`
--
ALTER TABLE `events`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT tabele `genres`
--
ALTER TABLE `genres`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT tabele `moviegenre`
--
ALTER TABLE `moviegenre`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT tabele `movies`
--
ALTER TABLE `movies`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT tabele `passwordreset`
--
ALTER TABLE `passwordreset`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=63;

--
-- AUTO_INCREMENT tabele `places`
--
ALTER TABLE `places`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT tabele `reservations`
--
ALTER TABLE `reservations`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=143;

--
-- AUTO_INCREMENT tabele `rooms`
--
ALTER TABLE `rooms`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT tabele `theaters`
--
ALTER TABLE `theaters`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT tabele `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Omejitve tabel za povzetek stanja
--

--
-- Omejitve za tabelo `bills`
--
ALTER TABLE `bills`
  ADD CONSTRAINT `FK181641FF6E28915` FOREIGN KEY (`FK_Discount_Id`) REFERENCES `discounts` (`ID`);

--
-- Omejitve za tabelo `events`
--
ALTER TABLE `events`
  ADD CONSTRAINT `FKA3707DF657D529F7` FOREIGN KEY (`FK_Room_Id`) REFERENCES `rooms` (`ID`),
  ADD CONSTRAINT `FKA3707DF65C4AB02D` FOREIGN KEY (`FK_Movie_Id`) REFERENCES `movies` (`ID`);

--
-- Omejitve za tabelo `moviegenre`
--
ALTER TABLE `moviegenre`
  ADD CONSTRAINT `FK61426FCA5C4AB02D` FOREIGN KEY (`FK_Movie_Id`) REFERENCES `movies` (`ID`),
  ADD CONSTRAINT `FK61426FCA813A51A4` FOREIGN KEY (`FK_Genre_Id`) REFERENCES `genres` (`ID`);

--
-- Omejitve za tabelo `passwordreset`
--
ALTER TABLE `passwordreset`
  ADD CONSTRAINT `FKC6BE6573BDC2065B` FOREIGN KEY (`FK_User_Id`) REFERENCES `users` (`ID`);

--
-- Omejitve za tabelo `places`
--
ALTER TABLE `places`
  ADD CONSTRAINT `FK5E8FADC6B69A7920` FOREIGN KEY (`FK_Country_Id`) REFERENCES `countries` (`ID`);

--
-- Omejitve za tabelo `reservations`
--
ALTER TABLE `reservations`
  ADD CONSTRAINT `FKB404305D61B8E19D` FOREIGN KEY (`FK_Event_Id`) REFERENCES `events` (`ID`),
  ADD CONSTRAINT `FKB404305D72C079D5` FOREIGN KEY (`FK_Bill_Id`) REFERENCES `bills` (`ID`),
  ADD CONSTRAINT `FKB404305DF0621ABD` FOREIGN KEY (`FK_User_Id`) REFERENCES `users` (`ID`);

--
-- Omejitve za tabelo `rooms`
--
ALTER TABLE `rooms`
  ADD CONSTRAINT `FK5D4CC4E0B8844FBD` FOREIGN KEY (`FK_Theater_Id`) REFERENCES `theaters` (`ID`);

--
-- Omejitve za tabelo `theaters`
--
ALTER TABLE `theaters`
  ADD CONSTRAINT `FK729E99AFB6491B01` FOREIGN KEY (`FK_Place_Id`) REFERENCES `places` (`ID`);

--
-- Omejitve za tabelo `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `FK2C1C7FE5B6491B01` FOREIGN KEY (`FK_Place_Id`) REFERENCES `places` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
