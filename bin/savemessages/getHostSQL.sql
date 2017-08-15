set nocount on
begin tran

create table ##hostandsql
(
host varchar(max),
sql varchar(max)
)


Declare @tablename varchar(max)
DECLARE vend_cursor CURSOR FOR
select REPLACE ( name , 'InstanceStateMessageReferences_' , '' ) from sys.tables
where name like 'InstanceStateMessageReferences_%'
OPEN vend_cursor

FETCH NEXT FROM vend_cursor
into @tablename
while @@FETCH_STATUS = 0
begin


declare @sql varchar(max)

set @sql = '
Create table #messageContrusted 
(
applicatie varchar(max),
instancetype varchar(max),
instanceid uniqueidentifier,
messageid  uniqueidentifier,
imgContext image,
  imgpart image,
  imgFrag image,
  imgPropBag image,
  uidPartID uniqueidentifier, 
  nFragmentNumber  int,
  dtCreated datetime 

)

insert
 into #messageContrusted
 (
applicatie,
instancetype, 
instanceid, 
messageid ,
imgContext ,
imgpart ,
imgFrag,
imgPropBag,
uidPartID, 
nFragmentNumber,
dtCreated
)
select 
a.nvcApplicationName as applicatie,
 sc.nvcname, 
 i.uidInstanceID,
  mp.uidMessageID,
  imgContext,
  imgpart,
  imgfrag,
  imgPropBag,
  p.uidPartID, 
  nFragmentNumber ,
  i.dtCreated
from  Instances i
inner join Applications a
on i.uidAppOwnerID = a.uidAppID
left join ' +@tablename +'Q q 
on i.uidInstanceID = q.uidInstanceID 
left join ' +@tablename +'Q_Suspended sus
on i.uidInstanceID = sus.uidInstanceID 
left join ' +@tablename +'Q_Scheduled sched
on i.uidInstanceID = sus.uidInstanceID 
left join  InstanceStateMessageReferences_' + @tablename +' ist
on i.uidInstanceID = ist.uidInstanceID 
inner join MessageParts mp 
on mp.[uidMessageID ]= ist.uidMessageID 
or mp.[uidMessageID ] = q.uidMessageID
or mp.[uidMessageID ] = sus.uidMessageID
or mp.[uidMessageID ] = sched.uidMessageID
inner join parts p
on mp.uidPartID = p.uidPartID
inner join spool sp 
on sp.[uidMessageID ] = mp.[uidMessageID ]
inner join ServiceClasses sc 
on i.uidClassID = sc.uidServiceClassID
left join Fragments f 
on mp.uidPartID = f.uidPartID

select * from 
(
select  
*,
row_number() over 
( partition by messageid,uidPartID, nFragmentNumber 
order by 
uidPartID, nFragmentNumber 
) as nummer
 from #messageContrusted
) as parts
where nummer = 1
order by
messageid,uidPartID, nFragmentNumber 


'

insert into ##hostandsql
(host, sql)
values
(
@tablename,
@sql
) 

--select * from ##messageContrusted


FETCH NEXT FROM vend_cursor
into @tablename
end
close vend_cursor
DEALLOCATE vend_cursor

select * from ##hostandsql

rollback


