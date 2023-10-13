---Create a new database in MSSQL server with the name as StudentDB or what you have named it in the config file
--- Create a new table with the below procedure
-- Also run the procedured to enable the crud possible.


---------- START CREATE TABLE STUDENT ----------

USE [StudentDB]
GO

/****** Object:  Table [dbo].[Student]    Script Date: 13/10/2023 7:04:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Age] [int] NULL,
	[RegistrationDate] [datetime] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

---------- END CREATE TABLE STUDENT ----------


---------- START GETSTUDENTDATA ----------
USE [StudentDB]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentData]    Script Date: 13/10/2023 6:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentData] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select FirstName,LastName,Age,RegistrationDate from Student;

END
---------- END GETSTUDENTDATA ----------


---------- START GETSTUDENTDATABYID ----------
USE [StudentDB]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentDataById]    Script Date: 13/10/2023 6:58:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentDataById] 
	-- Add the parameters for the stored procedure here
 @StudID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select * from student where StudentID=@StudID
END
---------- END GETSTUDENTDATABYID ----------

---------- START INSERTSTUDENTDATA ----------
USE [StudentDB]
GO
/****** Object:  StoredProcedure [dbo].[InsertStudentData]    Script Date: 13/10/2023 7:00:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertStudentData] 
	-- Add the parameters for the stored procedure here
 @FirstName varchar(50),
 @LastName varchar(50),
 @Age bigint,
 @RegistrationDate Datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO STUDENT(FirstName,LastName,Age,RegistrationDate)VALUES(@FirstName,@LastName,@Age,@RegistrationDate)
END
---------- END INSERTSTUDENTDATA ----------


---------- START DELETESTUDENTDATA ----------
USE [StudentDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudentData]    Script Date: 13/10/2023 7:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[DeleteStudentData] 
	-- Add the parameters for the stored procedure here
 @StudentID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	delete  from  STUDENT  where StudentID= @StudentID
END

---------- END DELETESTUDENTDATA ----------