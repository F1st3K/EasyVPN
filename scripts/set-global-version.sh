#!/bin/bash

# New version argument:
if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <new_version>"
    exit 1
fi
NEW_VERSION="$1"

# Set global version:
echo $NEW_VERSION > VERSION

# Set version backend:
echo "--- Set version backend: --------------------------------------"
cd ./backend
setversion -r $NEW_VERSION
cd ../

# Set version frontend:
echo "--- Set version frontend: --------------------------------------"
cd ./frontend
npm version --no-commit-hooks --no-git-tag-version $NEW_VERSION
cd ../

# Set version init:
echo "--- Set version init: --------------------------"
cd ./init
TMP_FILE=$(mktemp)
echo "#!/bin/sh" >> "$TMP_FILE"
echo "# version: $NEW_VERSION" >> "$TMP_FILE"
tail -n +3 "init.sh" >> "$TMP_FILE"
mv "$TMP_FILE" "init.sh"
head -2 "init.sh"
cd ../

# Set version services/WireguardVpn:
echo "--- Set version services/WireguardVpn --------------------------"
cd ./services/WireguardVpn
yq -i ".service.version = \"$NEW_VERSION\"" ./cmd/config.yml
yq ".service.version" ./cmd/config.yml
cd ../../

# Set version services/AmneziaWgVpn:
echo "--- Set version services/AmneziaWgVpn --------------------------"
cd ./services/AmneziaWgVpn
yq -i ".service.version = \"$NEW_VERSION\"" ./cmd/config.yml
yq ".service.version" ./cmd/config.yml
cd ../../
