## Alternative to dummy classes for this project

Connect to a SQL database and generate the models using EF Core tools.
 - Install-Package Microsoft.EntityFrameworkCore -Version 8.0.1
 - Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.1
 - Run Scaffold-DbContext command https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell#scaffold-dbcontext


## Connect to SQL using managed identities

    1. Since there are multiple services that need to access the database, create roles and grant only needed permssion to each role.

	2. Check if the user exists in the database and create it if it does not exist.

	-- Create user from external provider
	IF EXISTS ( SELECT [name] FROM sys.database_principals WHERE [name] = 'FILL IN MS Entra user principal id for web app')  
	BEGIN  
		Print 'User already exists!';
	END  
	ELSE
	BEGIN  
		Print 'User will be created';
    
		-- Creates the user in the database
		CREATE USER [FILL IN] FROM EXTERNAL PROVIDER;
	
		-- Adds the user to needed roles
		ALTER ROLE [ReaderRole] ADD MEMBER [FILL IN]; 
		ALTER ROLE [WriterRole] ADD MEMBER [FILL IN]; 
	END

	3. Connect to the database using the managed identity. 
		var sqlConnectionString = $"data source={sqlServerName}.database.windows.net;initial catalog={sqlDatabaseName};" +
                                   "ApplicationIntent=ReadOnly;" + // in case replication is used, add this to connect to the read only replica
                                   "Authentication=Active Directory Default;" +