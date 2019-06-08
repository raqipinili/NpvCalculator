BEGIN TRY
    BEGIN TRANSACTION
        -----[ Create Temp Table ]-----
        SELECT * INTO #Permissions FROM [Security].[Permissions] WHERE 1 = 0;

        INSERT INTO #Permissions ([Name], [Description]) VALUES ('NetPresentValue', 'Use Net Present Value Calculator');
        INSERT INTO #Permissions ([Name], [Description]) VALUES ('PresentValue', 'Use Present Value Calculator');
        INSERT INTO #Permissions ([Name], [Description]) VALUES ('FutureValue', 'Use Future Value Calculator');

        -----[ Merge ]-----
        MERGE [Security].[Permissions] AS [orig]
        USING (SELECT * FROM #Permissions) AS [temp]
        ON ([orig].[PermissionId] = [temp].[PermissionId])
        WHEN MATCHED THEN
            UPDATE SET
                  [orig].[Name] = [temp].[Name]
                , [orig].[Description] = [temp].[Description]
        WHEN NOT MATCHED THEN
            INSERT ([Name], [Description])
            VALUES ([temp].[Name], [temp].[Description]);

        -----[ Drop Temp Table ]-----
        DROP TABLE #Permissions;

        COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	--IF (XACT_STATE()) = -1
    ROLLBACK TRANSACTION;

    IF EXISTS (SELECT TOP 1 1 FROM tempdb.sys.tables WHERE [name] LIKE '#Permissions%')
    BEGIN
        DROP TABLE #Permissions;
    END;

	-- RAISERROR(ERROR_MESSAGE(), ERROR_SEVERITY(), ERROR_STATE());
    PRINT CONCAT(
          'Security.Permissions'
        , ', Message: ', ERROR_MESSAGE()
        , ', Line: ', CAST(ERROR_LINE() AS VARCHAR(5))
        , ', Severity: ', CAST(ERROR_SEVERITY() AS VARCHAR(5))
        , ', Number: ', CAST(ERROR_NUMBER() AS VARCHAR(5))
        , ', State: ', CAST(ERROR_STATE() AS VARCHAR(5))
        , ', Procedure: ', ERROR_PROCEDURE());
END CATCH;
