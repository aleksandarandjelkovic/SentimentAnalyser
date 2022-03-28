--FIRST--
CREATE DATABASE SentimentAnalyser;

--SECOND--
CREATE TABLE [SentimentAnalyser].[dbo].[Lexicon](
	[WordId] [int] IDENTITY(1,1) NOT NULL,
	[WordDesc] [nvarchar](50) NOT NULL,
	[SentimentScore] [float] NOT NULL,
 CONSTRAINT [PK_Lexicon] PRIMARY KEY CLUSTERED 
(
	[WordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--THIRD--
INSERT INTO [SentimentAnalyser].[dbo].[Lexicon]
           ([WordDesc]
           ,[SentimentScore])
     VALUES
           ('nice', 0.4),
           ('excellent', 0.8),
           ('modest', 0),
           ('horrible', -0.8),
           ('ugly', -0.5)
