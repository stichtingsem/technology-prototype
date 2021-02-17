-- CREATE script
create database Webhooks
go

use Webhooks
go

create table dbo.[Events] (
	Id uniqueidentifier not null primary key,
	[Name] nvarchar(256) not null
)
go

create table dbo.Webhooks (
	Id uniqueidentifier not null primary key,
	SchoolId uniqueidentifier not null,
	PostbackUrl nvarchar(1024) not null,
	[Secret] nvarchar(256) not null
)
go

create table dbo.Subscriptions (
	WebhookId uniqueidentifier not null,
	EventId uniqueidentifier not null,
	primary key (WebhookId, EventId),
	foreign key (WebhookId) references dbo.Webhooks(Id),
	foreign key (EventId) references dbo.[Events](Id),
)
go

insert into dbo.[Events] values(newid(), 'Catalog item added')
insert into dbo.[Events] values(newid(), 'Catalog item updated')
insert into dbo.[Events] values(newid(), 'Catalog item deleted')
go