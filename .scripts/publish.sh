#!/bin/bash
RELEASE_VERSION=$(head -n 1 VERSION)

# Finish release by git flow
git tag -d "$RELEASE_VERSION"
git push origin :refs/tags/"$RELEASE_VERSION"
git tag -a "$RELEASE_VERSION" -m "Release version $RELEASE_VERSION"
git push origin "release/$RELEASE_VERSION"
git push --tags

# Check status
if [ $? -ne 0 ]; then
    echo "Failed to finish the release."
    exit 1
fi

echo "Release $RELEASE_VERSION finished successfully."
