@echo off
REM This script adds all changes, commits them with a standard message, and pushes to multiple branches.

REM Step 1: Stage all changes
git add -A

REM Step 2: Commit changes
git commit -m "Updated SalesPerson components"

REM Step 3: Push to multiple branches
git push origin HEAD:master HEAD:mast HEAD:Team1

REM Done
echo "Changes pushed to master, mast, and Team1 branches successfully."
pause
