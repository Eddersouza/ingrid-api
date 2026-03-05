#!/bin/bash
set -euxo pipefail

echo "Installing backend..."

mkdir -p /var/www/backend

chown -R ec2-user:ec2-user /var/www/backend

chmod 644 /etc/systemd/system/backend.service

systemctl daemon-reload

echo "Install complete"