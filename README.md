# ProjectSem3 NGO
## Build to start

	ctrl+ shift+ B to build project
	
## DB
 	
	Delete entity framework and add new orther

## ConnectionString
	-- Hoang ConnectionString :  
	<connectionStrings>
    <add name="ProjectSem3Entities" connectionString="metadata=res://*/EF.DataContext.csdl|res://*/EF.DataContext.ssdl|res://*/EF.DataContext.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-61G807S\SQLEXPRESS;initial catalog=ProjectSem3;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
	</connectionStrings>
	-- Thanh ConnectionString
	<connectionStrings>
    <add name="ProjectSem3Entities" connectionString="metadata=res://*/EF.DataContext.csdl|res://*/EF.DataContext.ssdl|res://*/EF.DataContext.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.;initial catalog=ProjectSem3;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  	</connectionStrings>
