if exists (select 1
            from  sysobjects
           where  id = object_id('ccrm_consume_behavior')
            and   type = 'U')
   drop table ccrm_consume_behavior
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ccrm_consumer')
            and   type = 'U')
   drop table ccrm_consumer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ccrm_customer')
            and   type = 'U')
   drop table ccrm_customer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ccrm_groups')
            and   type = 'U')
   drop table ccrm_groups
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ccrm_users')
            and   type = 'U')
   drop table ccrm_users
go

/*==============================================================*/
/* Table: ccrm_consume_behavior                                 */
/*==============================================================*/
create table ccrm_consume_behavior (
   bhvr_id              uniqueidentifier     not null,
   csmr_id              uniqueidentifier     not null,
   csmr_name            nvarchar(100)        null,
   csmr_cellphone       nvarchar(20)         null,
   csmr_destination     nvarchar(255)        not null,
   csmr_loc_x           decimal(12, 8)       not null,
   csmr_loc_y           decimal(12,8)        not null,
   cstmr_id             uniqueidentifier     not null,
   bhvr_date            datetime             not null,
   goods_name           nvarchar(255)        not null,
   csm_amount           decimal(8,2)         null,
   tip_amount           decimal(8,2)         null,
   related_salesman     int                  null,
   bhvr_state           int                  not null,
   is_failed            bit                  null,
   failed_reason        nvarchar(255)        null,
   constraint PK_CCRM_CONSUME_BEHAVIOR primary key (bhvr_id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ะกทั',
   'user', @CurrentUser, 'table', 'ccrm_consume_behavior', 'column', 'csm_amount'
go

/*==============================================================*/
/* Table: ccrm_consumer                                         */
/*==============================================================*/
create table ccrm_consumer (
   cid                  uniqueidentifier     not null,
   real_name            nvarchar(20)         null,
   city                 int                  null,
   first_order_date     datetime             null,
   frequent_area        nvarchar(100)        null,
   personal_state       int                  null,
   last_order_date      datetime             null,
   constraint PK_CCRM_CONSUMER primary key (cid)
)
go

/*==============================================================*/
/* Table: ccrm_customer                                         */
/*==============================================================*/
create table ccrm_customer (
   uid                  uniqueidentifier     not null,
   store_name           nvarchar(100)        null,
   city                 int                  null,
   frequent_area        nvarchar(100)        null,
   store_state          int                  null,
   last_order_date      datetime             null,
   off_work_time        datetime             null,
   frequent_loc_x       decimal(12,8)        null,
   frequent_loc_y       decimal(12,8)        null,
   constraint PK_CCRM_CUSTOMER primary key (uid)
)
go

/*==============================================================*/
/* Table: ccrm_groups                                           */
/*==============================================================*/
create table ccrm_groups (
   group_id             uniqueidentifier     not null,
   title                nvarchar(100)        not null,
   parent_group_id      int                  null,
   is_default           bit                  not null,
   constraint PK_CCRM_GROUPS primary key (group_id)
)
go

/*==============================================================*/
/* Table: ccrm_users                                            */
/*==============================================================*/
create table ccrm_users (
   uid                  uniqueidentifier     not null,
   email                nvarchar(100)        not null,
   password             nvarchar(50)         not null,
   nick_name            nvarchar(20)         not null,
   mobile               nvarchar(20)         null,
   gender               int                  not null,
   group_id             uniqueidentifier     not null,
   avatar               nvarchar(255)        null,
   safe_question        nvarchar(100)        null,
   safe_answer          nvarchar(100)        null,
   qq                   nvarchar(20)         null,
   user_type            int                  not null,
   constraint PK_CCRM_USERS primary key (uid)
)
go
