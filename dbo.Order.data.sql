SET IDENTITY_INSERT [dbo].[Order] ON
INSERT INTO [dbo].[Order] ([Id], [ClientID], [FromDate], [ToDate], [NumOfAdults], [NumOfKids], [NumOfInfants], [TotalPrice]) VALUES (1, N'000000018', N'2021-03-02 00:00:00', N'2021-03-07 00:00:00', 2, 2, 0, 11000)
INSERT INTO [dbo].[Order] ([Id], [ClientID], [FromDate], [ToDate], [NumOfAdults], [NumOfKids], [NumOfInfants], [TotalPrice]) VALUES (3, N'111111111', N'2021-02-15 00:00:00', N'2021-02-18 00:00:00', 6, 5, 0, 22000)
INSERT INTO [dbo].[Order] ([Id], [ClientID], [FromDate], [ToDate], [NumOfAdults], [NumOfKids], [NumOfInfants], [TotalPrice]) VALUES (5, N'222222222', N'2021-04-21 00:00:00', N'2021-04-23 00:00:00', 2, 0, 0, 4000)
INSERT INTO [dbo].[Order] ([Id], [ClientID], [FromDate], [ToDate], [NumOfAdults], [NumOfKids], [NumOfInfants], [TotalPrice]) VALUES (8, N'333333333', N'2021-04-21 00:00:00', N'2021-04-26 00:00:00', 3, 1, 0, 14000)
INSERT INTO [dbo].[Order] ([Id], [ClientID], [FromDate], [ToDate], [NumOfAdults], [NumOfKids], [NumOfInfants], [TotalPrice]) VALUES (9, N'888888888', N'2021-05-05 00:00:00', N'2021-05-07 00:00:00', 4, 3, 0, 12000)
INSERT INTO [dbo].[Order] ([Id], [ClientID], [FromDate], [ToDate], [NumOfAdults], [NumOfKids], [NumOfInfants], [TotalPrice]) VALUES (11, N'999999999', N'2021-05-03 00:00:00', N'2021-05-08 00:00:00', 2, 2, 0, 14500)
SET IDENTITY_INSERT [dbo].[Order] OFF
