CREATE TABLE [Calculator].[PeriodAmounts]
(
      [PeriodAmountId] INT NOT NULL IDENTITY(1, 1)
    , [NetPresentValueId] INT NOT NULL
    , [Amount] FLOAT NOT NULL
    , [Period] INT NOT NULL

    , CONSTRAINT [PK_PeriodAmounts_PeriodAmountId]
      PRIMARY KEY NONCLUSTERED ([PeriodAmountId] ASC)

    , CONSTRAINT [FK_PeriodAmounts_NetPresentValueId]
      FOREIGN KEY ([NetPresentValueId])
      REFERENCES [Calculator].[NetPresentValues] ([NetPresentValueId])
      ON UPDATE CASCADE
      ON DELETE CASCADE
);
