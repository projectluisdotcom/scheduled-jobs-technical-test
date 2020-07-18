![Test](https://github.com/rescodeio/Ophen/workflows/Test/badge.svg)

## To Improve

- Increase usage of Task.All and so on.
- Use InMemory or DistributedMemory dotnetcore interfaces to increase task max processing
- Unit Test the JobSchedulerTask and his internal logics
- Apply an Open/Close principle with JobType to make future new types easier. We may want a complete new interface with every step, a validator o just use a patter like Factory to grap another type of Job and overwrite, to avoid the tipical if Type == Batch you can add a dicitonary as inject it as dependency so whenever you add a new type it registers itself and points to a class that will be created after without adding no more code
- Tons of more testing of course
