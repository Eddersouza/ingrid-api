#!/bin/bash
set -euxo pipefail

echo "Stopping old process..."

sudo systemctl stop backend || true

echo "Starting backend..."

sudo systemctl start backend

echo "Starting backend service..."

systemctl enable backend
systemctl restart backend

echo "Backend started"