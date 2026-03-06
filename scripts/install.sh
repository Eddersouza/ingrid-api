#!/bin/bash
set -euxo pipefail

echo "Installing backend..."

mkdir -p /var/www/backend

chmod 644 /etc/systemd/system/wwwhost.service

systemctl daemon-reload

echo "Install complete"