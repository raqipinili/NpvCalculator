CREATE TABLE [Calculator].[NetPresentValues]
(
      [NetPresentValueId] INT NOT NULL IDENTITY(1, 1)
    , [Name] VARCHAR(128)
    , [InitialInvestment] DECIMAL(19, 4) NOT NULL
    , [LowerBoundDiscountRate] DECIMAL(13, 4) NOT NULL
    , [UpperBoundDiscountRate] DECIMAL(13, 4) NOT NULL
    , [DiscountRateIncrement] DECIMAL(13, 4) NOT NULL
    , [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
    , [LastUpdatedDate] DATETIME

    , CONSTRAINT [PK_NetPresentValues_NetPresentValueId]
      PRIMARY KEY NONCLUSTERED ([NetPresentValueId] ASC)
);
