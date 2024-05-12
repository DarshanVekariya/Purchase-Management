CREATE PROCEDURE RemoveToCart
    @ProductId INT,
    @UserId INT
AS
BEGIN
    DELETE FROM Cart
    WHERE ProductId = @ProductId
      AND UserId = @UserId;

    SELECT Id, UserId, ProductId
    FROM Cart
    WHERE UserId = @UserId;
END
