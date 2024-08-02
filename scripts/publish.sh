#!/bin/bash
RELEASE_VERSION= cat VERSION

# Finish release by git flow
git tag -a $RELEASE_VERSION -m "Release version $RELEASE_VERSION"
git push origin "release/$RELEASE_VERSION"

# Check status
if [ $? -ne 0 ]; then
    echo "Failed to finish the release."
    exit 1
fi

echo "Release $RELEASE_VERSION finished successfully."