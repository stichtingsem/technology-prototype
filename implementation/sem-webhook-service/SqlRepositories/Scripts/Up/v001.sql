-- CREATE script
create database Webhooks
go

use Webhooks
go

-- BELOW THIS LINE IS OBSOLETE AND WILL BE REMOVED
-- ONCE THE REFACTORING TOWARDS ABP STORES IS DONE
create table dbo.[EventTypes] (
	Id uniqueidentifier not null primary key,
	[Name] nvarchar(256) not null
)
go

create table dbo.ObsoleteWebhooks (
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
	foreign key (WebhookId) references dbo.ObsoleteWebhooksWebhooks(Id),
	foreign key (EventTypeId) references dbo.[EventTypes](Id),
)
go

insert into dbo.[EventTypes] values(newid(), 'Catalog item added')
insert into dbo.[EventTypes] values(newid(), 'Catalog item updated')
insert into dbo.[EventTypes] values(newid(), 'Catalog item deleted')
go

-- ABOVE THIS LINE IS OBSOLETE AND WILL BE REMOVED
-- ONCE THE REFACTORING TOWARDS ABP STORES IS DONE

create table dbo.WebhookSubscriptionInfo (
	Id uniqueidentifier not null primary key,
	TenantId int not null,
	WebhookUri nvarchar(1024) not null,
	[Secret] nvarchar(256) not null,
	IsActive bit not null,
	CreationTime datetime not null
)
go

create table dbo.Webhooks (
	WebhookSubscriptionInfoId uniqueidentifier not null,
	[Name] varchar(256) not null,
	primary key (WebhookSubscriptionInfoId, [Name]),
	foreign key (WebhookSubscriptionInfoId) references dbo.WebhookSubscriptionInfo(Id),
)
go