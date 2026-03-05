#!/bin/bash
set -euxo pipefail

echo "Starting backend service..."

systemctl enable backend
systemctl restart backend

echo "Backend started"