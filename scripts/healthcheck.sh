#!/bin/bash

sleep 10

STATUS=$(curl -s -o /dev/null -w "%{http_code}" http://localhost:80/health)

if [ "$STATUS" != "200" ]; then
  echo "Health check failed"
  exit 1
fi

echo "Application healthy"