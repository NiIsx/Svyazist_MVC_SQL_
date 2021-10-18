USE [Svyazist_MVC_SQL]
GO

/****** Object: Table [dbo].[SalesStatistics] Script Date: 17.10.2021 16:42:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SalesStatistics] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [SaleDate]        DATE         NOT NULL,
    [CumulativeSales] INT          NOT NULL,
    [CumulativeCost]  DECIMAL (18) NOT NULL
);

GO 
DELETE FROM SalesStatistics


DECLARE  
--	@tempTableCount INT,
--    @minPrice MONEY, 
--    @dif MONEY, 
    @count INT


--SET @tempTable = (SELECT ProductId FROM Sales GROUP BY SaleDate)

--SET @tempTableCount = (SELECT COUNT(ProductId) FROM @tempTable)

--INSERT INTO SalesStatistics SELECT * FROM @tempTableCount 

CREATE TABLE #tempTable
(id INT IDENTITY (1, 1), SaleDate DATE, DaySales INT)
SET @count = (SELECT COUNT(*) FROM @tempTable)


INSERT INTO #tempTable
SELECT 
        SaleDate, 
        DaySales = COUNT(*)
        --CumulativeSales = (SELECT COUNT(*) FROM Sales WHERE Sales.SaleDate = tempSaleDate),  --(SELECT Cost FROM Products WHERE Products.Id = ProductId GROUP BY ProductId),
        --CumulativeCost = CumulativeSales * SELECT Cost FROM Products WHERE SaleDate = tempSaleDate)
FROM Sales 
GROUP BY SaleDate



WHILE @number > 0
    BEGIN
        INSERT INTO SalesStatistics
        SELECT 
                SaleDate, 
                CumulativeSale =    (
                                        SELECT COUNT(*) FROM #tempTable WHERE Sales.SaleDate = (SELECT COUNT(*) FROM Products WHERE Sales.SaleDate = tempSaleDate)
                                    ),
                CumulativeCost = 0
        FROM #tempTable 
GROUP BY SaleDate


        SET @number = @number + 1
    END;
