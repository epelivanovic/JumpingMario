USE [Igrica]
GO
/****** Object:  Table [dbo].[Rezultat]    Script Date: 4/18/2019 9:06:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rezultat](
	[Ime] [nvarchar](50) NULL,
	[Score] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[usp_Upisi]    Script Date: 4/18/2019 9:06:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Upisi]
@score as int,
@ime as nvarchar(50)=null
as
insert into Rezultat(Ime,Score)
values(@ime,@score)
GO
