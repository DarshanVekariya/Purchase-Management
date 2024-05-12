CREATE PROCEDURE AddToCart
    @ProductId INT,
    @UserId INT
AS
BEGIN
    -- Check if ProductId is null
    IF @ProductId IS NOT NULL and @ProductId != 0
    BEGIN
        -- Add the product to the cart
        INSERT INTO Cart (ProductId, UserId)
        VALUES (@ProductId, @UserId);

		SELECT P.Id, P.Name, P.Price
		FROM Products P
    END
	Else
	BEGIN
		SELECT P.Id, P.Name, P.Price
		FROM Products P
		INNER JOIN Cart C ON P.Id = C.ProductId
		WHERE C.UserId = @UserId;
	END
END
