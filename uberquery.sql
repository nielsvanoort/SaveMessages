--- nieuwe functie voor het ophalen van berichten
begin tran
declare @host varchar(max)
declare @sql varchar(max)


set nocount on

DECLARE @name VARCHAR(50) 


create table ##aantalmsgs
(
name varchar(max),
aantal int
)


create table ##result
(
uidPartID	uniqueidentifier,
imgpart	image,
nFragmentNumber	int,
imgContext	image,
imgPropBag	image,
host varchar(100)


)

DECLARE db_cursor CURSOR FOR  

select t.name from sys.tables t
where 
t.name like '%_MessageRefCountLog%'



OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @name   

WHILE @@FETCH_STATUS = 0   
BEGIN 

select @sql = 'insert into ##aantalmsgs (name, aantal) select ''' + @name + ''' as name, count(1) as aantal from ' + @name

--print @sql
exec(@sql)


FETCH NEXT FROM db_cursor INTO @name   
END   

CLOSE db_cursor   
DEALLOCATE db_cursor 




DECLARE db_cursor2 CURSOR FOR  

select name from ##aantalmsgs
where aantal > 0



OPEN db_cursor2   
FETCH NEXT FROM db_cursor2 INTO @host   
WHILE @@FETCH_STATUS = 0   
begin


print @host


select @sql = '
WITH testcte as 
(
select
p.uidPartID,
imgpart
, 0 as nFragmentNumber,
s.imgContext ,
 imgPropBag
 ,'''+ @host + ''' as host
--* 
from 
[dbo].'+@host+'  bts
inner join  dbo.MessageParts mp
on bts.uidMessageID = mp.[uidMessageID ]
inner join dbo.parts p
on mp.uidPartID = p.uidPartID
inner join spool s
on mp.[uidMessageID ] = s.[uidMessageID ]
)
insert into ##result
(uidPartID	,
imgpart	,
nFragmentNumber	,
imgContext	,
imgPropBag	,
host)   
select
    uidPartID,
    imgpart,
    nFragmentNumber,
	imgContext,
	imgPropBag
	,'''+ @host + ''' as host
	
	
from testcte
union all 
select
f.uidPartID uidPartID,
f.imgFrag  as imgPart
,f.nFragmentNumber nFragmentNumber
,null as imgContext
,null as imgPropBag
,'''+ @host + ''' as host
from Fragments f
inner join testcte on 
f.uidPartID = testcte.uidPartID
order by uidPartID, nFragmentNumber



'





--print @sql

exec(@sql)




FETCH NEXT FROM db_cursor2 INTO @host   
END   

CLOSE db_cursor2
DEALLOCATE db_cursor2

select * from ##result


rollback