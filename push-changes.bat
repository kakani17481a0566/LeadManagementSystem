@echo off
setlocal enabledelayedexpansion

REM Step 1: Show current status
set /p choice="Do you want to view git status? (y/n): "
if /i "%choice%"=="y" (
    git status
) else (
    echo Skipping git status.
)

REM Step 2: Collect changed file names into a variable
set FILE_LIST=
for /f "tokens=* delims=" %%f in ('git status --porcelain') do (
    set "line=%%f"
    set "fileName=!line:~3!"
    set "FILE_LIST=!FILE_LIST! !fileName!"
)

REM If no changes, exit
if "%FILE_LIST%"=="" (
    echo No changes to commit.
    pause
    exit /b
)

REM Step 3: Stage all changes
set /p choice="Do you want to add all changes (git add -A)? (y/n): "
if /i "%choice%"=="y" (
    git add -A
) else (
    echo Skipping git add.
    pause
    exit /b
)

REM Step 4: Commit changes
set /p choice="Do you want to commit with message: 'Updated files: %FILE_LIST%'? (y/n): "
if /i "%choice%"=="y" (
    git commit -m "Updated files: %FILE_LIST%"
) else (
    echo Skipping commit.
    pause
    exit /b
)

REM Step 5: Push to master
set /p choice="Do you want to push to origin master? (y/n): "
if /i "%choice%"=="y" (
    git push origin HEAD:master
    echo Changes pushed to master branch successfully.
) else (
    echo Skipping push.
)

pause
