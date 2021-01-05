
SET IDENTITY_INSERT [dbo].[Worker] ON
INSERT INTO [dbo].[Worker] ([Id], [WorkerId], [Name], [PhoneNumber], [Email], [WorkerType]) VALUES (1, N'1234', N'AVI', N'0500000000', N'gg@jj.bb', 1)
INSERT INTO [dbo].[Worker] ([Id], [WorkerId], [Name], [PhoneNumber], [Email], [WorkerType]) VALUES (2, N'2345', N'MOSHE', N'0500000000', N'aa@aa.com', 2)
INSERT INTO [dbo].[Worker] ([Id], [WorkerId], [Name], [PhoneNumber], [Email], [WorkerType]) VALUES (3, N'3456', N'DAVID', N'0500000000', N'aa@a.com', 1)
INSERT INTO [dbo].[Worker] ([Id], [WorkerId], [Name], [PhoneNumber], [Email], [WorkerType]) VALUES (4, N'4567', N'SHIRA', N'0500000000', N'aa@a.com', 2)
INSERT INTO [dbo].[Worker] ([Id], [WorkerId], [Name], [PhoneNumber], [Email], [WorkerType]) VALUES (5, N'5678', N'LIA', N'0500000000', N'aa@a.com', 3)
SET IDENTITY_INSERT [dbo].[Worker] OFF