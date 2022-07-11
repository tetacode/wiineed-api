### Empty Db Template
```sql
--pasword: 123456asd
INSERT INTO "user" (id, name, username, email, permission_id_list, visible_name, phone, lastname, password_hash, password_salt) VALUES ('b0c9798b-37db-4b13-930f-4c38d3e45f0d', 'admin', 'admin', 'admin@test.com', '', '', '', 'admin', 'ZwAZlY2tfvccgaAFi4oQfO94uUURcCI4Q0SZ+K9NyKg=', 'i/aoxTedHI1q9ZxtmCTnQw==');
```

### Mongodb
```shell
docker run \
--name mongodb \
-e MONGO_INITDB_ROOT_USERNAME=Handclasp8310 \
-e MONGO_INITDB_ROOT_PASSWORD="MG7a38JT7SBrwz2DSU" \
-p 7017:27017 \
-v /mnt/mongodb:/data/db \
-d mongo
```

