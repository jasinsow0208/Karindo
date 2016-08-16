CREATE PROCEDURE [dbo].[spOpeningBalancePosting]
	AS

	declare @Jml int
set @Jml= (select count(*) from [dbo].[KasBanks])
RETURN @Jml
