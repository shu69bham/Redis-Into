# Redis-Into
Introduction to Redis using C#

# Setting up Redis Container on local machine
Run the following command -

docker run --name my-redis -p 5002:6379 -d redis

The above command starts the redis container at port 5002(which will keep running in the background till system re-starts as we have used -d in the command)
check the same using command docker ps -a (Shows the list of all containers in system)

# Enter Redis container
docker exec it my-redis sh

By using the above command, we enter into the redis container
After entering into the container write redis-cli to use the redis cli.

You can now interact with the redis db using redis-cli commands
