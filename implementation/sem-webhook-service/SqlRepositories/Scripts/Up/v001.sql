-- CREATE script
create database Webhooks
go

use Webhooks
go

create table dbo.WebhookSubcriptionInfo (
	Id uniqueidentifier not null primary key,
	TenantId int not null,
	WebhookUri nvarchar(1024) not null,
	[Secret] nvarchar(256) not null,
	IsActive bit not null,
	CreationTime datetime not null
)
go

create table dbo.Webhooks (
	WebhookSubcriptionInfoId uniqueidentifier not null,
	[Name] varchar(256) not null,
	primary key (WebhookSubcriptionInfoId, [Name]),
	foreign key (WebhookSubcriptionInfoId) references dbo.WebhookSubcriptionInfo(Id),
)
go