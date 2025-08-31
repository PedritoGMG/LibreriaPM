-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 14-03-2025 a las 12:03:57
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `libreriaapi`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteAutor` (IN `@id` INT)   DELETE FROM `autores` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteEdicion` (IN `@id` INT)   DELETE FROM `edicion` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteFormato` (IN `@id` INT)   DELETE FROM `formato` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteLibro` (IN `@isbn` VARCHAR(13))   BEGIN
DELETE FROM `libros` WHERE `isbn`=`@isbn`;
DELETE FROM `almacen` WHERE `libro`=`@isbn`;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteTema` (IN `@id` INT)   DELETE FROM `temas` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAutores` ()   SELECT * FROM autores$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getEdiciones` ()   SELECT * FROM edicion$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getFormatos` ()   SELECT * FROM formato$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLibroByISBN` (IN `@isbn` VARCHAR(13))   SELECT
T0.id, T0.nombre, T0.isbn,
T1.nombre as tema, T2.nombre as formato, T3.nombre as
autor, T4.nombre as edicion, T0.precio, T5.cantidad, T0.imgname
FROM 
`libros` T0 INNER JOIN `temas` T1 on T1.id = T0.tema 
INNER JOIN `formato` T2 ON T2.id = T0.formato 
INNER JOIN `autores` T3 ON T3.id = T0.autor 
INNER JOIN `edicion` T4 ON T4.id = T0.edicion 
INNER JOIN `almacen` T5 ON T0.isbn = T5.libro 
WHERE T0.isbn=`@isbn`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLibros` ()   SELECT
T0.id, T0.nombre, T0.isbn,
T1.nombre as tema, T2.nombre as formato, T3.nombre as
autor, T4.nombre as edicion, T0.precio, T5.cantidad,T0.imgname
FROM 
`libros` T0 INNER JOIN `temas` T1 on T1.id = T0.tema INNER JOIN `formato` T2 ON T2.id = T0.formato 
INNER JOIN `autores` T3 ON T3.id = T0.autor 
INNER JOIN `edicion` T4 ON T4.id = T0.edicion 
INNER JOIN `almacen` T5 ON T0.isbn = T5.libro 
WHERE 1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLibrosIDs` ()   SELECT * FROM libros$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getStock` ()   SELECT * FROM almacen$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getTemas` ()   SELECT * FROM temas$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `isAdmin` (IN `p_email` VARCHAR(255))   BEGIN
    DECLARE es_admin INT;

    -- Verifica si el email proporcionado pertenece a un administrador
    SELECT COUNT(*) INTO es_admin
    FROM administradores a
    JOIN userapp u ON u.id = a.id_usuario
    WHERE u.email = p_email;

    -- Si el conteo es mayor a 0, significa que el usuario es admin
    IF es_admin > 0 THEN
        -- Retorna 1 si es admin
        SELECT 1 AS resultado;
    ELSE
        -- Retorna 0 si no es admin
        SELECT 0 AS resultado;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `isUserRegister` (IN `email` VARCHAR(100), IN `psw` VARCHAR(500))   BEGIN
IF (SELECT COUNT(*) from userapp WHERE userapp.psw = psw and
userapp.email=email) THEN
SELECT * from userapp where userapp.psw=psw and
userapp.email=email;

ELSE

SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Usuario
no existe';

end if;
end$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postAutor` (IN `@id` INT(11), IN `@nombre` VARCHAR(50))   UPDATE `autores` SET `nombre`=`@nombre` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postBank` (IN `idEntrada` VARCHAR(9), IN `codigoEntrada` VARCHAR(16), IN `cvvEntrada` VARCHAR(3), IN `titularEntrada` VARCHAR(150))   INSERT INTO `datosbank` (`userId`, `cardNum`, `cvv`, `titular`) VALUES (idEntrada, codigoEntrada, cvvEntrada, titularEntrada)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postBankv2` (IN `@userId` VARCHAR(9), IN `@cardNum` VARCHAR(16), IN `@cvv` VARCHAR(3), IN `@titular` VARCHAR(150))   INSERT INTO `datosbank` ( `userId`, `cardNum`, `cvv`,
`titular`) VALUES (@userId,@cardNum,@cvv,@titular)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postEdicion` (IN `@id` INT(11), IN `@nombre` VARCHAR(50))   UPDATE `edicion` SET `nombre`=`@nombre` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postFormato` (IN `@id` INT(11), IN `@nombre` VARCHAR(50))   UPDATE `formato` SET `nombre`=`@nombre` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postLibro` (IN `@nombre` VARCHAR(250), IN `@autor` INT, IN `@tema` INT, IN `@precio` DOUBLE, IN `@edicion` INT, IN `@formato` INT, IN `@isbn` VARCHAR(13), IN `@imgname` VARCHAR(300))   UPDATE `libros` SET
`nombre`=`@nombre`,
`autor`=`@autor`,
`tema`=`@tema`,
`precio`=`@precio`,
`edicion`=`@edicion`,
`formato`=`@formato`,
`imgname`=`@imgname`
WHERE `isbn`=`@isbn`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postTema` (IN `@id` INT, IN `@nombre` VARCHAR(50))   UPDATE `temas` SET `nombre`=`@nombre` WHERE `id`=`@id`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `postUser` (IN `name` VARCHAR(50), IN `apellidos` VARCHAR(100), IN `cp` CHAR(5), IN `direccion` VARCHAR(100), IN `poblacion` VARCHAR(100), IN `dni` VARCHAR(9), IN `email` VARCHAR(100), IN `pw` VARCHAR(500))   INSERT INTO `userapp` (`nombre`, `apellidos`, `cp`,
`direccion`, `poblacion`, `dni`, `email`, `psw` ) VALUES
(name, apellidos, cp, direccion, poblacion, dni, email, pw)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `putAutor` (IN `nombre` VARCHAR(50))   INSERT INTO autores(`nombre`) VALUES (nombre)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `putEdicion` (IN `nombre` VARCHAR(50))   INSERT INTO edicion(`nombre`) VALUES (nombre)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `putFormato` (IN `nombre` VARCHAR(50))   INSERT INTO formato(`nombre`) VALUES (nombre)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `putLibro` (IN `@nombre` VARCHAR(250), IN `@autor` INT, IN `@tema` INT, IN `@precio` DOUBLE, IN `@edicion` INT, IN `@formato` INT, IN `@isbn` VARCHAR(13), IN `@cantidad` INT, IN `@imgname` VARCHAR(300))   BEGIN 
DECLARE libroISBN varchar(13) ; 
DECLARE cantidadLibros int; 
SET libroISBN =`@isbn`; 
SELECT cantidad into cantidadLibros from almacen WHERE almacen.libro=libroISBN; 
IF (SELECT count(*) from libros where libros.isbn =  `@isbn`) > 0 THEN 
UPDATE `almacen` SET almacen.`cantidad`=cantidadLibros+`@cantidad` WHERE   almacen.libro=libroISBN ; 
ELSE 
INSERT INTO `libros`( `nombre`, `autor`, `tema`, `precio`, `edicion`, `formato`, `isbn`,`imgname`) VALUES 
(`@nombre`,`@autor`,`@tema`,`@precio`,`@edicion`,`@formato`,`@isbn`,`@imgname`); 
INSERT INTO `almacen`(`libro`, `cantidad`) VALUES ( `@isbn`,`@cantidad`); 
end  if; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `putTema` (IN `nombre` VARCHAR(50))  NO SQL INSERT INTO temas(`nombre`) VALUES (nombre)$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `administradores`
--

