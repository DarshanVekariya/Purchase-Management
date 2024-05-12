create PROCEDURE GenerateOrderAndItems
    @UserId INT,
    @OrderItems OrderItemType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OrderId INT;

    INSERT INTO Orders (UserId, OrderDate)
    VALUES (@UserId, GETDATE());

    SET @OrderId = SCOPE_IDENTITY();

    INSERT INTO OrderItems (OrderId, ProductId, Quantity)
    SELECT @OrderId, ProductId, Quantity
    FROM @OrderItems;

    DELETE FROM Cart WHERE UserId = @UserId;

    SELECT p.Id, p.Name, p.Price, oi.Quantity 
    FROM Orders o
    INNER JOIN OrderItems oi ON oi.OrderId = o.Id
    INNER JOIN Products p ON p.Id = oi.ProductId
    WHERE UserId = @UserId;
END
