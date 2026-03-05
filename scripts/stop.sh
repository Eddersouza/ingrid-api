#!/bin/bash
set -euxo pipefail

echo "Stopping backend..."

systemctl stop backend || true