#!/bin/bash
set -euxo pipefail

echo "Installing backend..."

mkdir -p /var/www/backend

systemctl daemon-reload

echo "Install complete"