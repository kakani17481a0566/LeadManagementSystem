@echo off
cd /d "%~dp0"

echo The following files have uncommitted changes:
git status
echo.

set /p confirm="This will discard ALL changes and delete untracked files. Proceed? (Y/N): "
if /i not "%confirm%"=="Y" (
    echo Operation cancelled.
    pause
    exit /b
)

:: Confirm restore of tracked files
echo.
set /p confirm_restore="Restore tracked file changes (git restore .)? (Y/N): "
if /i "%confirm_restore%"=="Y" (
    git restore .
    echo Tracked changes restored.
    echo.
    echo === Git Status After Restore ===
    git status
) else (
    echo Skipped restoring tracked changes.
)

:: Confirm cleaning of untracked files/folders
echo.
set /p confirm_clean="Delete untracked files and folders (git clean -fd)? (Y/N): "
if /i "%confirm_clean%"=="Y" (
    git clean -fd
    echo Untracked files and folders deleted.
    echo.
    echo === Git Status After Clean ===
    git status
) else (
    echo Skipped deleting untracked files.
)

echo.
echo [DONE] Cleanup operation completed.
pause
