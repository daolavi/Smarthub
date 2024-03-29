 /** Insert Dummy Data for Testing **/

 delete from [dbo].[Connection]
 delete from [dbo].[UserChannel]
 delete from [dbo].[ChannelMagento]
 delete from [dbo].[ChannelEbay]
 delete from [dbo].[User]
 
 if (not exists(select * from [dbo].[User] where [dbo].[User].[Account] like 'daolavi@gmail.com'))
 begin
 insert into [dbo].[User]([Firstname],[LastName],[Account],[Password]) values('Dao','Lam','daolavi@gmail.com','smarthubdev123*')
 declare @userid int
 set @userid = SCOPE_IDENTITY()

 insert into [dbo].[ChannelEbay]([Token],[ExpiredDate],[CreatedDate],[Message],[LastSyncedDate_MemberMessage]) values ('AgAAAA**AQAAAA**aAAAAA**TLCEWQ**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AClYanCpGGpAWdj6x9nY+seQ**m9gDAA**AAMAAA**XN0ZEwY0oPxuK6hN3KdFbZZITAlGisZim8bbgPVCIm9I8GF06wPmSWDmx1OPBsYpJN3Mj28smufjNJJOmvtpiXFYuBNEdDMAvy6vJ2bjiCNp34MvzNaSJVwCYvDTTyY1dPgwMta3QkHEtG5uvONhuaFpP0SQAe8CyABVRWxM23gXCVgDwVNUDtMw9l4aRtUpC+9/P4pFRDBESCT4ClYjf4CaYPAS+n6IPVfPJ3r5tZZr1cjMTJpC9TtMxZAcTYRpz9D156Bu2KU5K3/yvFAw63RF3HmoGwH3Q4Wv/iPQHCukky5kLTjRkJW1EfYXgyIQGJ9eOch/EPdAQSqJDs/jHwjRLeyYz1BB/lTvqxmSQXzOUmUxDkD5uho/5ejD48+nicpJzz/DsW2erioUuLPvNs5Ntd87vSsedeqMi9SQwzIEfYEH7zSZIUZkfmGmlFJOXQ1ShX3YXmA/xbVoUb4siV3lP3FwAac3F/1gpxbK32g6VHSjq7VnqSh6B2uvT5MklTBqpn0tbYBEzeY7PLUd/PeHRgYDgoO2+ppFmFyi84DIGEBejXuk3QguJLUn2QXAV+afRpDdZ5GGAZs3MYJacwHIII3kFJ3xhGEpJk87aW2IkznLcBh9bAUyG+gKbrLsRwy8sqm5O31UmdU1cLZGv5Wc/nPzZ8JAU2U+gV8XwuzsJLPAnZH62OqE/0qVbNf3eq8Zj/Ss+Z1m4cUqxzq0NkYZF9wC8jrztiCfzPvZgzdXkLGui49cgJnsJZkaWC3I',
																														'2019-01-26 17:35:08.000',GETDATE(),null,null)
 declare @channelEbayId int
 set @channelEbayId = SCOPE_IDENTITY()

 insert into [dbo].[ChannelMagento]([CreatedDate],[Username],[Password],[Host],[StoreId],[LastSyncedDate_Ticket],[LastSyncedDate_Message],[Message]) 
	values (GETDATE(),'admin','admin123','http://test.rainstormstudio.com.au/maghelpdesk','1',null,null,null)
 declare @channelMagentoId int
 set @channelMagentoId = SCOPE_IDENTITY()

 insert into [dbo].[UserChannel]([UserId],[ChannelId],[ChannelType],[IsActive]) values(@userid,@channelEbayId,1,1)
 declare @userChannelEbayId int
 set @userChannelEbayId = SCOPE_IDENTITY()

 insert into [dbo].[UserChannel]([UserId],[ChannelId],[ChannelType],[IsActive]) values(@userid,@channelEbayId,0,1)
 declare @userChannelMagentoId int
 set @userChannelMagentoId = SCOPE_IDENTITY()

 insert into [dbo].[Connection]([UserChannelSource],[UserChannelTarget],[CreatedDate],[LastSyncedDate],[NextSyncedDate],[Status],[Counter],[Message]) 
	values(@userChannelEbayId,@userChannelMagentoId,GETDATE(),null,null,1,0,null)
end
