### Empty Db Template
```sql
--pasword: 123456asd
INSERT INTO "user" (id, name, username, email, permission_id_list, visible_name, phone, lastname, password_hash, password_salt) VALUES ('b0c9798b-37db-4b13-930f-4c38d3e45f0d', 'admin', 'admin', 'admin@test.com', '', '', '', 'admin', 'ZwAZlY2tfvccgaAFi4oQfO94uUURcCI4Q0SZ+K9NyKg=', 'i/aoxTedHI1q9ZxtmCTnQw==');
```

### Db Migrations
```shell
$ cd Data
$ dotnet ef dbcontext scaffold "User ID=dbuser;Password=y2VtyFGgM3U9;Server=db.phoesoftware.com;Port=54320;Database=empty_db;Integrated Security=true;Pooling=true;" Npgsql.EntityFrameworkCore.PostgreSQL --context ApplicationDbContext -o Entity -f
```