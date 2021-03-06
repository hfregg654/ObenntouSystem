USE [ObenntouSystem]
GO
/****** Object:  Table [dbo].[Dishes]    Script Date: 2021/6/2 下午 03:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dishes](
	[dish_id] [int] IDENTITY(1,1) NOT NULL,
	[dish_omiseid] [int] NOT NULL,
	[dish_name] [nvarchar](25) NOT NULL,
	[dish_price] [decimal](18, 0) NOT NULL,
	[dish_cre] [int] NOT NULL,
	[dish_credate] [datetime] NOT NULL,
	[dish_upd] [int] NULL,
	[dish_upddate] [datetime] NULL,
	[dish_del] [int] NULL,
	[dish_deldate] [datetime] NULL,
	[dish_pic] [nvarchar](250) NULL,
 CONSTRAINT [PK_Dishes] PRIMARY KEY CLUSTERED 
(
	[dish_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupImageMaster]    Script Date: 2021/6/2 下午 03:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupImageMaster](
	[gpic_id] [int] IDENTITY(1,1) NOT NULL,
	[gpic_path] [nvarchar](250) NOT NULL,
	[gpic_cre] [int] NULL,
	[gpic_credate] [datetime] NULL,
	[gpic_upd] [int] NULL,
	[gpic_upddate] [datetime] NULL,
	[gpic_del] [int] NULL,
	[gpic_deldate] [datetime] NULL,
 CONSTRAINT [PK_GroupImageMaster] PRIMARY KEY CLUSTERED 
(
	[gpic_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 2021/6/2 下午 03:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[group_id] [int] IDENTITY(1,1) NOT NULL,
	[group_userid] [int] NOT NULL,
	[group_omiseid] [int] NOT NULL,
	[group_name] [nvarchar](15) NOT NULL,
	[group_cre] [int] NOT NULL,
	[group_credate] [datetime] NOT NULL,
	[group_upd] [int] NULL,
	[group_upddate] [datetime] NULL,
	[group_del] [int] NULL,
	[group_deldate] [datetime] NULL,
	[group_pic] [nvarchar](250) NULL,
	[group_type] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OmiseMaster]    Script Date: 2021/6/2 下午 03:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OmiseMaster](
	[omise_id] [int] IDENTITY(1,1) NOT NULL,
	[omise_name] [nvarchar](15) NOT NULL,
	[omise_cre] [int] NOT NULL,
	[omise_credate] [datetime] NOT NULL,
	[omise_upd] [int] NULL,
	[omise_upddate] [datetime] NULL,
	[omise_del] [int] NULL,
	[omise_deldate] [datetime] NULL,
 CONSTRAINT [PK_OmiseMaster] PRIMARY KEY CLUSTERED 
(
	[omise_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2021/6/2 下午 03:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[order_groupid] [int] NOT NULL,
	[order_userid] [int] NOT NULL,
	[order_dishesid] [int] NOT NULL,
	[order_num] [int] NOT NULL,
	[order_cre] [int] NOT NULL,
	[order_credate] [datetime] NOT NULL,
	[order_upd] [int] NULL,
	[order_upddate] [datetime] NULL,
	[order_del] [int] NULL,
	[order_deldate] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021/6/2 下午 03:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](15) NOT NULL,
	[user_phone] [char](10) NOT NULL,
	[user_mail] [varchar](100) NOT NULL,
	[user_cre] [int] NOT NULL,
	[user_credate] [datetime] NOT NULL,
	[user_upd] [int] NULL,
	[user_upddate] [datetime] NULL,
	[user_del] [int] NULL,
	[user_deldate] [datetime] NULL,
	[user_acc] [varchar](50) NOT NULL,
	[user_pwd] [varchar](50) NOT NULL,
	[user_pri] [varchar](15) NOT NULL,
	[user_pic] [nvarchar](250) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Dishes] ON 

INSERT [dbo].[Dishes] ([dish_id], [dish_omiseid], [dish_name], [dish_price], [dish_cre], [dish_credate], [dish_upd], [dish_upddate], [dish_del], [dish_deldate], [dish_pic]) VALUES (1, 1, N'菜1', CAST(50 AS Decimal(18, 0)), 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Black.jpg')
INSERT [dbo].[Dishes] ([dish_id], [dish_omiseid], [dish_name], [dish_price], [dish_cre], [dish_credate], [dish_upd], [dish_upddate], [dish_del], [dish_deldate], [dish_pic]) VALUES (2, 1, N'菜2', CAST(55 AS Decimal(18, 0)), 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg')
INSERT [dbo].[Dishes] ([dish_id], [dish_omiseid], [dish_name], [dish_price], [dish_cre], [dish_credate], [dish_upd], [dish_upddate], [dish_del], [dish_deldate], [dish_pic]) VALUES (3, 1, N'菜3', CAST(45 AS Decimal(18, 0)), 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Green.jpg')
INSERT [dbo].[Dishes] ([dish_id], [dish_omiseid], [dish_name], [dish_price], [dish_cre], [dish_credate], [dish_upd], [dish_upddate], [dish_del], [dish_deldate], [dish_pic]) VALUES (5, 2, N'菜菜1', CAST(20 AS Decimal(18, 0)), 1, CAST(N'2021-06-02T11:58:18.797' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Black.jpg')
INSERT [dbo].[Dishes] ([dish_id], [dish_omiseid], [dish_name], [dish_price], [dish_cre], [dish_credate], [dish_upd], [dish_upddate], [dish_del], [dish_deldate], [dish_pic]) VALUES (6, 2, N'菜菜2', CAST(80 AS Decimal(18, 0)), 1, CAST(N'2021-06-02T11:59:15.563' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/2d59b3eb-c8ce-4e2e-85bc-f43ad84e5874.BMP')
SET IDENTITY_INSERT [dbo].[Dishes] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupImageMaster] ON 

INSERT [dbo].[GroupImageMaster] ([gpic_id], [gpic_path], [gpic_cre], [gpic_credate], [gpic_upd], [gpic_upddate], [gpic_del], [gpic_deldate]) VALUES (1, N'/Images/Group_Black.jpg', 1, CAST(N'2021-06-02T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[GroupImageMaster] ([gpic_id], [gpic_path], [gpic_cre], [gpic_credate], [gpic_upd], [gpic_upddate], [gpic_del], [gpic_deldate]) VALUES (2, N'/Images/Group_Red.jpg', 1, CAST(N'2021-06-02T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[GroupImageMaster] ([gpic_id], [gpic_path], [gpic_cre], [gpic_credate], [gpic_upd], [gpic_upddate], [gpic_del], [gpic_deldate]) VALUES (3, N'/Images/Group_Green.jpg', 1, CAST(N'2021-06-02T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[GroupImageMaster] ([gpic_id], [gpic_path], [gpic_cre], [gpic_credate], [gpic_upd], [gpic_upddate], [gpic_del], [gpic_deldate]) VALUES (4, N'/Images/Group_Blue.jpg', 1, CAST(N'2021-06-02T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[GroupImageMaster] ([gpic_id], [gpic_path], [gpic_cre], [gpic_credate], [gpic_upd], [gpic_upddate], [gpic_del], [gpic_deldate]) VALUES (6, N'/Images/fe0d6056-2bd2-4495-b421-aa7f9bac3b77.png', 1, CAST(N'2021-06-02T13:30:47.530' AS DateTime), NULL, NULL, 1, CAST(N'2021-06-02T13:35:19.173' AS DateTime))
INSERT [dbo].[GroupImageMaster] ([gpic_id], [gpic_path], [gpic_cre], [gpic_credate], [gpic_upd], [gpic_upddate], [gpic_del], [gpic_deldate]) VALUES (7, N'/Images/a8f54cb2-f1bf-4e26-82dc-ee4965319d2e.png', 1, CAST(N'2021-06-02T13:35:29.117' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[GroupImageMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (2, 3, 1, N'團1', 3, CAST(N'2021-05-30T00:00:00.000' AS DateTime), 3, CAST(N'2021-06-01T19:10:35.000' AS DateTime), NULL, NULL, N'/Images/Group_Blue.jpg', N'結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (3, 3, 1, N'團2', 3, CAST(N'2021-05-30T18:17:10.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Green.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (4, 3, 1, N'團3', 3, CAST(N'2021-05-31T15:48:47.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Red.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (5, 3, 1, N'隊1', 3, CAST(N'2021-05-31T15:49:04.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (6, 1, 1, N'隊2', 1, CAST(N'2021-05-31T15:49:27.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Green.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (7, 1, 1, N'隊3', 1, CAST(N'2021-05-31T15:49:37.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (8, 1, 1, N'隊4', 1, CAST(N'2021-05-31T15:49:46.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (9, 1, 1, N'團隊1', 1, CAST(N'2021-05-31T15:50:09.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Green.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (10, 1, 1, N'團隊2', 1, CAST(N'2021-05-31T15:50:26.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Green.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (11, 1, 1, N'團隊3', 1, CAST(N'2021-05-31T15:50:40.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Red.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (12, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:04:42.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (13, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:04:46.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (14, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:04:49.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (15, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:04:54.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (16, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:04:58.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (17, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:05:08.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (18, 1, 1, N'團2', 1, CAST(N'2021-05-31T17:05:12.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Blue.jpg', N'未結團')
INSERT [dbo].[Groups] ([group_id], [group_userid], [group_omiseid], [group_name], [group_cre], [group_credate], [group_upd], [group_upddate], [group_del], [group_deldate], [group_pic], [group_type]) VALUES (19, 3, 2, N'吃吃吃', 3, CAST(N'2021-06-01T12:11:58.000' AS DateTime), NULL, NULL, NULL, NULL, N'/Images/Group_Green.jpg', N'未結團')
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[OmiseMaster] ON 

INSERT [dbo].[OmiseMaster] ([omise_id], [omise_name], [omise_cre], [omise_credate], [omise_upd], [omise_upddate], [omise_del], [omise_deldate]) VALUES (1, N'店1', 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[OmiseMaster] ([omise_id], [omise_name], [omise_cre], [omise_credate], [omise_upd], [omise_upddate], [omise_del], [omise_deldate]) VALUES (2, N'店2', 1, CAST(N'2021-05-31T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[OmiseMaster] ([omise_id], [omise_name], [omise_cre], [omise_credate], [omise_upd], [omise_upddate], [omise_del], [omise_deldate]) VALUES (3, N'店3', 3, CAST(N'2021-06-01T19:36:20.237' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[OmiseMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (1, 2, 1, 2, 1, 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (2, 2, 1, 1, 3, 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (8, 3, 3, 1, 2, 3, CAST(N'2021-05-31T12:23:57.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (9, 3, 1, 2, 3, 1, CAST(N'2021-05-31T12:24:23.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (10, 2, 1, 2, 2, 1, CAST(N'2021-05-31T12:46:42.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (11, 2, 3, 2, 2, 3, CAST(N'2021-05-31T12:57:12.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (12, 2, 3, 2, 3, 3, CAST(N'2021-05-31T12:57:22.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (13, 2, 3, 3, 1, 3, CAST(N'2021-05-31T12:57:22.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (14, 2, 3, 2, 2, 3, CAST(N'2021-05-31T13:48:08.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (15, 2, 3, 3, 2, 3, CAST(N'2021-05-31T13:48:08.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (16, 2, 3, 1, 1, 3, CAST(N'2021-05-31T13:48:25.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (17, 3, 3, 1, 2, 3, CAST(N'2021-05-31T13:49:52.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (18, 9, 3, 2, 1, 3, CAST(N'2021-05-31T18:57:24.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (19, 9, 3, 1, 1, 3, CAST(N'2021-05-31T18:57:24.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[Orders] ([order_id], [order_groupid], [order_userid], [order_dishesid], [order_num], [order_cre], [order_credate], [order_upd], [order_upddate], [order_del], [order_deldate]) VALUES (20, 9, 3, 3, 1, 3, CAST(N'2021-05-31T18:57:24.000' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [user_name], [user_phone], [user_mail], [user_cre], [user_credate], [user_upd], [user_upddate], [user_del], [user_deldate], [user_acc], [user_pwd], [user_pri], [user_pic]) VALUES (1, N'Manager', N'0988123456', N'XXX@gmail.com', 0, CAST(N'2021-05-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, N'aaa', N'aaa', N'SuperManager', N'/Images/Group_Green.jpg')
INSERT [dbo].[Users] ([user_id], [user_name], [user_phone], [user_mail], [user_cre], [user_credate], [user_upd], [user_upddate], [user_del], [user_deldate], [user_acc], [user_pwd], [user_pri], [user_pic]) VALUES (3, N'UserAAA', N'0977123456', N'AAAgmailcom', 1, CAST(N'2021-05-30T00:00:00.000' AS DateTime), 1, CAST(N'2021-06-01T18:35:26.803' AS DateTime), NULL, NULL, N'zzz', N'zzz', N'Manager', N'/Images/87bef694-260f-4dbf-8f7e-2c9953cad96a.png')
INSERT [dbo].[Users] ([user_id], [user_name], [user_phone], [user_mail], [user_cre], [user_credate], [user_upd], [user_upddate], [user_del], [user_deldate], [user_acc], [user_pwd], [user_pri], [user_pic]) VALUES (4, N'UserBBB', N'0911223654', N'assaFFFcom', 1, CAST(N'2021-06-01T18:35:10.163' AS DateTime), 1, CAST(N'2021-06-01T18:35:16.677' AS DateTime), NULL, NULL, N'aaaa', N'aaa', N'User', N'/Images/Group_Black.jpg')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Dishes]  WITH CHECK ADD  CONSTRAINT [FK_Dishes_OmiseMaster] FOREIGN KEY([dish_omiseid])
REFERENCES [dbo].[OmiseMaster] ([omise_id])
GO
ALTER TABLE [dbo].[Dishes] CHECK CONSTRAINT [FK_Dishes_OmiseMaster]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_OmiseMaster] FOREIGN KEY([group_omiseid])
REFERENCES [dbo].[OmiseMaster] ([omise_id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_OmiseMaster]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Users] FOREIGN KEY([group_userid])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Users]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Dishes] FOREIGN KEY([order_dishesid])
REFERENCES [dbo].[Dishes] ([dish_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Dishes]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Groups] FOREIGN KEY([order_groupid])
REFERENCES [dbo].[Groups] ([group_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Groups]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([order_userid])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
