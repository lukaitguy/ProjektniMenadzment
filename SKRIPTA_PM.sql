USE [ProjektniMenadzment]
GO
ALTER TABLE [dbo].[Zadaci] DROP CONSTRAINT [FK_Zadaci_Korisnici1]
GO
ALTER TABLE [dbo].[Zadaci] DROP CONSTRAINT [FK_Zadaci_Korisnici]
GO
ALTER TABLE [dbo].[Resursi] DROP CONSTRAINT [FK_Resursi_Projekti]
GO
ALTER TABLE [dbo].[Resursi] DROP CONSTRAINT [FK_Resursi_Korisnici]
GO
ALTER TABLE [dbo].[Projekti] DROP CONSTRAINT [FK_Projekti_Zanrovi]
GO
ALTER TABLE [dbo].[KomentariZadatak] DROP CONSTRAINT [FK_KomentariZadatak_Zadaci]
GO
ALTER TABLE [dbo].[KomentariZadatak] DROP CONSTRAINT [FK_KomentariZadatak_Korisnici]
GO
ALTER TABLE [dbo].[ClanoviProjekta] DROP CONSTRAINT [FK_ClanoviProjekta_Projekti]
GO
ALTER TABLE [dbo].[ClanoviProjekta] DROP CONSTRAINT [FK_ClanoviProjekta_Korisnici]
GO
/****** Object:  Table [dbo].[Zanrovi]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zanrovi]') AND type in (N'U'))
DROP TABLE [dbo].[Zanrovi]
GO
/****** Object:  Table [dbo].[Zadaci]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zadaci]') AND type in (N'U'))
DROP TABLE [dbo].[Zadaci]
GO
/****** Object:  Table [dbo].[Resursi]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resursi]') AND type in (N'U'))
DROP TABLE [dbo].[Resursi]
GO
/****** Object:  Table [dbo].[Projekti]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekti]') AND type in (N'U'))
DROP TABLE [dbo].[Projekti]
GO
/****** Object:  Table [dbo].[Korisnici]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Korisnici]') AND type in (N'U'))
DROP TABLE [dbo].[Korisnici]
GO
/****** Object:  Table [dbo].[KomentariZadatak]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KomentariZadatak]') AND type in (N'U'))
DROP TABLE [dbo].[KomentariZadatak]
GO
/****** Object:  Table [dbo].[ClanoviProjekta]    Script Date: 7/9/2025 9:06:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClanoviProjekta]') AND type in (N'U'))
DROP TABLE [dbo].[ClanoviProjekta]
GO
/****** Object:  Table [dbo].[ClanoviProjekta]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClanoviProjekta](
	[ProjekatId] [uniqueidentifier] NOT NULL,
	[KorisnikId] [uniqueidentifier] NOT NULL,
	[Uloga] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KomentariZadatak]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KomentariZadatak](
	[Id] [uniqueidentifier] NOT NULL,
	[Sadrzaj] [nvarchar](150) NOT NULL,
	[ZadatakId] [uniqueidentifier] NOT NULL,
	[KorisnikId] [uniqueidentifier] NOT NULL,
	[DatumKreiranja] [datetime] NOT NULL,
 CONSTRAINT [PK_KomentariZadatak] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Korisnici]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Korisnici](
	[Id] [uniqueidentifier] NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[BrojTelefona] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Biografija] [nvarchar](50) NULL,
	[DatumKreiranja] [datetime] NOT NULL,
 CONSTRAINT [PK_Korisnici] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projekti]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projekti](
	[Id] [uniqueidentifier] NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Opis] [nvarchar](150) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Budzet] [decimal](18, 2) NULL,
	[DatumPocetka] [datetime] NOT NULL,
	[Rok] [date] NULL,
	[KreiraoKorisnikId] [uniqueidentifier] NOT NULL,
	[ZanrId] [uniqueidentifier] NULL,
	[DatumKreiranja] [datetime] NOT NULL,
 CONSTRAINT [PK_Projekti] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resursi]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resursi](
	[Id] [uniqueidentifier] NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Tip] [nvarchar](50) NOT NULL,
	[Opis] [nvarchar](150) NULL,
	[Cena] [decimal](18, 2) NULL,
	[ProjekatId] [uniqueidentifier] NULL,
	[DodeljenKorisniku] [uniqueidentifier] NULL,
	[DatumKreiranja] [datetime] NOT NULL,
 CONSTRAINT [PK_Resursi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zadaci]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zadaci](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjekatId] [uniqueidentifier] NOT NULL,
	[Naslov] [nvarchar](50) NOT NULL,
	[Opis] [nvarchar](150) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Prioritet] [nvarchar](50) NOT NULL,
	[KreiraoKorisnikId] [uniqueidentifier] NOT NULL,
	[DodeljenKorisnikuId] [uniqueidentifier] NULL,
	[Rok] [date] NULL,
	[DatumKreiranja] [datetime] NOT NULL,
 CONSTRAINT [PK_Zadaci] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zanrovi]    Script Date: 7/9/2025 9:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zanrovi](
	[Id] [uniqueidentifier] NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Zanrovi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ClanoviProjekta] ([ProjekatId], [KorisnikId], [Uloga]) VALUES (N'3e8dbf36-b259-48e4-bf83-491bae476cb5', N'9ceac268-c23b-4c98-9d28-02efa2e49deb', N'Dizajner')
INSERT [dbo].[ClanoviProjekta] ([ProjekatId], [KorisnikId], [Uloga]) VALUES (N'3e8dbf36-b259-48e4-bf83-491bae476cb5', N'a75c8586-e4db-4c81-9802-0b96e286367a', N'Developer')
INSERT [dbo].[ClanoviProjekta] ([ProjekatId], [KorisnikId], [Uloga]) VALUES (N'3e8dbf36-b259-48e4-bf83-491bae476cb5', N'843ddcd6-22ec-4e79-ad9c-58ab2f9efe02', N'Tester')
GO
INSERT [dbo].[Korisnici] ([Id], [Ime], [Prezime], [BrojTelefona], [Email], [Biografija], [DatumKreiranja]) VALUES (N'9ceac268-c23b-4c98-9d28-02efa2e49deb', N'Ana', N'Petrovic', N'061222222', N'ana@example.com', N'Game Designer', CAST(N'2025-07-05T19:41:01.647' AS DateTime))
INSERT [dbo].[Korisnici] ([Id], [Ime], [Prezime], [BrojTelefona], [Email], [Biografija], [DatumKreiranja]) VALUES (N'a75c8586-e4db-4c81-9802-0b96e286367a', N'Luka', N'Djukic', N'060111111', N'luka@example.com', N'Backend Developer', CAST(N'2025-07-05T19:41:01.647' AS DateTime))
INSERT [dbo].[Korisnici] ([Id], [Ime], [Prezime], [BrojTelefona], [Email], [Biografija], [DatumKreiranja]) VALUES (N'843ddcd6-22ec-4e79-ad9c-58ab2f9efe02', N'Marko', N'Jovanovic', N'062333333', N'marko@example.com', N'QA Tester', CAST(N'2025-07-05T19:41:01.647' AS DateTime))
GO
INSERT [dbo].[Projekti] ([Id], [Naziv], [Opis], [Status], [Budzet], [DatumPocetka], [Rok], [KreiraoKorisnikId], [ZanrId], [DatumKreiranja]) VALUES (N'3e8dbf36-b259-48e4-bf83-491bae476cb5', N'Space Game', N'Razvijamo svemirsku strategiju', N'U toku', CAST(5000.00 AS Decimal(18, 2)), CAST(N'2025-07-01T00:00:00.000' AS DateTime), CAST(N'2025-12-31' AS Date), N'9ceac268-c23b-4c98-9d28-02efa2e49deb', N'7a0e79b7-e3f9-4586-8836-b0d0d9cbbec6', CAST(N'2025-07-05T19:42:49.417' AS DateTime))
INSERT [dbo].[Projekti] ([Id], [Naziv], [Opis], [Status], [Budzet], [DatumPocetka], [Rok], [KreiraoKorisnikId], [ZanrId], [DatumKreiranja]) VALUES (N'bd9c7370-b84c-459e-b447-85f63e5c38c8', N'Zombie Shooter', N'Akciona igra sa zombijima', N'Planiran', CAST(10000.00 AS Decimal(18, 2)), CAST(N'2025-08-10T00:00:00.000' AS DateTime), NULL, N'9ceac268-c23b-4c98-9d28-02efa2e49deb', N'7ba4fcd3-25ec-4ffa-a478-d27d3b408e58', CAST(N'2025-07-05T19:42:49.417' AS DateTime))
GO
INSERT [dbo].[Zadaci] ([Id], [ProjekatId], [Naslov], [Opis], [Status], [Prioritet], [KreiraoKorisnikId], [DodeljenKorisnikuId], [Rok], [DatumKreiranja]) VALUES (N'9be95f19-5c46-4d2f-a9bb-24bd51a209e2', N'3e8dbf36-b259-48e4-bf83-491bae476cb5', N'Test zadatak', N'Ovo je opis zadatka
I ovde nema zajebancije
Ko hoce da radi neka radi.
Ko nece neka ide kuci.
Pozdrav!!!', N'Nije zapocet', N'Srednji', N'a75c8586-e4db-4c81-9802-0b96e286367a', NULL, CAST(N'2025-08-10' AS Date), CAST(N'2025-07-07T13:59:37.990' AS DateTime))
INSERT [dbo].[Zadaci] ([Id], [ProjekatId], [Naslov], [Opis], [Status], [Prioritet], [KreiraoKorisnikId], [DodeljenKorisnikuId], [Rok], [DatumKreiranja]) VALUES (N'b2e586da-c366-4d2e-8dfa-a7db53f848db', N'3e8dbf36-b259-48e4-bf83-491bae476cb5', N'Funkcionalnosti zadataka', N'Nastaviti rad sa uredi i obrisi funkcionalnostima vezanim za zadatke.', N'Nije zapocet', N'Visok', N'a75c8586-e4db-4c81-9802-0b96e286367a', NULL, CAST(N'2025-07-12' AS Date), CAST(N'2025-07-09T13:16:24.207' AS DateTime))
GO
INSERT [dbo].[Zanrovi] ([Id], [Naziv]) VALUES (N'b7a562c8-0428-4826-ba85-44993bcf94dc', N'Strategija')
INSERT [dbo].[Zanrovi] ([Id], [Naziv]) VALUES (N'7a0e79b7-e3f9-4586-8836-b0d0d9cbbec6', N'Akcija')
INSERT [dbo].[Zanrovi] ([Id], [Naziv]) VALUES (N'7ba4fcd3-25ec-4ffa-a478-d27d3b408e58', N'RPG')
GO
ALTER TABLE [dbo].[ClanoviProjekta]  WITH CHECK ADD  CONSTRAINT [FK_ClanoviProjekta_Korisnici] FOREIGN KEY([KorisnikId])
REFERENCES [dbo].[Korisnici] ([Id])
GO
ALTER TABLE [dbo].[ClanoviProjekta] CHECK CONSTRAINT [FK_ClanoviProjekta_Korisnici]
GO
ALTER TABLE [dbo].[ClanoviProjekta]  WITH CHECK ADD  CONSTRAINT [FK_ClanoviProjekta_Projekti] FOREIGN KEY([ProjekatId])
REFERENCES [dbo].[Projekti] ([Id])
GO
ALTER TABLE [dbo].[ClanoviProjekta] CHECK CONSTRAINT [FK_ClanoviProjekta_Projekti]
GO
ALTER TABLE [dbo].[KomentariZadatak]  WITH CHECK ADD  CONSTRAINT [FK_KomentariZadatak_Korisnici] FOREIGN KEY([KorisnikId])
REFERENCES [dbo].[Korisnici] ([Id])
GO
ALTER TABLE [dbo].[KomentariZadatak] CHECK CONSTRAINT [FK_KomentariZadatak_Korisnici]
GO
ALTER TABLE [dbo].[KomentariZadatak]  WITH CHECK ADD  CONSTRAINT [FK_KomentariZadatak_Zadaci] FOREIGN KEY([ZadatakId])
REFERENCES [dbo].[Zadaci] ([Id])
GO
ALTER TABLE [dbo].[KomentariZadatak] CHECK CONSTRAINT [FK_KomentariZadatak_Zadaci]
GO
ALTER TABLE [dbo].[Projekti]  WITH CHECK ADD  CONSTRAINT [FK_Projekti_Zanrovi] FOREIGN KEY([ZanrId])
REFERENCES [dbo].[Zanrovi] ([Id])
GO
ALTER TABLE [dbo].[Projekti] CHECK CONSTRAINT [FK_Projekti_Zanrovi]
GO
ALTER TABLE [dbo].[Resursi]  WITH CHECK ADD  CONSTRAINT [FK_Resursi_Korisnici] FOREIGN KEY([DodeljenKorisniku])
REFERENCES [dbo].[Korisnici] ([Id])
GO
ALTER TABLE [dbo].[Resursi] CHECK CONSTRAINT [FK_Resursi_Korisnici]
GO
ALTER TABLE [dbo].[Resursi]  WITH CHECK ADD  CONSTRAINT [FK_Resursi_Projekti] FOREIGN KEY([ProjekatId])
REFERENCES [dbo].[Projekti] ([Id])
GO
ALTER TABLE [dbo].[Resursi] CHECK CONSTRAINT [FK_Resursi_Projekti]
GO
ALTER TABLE [dbo].[Zadaci]  WITH CHECK ADD  CONSTRAINT [FK_Zadaci_Korisnici] FOREIGN KEY([DodeljenKorisnikuId])
REFERENCES [dbo].[Korisnici] ([Id])
GO
ALTER TABLE [dbo].[Zadaci] CHECK CONSTRAINT [FK_Zadaci_Korisnici]
GO
ALTER TABLE [dbo].[Zadaci]  WITH CHECK ADD  CONSTRAINT [FK_Zadaci_Korisnici1] FOREIGN KEY([KreiraoKorisnikId])
REFERENCES [dbo].[Korisnici] ([Id])
GO
ALTER TABLE [dbo].[Zadaci] CHECK CONSTRAINT [FK_Zadaci_Korisnici1]
GO
