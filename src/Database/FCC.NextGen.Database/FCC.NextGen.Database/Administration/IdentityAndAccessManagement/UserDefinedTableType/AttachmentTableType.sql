/*
NAME: [dbo].[AttachmentTableType]
CREATEDBY: DHESH KUMAAR A
*/

CREATE TYPE [dbo].[AttachmentTableType] 
AS TABLE
(
[FileName] NVARCHAR(250),
[FileURI] NVARCHAR(500) 
);