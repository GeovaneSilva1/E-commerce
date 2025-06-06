-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: havandb
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20250605005734_Inicial','8.0.13');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CNPJ` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RazaoSocial` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (2,'78.895.299/0001-87','Filipe e Leonardo Advocacia Ltda'),(3,'19.285.247/0001-20','Carla e Thomas Pizzaria ME'),(4,'54.955.338/0001-26','Edson e Francisca Construções ME'),(5,'66.969.379/0001-50','Thiago e Pietro Consultoria Financeira Ltda');
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `condicaopagamentos`
--

DROP TABLE IF EXISTS `condicaopagamentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `condicaopagamentos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Dias` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `condicaopagamentos`
--

LOCK TABLES `condicaopagamentos` WRITE;
/*!40000 ALTER TABLE `condicaopagamentos` DISABLE KEYS */;
INSERT INTO `condicaopagamentos` VALUES (1,'boleto','30'),(2,'cartao','60'),(4,'debito','10'),(5,'cartao','30'),(6,'avista','10'),(7,'prazo','120');
/*!40000 ALTER TABLE `condicaopagamentos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notificacoes`
--

DROP TABLE IF EXISTS `notificacoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notificacoes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ClienteId` int NOT NULL,
  `ProdutoId` int NOT NULL,
  `Mensagem` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DataEnvio` datetime(6) NOT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Notificacoes_ClienteId` (`ClienteId`),
  KEY `IX_Notificacoes_ProdutoId` (`ProdutoId`),
  CONSTRAINT `FK_Notificacoes_clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `clientes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Notificacoes_produtos_ProdutoId` FOREIGN KEY (`ProdutoId`) REFERENCES `produtos` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notificacoes`
--

LOCK TABLES `notificacoes` WRITE;
/*!40000 ALTER TABLE `notificacoes` DISABLE KEYS */;
/*!40000 ALTER TABLE `notificacoes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `precoprodutoclientes`
--

DROP TABLE IF EXISTS `precoprodutoclientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `precoprodutoclientes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProdutoId` int NOT NULL,
  `ClienteId` int NOT NULL,
  `TabelaPrecoId` int NOT NULL,
  `Valor` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_PrecoProdutoClientes_ClienteId` (`ClienteId`),
  KEY `IX_PrecoProdutoClientes_ProdutoId` (`ProdutoId`),
  KEY `IX_PrecoProdutoClientes_TabelaPrecoId` (`TabelaPrecoId`),
  CONSTRAINT `FK_PrecoProdutoClientes_clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `clientes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PrecoProdutoClientes_produtos_ProdutoId` FOREIGN KEY (`ProdutoId`) REFERENCES `produtos` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PrecoProdutoClientes_TabelaPrecos_TabelaPrecoId` FOREIGN KEY (`TabelaPrecoId`) REFERENCES `tabelaprecos` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `precoprodutoclientes`
--

LOCK TABLES `precoprodutoclientes` WRITE;
/*!40000 ALTER TABLE `precoprodutoclientes` DISABLE KEYS */;
/*!40000 ALTER TABLE `precoprodutoclientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtos`
--

DROP TABLE IF EXISTS `produtos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produtos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SKU` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Descricao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Preco` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtos`
--

LOCK TABLES `produtos` WRITE;
/*!40000 ALTER TABLE `produtos` DISABLE KEYS */;
INSERT INTO `produtos` VALUES (1,'CAM-AZ-P','camisa azul pequena',59.900000000000000000000000000000),(2,'CAL-PR-M','calça preta media',99.900000000000000000000000000000),(3,'BON-BE-G','bone berge grande',32.000000000000000000000000000000),(4,'MOL-PR-G','moletom preto g',290.350000000000000000000000000000);
/*!40000 ALTER TABLE `produtos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabelaprecos`
--

DROP TABLE IF EXISTS `tabelaprecos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabelaprecos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DataInicio` datetime(6) NOT NULL,
  `DataFim` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabelaprecos`
--

LOCK TABLES `tabelaprecos` WRITE;
/*!40000 ALTER TABLE `tabelaprecos` DISABLE KEYS */;
/*!40000 ALTER TABLE `tabelaprecos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendaitens`
--

DROP TABLE IF EXISTS `vendaitens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendaitens` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `VendaId` int NOT NULL,
  `ProdutoId` int NOT NULL,
  `Quantidade` int NOT NULL,
  `ValorUnitario` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_VendaItens_ProdutoId` (`ProdutoId`),
  KEY `IX_VendaItens_VendaId` (`VendaId`),
  CONSTRAINT `FK_VendaItens_produtos_ProdutoId` FOREIGN KEY (`ProdutoId`) REFERENCES `produtos` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_VendaItens_Vendas_VendaId` FOREIGN KEY (`VendaId`) REFERENCES `vendas` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendaitens`
--

LOCK TABLES `vendaitens` WRITE;
/*!40000 ALTER TABLE `vendaitens` DISABLE KEYS */;
INSERT INTO `vendaitens` VALUES (1,4,3,2,64.000000000000000000000000000000),(2,4,2,1,99.900000000000000000000000000000),(3,5,4,1,290.350000000000000000000000000000),(4,6,3,2,64.000000000000000000000000000000),(5,6,2,3,299.700000000000000000000000000000),(6,6,1,1,59.900000000000000000000000000000);
/*!40000 ALTER TABLE `vendaitens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendas`
--

DROP TABLE IF EXISTS `vendas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendas` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ClienteId` int NOT NULL,
  `Data` datetime(6) NOT NULL,
  `CondicaoPagamentoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Vendas_ClienteId` (`ClienteId`),
  KEY `IX_Vendas_CondicaoPagamentoId` (`CondicaoPagamentoId`),
  CONSTRAINT `FK_Vendas_clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `clientes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Vendas_condicaopagamentos_CondicaoPagamentoId` FOREIGN KEY (`CondicaoPagamentoId`) REFERENCES `condicaopagamentos` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendas`
--

LOCK TABLES `vendas` WRITE;
/*!40000 ALTER TABLE `vendas` DISABLE KEYS */;
INSERT INTO `vendas` VALUES (4,2,'2025-06-05 14:17:31.143098',2),(5,4,'2025-06-05 18:39:40.024133',7),(6,4,'2025-06-05 20:34:07.065701',5);
/*!40000 ALTER TABLE `vendas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'havandb'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-06 12:11:04
