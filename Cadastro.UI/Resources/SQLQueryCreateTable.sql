create table Alunos(
	CodigoID int IDENTITY(1,1) NOT NULL,
	Nome varchar(100),
	cpf varchar(15),
    Rg varchar(30),
	DataNascimento datetime,
	EnderecoComplemento varchar(50),
	Matricula varchar(30),
	Idade int,
	Sexo char(1),
	Telefone varchar(11),
	DataCadastro datetime,
	Cidade varchar(50),
	Email varchar(50),
	constraint pk_codigoId_aluno primary key (CodigoID)
)
create table Cidade(
        CodigoCidade int IDENTITY(1,1) NOT NULL,
        NomeCidade varchar(50),
        UF varchar(2),
        Cep varchar(15),
		constraint pk_codigoCidade primary key (CodigoCidade)
)

create table Usuario(
    CodigoID int IDENTITY(1,1) NOT NULL,
    Nome varchar(50),
	Rg varchar(30),
	Senha varchar(15),
	cpf varchar(15),
	sexo varchar(1),
	DataCadastro datetime,
	cidade varchar(50),
	Email varchar(50),
	Telefone varchar(15),
	CONSTRAINT PK_CODIGOID PRIMARY KEY (CodigoID)
)

create table Filiacao(
 CodigoID_Aluno int,
 NomePai varchar(50),
 NomeMae varchar(50),
 ProfissaoMae varchar(50),
 ProfissaoPai varchar(50),
 constraint pk_codigoaluno_id primary key (CodigoID_Aluno)
 
 
)
insert into Cidade (NomeCidade, UF, Cep) Values ('DOURADOS','MS','79800-000');