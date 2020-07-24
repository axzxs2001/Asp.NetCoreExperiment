--创建表
CREATE TABLE [dbo].[SNs](
	[typename] [varchar](4) NOT NULL,
	[MaxSN] [bigint] NULL,
	[SNDate] [date] NULL,
 CONSTRAINT [PK_Lshs] PRIMARY KEY CLUSTERED 
(
	[typename] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SNs] ([typename], [MaxSN], [SNDate]) VALUES (N'CG', 699, CAST(0xBE3C0B00 AS Date))
INSERT [dbo].[SNs] ([typename], [MaxSN], [SNDate]) VALUES (N'XS', 669, CAST(0xBE3C0B00 AS Date))
INSERT [dbo].[SNs] ([typename], [MaxSN], [SNDate]) VALUES (N'YZ', 632, CAST(0xBE3C0B00 AS Date))

--创建存储过程
CREATE PROC GetSN
    @typename VARCHAR(6) ,
    @sn VARCHAR(30) OUTPUT
AS
    BEGIN
 --启动事务处理
        DECLARE @tran_point INT			--控制事务嵌套
        SET @tran_point = @@trancount	--保存事务点
        IF @tran_point = 0
            BEGIN TRAN tran_SOF_getmaxdjbh
        ELSE
            SAVE TRAN tran_SOF_getmaxdjbh

		DECLARE @tempsn BIGINT
		--锁表
		--IF EXISTS(SELECT 1 FROM SNs WITH (TABLOCKX) WHERE typename=@typename AND sndate=CONVERT(VARCHAR(10),GETDATE(),126))
	 --   BEGIN
  --      SELECT  @tempsn = MaxSN  + 1 
  --      FROM    dbo.SNs 
  --      WHERE   typename = @typename 
  --      UPDATE  SNs
  --      SET     MaxSN = @tempsn  
  --      WHERE   typename = @typename 
		--END
		--ELSE
		--BEGIN
  --      UPDATE  SNs
  --      SET     MaxSN = 1,sndate=CONVERT(VARCHAR(10),GETDATE(),126)
  --      WHERE   typename = @typename
		--end
		--锁行
		UPDATE  SNs
        SET    @tempsn = maxsn= CASE WHEN sndate=CONVERT(VARCHAR(10),GETDATE(),126) THEN maxsn+1 ELSE 1 end ,sndate=CONVERT(VARCHAR(10),GETDATE(),126)
        WHERE   typename = @typename
		--获取编号
		SET @sn=@typename+REPLACE(CONVERT(VARCHAR(10),GETDATE(),126),'-','')+REPLICATE('0',6-LEN(@tempsn))+CONVERT(VARCHAR(10),@tempsn)

        IF @@error <> 0
            BEGIN
                ROLLBACK TRAN tran_SOF_getmaxdjbh
            END

        IF @tran_point = 0
            BEGIN
                COMMIT TRAN tran_SOF_getmaxdjbh
				RETURN 0
            END
    END

