@echo off
:: Set title
title Git Workflow Script

echo ===============================
echo Git Automation Script
echo For team collaboration on master branch
echo ===============================
echo.

:: Step 1 - Pull latest changes
set /p choice="Step 1: Pull latest code from remote? (Y/N): "
if /I "%choice%"=="Y" (
    echo Pulling latest changes from origin/master...
    git pull origin master
    echo ✅ Done pulling from remote.
) else (
    echo ⏩ Skipped pulling from remote.
)

echo.

:: Step 2 - Stash local changes (if any)
set /p choice="Step 2: Stash your local uncommitted changes? (Y/N): "
if /I "%choice%"=="Y" (
    echo Saving local changes temporarily...
    git stash
    echo ✅ Changes stashed.
) else (
    echo ⏩ Skipped stashing.
)

echo.

:: Step 3 - Pull again to ensure updated state
set /p choice="Step 3: Pull again after stash to make sure up to date? (Y/N): "
if /I "%choice%"=="Y" (
    echo Pulling latest changes again...
    git pull origin master
    echo ✅ Repository updated.
) else (
    echo ⏩ Skipped second pull.
)

echo.

:: Step 4 - Apply stashed changes
set /p choice="Step 4: Re-apply your stashed changes? (Y/N): "
if /I "%choice%"=="Y" (
    echo Applying stashed changes...
    git stash pop
    echo ✅ Changes reapplied from stash.
) else (
    echo ⏩ Skipped applying stash.
)

echo.

:: Step 5 - Add, commit, and push
set /p choice="Step 5: Add, commit, and push your code to master? (Y/N): "
if /I "%choice%"=="Y" (
    echo Checking for changes to commit...
    git status

    echo Adding all changes...
    git add .

    :: Check if anything to commit
    git diff --cached --quiet
    if %errorlevel%==1 (
        set /p commitmsg="Enter your commit message: "
        git commit -m "%commitmsg%"
        echo ✅ Changes committed.
        git push origin master
        echo 🚀 Code pushed to master!
    ) else (
        echo ⚠️ No changes detected. Nothing to commit.
    )
) else (
    echo ⏩ Skipped commit and push.
)

:: Final message
echo.
echo ===============================
echo ✅ Git operations completed.
echo 💡 Please verify your repository on GitHub.
echo 📝 Make sure your changes are reflected correctly.
echo ===============================
pause
