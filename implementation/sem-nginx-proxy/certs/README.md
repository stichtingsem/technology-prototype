# get certificates
Set up a local CA and generate certificates for the localhost domain:
- get mkcert: https://github.com/FiloSottile/mkcert#installation
- run `mkcert --install`
- in the command line, navigate to folder `technology-prototype/implementation/sem-nginx-proxy/certs` and generate certificates with `mkcert localhost 127.0.0.1`. This will generate two files, `localhost+1.pem` and `localhost+1-key.pem` in the current directory. Make sure the names are exactly those!

Full README in the implementation folder.
