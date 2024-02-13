
CREATE TABLE [dbo].[Product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[nameProduct] [nvarchar](255) NOT NULL,
	[shortDescriptionProduct] [nvarchar](500) NOT NULL,
	[categoryProduct] [nvarchar](100) NOT NULL,
	[productImage] [varbinary](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE #TempProducts (
    productId INT,
    nameProduct NVARCHAR(255),
    shortDescriptionProduct NVARCHAR(500),
    categoryProduct NVARCHAR(100),
    productImage VARBINARY(MAX)
);


DECLARE @Counter INT = 1;

WHILE @Counter <= 1000
BEGIN
    INSERT INTO #TempProducts
    VALUES (
        @Counter,
        'Producto' + CAST(@Counter AS NVARCHAR(10)),
        'Descripción corta del producto ' + CAST(@Counter AS NVARCHAR(10)),
        'Categoría' + CAST((@Counter % 10) + 1 AS NVARCHAR(10)),
        CAST(NEWID() AS VARBINARY(MAX))
    );

    SET @Counter = @Counter + 1;
END;

INSERT INTO dbo.Product (nameProduct, shortDescriptionProduct, categoryProduct, productImage)
SELECT nameProduct, shortDescriptionProduct, categoryProduct, productImage
FROM #TempProducts;


DROP TABLE #TempProducts;