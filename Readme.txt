How to run the web application:

1- Copy the database files GSB(GSB.mdf and GSB_log.ldf) to (Microsoft SQL ServerMSSQL/MSSQL/DATA/).

2- Open SQL Server Management Studio then attach the database Library by clicking on button right of the mouse on Databases.

3- In Visual Studio import the solution.

4 - Go to Solution Explorer and open the file Web.config

5- Modify the file by the name of your server in the following script:

	<connectionStrings>
		<add
		  name="GSB"
		  providerName="System.Data.SqlClien"
		  connectionString="Data Source=NameOfyourServer;Initial Catalog=GSB;Integrated Security=True;Uid=auth_windows;" />
	</connectionStrings>
	
PS: The name of the server can be gotten in the SQL Server Management Studio by clicking on button right of the mouse on the server 
	then click on properties and copy the name then replace the 'NameOfyourServer' by the copied name.

6- The username and password of an admin account: these accounts have no password just enter the username 
		username : admin
		password : 
	And for a pharmacien account:
		username : Thomas
		password :
	And for a medecin account:
		username : AFlorence
		password :
	And for a patient account:
		username : mallou
		password :  