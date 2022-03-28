PROJECT SETUP

1. SQL Server - Run script "InitialScript.sql" from SolutionItems folder.
2. Set multiple startup projects to run both of them.
3. SentimentAnalyserAPI - set connection string in appsettings.json
4. SentimentAnalyser/Services/HttpHelper.cs (Line 127) set port to point API project.