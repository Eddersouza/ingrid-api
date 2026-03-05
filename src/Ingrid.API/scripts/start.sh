#!/bin/bash
cd /usr/share/nginx/html
nohup dotnet Ingrid.API.dll > app.log 2>&1 &
chmod +x scripts/*.sh