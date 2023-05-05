Pour lancer l'application Web :

1- Copier les fichiers de bases de données (GSB.mdf et GSB_log.ldf) sur (Microsoft SQL ServerMSSQL/MSSQL/DATA/).

2- Ouvrir SQL Server Management Studio puis lier la database en faisant clic droit sur Databases.

3- Ouvrir la solution sur Visual Studio.

4 - Aller dans l'explorateur de solutions et ouvrez le fichier Web.config

5- Modifier le fichier en mettant le nom de votre serveur dans le script suivant :

	<connectionStrings>
		<add
		  name="GSB"
		  providerName="System.Data.SqlClien"
		  connectionString="Data Source=NomDuServeur;Initial Catalog=GSB;Integrated Security=True;Uid=auth_windows;" />
	</connectionStrings>
	
PS: Le nom du serveur peut être obtenu sur SQL Server Management Studio en faisant un clic droit sur le serveur puis en allant dans Propriétés

6- Les identifiants admin pour se connecter à l'application sont les suivants :
		username : admin
		password : admin
	Pour un compte Pharmacien :
		username : Pharmacien
		password :
	Pour un compte Médecin :
		username : Médecin
		password :
	Pour un compte Patient :
		username : Patient
		password :  
