@echo off

echo Checking JavaScript files for code convention issues
c:\Python27\Scripts\gjslint.exe --custom_jsdoc_tags "since,version,fileOverview,namespace,requires,example,private,ignore,name,memberOf,this" "..\Website\javaScript\graphite.js" "..\Website\javaScript\graphite.blocks.js" "..\Website\javaScript\graphite.blocks.navigation.js" "..\Website\javaScript\graphite.blocks.navigation.menu.js" ".Website\internal\javascript\graphite.internal.js"

echo .
echo .
echo .
pause