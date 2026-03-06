#!/bin/bash
set -euxo pipefail

echo "Stopping wwwhost..."

systemctl stop wwwhost || true