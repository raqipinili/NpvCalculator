BEGIN TRY
    BEGIN TRANSACTION
        -----[ Create Temp Table ]-----
        SELECT * INTO #CalculatorTypes FROM [Lookup].[CalculatorTypes] WHERE 1 = 0;

        INSERT INTO #CalculatorTypes ([Name]) VALUES ('Net Present Value');
        INSERT INTO #CalculatorTypes ([Name]) VALUES ('Present Value');
        INSERT INTO #CalculatorTypes ([Name]) VALUES ('Future Value');

        -----[ Merge ]-----
        MERGE [Lookup].[CalculatorTypes] AS [orig]
        USING (SELECT * FROM #CalculatorTypes) AS [temp]
        ON ([orig].[CalculatorTypeId] = [temp].[CalculatorTypeId])
        WHEN MATCHED THEN
            UPDATE SET
                [orig].[Name] = [temp].[Name]
        WHEN NOT MATCHED THEN
            INSERT ([Name]) VALUES ([temp].[Name]);

        -----[ Drop Temp Table ]-----
        DROP TABLE #CalculatorTypes;

        COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	--IF (XACT_STATE()) = -1
    ROLLBACK TRANSACTION;

    IF EXISTS (SELECT TOP 1 1 FROM tempdb.sys.tables WHERE [name] LIKE '#CalculatorTypes%')
    BEGIN
        DROP TABLE #CalculatorTypes;
    END;

    -- RAISERROR(ERROR_MESSAGE(), ERROR_SEVERITY(), ERROR_STATE());
    PRINT CONCAT(
          'Lookup.CalculatorTypes'
        , ', Message: ', ERROR_MESSAGE()
        , ', Line: ', CAST(ERROR_LINE() AS VARCHAR(5))
        , ', Severity: ', CAST(ERROR_SEVERITY() AS VARCHAR(5))
        , ', Number: ', CAST(ERROR_NUMBER() AS VARCHAR(5))
        , ', State: ', CAST(ERROR_STATE() AS VARCHAR(5))
        , ', Procedure: ', ERROR_PROCEDURE());
END CATCH;
