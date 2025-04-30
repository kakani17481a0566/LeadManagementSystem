@echo off
setlocal enabledelayedexpansion
title Git Merge & Push Tool

:: === CONFIGURATION ===
set BRANCH=master
set REMOTE=origin
set BACKUP_BRANCH=backup-%BRANCH%

:: === CHECK IF INSIDE A GIT REPOSITORY ===
git rev-parse --is-inside-work-tree >nul 2>&1
if errorlevel 1 (
    echo ERROR: This is not a Git repository.
    echo Please navigate to a valid Git folder.
    goto pause
)

:: === GET FORMATTED DATE ===
for /f "tokens=2-4 delims=/ " %%a in ("%date%") do (
    set TODAY=%%c-%%a-%%b
)

echo.
echo ==========================================
echo Git Automation Tool Started [%TODAY%]
echo Working on branch: %BRANCH%
echo ==========================================
echo.

:: === STEP 1: FETCH FROM REMOTE ===
set /p fetchChoice=Step 1 - Fetch latest changes from %REMOTE%? (Y/N): 
if /I "%fetchChoice%"=="Y" (
    echo Fetching from %REMOTE%...
    git fetch %REMOTE%
    if %errorlevel%==0 (
        echo Fetch completed successfully.
    ) else (
        echo ERROR: Failed to fetch from %REMOTE%.
        goto end
    )
) else (
    echo Skipped fetch.
)

echo.

:: === STEP 2: CREATE BACKUP BRANCH IF IT DOESN'T EXIST ===
set /p backupChoice=Step 2 - Create backup branch '%BACKUP_BRANCH%'? (Y/N): 
if /I "%backupChoice%"=="Y" (
    git show-ref --quiet refs/heads/%BACKUP_BRANCH%
    if errorlevel 1 (
        echo Creating backup branch %BACKUP_BRANCH%...
        git checkout -b %BACKUP_BRANCH%
        if %errorlevel%==0 (
            echo Backup branch created.
        ) else (
            echo ERROR: Could not create backup branch.
            goto end
        )
    ) else (
        echo Backup branch %BACKUP_BRANCH% already exists. Skipping creation.
    )
) else (
    echo Skipped backup creation.
)

echo.

:: === STEP 3: CHECKOUT TARGET BRANCH ===
set /p checkoutChoice=Step 3 - Checkout branch "%BRANCH%"? (Y/N): 
if /I "%checkoutChoice%"=="Y" (
    for /f %%b in ('git branch --show-current') do set CURRENT_BRANCH=%%b
    if /I "!CURRENT_BRANCH!"=="%BRANCH%" (
        echo Already on branch %BRANCH%.
    ) else (
        git checkout %BRANCH%
        if %errorlevel%==0 (
            echo Switched to branch: %BRANCH%.
        ) else (
            echo ERROR: Could not checkout %BRANCH%.
            goto end
        )
    )
) else (
    echo Skipped branch checkout.
)

echo.

:: === STEP 4: MERGE WITH AUTO-RESOLVE ===
set /p mergeChoice=Step 4 - Merge %REMOTE%/%BRANCH% into local %BRANCH% with 'theirs' strategy? (Y/N): 
if /I "%mergeChoice%"=="Y" (
    echo Merging changes using 'theirs' strategy...
    git merge %REMOTE%/%BRANCH% --strategy=recursive --strategy-option=theirs
    if %errorlevel%==0 (
        echo Merge completed successfully.
    ) else (
        echo ERROR: Merge failed. Aborting merge...
        git merge --abort
        goto end
    )
) else (
    echo Skipped merge.
)

echo.

:: === STEP 5: PUSH TO REMOTE ===
set /p pushChoice=Step 5 - Push changes to %REMOTE%/%BRANCH%? (Y/N): 
if /I "%pushChoice%"=="Y" (
    echo Pushing changes...
    git push %REMOTE% %BRANCH%
    if %errorlevel%==0 (
        echo Push completed successfully.
    ) else (
        echo ERROR: Push failed.
        goto end
    )
) else (
    echo Skipped push.
)

echo.

:: === FINAL MESSAGE ===
echo ==========================================
echo Git operations completed successfully.
echo Branch: %BRANCH%
echo Remote: %REMOTE%
echo ==========================================
goto pause

:end
echo.
echo ==========================================
echo One or more steps failed. Check output above.
echo ==========================================

:pause
echo.
pause
exit /b
