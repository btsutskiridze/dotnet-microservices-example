# dotnet-microservices-example

.net microservices example according to https://www.youtube.com/watch?v=DgVjEo3OGBI youtube course.

## Setup

1. build both services and push to docker hub
2. then update K8S files with your image names
3. then apply kubernetes files in /K8S folder

   ```bash
   kubectl apply -f .
   ```

4. then run below command to get the external ip of the ingress service

   ```bash
   kubectl get ingress
   ```

5. then add the external ip to your hosts file

   ```bash
   sudo nano /etc/hosts
   ```

6. then add the external ip to your hosts file

   ```bash
   <external-ip> gateway.local
   ```

7. then you can access the services via below urls

   ```bash
   http://gateway.local/api/platforms
   http://gateway.local/api/c/platforms
   ...
   ```
