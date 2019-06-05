CREATE TABLE [Calculator].[NetPresentValues]
(
      [NetPresentValueId] INT NOT NULL IDENTITY(1, 1)
    , [Name] VARCHAR(128)
    , [InitialInvestment] FLOAT NOT NULL
    , [LowerBoundDiscountRate] FLOAT NOT NULL
    , [UpperBoundDiscountRate] FLOAT NOT NULL
    , [DiscountRateIncrement] FLOAT NOT NULL
    , [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
    , [LastUpdatedDate] DATETIME

    , CONSTRAINT [PK_NetPresentValues_NetPresentValueId]
      PRIMARY KEY NONCLUSTERED ([NetPresentValueId] ASC)
);
