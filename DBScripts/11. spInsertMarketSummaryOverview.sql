USE [PSXDataWarehouse]
GO
/****** Object:  StoredProcedure [dbo].[spInsertMarketSummaryOverview]    Script Date: 8/18/2020 4:30:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[spInsertMarketSummaryOverview]
		@DATE DATETIME,
		@STATUS varchar(300),
		@VOLUME float,
		@VALUE float,
		@TRADES float
		AS
		BEGIN
		TRUNCATE TABLE MarketSummaryOverview
INSERT INTO [dbo].[MarketSummaryOverview]
           ([MS_DATE]
           ,[MS_STATUS]
           ,[MS_VOLUME]
           ,[MS_VALUE]
           ,[MS_TRADES])
     VALUES
           (@DATE
           ,@STATUS
           ,@VOLUME
           ,@VALUE
           ,@TRADES)
END


