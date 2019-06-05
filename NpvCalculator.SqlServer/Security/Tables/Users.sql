CREATE TABLE [Security].[Users]
(
      [UserId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()
	, [FirstName] VARCHAR(128) NOT NULL
	, [LastName] VARCHAR(128) MASKED WITH (FUNCTION='partial(2, "----", 0)') NOT NULL
	, [UserName] VARCHAR(128) MASKED WITH (FUNCTION='default()') NOT NULL
    , [EmailAddress] VARCHAR(128) MASKED WITH (FUNCTION='email()') NULL
    , [PasswordHash] VARBINARY(260) NOT NULL
	, [PasswordSalt] VARBINARY(260) NOT NULL

    , [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
    , [LastUpdatedDate] DATETIME

    , CONSTRAINT [PK_Users_UserId]
      PRIMARY KEY NONCLUSTERED ([UserId] ASC)

    , CONSTRAINT [UK_Users_UserName]
      UNIQUE ([UserName])
);
