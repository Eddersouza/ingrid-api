#!/bin/bash
set -euxo pipefail

echo "Stopping old process..."

sudo systemctl stop wwwhost || true

echo "Starting wwwhost service..."

sudo systemctl restart nginx

systemctl daemon-reload

systemctl enable wwwhost
systemctl restart wwwhost

echo "wwwhost started"