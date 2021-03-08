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
	foreign key (WebhookId) references dbo.ObsoleteWebhooks(Id),
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
	Webhooks nvarchar(4000) null,
	Headers nvarchar(4000) null,
	CreationTime datetime not null
)
go

create table dbo.WebhookEvent (
	Id uniqueidentifier not null primary key,
	TenantId int not null,
	WebhookName nvarchar(256) not null,
	[Data] nvarchar(max) not null,
	CreationTime datetime not null,
	DeletionTime datetime null,
	IsDeleted bit not null,
)
go

create table dbo.WebhookSendAttempt (
	Id uniqueidentifier not null primary key,
	TenantId int not null,
	CreationTime datetime not null,
	LastModificationTime datetime null,
	Response nvarchar(max) null,
	ResponseStatusCode int null,
	WebhookEventId uniqueidentifier foreign key references WebhookEvent(Id),
	WebhookSubscriptionId uniqueidentifier foreign key references WebhookSubscriptionInfo(Id)
)
go