CREATE TABLE `administradores` (
  `id_usuario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `almacen`
--

CREATE TABLE `almacen` (
  `libro` varchar(13) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cantidad` int(11) NOT NULL,
  `id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `autores`
--

CREATE TABLE `autores` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `datosbank`
--

CREATE TABLE `datosbank` (
  `id` int(11) NOT NULL,
  `userId` varchar(9) NOT NULL,
  `cardNum` varchar(16) NOT NULL,
  `cvv` varchar(3) NOT NULL,
  `titular` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


--
-- Estructura de tabla para la tabla `edicion`
--

CREATE TABLE `edicion` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `formato`
--

CREATE TABLE `formato` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `libros`
--

CREATE TABLE `libros` (
  `id` int(11) NOT NULL,
  `nombre` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `autor` int(11) NOT NULL,
  `tema` int(11) NOT NULL,
  `precio` double NOT NULL,
  `edicion` int(11) NOT NULL,
  `formato` int(11) NOT NULL,
  `isbn` varchar(13) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `imgname` varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `temas`
--

CREATE TABLE `temas` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `userapp`
--

CREATE TABLE `userapp` (
  `nombre` varchar(50) NOT NULL,
  `apellidos` varchar(100) NOT NULL,
  `cp` char(5) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `poblacion` varchar(100) NOT NULL,
  `dni` varchar(9) NOT NULL,
  `email` varchar(100) NOT NULL,
  `psw` varchar(500) NOT NULL,
  `id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `administradores`
--
ALTER TABLE `administradores`
  ADD PRIMARY KEY (`id_usuario`);

--
-- Indices de la tabla `almacen`
--
ALTER TABLE `almacen`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `libro` (`libro`);

--
-- Indices de la tabla `autores`
--
ALTER TABLE `autores`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- Indices de la tabla `datosbank`
--
ALTER TABLE `datosbank`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userId` (`userId`);

--
-- Indices de la tabla `edicion`
--
ALTER TABLE `edicion`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `formato`
--
ALTER TABLE `formato`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `libros`
--
ALTER TABLE `libros`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `isbn` (`isbn`);

--
-- Indices de la tabla `temas`
--
ALTER TABLE `temas`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- Indices de la tabla `userapp`
--
ALTER TABLE `userapp`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `dni` (`dni`,`email`) USING BTREE;

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `almacen`
--
ALTER TABLE `almacen`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `autores`
--
ALTER TABLE `autores`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `datosbank`
--
ALTER TABLE `datosbank`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `edicion`
--
ALTER TABLE `edicion`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `formato`
--
ALTER TABLE `formato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `libros`
--
ALTER TABLE `libros`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT de la tabla `temas`
--
ALTER TABLE `temas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `userapp`
--
ALTER TABLE `userapp`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `administradores`
--
ALTER TABLE `administradores`
  ADD CONSTRAINT `administradores_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `userapp` (`id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `datosbank`
--
ALTER TABLE `datosbank`
  ADD CONSTRAINT `datosbank_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `userapp` (`dni`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
