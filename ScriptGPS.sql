select * from Logs
select * from Scouts where JogadorId = 6
select * from Lancamentos where Year(DtPrevisao) = 2022 and Month(DtPrevisao) = 04
select * from Parametros
select * from Jogadores
select * from Contas

--DBCC CHECKIDENT('Logs', RESEED, 0) -- Para zerar o contador do Identity (autoIncrement)

select RANK() OVER(Order By SUM(b.Ponto + a.Presente) Desc) #, 
c.NomeJogador, c.Id, Convert(int, SUM(b.Ponto + a.Presente)) Pontos,
(SUM(b.Ponto + a.Presente) / Convert(int, SUM(a.Presente))) M_Pontos,
(SUM(a.Gol)) Gols, Convert(Decimal(10,2), Round(SUM(a.Gol),2,0)) / Convert(int, SUM(a.Presente)) M_Gols,
Convert(int, SUM(a.Presente)) Presença,
--Count(a.Fal) Falta,
sum(IIF(b.Id=3,1,0)) Vitoria,
sum(IIF(b.Id=5,1,0)) Empate,
sum(IIF(b.Id=4,1,0)) Derrota
from Scouts a 
join Parametros b on a.ParametroId = b.Id 
join Jogadores c on a.JogadorId = c.Id
where 
a.Inativo is null
and
year(a.DtPartida) = 2022
and
a.Presente <> 0
Group by c.NomeJogador, c.Id
Order by Pontos desc, Gols desc

--union all 
pdate Scouts set Inativo = '2022-04-04 15:05:26.8708020' where Id = 59

select b.NomeConta, sum(a.Valor) 
from Lancamentos a join Contas b
on a.ContaId = b.Id
where
YEAR(a.DtPrevisao) = 2022
and
MONTH(a.DtPrevisao) = 03
and
b.Tipo = 'E'
and
a.Inativo is null
and
a.DtBaixa is null
group by b.NomeConta, b.Tipo
order by b.Tipo

select * 
from Jogadores a 
where a.Mensalista = 'S'
and a.Id not in (2, 7)


select 'Anterior' Mes, 
(select Sum(a.Valor) from Lancamentos a join Contas b on a.ContaId = b.Id where YEAR(a.DtPrevisao) = 2022 and MONTH(a.DtPrevisao) = 4 and b.Tipo = 'E') as Entrada,
(select Sum(a.Valor) from Lancamentos a join Contas b on a.ContaId = b.Id where YEAR(a.DtPrevisao) = 2022 and MONTH(a.DtPrevisao) = 4 and b.Tipo = 'S') as Saida,
((select Sum(a.Valor) from Lancamentos a join Contas b on a.ContaId = b.Id where YEAR(a.DtPrevisao) = 2022 and MONTH(a.DtPrevisao) = 4 and b.Tipo = 'E') +
(select Sum(a.Valor) from Lancamentos a join Contas b on a.ContaId = b.Id where YEAR(a.DtPrevisao) = 2022 and MONTH(a.DtPrevisao) = 4 and b.Tipo = 'S')) S_Mes,
((select Sum(a.Valor) from Lancamentos a join Contas b on a.ContaId = b.Id where a.DtPrevisao <= EOMONTH(a.DtPrevisao) and b.Tipo = 'E')+
(select Sum(a.Valor) from Lancamentos a join Contas b on a.ContaId = b.Id where a.DtPrevisao <= EOMONTH(a.DtPrevisao) and b.Tipo = 'S')) S_Geral,




select EOMONTH(DtPrevisao) from Lancamentos -- Retorna o ultimo dia do mes da colecao DtPrevisao
select EOMONTH('2022-01-01', 12)

----------- Trigger
--Log de Inserções Contas

create trigger T_LogInsertContas
on Contas
after insert
as

	declare @newNomeConta varchar(100)
	declare @newTipoConta varchar(1)
	declare @newObsConta varchar(100)
	declare @newInativo varchar(20)

	select @newNomeConta = (select inserted.NomeConta from inserted)
	select @newTipoConta = (select inserted.Tipo from inserted)
	if((select inserted.ObsConta from inserted) is NULL) select @newObsConta = (select 'Nulo' from inserted) else select @newObsConta = (select inserted.ObsConta from inserted)
	if((select inserted.Inativo from inserted) is NULL) select @newInativo = (select 'Nulo' from inserted) else select @newInativo = (select inserted.Inativo from inserted)

	begin
		insert into Logs Values (null, 'Insercao', 'Contas', '{NomeConta:'+@newNomeConta+ ' | Tipo:'+@newTipoConta+ ' | ObsConta:'+@newObsConta+ ' | Inativo:'+@newInativo+ '} Incluido com sucesso!', GETDATE());
		print('T. Registro inserido com sucesso!');
	end

-- Log de Inserção Jogador
create trigger T_LogInsertJogadores
on Jogadores
after insert
as

	declare @newNomeJogador varchar(100)
	declare @newMensalista varchar(1)
	declare @newObsJogador varchar(100)
	declare @newInativo varchar(20)

	select @newNomeJogador = (select inserted.NomeJogador from inserted)
	select @newMensalista = (select inserted.Mensalista from inserted)
	if((select inserted.ObsJogador from inserted) is NULL) select @newObsJogador = (select 'Nulo' from inserted) else select @newObsJogador = (select inserted.ObsJogador from inserted)
	if((select inserted.Inativo from inserted) is NULL) select @newInativo = (select 'Nulo' from inserted) else select @newInativo = (select inserted.Inativo from inserted)

	begin
		insert into Logs Values (null, 'Insercao', 'Jogadores', '{NomeJogador:'+@newNomeJogador+ ' | Mensalista:'+@newMensalista+ ' | ObsJogador:'+@newObsJogador+ ' | Inativo:'+@newInativo+ '} Incluido com sucesso!', GETDATE());
		print('T. Registro inserido com sucesso!');
	end


