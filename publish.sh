#!/bin/bash

# Define the application name
APP_NAME="Sightings"
EXE_NAME="GU1"
COPYRIGHT="Class of 2023-24 | Team4 © 2024"

# Define paths
PUBLISH_DIR="./publish"
BUILD_DIR="./build_game"
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
publish "win-x64"
publish "linux-x64"
publish "osx-x64"
publish "osx-arm64"

# Create the application bundle for osx-arm64
echo "Creating application bundle for osx-arm64..."
mkdir -p $MACOS_DIR
mkdir -p $RESOURCES_DIR

# Copy the published single file to the MacOS directory
cp ${PUBLISH_DIR}/osx-arm64/* $MACOS_DIR/

# Copy the icon.icns file to the Resources directory
cp ./icon.icns $RESOURCES_DIR/

# Copy the Content folder to the Resources directory
mv ${PUBLISH_DIR}/osx-arm64/Content $RESOURCES_DIR

# Create Info.plist
cat > ${CONTENTS_DIR}/Info.plist <<EOL
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleDevelopmentRegion</key>
    <string>en</string>
    <key>CFBundleExecutable</key>
    <string>${EXE_NAME}</string>
    <key>CFBundleIconFile</key>
    <string>icon</string>
    <key>CFBundleIdentifier</key>
    <string>mono.${APP_NAME}</string>
    <key>CFBundleInfoDictionaryVersion</key>
    <string>6.0</string>
    <key>CFBundleName</key>
    <string>${APP_NAME}</string>
    <key>CFBundlePackageType</key>
    <string>APPL</string>
    <key>CFBundleShortVersionString</key>
    <string>1.0</string>
    <key>CFBundleSignature</key>
    <string>FONV</string>
    <key>CFBundleVersion</key>
    <string>1</string>
    <key>LSApplicationCategoryType</key>
    <string>public.app-category.games</string>
    <key>LSMinimumSystemVersion</key>
    <string>10.15</string>
    <key>NSHumanReadableCopyright</key>
    <string>${COPYRIGHT}</string>
    <key>NSPrincipalClass</key>
    <string>NSApplication</string>
    <key>LSRequiresNativeExecution</key>
    <true/>
    <key>LSArchitecturePriority</key>
    <array>
        <string>arm64</string>
    </array>
</dict>
</plist>
EOL

echo "Application bundle created at ${APP_BUNDLE_DIR}"

echo "All tasks completed successfully."

#create-dmg Sightings.app