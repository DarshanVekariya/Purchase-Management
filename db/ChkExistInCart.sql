CREATE PROCEDURE ChkExistInCart
    @ProductId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Message NVARCHAR(MAX);
    DECLARE @isexist BIT;

    -- Check if the product exists in the user's cart
    IF EXISTS (SELECT 1 FROM Cart WHERE ProductId = @ProductId AND UserId = @UserId)
    BEGIN
        -- Product exists in the cart
        SET @Message = 'Product exists in the cart.';
        SET @isexist = 1; -- Product exists
    END
    ELSE
    BEGIN
        -- Product does not exist in the cart
        SET @Message = 'Product does not exist in the cart.';
        SET @isexist = 0; -- Product does not exist
    END

    -- Return the message and existence status
    SELECT @Message AS message, @isexist AS isexist;
END
