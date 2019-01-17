# Instructions

Run the following command to generate the certificate and its key:

```openssl req -new -x509 -newkey rsa:2048 -sha256 -nodes -keyout {output-file}.key -days 3560 -out {output-file}.cert -config {input-file}.cnf```

An example of the input file can be found below:

```[req]
default_bits = 2048
prompt = no
default_md = sha256
x509_extensions = v3_req
distinguished_name = dn

[dn]
C = GB
ST = London
L = London
O = Kindly
OU = Kindly
emailAddress = noreply@kindly.com
CN = kindly.com

[v3_req]
subjectAltName = @alt_names

[alt_names]
DNS.1 = kindly.com```