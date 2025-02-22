USE [PurchaseManagementDB]
GO
/****** Object:  StoredProcedure [dbo].[GenerateOrderList]    Script Date: 5/4/2024 6:05:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GenerateOrderList]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Your SQL query to retrieve purchase order details based on UserId
    SELECT 
        p.Id AS ProductId,
        p.Name AS ProductName,
        p.Price AS ProductPrice,
        oi.Quantity,
        o.OrderDate AS OrderDate  -- Convert date to dd/MM/yyyy format
    FROM 
        Orders o
    INNER JOIN 
        OrderItems oi ON oi.OrderId = o.Id
    INNER JOIN 
        Products p ON p.Id = oi.ProductId
    WHERE 
        o.UserId = @UserId;
END
