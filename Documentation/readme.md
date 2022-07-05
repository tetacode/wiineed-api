### Empty Db Template
```sql
create table permission_group
(
    id   integer      not null
        constraint permission_group_pk
            primary key,
    name varchar(255) not null
);

create unique index permission_group_id_uindex
    on permission_group (id);

create table permission
(
    id                   integer      not null
        constraint permission_pk
            primary key,
    name                 varchar(255) not null,
    description          varchar(500),
    created_time         timestamp with time zone default CURRENT_TIMESTAMP,
    permission_group     integer
        constraint permission_permission_group_id_fk
            references permission_group,
    parent_permission_id integer
        constraint permission_permission_id_fk
            references permission,
    dependencies         integer[]                default '{}'::integer[]
);

create unique index permission_name_uindex
    on permission (name);

create table role
(
    id                 bigserial
        constraint role_pk
            primary key,
    name               varchar(255)                      not null,
    permission_id_list integer[] default '{}'::integer[] not null
);

create table "user"
(
    id                 uuid                                       not null
        constraint user_pk
            primary key,
    name               varchar(255)                               not null,
    lastname           varchar(255)                               not null,
    username           varchar(500)                               not null,
    email              varchar(500),
    permission_id_list integer[]    default '{}'::integer[]       not null,
    phone              varchar(20)  default ''::character varying not null,
    password_hash      varchar(1024)                              not null,
    password_salt      varchar(256)                               not null
);

create unique index user_id_uindex
    on "user" (id);

create unique index user_username_uindex
    on "user" (username);

create unique index user_email_uindex
    on "user" (email);

--pasword: 123456asd
INSERT INTO "user" (id, name, username, email, permission_id_list, visible_name, phone, lastname, password_hash, password_salt) VALUES ('b0c9798b-37db-4b13-930f-4c38d3e45f0d', 'admin', 'admin', 'admin@test.com', '', '', '', 'admin', 'ZwAZlY2tfvccgaAFi4oQfO94uUURcCI4Q0SZ+K9NyKg=', 'i/aoxTedHI1q9ZxtmCTnQw==');


create table log
(
    id           bigserial
        constraint log_pk
            primary key,
    data         text,
    created_date timestamp with time zone default CURRENT_TIMESTAMP not null,
    user_id      uuid
);

create table user_role
(
    user_id uuid   not null
        constraint user_role_user_id_fk
            references "user",
    role_id bigint not null
        constraint user_role_role_id_fk
            references role,
    constraint user_role_pk
        primary key (user_id, role_id)
);

```

### Db Migrations
```shell
$ cd Data
$ dotnet ef dbcontext scaffold "User ID=dbuser;Password=y2VtyFGgM3U9;Server=db.phoesoftware.com;Port=54320;Database=empty_db;Integrated Security=true;Pooling=true;" Npgsql.EntityFrameworkCore.PostgreSQL --context ApplicationDbContext -o Entity -f
```