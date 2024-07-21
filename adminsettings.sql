-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 17, 2024 at 09:48 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `vendo`
--

-- --------------------------------------------------------

--
-- Table structure for table `adminsettings`
--

CREATE TABLE `adminsettings` (
  `id` int(99) NOT NULL,
  `colored` int(99) NOT NULL,
  `grayscale` int(99) NOT NULL,
  `scan` int(99) NOT NULL,
  `longbondpaper` int(99) NOT NULL,
  `shortbondpaper` int(99) NOT NULL,
  `printerforlong` varchar(99) NOT NULL,
  `printerforshort` varchar(99) NOT NULL,
  `port` varchar(99) NOT NULL,
  `location` varchar(99) NOT NULL,
  `username` varchar(99) NOT NULL,
  `password` varchar(99) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `adminsettings`
--

INSERT INTO `adminsettings` (`id`, `colored`, `grayscale`, `scan`, `longbondpaper`, `shortbondpaper`, `printerforlong`, `printerforshort`, `port`, `location`, `username`, `password`) VALUES
(1, 5, 2, 5, 1, 1, 'HP Ink Tank 310 series', 'HP Ink Tank 310 series', 'COM3', '', 'admin', '12345');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
