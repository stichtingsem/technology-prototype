# Set up development environment

Set up local environment with docker:
- get docker: https://docs.docker.com/get-docker/
- run docker. On windows you might get some error with WSL2, if that happens, just follow the link in the message and the instructions to fix it
- add a configuration file (if it exists, just add the bellow configuration to it). On Windows the location is 'C:\ProgramData\Docker\config\daemon.json', on Linux/Mac it's '/etc/docker/daemon.json'. Add the bellow to be able to reach both internal network and internet from the running containers (this is important for the nginx proxy to work):
```
{
  "iptables" : false,
  "dns" : ["8.8.8.8","8.8.4.4","127.0.1.1"]
}
```

To be able to authenticate with a remote IDP we need a few more additional steps:

Get credentials to the infinitas registry which contains the avatar library (for now we have no place where to store secrets, so ask a team-mate who's working on sem):
- .npmrc file. Place this file into `technology-prototype/implementation/sem-admin-client/` folder
vsts-npm-auth -config .npmrc -T .npmrc

Next we set up local environment that runs on an https domain, so that it can be authenticated with a (remote) IDP

Set up a local CA and generate certificates for the localhost domain:
- get mkcert: https://github.com/FiloSottile/mkcert#installation
- run `mkcert --install`
- in the command line, navigate to folder `technology-prototype/implementation/sem-nginx-proxy/certs` and generate certificates with `mkcert localhost 127.0.0.1`. This will generate two files, `localhost+1.pem` and `localhost+1-key.pem` in the current directory. Make sure the names are exactly those!

---
All the above instructions only need to be executed once.
Bellow are the commands to run the  local environment.
---

# Run local environment
After setting up your local environment you can run the project using docker-compose.  Navigate to `technology-prototype/implementation where the docker-compose.yaml file is located. Build and run the client, nginx-proxy, and backend.:
```
docker-compose build
docker-compose up -d
```
Now you can access the whole service from
```
https://localhost:3010
```

The nginx proxy will automatically route the requests to the correct url: client requests to the sem-admin-client container, and apis to the sem-webhook-service backend.

Stop the environment with
```
docker-compose down
```

The changes in the react app are automatically watched and the container will rebuild. Just refresh the page. The backend is not watched, therefore changes only apply if the container is stopped, rebuilt and spun back up. You can do that with the following commands:
```
docker-compose stop implementation_sem-webhook-service
docker-compose build implementation_sem-webhook-service
docker-compose up -d implementation_sem-webhook-service
```

You can use (parts of) this command to also run separate containers, e.g. without the frontend or without the backend.
