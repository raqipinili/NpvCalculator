CREATE TABLE [Lookup].[CalculatorTypes]
(
      [CalculatorTypeId] INT NOT NULL IDENTITY(1, 1)
    , [Name] VARCHAR(128)

    , CONSTRAINT [PK_CalculatorTypes_CalculatorTypeId]
      PRIMARY KEY NONCLUSTERED ([CalculatorTypeId] ASC)
)
