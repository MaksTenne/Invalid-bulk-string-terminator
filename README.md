# Invalid-bulk-string-terminator

DragonflyDB with StackExchange.Redis: Invalid bulk string terminator reproduction.

## Reproduction
Clone the repository and run the project.

Start the DragonflyDB container:
```bash
docker run -p 6379:6379 --ulimit memlock=-1 docker.dragonflydb.io/dragonflydb/dragonfly
```

generate cache: `https://localhost:7262/api/Redis/generate`
list companies: `https://localhost:7262/api/Redis/data`

First time will return the list of companies, but the second time will throw the invalid bulk string terminator exception.
Thir time will return the list of companies again etc.....