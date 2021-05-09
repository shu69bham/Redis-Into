# Redis-Into
Introduction to Redis using C#

# Setting up Redis Container on local machine
Run the following command -

docker run --name my-redis -p 5002:6379 -d redis

The above command starts the redis container at port 5002(which will keep running in the background till system re-starts as we have used -d in the command)
