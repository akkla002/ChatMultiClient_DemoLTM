create database SQL_ChatMulti
go
use SQL_ChatMulti
go
create table theUsers(username varchar(30) primary key, pwd varchar(30), nickName nvarchar(30), createTime datetime);
create table chatContent(sender varchar(30) references dbo.theUsers(username),receiver varchar(30) references dbo.theUsers(username),content nvarchar(900), timeContent datetime, primary key(sender,receiver, timeContent));



go
create proc usp_create_theUser @username varchar(30), @pwd varchar(30), @nickName nvarchar(30)
as
begin
	if exists (SELECT * FROM theUsers where username=@username)
		print(N'Đã tồn tại')
	else Insert into theUsers values (@username,@pwd,@nickName,GETDATE())
end
go
create proc usp_create_chatContent @sender varchar(30), @receiver varchar(30), @content nvarchar(900)
as
begin
	insert into chatContent values (@sender, @receiver, @content, getdate())
end;
go
create proc usp_checkUser @username varchar(30), @pwd varchar(30)
as
begin
	select * from theUsers where username=@username and pwd = @pwd
end
go
create proc usp_getHistoryOfChatContent @userName varchar(30)
as
begin
	select top 10 * from chatContent where sender = @userName or receiver = @userName order by timeContent
end
go

exec usp_create_theUser 'admin','1','admin'
exec usp_create_theUser 'a','1','a'
exec usp_create_theUser 'b','1','b'
exec usp_create_theUser 'c','1','c'
exec usp_create_theUser 'd','1','d'



-- exec usp_checkUser @userName , @password
