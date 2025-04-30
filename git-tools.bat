@echo off
title Git Tools - Master Batch Script

:main
cls
echo =================================================
echo Git Tools Menu - Simplified Workflow for Teams
echo =================================================
echo 1. Clone a Git Repository
echo 2. Check Repository Status
echo 3. Stash Uncommitted Changes (if any)
echo 4. Pull Latest Changes (with rebase)
echo 5. Apply Last Stashed Changes
echo 6. Add and Commit Changes
echo 7. Push Changes to Remote
echo 8. Fetch and Rebase
echo 9. Show Last 5 Commits
echo 0. Exit
echo =================================================
set /p choice=Select an option (0-9): 

if "%choice%"=="1" goto clone
if "%choice%"=="2" goto status
if "%choice%"=="3" goto stash
if "%choice%"=="4" goto pull
if "%choice%"=="5" goto pop
if "%choice%"=="6" goto commit
if "%choice%"=="7" goto push
if "%choice%"=="8" goto fetch
if "%choice%"=="9" goto log
if "%choice%"=="0" exit
goto main

:clone
cls
set /p url=Enter Git repository URL to clone: 
git clone %url%
pause
goto main

:status
cls
echo === Git Status ===
git status
pause
goto main

:stash
cls
echo Checking for uncommitted changes...
git diff --quiet
if %errorlevel%==1 (
    git stash
    echo Changes stashed.
) else (
    echo No uncommitted changes to stash.
)
pause
goto main

:pull
cls
echo Pulling latest changes using rebase...
git pull --rebase origin master
if %errorlevel%==1 (
    echo Pull failed. Check for conflicts.
) else (
    echo Pull successful.
)
pause
goto main

:pop
cls
echo Attempting to reapply last stashed changes...
git stash pop
if %errorlevel%==1 (
    echo Failed to apply stash. Resolve conflicts manually.
) else (
    echo Stash applied.
)
pause
goto main

:commit
cls
git status
echo.
set /p commitmsg=Enter commit message: 
git add .
git diff --cached --quiet
if %errorlevel%==1 (
    git commit -m "%commitmsg%"
    echo Commit successful.
) else (
    echo Nothing to commit.
)
pause
goto main

:push
cls
echo Pushing changes to remote...
git push origin master
pause
goto main

:fetch
cls
echo Fetching latest changes and rebasing...
git fetch origin
git rebase origin/master
pause
goto main

:log
cls
echo Showing last 5 commits...
git log -5 --oneline --graph --decorate
pause
goto main
