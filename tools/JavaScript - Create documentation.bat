@echo off

java -jar jsdoc-toolkit\jsrun.jar jsdoc-toolkit\app\run.js -d=..\Website\Internal\Pages\Documentation\JavaScript\ -D="title:Graphite JavaScript Reference" -t=jsdoc-toolkit\templates\codeview -p -v "..\Website\javaScript\graphite.js" "..\Website\javaScript\graphite.blocks.js" "..\Website\javaScript\graphite.blocks.navigation.js" "..\Website\javaScript\graphite.blocks.navigation.menu.js" "..\Website\internal\javascript\graphite.internal.js"

echo .
echo .
echo .
echo Batch is done.
pause
