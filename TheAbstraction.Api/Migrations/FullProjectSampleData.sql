-- Seeds SQL Server data for Identity, catalog, orders, and order statuses.
-- Demo login: demo-admin@example.test / Demo123!
-- All records are marked DEMO and stable IDs/names make this safe to rerun.
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Now datetime2(7) = SYSUTCDATETIME();
    DECLARE @PasswordHash nvarchar(max) = N'AQAAAAIAAYagAAAAEP73s9eWzBeeUXv6IWRg2OjKFgWo+YgnjxVI1h38fEvESpSCkw7RUNllFz37cyCbGg==';
    DECLARE @Numbers TABLE (SequenceNumber int NOT NULL PRIMARY KEY);
    DECLARE @Samples TABLE
    (
        SequenceNumber int NOT NULL PRIMARY KEY,
        ProductName nvarchar(200) NOT NULL,
        ProductDescription nvarchar(1000) NOT NULL
    );

    ;WITH Numbers AS
    (
        SELECT 1 AS SequenceNumber
        UNION ALL
        SELECT SequenceNumber + 1 FROM Numbers WHERE SequenceNumber < 100
    )
    INSERT INTO @Numbers (SequenceNumber)
    SELECT SequenceNumber FROM Numbers OPTION (MAXRECURSION 100);

    INSERT INTO @Samples (SequenceNumber, ProductName, ProductDescription)
    SELECT SequenceNumber,
        CONCAT(N'DEMO-Product-', RIGHT(CONCAT(N'000', SequenceNumber), 3)),
        CONCAT(N'Dữ liệu mẫu sản phẩm số ', SequenceNumber, N' dùng để thử nghiệm quản lý sản phẩm.')
    FROM @Numbers;

    -- Identity roles.
    IF NOT EXISTS (SELECT 1 FROM dbo.AspNetRoles WHERE NormalizedName = N'ADMIN')
        INSERT INTO dbo.AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
        VALUES (N'DEMO-ROLE-ADMIN', N'admin', N'ADMIN', CONVERT(nvarchar(36), NEWID()));
    IF NOT EXISTS (SELECT 1 FROM dbo.AspNetRoles WHERE NormalizedName = N'USER')
        INSERT INTO dbo.AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
        VALUES (N'DEMO-ROLE-USER', N'user', N'USER', CONVERT(nvarchar(36), NEWID()));

    -- One administrator and 100 sign-in capable sample users. Password for every demo account: Demo123!
    IF NOT EXISTS (SELECT 1 FROM dbo.AspNetUsers WHERE NormalizedUserName = N'DEMO-ADMIN')
        INSERT INTO dbo.AspNetUsers
            (Id, FullName, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed,
             PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed,
             TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
        VALUES
            (N'FFFFFFFFFFFFFFFFFFFFFFFF', N'Demo Administrator', N'demo-admin', N'DEMO-ADMIN',
             N'demo-admin@example.test', N'DEMO-ADMIN@EXAMPLE.TEST', 1, @PasswordHash,
             CONVERT(nvarchar(36), NEWID()), CONVERT(nvarchar(36), NEWID()), NULL, 0, 0, NULL, 1, 0);

    INSERT INTO dbo.AspNetUsers
        (Id, FullName, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed,
         PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed,
         TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
    SELECT
        CONCAT(N'000000000000000000000', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)),
        CONCAT(N'Demo User ', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)),
        CONCAT(N'demo-user-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)),
        UPPER(CONCAT(N'demo-user-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))),
        CONCAT(N'demo-user-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3), N'@example.test'),
        UPPER(CONCAT(N'demo-user-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3), N'@example.test')),
        1, @PasswordHash, CONVERT(nvarchar(36), NEWID()), CONVERT(nvarchar(36), NEWID()),
        NULL, 0, 0, NULL, 1, 0
    FROM @Numbers AS n
    WHERE NOT EXISTS
    (
        SELECT 1 FROM dbo.AspNetUsers AS u
        WHERE u.NormalizedUserName = UPPER(CONCAT(N'demo-user-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)))
    );

    DECLARE @AdminRoleId nvarchar(450) = (SELECT TOP (1) Id FROM dbo.AspNetRoles WHERE NormalizedName = N'ADMIN');
    DECLARE @UserRoleId nvarchar(450) = (SELECT TOP (1) Id FROM dbo.AspNetRoles WHERE NormalizedName = N'USER');

    INSERT INTO dbo.AspNetUserRoles (UserId, RoleId)
    SELECT u.Id, @AdminRoleId FROM dbo.AspNetUsers AS u
    WHERE u.NormalizedUserName = N'DEMO-ADMIN'
      AND NOT EXISTS (SELECT 1 FROM dbo.AspNetUserRoles ur WHERE ur.UserId = u.Id AND ur.RoleId = @AdminRoleId);

    INSERT INTO dbo.AspNetUserRoles (UserId, RoleId)
    SELECT u.Id, @UserRoleId FROM dbo.AspNetUsers AS u
    WHERE u.NormalizedUserName LIKE N'DEMO-USER-%'
      AND NOT EXISTS (SELECT 1 FROM dbo.AspNetUserRoles ur WHERE ur.UserId = u.Id AND ur.RoleId = @UserRoleId);

    -- Products and two variants per product.
    INSERT INTO dbo.Products (Id, Name, Description, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-PRODUCT-ID-', RIGHT(CONCAT(N'000', s.SequenceNumber), 3)),
        s.ProductName, s.ProductDescription, @Now, @Now
    FROM @Samples AS s
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Products p WHERE p.Name = s.ProductName);

    INSERT INTO dbo.ProductVariants
        (Id, ProductId, Price, Quantity, Model, Color, Size, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-VARIANT-A-', RIGHT(CONCAT(N'000', s.SequenceNumber), 3)),
        p.Id, CONVERT(decimal(18,2), 99000 + s.SequenceNumber * 1000),
        10 + s.SequenceNumber % 90, N'Sample-Model-A', N'Đen', N'M', @Now, @Now
    FROM @Samples AS s
    INNER JOIN dbo.Products AS p ON p.Name = s.ProductName
    WHERE NOT EXISTS
        (SELECT 1 FROM dbo.ProductVariants v WHERE v.ProductId = p.Id AND v.Model = N'Sample-Model-A');

    INSERT INTO dbo.ProductVariants
        (Id, ProductId, Price, Quantity, Model, Color, Size, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-VARIANT-B-', RIGHT(CONCAT(N'000', s.SequenceNumber), 3)),
        p.Id, CONVERT(decimal(18,2), 149000 + s.SequenceNumber * 1500),
        5 + s.SequenceNumber % 60, N'Sample-Model-B', N'Trắng', N'L', @Now, @Now
    FROM @Samples AS s
    INNER JOIN dbo.Products AS p ON p.Name = s.ProductName
    WHERE NOT EXISTS
        (SELECT 1 FROM dbo.ProductVariants v WHERE v.ProductId = p.Id AND v.Model = N'Sample-Model-B');

    -- Order status lookup records.
    INSERT INTO dbo.Status (Id, OrderStatus, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-STATUS-', s.StatusName), s.StatusName, @Now, @Now
    FROM (VALUES (N'Pending'), (N'Paid'), (N'Shipped'), (N'Completed')) AS s(StatusName)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Status st WHERE st.OrderStatus = s.StatusName);

    -- The Order entity has an OrderStatus property, while the original SQL snapshot omitted this column.
    IF COL_LENGTH(N'dbo.Orders', N'OrderStatus') IS NULL
        ALTER TABLE dbo.Orders ADD OrderStatus nvarchar(50) NOT NULL
            CONSTRAINT DF_Orders_OrderStatus DEFAULT ('Pending') WITH VALUES;

    -- One order per sample user. Each order contains both variants of its matching demo product.
    INSERT INTO dbo.Orders (Id, UserId, TotalPrice, OrderStatus, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-ORDER-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)),
        u.Id, variantA.Price + variantB.Price,
        CASE n.SequenceNumber % 4
            WHEN 0 THEN 'Pending'
            WHEN 1 THEN 'Paid'
            WHEN 2 THEN 'Shipped'
            ELSE 'Completed'
        END,
        @Now, @Now
    FROM @Numbers AS n
    INNER JOIN dbo.AspNetUsers AS u
        ON u.NormalizedUserName = UPPER(CONCAT(N'demo-user-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)))
    INNER JOIN dbo.Products AS p
        ON p.Name = CONCAT(N'DEMO-Product-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))
    INNER JOIN dbo.ProductVariants AS variantA ON variantA.ProductId = p.Id AND variantA.Model = N'Sample-Model-A'
    INNER JOIN dbo.ProductVariants AS variantB ON variantB.ProductId = p.Id AND variantB.Model = N'Sample-Model-B'
    WHERE NOT EXISTS
        (SELECT 1 FROM dbo.Orders o WHERE o.Id = CONCAT(N'DEMO-ORDER-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)));

    INSERT INTO dbo.OrderDetails (Id, OrderId, ProductVariantId, Quantity, Price, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-DETAIL-A-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)),
        o.Id, v.Id, 1, v.Price, @Now, @Now
    FROM @Numbers AS n
    INNER JOIN dbo.Orders AS o ON o.Id = CONCAT(N'DEMO-ORDER-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))
    INNER JOIN dbo.Products AS p ON p.Name = CONCAT(N'DEMO-Product-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))
    INNER JOIN dbo.ProductVariants AS v ON v.ProductId = p.Id AND v.Model = N'Sample-Model-A'
    WHERE NOT EXISTS (SELECT 1 FROM dbo.OrderDetails d WHERE d.OrderId = o.Id AND d.ProductVariantId = v.Id);

    INSERT INTO dbo.OrderDetails (Id, OrderId, ProductVariantId, Quantity, Price, CreatedDate, ModifiedDate)
    SELECT CONCAT(N'DEMO-DETAIL-B-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3)),
        o.Id, v.Id, 1, v.Price, @Now, @Now
    FROM @Numbers AS n
    INNER JOIN dbo.Orders AS o ON o.Id = CONCAT(N'DEMO-ORDER-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))
    INNER JOIN dbo.Products AS p ON p.Name = CONCAT(N'DEMO-Product-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))
    INNER JOIN dbo.ProductVariants AS v ON v.ProductId = p.Id AND v.Model = N'Sample-Model-B'
    WHERE NOT EXISTS (SELECT 1 FROM dbo.OrderDetails d WHERE d.OrderId = o.Id AND d.ProductVariantId = v.Id);

    INSERT INTO dbo.OrderStatus (OrderId, StatusId, CreatedDate, ModifiedDate)
    SELECT o.Id, st.Id, @Now, @Now
    FROM @Numbers AS n
    INNER JOIN dbo.Orders AS o ON o.Id = CONCAT(N'DEMO-ORDER-', RIGHT(CONCAT(N'000', n.SequenceNumber), 3))
    INNER JOIN dbo.Status AS st ON st.OrderStatus =
        CASE n.SequenceNumber % 4
            WHEN 0 THEN N'Pending'
            WHEN 1 THEN N'Paid'
            WHEN 2 THEN N'Shipped'
            ELSE N'Completed'
        END
    WHERE NOT EXISTS (SELECT 1 FROM dbo.OrderStatus os WHERE os.OrderId = o.Id AND os.StatusId = st.Id);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

SELECT
    (SELECT COUNT(*) FROM dbo.AspNetUsers WHERE NormalizedUserName LIKE N'DEMO-%') AS DemoUsers,
    (SELECT COUNT(*) FROM dbo.Products WHERE Name LIKE N'DEMO-Product-%') AS DemoProducts,
    (SELECT COUNT(*) FROM dbo.ProductVariants v INNER JOIN dbo.Products p ON p.Id = v.ProductId WHERE p.Name LIKE N'DEMO-Product-%') AS DemoVariants,
    (SELECT COUNT(*) FROM dbo.Orders WHERE Id LIKE N'DEMO-ORDER-%') AS DemoOrders,
    (SELECT COUNT(*) FROM dbo.OrderDetails WHERE Id LIKE N'DEMO-DETAIL-%') AS DemoOrderDetails;
