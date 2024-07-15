GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 05/01/2024 16:36:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('[dbo].[Empresa]') IS NOT NULL 
	DROP TABLE [dbo].[Empresa]
GO
CREATE TABLE [dbo].[Empresa](
	Id [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	codigo varchar(10) not null, 
	nome varchar(50) not null, 
	descricao varchar(100) not null,
	razaoSocial varchar(100) not null, 
	cpnj varchar(100) not null, 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- EMPRESA 
	dtCreation datetime not null, 
	dtLastUpdate datetime,
	CONSTRAINT AK_Usuario_Email UNIQUE(Email)  
)

GO
IF OBJECT_ID('[dbo].[Usuario]') IS NOT NULL 
	DROP TABLE [dbo].[Usuario]
GO
CREATE TABLE [dbo].[Usuario](
	Id [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nome [varchar](50) NOT NULL,
	email [varchar](100) NOT NULL,
	senha [varchar](100) NOT NULL, 
	costCenter varchar(20) not null, 
	idRole int  null,  -- tabela de Role pode ser implementada com tipos e niveis de acesso - Versão 1, ex: 0-MASTER, CORRETOR, UNDERWRITTER_VIDA, UNDERWRITTER_AGRO
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- USER 
	dtCreation datetime  not null, 
	dtLastUpdate datetime,
	CONSTRAINT AK_Usuario_Email UNIQUE(Email)  
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('[dbo].[Segurado]') IS NOT NULL 
	DROP TABLE [dbo].[Segurado]
GO
create TABLE [dbo].[Segurado] 
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	idEmpresa int not null, 
	tipoSegurado char(1) not null, -- EMPRESA ou PESSOA FISICA
	nome varchar(100) not null, 
	nomeIdentificacao varchar(100) not null, 
	sexo char(1) not null, 
	estrangeiro bit not null, 
	profissao varchar(50) not null, 
	rendaMensal decimal(18,6) not null,
	legalCodeType1 varchar(4) not null, -- CPF, CNPJ, PASSAPORTE, RNE, 
	legalCodeType2 varchar(4) not null, -- CPF, CNPJ, PASSAPORTE, RNE, RG
	legalCode1 varchar(20) not null, -- CPNJ \ CPF \ PASSAPORTE, RNE,  RG
	legalCode2 varchar(20) not null, -- CPNJ \ CPF \ PASSAPORTE, RNE,  RG
	nacionalidade varchar(30)  null, 
	naturalidade varchar(30) null, 
	dataNascimento datetime not null, 
	enderecoLogradouro varchar(50) not null, 
	enderecoNumero varchar(10) not null, 
	enderecoCompl varchar(20) not null, 
	enderecoCEP varchar(9) not null, 
	enderecoBairro varchar(20) not null, 
	enderecoCidade varchar(20) not null, 
	enderecoUF char(2) not null, 
	enderecoPais varchar(2) not null,  -- BR 
	telefoneCelular varchar(15) not null, 
	telefoneCom varchar(15) null, 
	telefoneRes varchar(15) null, 
	statusSegurado int not null, -- 1 é ativo -- 2 desativado
	acordoLGPDdata datetime not null, 
	aceitaLGPD bit not null, 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- SEGUR 
	dtCreation datetime not null, 
	dtLastUpdate datetime
)
GO
IF OBJECT_ID('[dbo].[AdditionalDataModel]') IS NOT NULL 
	DROP TABLE [dbo].[AdditionalDataModel]
GO
create table dbo.AdditionalDataModel 
(
	Id int identity(1,1) NOT NULL PRIMARY KEY, 
	idEmpresa int not null, 
	produto varchar(6) not null, -- BRONCO, SEGVID, SEGPRE, SEGAUT, 
	codigo varchar(30) not null, 
	fieldOrder int not null, 
	shortDescricao varchar(30) not null,
	descricao varchar(50) not null, 
	--objectType varchar(5) not null, -- SEGURADO, PROPOSTA, APOLICE, ENDERECO, 
	dataTypeEnum varchar(10) not null,  -- TEXT \ LIST \ INT \ DECIMAL \ 
	minValueForDataType int  null, 
	maxValueForDataType int null, 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- ADDIT 
	dtCreation datetime, 
	dtLastUpdate datetime
)
GO
IF OBJECT_ID('[dbo].[SeguradoAdditional]') IS NOT NULL 
	DROP TABLE [dbo].[SeguradoAdditional]
GO

create table dbo.SeguradoAdditional
(
	idSegurado int, 
	idAdditionalDataModel int, 
	valueObject varchar(500), 
	idStatus int,
	dtCreation datetime, 
	dtLastUpdate datetime
	CONSTRAINT AK_SeguradoAdditional_Email UNIQUE(idSegurado, idAdditionalDataModel)  

)
GO
GO
IF OBJECT_ID('[dbo].[Proposta]') IS NOT NULL 
	DROP TABLE [dbo].[Proposta]
GO
CREATE TABLE [dbo].[Proposta](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	idEmpresa int not null, 
	idCorretor int, 
	idRamoPrincipal int not null,  -- id do ramo principal
	codigoInterno [varchar](50) NOT NULL,
	--codigoEmpresa [varchar](5) NOT NULL,
	idSegurado int not null, 
	idCondicaoPagto int not null, 
	parcelamento int, 
	premioEstimadoLiquido decimal(18,6) not null,
	premioEstimadoTotal decimal(18,6) not null,  
	limiteExposicao decimal(18,6) not null, 
	comissaoSeguro decimal(18,6) not null, 
	totalImpostos decimal(18,6) not null, 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- PROPO 
	dtCreation datetime, 
	dtLastUpdate datetime
) ON [PRIMARY]
GO
GO
IF OBJECT_ID('[dbo].[PropostaAdditional]') IS NOT NULL 
	DROP TABLE [dbo].[PropostaAdditional]
GO
create table dbo.[PropostaAdditional]
(
	idProposta int, 
	idAdditionalDataModel int, 
	valueObject varchar(500), 
	idStatus int,-- PROPO 
	dtCreation datetime, 
	dtLastUpdate datetime
	CONSTRAINT AK_PropostaAdditional_Proposta_AdditionalModel UNIQUE(idProposta, idAdditionalDataModel)  

)
GO


IF OBJECT_ID('[dbo].[Ramo]') IS NOT NULL 
	DROP TABLE [dbo].[Ramo]
GO
CREATE TABLE [dbo].Ramo(
	idRamo int primary key, 
	idGrupo int, 
	codigo varchar(20) , 
	descricao varchar(50), 
	codigoSusep varchar(20) null,
	descricaoSusep varchar(100) null,
	idGrupo int,
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- RAMO 
	dtCreation datetime not null, 
	dtLastUpdate datetime,
	CONSTRAINT AK_Ramo_CodigoRamo UNIQUE(codigo)  	
)
GO
IF OBJECT_ID('[dbo].[Grupo]') IS NOT NULL 
	DROP TABLE [dbo].[Grupo]
GO
CREATE TABLE [dbo].Grupo(
	Id int identity(1,1) not null,
	codigo varchar(20), 
	descricao varchar(50), 
	codigoSusep varchar(20) null,
	descricaoSusep varchar(100) null,
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- GRUPO
	dtCreation datetime not null, 
	dtLastUpdate datetime,
	CONSTRAINT AK_Grupo_CodigoGrupo UNIQUE(codigo)  
)
GO
IF OBJECT_ID('[dbo].[Cobertura]') IS NOT NULL 
	DROP TABLE [dbo].[Cobertura]
GO
CREATE TABLE [dbo].Cobertura
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	idRamo int not null,
	codigo varchar(30) not null, 
	descricao varchar(50) not null, 
	descricaoCompleta varchar(250), 
	susepCodigo varchar(50), 
	capitalCobertura decimal(18,6), 
	premioEstimado decimal(18,6), 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- COBER
	dtCreation datetime, 
	dtLastUpdate datetime,

)
GO
IF OBJECT_ID('[dbo].[CoberturaProposta]') IS NOT NULL 
	DROP TABLE [dbo].[CoberturaXProposta]
GO
GO
CREATE TABLE [dbo].[CoberturaXProposta](
	idProposta int,
	idCobertura int, 
	capitalSegurado decimal(18,6), 
	premioEstimado decimal(18,6), 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- COXPR
	dtCreation datetime not null,  
	dtLastUpdate datetime,
	CONSTRAINT AK_CoberturaProposta_Proposta_Cobertura UNIQUE(idProposta, idCobertura), 
) ON [PRIMARY]
GO
IF OBJECT_ID('[dbo].[PricingEngine]') IS NOT NULL 
	DROP TABLE [dbo].[PricingEngine]
GO
CREATE TABLE [dbo].[PricingEngine](
	idCobertura int not null, 
	capitalSegurado decimal(18,6) not null, 
	premioEstimado decimal(18,6) not null, 
	fumante bit not null, 
	doencasCronicas bit not null, 
	atestadoSaude bit not null, 
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- PRICI
	dtCreation datetime not null,  
	dtLastUpdate datetime,
	CONSTRAINT AK_PricingEngine_Parametros UNIQUE(idCobertura, fumante, doencas, atestadoSaude) 
) ON [PRIMARY]
GO
IF OBJECT_ID('[dbo].[Log]') IS NOT NULL 
	DROP TABLE [dbo].[Log]
GO
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[dataLog] DATETIME NOT NULL,
	[tipoLog] [varchar](20) NOT NULL, 
	[mensagem] [varchar](500) NOT NULL,
	[stackTrace] [varchar](1000) NULL,
	[moduleName] [varchar](100) NOT NULL,
	[origemDevice] varchar(50) not null
) ON [PRIMARY]
GO
GO
IF OBJECT_ID('[dbo].[CondicaoPagto]') IS NOT NULL 
	DROP TABLE [dbo].[CondicaoPagto]
GO
GO
CREATE TABLE [dbo].[CondicaoPagto](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[descricao] [varchar](50) NOT NULL,
	[codigo] [varchar](20) NOT NULL, 
	[maxParc] int NOT NULL,
	idStatus int not null, -- 0 inactive -- 1 active
	idObjectType varchar(5) not null, -- CONDP
	dtCreation datetime not null, 
	dtLastUpdate datetime,
) ON [PRIMARY]
GO