-- Log de Inserção Parametros
create trigger T_LogInsertParametros
on Parametros
after insert
as

	declare @newDescParametro varchar(100)
	declare @newCodParametro varchar(2)
	declare @newPonto varchar(100)
	declare @newInativo varchar(20)

	select @newDescParametro = (select inserted.DescParametro from inserted)
	select @newCodParametro = (select inserted.CodParametro from inserted)
	select @newPonto = (select inserted.CodParametro from inserted)
	if((select inserted.Inativo from inserted) is NULL) select @newInativo = (select 'Nulo' from inserted) else select @newInativo = (select inserted.Inativo from inserted)

	begin
		insert into Logs Values (null, 'Insercao', 'Parametros', '{DescParametro:'+@newDescParametro+ ' | CodParametro:'+@newCodParametro+ ' | Ponto:'+@newPonto+ ' | Inativo:'+@newInativo+ '} Incluido com sucesso!', GETDATE());
		print('T. Registro inserido com sucesso!');
	end

-- Log de Inserção Lançamento
create trigger T_LogInsertLancamentos
on Lancamentos
after insert
as

	declare @newContaid varchar(100)
	declare @newJogadorId varchar(100)
	declare @newValor varchar(100)
	declare @newObsLancamento varchar(100)
	declare @newDtPrevisao varchar(20)
	declare @newDtBaixa varchar(20)
	declare @newInativo varchar(20)

	select @newContaid = (select inserted.Contaid from inserted)
	if((select inserted.JogadorId from inserted) is NULL) select @newJogadorId = (select 'Nulo' from inserted) else select @newJogadorId = (select inserted.JogadorId from inserted)
	select @newValor = (select inserted.Valor from inserted)
	if((select inserted.ObsLancamento from inserted) is NULL) select @newObsLancamento = (select 'Nulo' from inserted) else select @newObsLancamento = (select inserted.ObsLancamento from inserted)
	select @newDtPrevisao = (select inserted.DtPrevisao from inserted)
	if((select inserted.DtBaixa from inserted) is NULL) select @newDtBaixa = (select 'Nulo' from inserted) else select @newDtBaixa = (select inserted.DtBaixa from inserted)
	if((select inserted.Inativo from inserted) is NULL) select @newInativo = (select 'Nulo' from inserted) else select @newInativo = (select inserted.Inativo from inserted)

	begin
		insert into Logs Values (null, 'Insercao', 'Lancamentos', '{Contaid:'+@newContaid+ ' | JogadorId:'+@newJogadorId+ ' | Valor:'+@newValor+ ' | ObsLancamento:'+@newObsLancamento+ ' | DtPrevisao:'+@newDtPrevisao+ ' | DtBaixa:'+@newDtBaixa+ ' | Inativo:'+@newInativo+ '} Incluido com sucesso!', GETDATE());
		print('T. Registro inserido com sucesso!');
	end

-- Log de Inserção Scout
create trigger T_LogInsertScouts
on Scouts
after insert
as
	declare @newDtPartida varchar(20)
	declare @newJogadorId varchar(100)
	declare @newPresente varchar(5)
	declare @newParametroId varchar(100)
	declare @newGol varchar(100)
	declare @newAssistencia varchar(100)
	declare @newObsScout varchar(100)
	declare @newInativo varchar(20)
	declare @newTime varchar(100)


	select @newDtPartida = (select inserted.DtPartida from inserted)
	select @newJogadorId = (select inserted.JogadorId from inserted)
	select @newPresente = (select inserted.Presente from inserted)
	select @newParametroId = (select inserted.ParametroId from inserted)
	if((select inserted.Gol from inserted) is NULL) select @newGol = (select 'Nulo' from inserted) else select @newGol = (select inserted.Gol from inserted)
	if((select inserted.Assistencia from inserted) is NULL) select @newAssistencia = (select 'Nulo' from inserted) else select @newAssistencia = (select inserted.Assistencia from inserted)
	if((select inserted.ObsScout from inserted) is NULL) select @newObsScout = (select 'Nulo' from inserted) else select @newObsScout = (select inserted.ObsScout from inserted)
	if((select inserted.Inativo from inserted) is NULL) select @newInativo = (select 'Nulo' from inserted) else select @newInativo = (select inserted.Inativo from inserted)
	if((select inserted.Time from inserted) is NULL) select @newTime = (select 'Nulo' from inserted) else select @newTime = (select inserted.Time from inserted)

	begin
		insert into Logs Values (null, 'Insercao', 'Scouts', '{DtPartida:'+@newDtPartida+ ' | JogadorId:'+@newJogadorId+ ' | Presente:'+@newPresente+ ' | ParametroId:'+@newParametroId+ ' | Gol:'+@newGol+ ' | Assistencia:'+@newAssistencia+ ' | ObsScout:'+@newObsScout+ ' | Inativo:'+@newInativo+ ' | Time:'+@newTime+ '} Incluido com sucesso!', GETDATE());
		print('T. Registro inserido com sucesso!');
	end