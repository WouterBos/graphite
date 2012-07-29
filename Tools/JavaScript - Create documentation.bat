@echo off

:: If this line below fails, try to replace "java" with something like "C:\Program Files (x86)\Java\jre6\bin\java.exe"
java -jar jsdoc-toolkit\jsrun.jar jsdoc-toolkit\app\run.js -d=..\Website\Internal\Pages\Documentation\JavaScript\ -D="title:Graphite JavaScript Reference" -t=jsdoc-toolkit\templates\codeview -p -v "..\Website\javaScript\graphite.js" "..\Website\javaScript\graphite.blocks.js" "..\Website\javaScript\graphite.blocks.navigation.js" "..\Website\javaScript\graphite.blocks.navigation.menu.js" "..\Website\internal\javascript\graphite.internal.js"

pause
