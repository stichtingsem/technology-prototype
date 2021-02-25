-- CREATE script
create database Webhooks
go

use Webhooks
go

create table dbo.[EventTypes] (
	Id uniqueidentifier not null primary key,
	[Name] nvarchar(256) not null
)
go

create table dbo.Webhooks (
	Id uniqueidentifier not null primary key,
	TenantId uniqueidentifier not null,
	PostbackUrl nvarchar(1024) not null,
	[Secret] nvarchar(256) not null
)
go

create table dbo.Subscriptions (
	WebhookId uniqueidentifier not null,
	EventTypeId uniqueidentifier not null,
	primary key (WebhookId, EventTypeId),
	foreign key (WebhookId) references dbo.Webhooks(Id),
	foreign key (EventTypeId) references dbo.[EventTypes](Id),
)
go

insert into dbo.[EventTypes] values(newid(), 'Catalog item added')
insert into dbo.[EventTypes] values(newid(), 'Catalog item updated')
insert into dbo.[EventTypes] values(newid(), 'Catalog item deleted')
go