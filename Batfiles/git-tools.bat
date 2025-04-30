@echo off
setlocal enabledelayedexpansion
title Advanced Git Automation Tool

:: Default Git Settings
set GIT_USER_NAME=YourName
set GIT_USER_EMAIL=youremail@example.com
set BRANCH=master
set LOG_FILE=git_operations.log
set SLACK_WEBHOOK_URL=https://your.slack.webhook.url
set SEND_NOTIFICATION=true

:: Check for Git
git --version >nul 2>&1
if %errorlevel% neq 0 (
    call :log "ERROR: Git is not installed. Exiting."
    exit /b
)

:: Log Start Time
call :log "Starting Git operation at: %date% %time%"

:: User Setup (Configure Git Identity)
call :configure_git_user

:: Main Menu
:menu
cls
echo ===================================================
echo Git Tool - Advanced Automation for Teams
echo ===================================================
echo 1. Clone a Repository
echo 2. Check Repository Status
echo 3. Stash Changes
echo 4. Pull Latest Changes
echo 5. Push Changes
echo 6. Commit Changes
echo 7. Switch Branches
echo 8. Create New Branch
echo 9. Show Git Log
echo 10. Exit
echo ===================================================
set /p choice="Select an option (1-10): "

if "%choice%"=="1" goto clone
if "%choice%"=="2" goto status
if "%choice%"=="3" goto stash
if "%choice%"=="4" goto pull
if "%choice%"=="5" goto push
if "%choice%"=="6" goto commit
if "%choice%"=="7" goto switch_branch
if "%choice%"=="8" goto create_branch
if "%choice%"=="9" goto log
if "%choice%"=="10" exit
goto menu

:: Clone Repository
:clone
cls
set /p repo_url="Enter the Git repository URL: "
git clone %repo_url%
if %errorlevel% neq 0 (
    call :log "ERROR: Failed to clone repository."
    exit /b
)
call :log "Successfully cloned repository."
pause
goto menu

:: Check Status
:status
cls
echo === Git Status ===
git status
pause
goto menu

:: Stash Changes (If Any)
:stash
cls
call :log "Checking for uncommitted changes..."
git diff --quiet
if %errorlevel%==1 (
    call :log "Uncommitted changes found. Stashing changes..."
    git stash
    if %errorlevel% neq 0 (
        call :log "ERROR: Failed to stash changes."
        exit /b
    )
    call :log "Changes stashed successfully."
) else (
    call :log "No uncommitted changes found."
)
pause
goto menu

:: Pull Latest Changes (with rebase)
:pull
cls
call :log "Pulling latest changes from origin/%BRANCH%..."
git fetch origin
git rebase origin/%BRANCH%
if %errorlevel% neq 0 (
    call :log "ERROR: Rebase failed. Please resolve conflicts."
    exit /b
)
call :log "Successfully pulled and rebased from origin/%BRANCH%."
pause
goto menu

:: Push Changes
:push
cls
call :log "Pushing changes to origin/%BRANCH%..."
git push origin %BRANCH%
if %errorlevel% neq 0 (
    call :log "ERROR: Push failed."
    exit /b
)
call :log "Successfully pushed changes to origin/%BRANCH%."
pause
goto menu

:: Commit Changes
:commit
cls
git status --short
git diff --cached --quiet
if %errorlevel%==1 (
    set commitmsg=Auto commit on %date% %time%
    call :log "Changes found. Adding and committing changes..."
    git add .
    git commit -m "%commitmsg%"
    if %errorlevel% neq 0 (
        call :log "ERROR: Commit failed."
        exit /b
    )
    call :log "Commit successful."
) else (
    call :log "No changes to commit."
)
pause
goto menu

:: Switch Branch
:switch_branch
cls
set /p new_branch="Enter branch name to switch to: "
git checkout %new_branch%
if %errorlevel% neq 0 (
    call :log "ERROR: Failed to switch to branch %new_branch%."
    exit /b
)
call :log "Successfully switched to branch %new_branch%."
pause
goto menu

:: Create New Branch
:create_branch
cls
set /p new_branch="Enter name of the new branch: "
git checkout -b %new_branch%
if %errorlevel% neq 0 (
    call :log "ERROR: Failed to create branch %new_branch%."
    exit /b
)
call :log "Successfully created and switched to branch %new_branch%."
pause
goto menu

:: Show Git Log (Last 5 commits)
:log
cls
echo === Last 5 Git Commits ===
git log -5 --oneline --graph --decorate
pause
goto menu

:: Logging Subroutine
:log
echo %1
echo %1 >> %LOG_FILE%
exit /b

:: Send Notification (Slack/Email)
:send_notification
if "%SEND_NOTIFICATION%"=="true" (
    echo Sending notification to Slack...
    curl -X POST -H "Content-type: application/json" --data "{\"text\":\"%1\"}" %SLACK_WEBHOOK_URL%
    if %errorlevel% neq 0 (
        call :log "ERROR: Failed to send notification."
    ) else (
        call :log "Notification sent successfully."
    )
)
exit /b

:: Configure Git User (if needed)
:configure_git_user
git config --global user.name "%GIT_USER_NAME%"
git config --global user.email "%GIT_USER_EMAIL%"
call :log "Git user configured: %GIT_USER_NAME% <%GIT_USER_EMAIL%>"
exit /b
