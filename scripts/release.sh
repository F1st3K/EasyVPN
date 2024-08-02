#!/bin/bash

# New version argument:
if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <new_version>"
    exit 1
fi
NEW_VERSION="$1"

git flow release start $NEW_VERSION && \
    ./scripts/set-global-version.sh $NEW_VERSION && \
    git add . && \
    git commit -am "chore(release): increment global version to $NEW_VERSION"