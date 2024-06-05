#!/bin/bash

# Define the application name
APP_NAME="Sightings"

# Define paths
PUBLISH_DIR="./publish"
APP_BUNDLE_DIR="${PUBLISH_DIR}/${APP_NAME}.app"
CONTENTS_DIR="${APP_BUNDLE_DIR}/Contents"
MACOS_DIR="${CONTENTS_DIR}/MacOS"
RESOURCES_DIR="${CONTENTS_DIR}/Resources"

# Function to publish for a specified runtime
publish() {
    local runtime=$1
    echo "Publishing for $runtime..."
    dotnet publish -c Release -r $runtime -p:PublishSingleFile=true --self-contained -o ${PUBLISH_DIR}/$runtime
    if [ $? -ne 0 ]; then
        echo "Failed to publish for $runtime"
        exit 1
    fi
    echo "Successfully published for $runtime"
}

# Clean up previous publish directory
rm -rf $PUBLISH_DIR
mkdir -p $PUBLISH_DIR

# Publish for each runtime
publish "osx-arm64"
publish "osx-x64"
publish "win-x64"

# Create the application bundle for osx-arm64
echo "Creating application bundle for osx-arm64..."
mkdir -p $MACOS_DIR
mkdir -p $RESOURCES_DIR

# Copy the published single file to the MacOS directory
cp ${PUBLISH_DIR}/osx-arm64/* $MACOS_DIR/

# Create Info.plist
cat > ${CONTENTS_DIR}/Info.plist <<EOL
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleDisplayName</key>
    <string>${APP_NAME}</string>
    <key>CFBundleName</key>
    <string>${APP_NAME}</string>
    <key>CFBundleIdentifier</key>
    <string>com.example.${APP_NAME}</string>
    <key>CFBundleVersion</key>
    <string>1.0</string>
    <key>CFBundlePackageType</key>
    <string>APPL</string>
    <key>CFBundleSignature</key>
    <string>????</string>
    <key>CFBundleExecutable</key>
    <string>${APP_NAME}</string>
    <key>LSMinimumSystemVersion</key>
    <string>10.10</string>
</dict>
</plist>
EOL

# Rename the published single file to match the executable name
mv ${MACOS_DIR}/* ${MACOS_DIR}/${APP_NAME}

echo "Application bundle created at ${APP_BUNDLE_DIR}"

echo "All tasks completed successfully."
