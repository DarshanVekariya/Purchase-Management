USE [PurchaseManagementDB]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'Admin', N'ADMIN', N'')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'474a333f-2198-4df6-b379-dfda6aafa5b4', N'Admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAIAAYagAAAAEEcjzxufclJDBp69IuSHGsz37Ac3ot8KH2ObhqxIMDZe7nOK6jQf2BT5etqMM3Kfzw==', N'OVIH6ATUPVC46U6E4ZRHZNEURS43O6YR', N'6aecb2c6-7d99-428f-abc2-a89b4af7ca14', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e23a84d9-821c-4cbc-8d92-af7b389de4b8', N'Darshan', N'DARSHAN', NULL, NULL, 0, N'AQAAAAIAAYagAAAAEHyn+T60R+oHo5Ct/llvhHRatFzzS2a8FphILMrXClBUxJgSmhfCHtwPto4n9ZFy5A==', N'BO5RZZQQ3IYG5D4B7LO3YJYGSA4ZLRML', N'a2f5b5dd-673d-4ac8-8fae-afdae9f00987', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'474a333f-2198-4df6-b379-dfda6aafa5b4', N'1')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Price]) VALUES (45, N'iphone 11', CAST(345499.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([Id], [Name], [Price]) VALUES (46, N'Vivo T-22', CAST(23467.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password]) VALUES (1, N'Darshan', N'Darshan@123')
INSERT [dbo].[Users] ([Id], [Username], [Password]) VALUES (2, N'Admin', N'Admin@123')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate]) VALUES (1, 1, CAST(N'2024-05-04T17:15:44.8033333' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate]) VALUES (2, 1, CAST(N'2024-05-04T17:22:53.0666667' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate]) VALUES (3, 1, CAST(N'2024-05-04T17:58:16.7100000' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate]) VALUES (4, 1, CAST(N'2024-05-04T18:04:25.8366667' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate]) VALUES (5, 1, CAST(N'2024-05-04T18:07:30.9666667' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240504103430_Identity', N'8.0.4')
GO
