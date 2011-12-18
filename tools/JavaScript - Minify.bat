@echo off

set folder=..\Website\javascript\
copy/b %folder%\graphite.js %folder%\all.js
copy/b %folder%\all.js + %folder%\graphite.blocks.js %folder%\all.js
copy/b %folder%\all.js + %folder%\graphite.blocks.navigation.js %folder%\all.js
copy/b %folder%\all.js + %folder%\graphite.blocks.navigation.menu.js %folder%\all.js

java -jar yuicompressor-2.4.2\build\yuicompressor-2.4.2.jar %folder%all.js -o %folder%all.min.js

echo .
echo .
echo .
echo Selected Javascript files are minified and combined into '%folder%all.min.js'.
pause
