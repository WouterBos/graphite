@echo off

:: Checks JavaScript files for code convention issues
c:\Python27\Scripts\gjslint.exe --custom_jsdoc_tags "since,version,fileOverview,namespace,requires,example,private,ignore,name,memberOf,this" "..\Website\internal\JavaScript\graphite.js" "..\Website\internal\JavaScript\graphite.blocks.js" "..\Website\internal\JavaScript\graphite.blocks.navigation.js" "..\Website\internal\JavaScript\graphite.blocks.navigation.menu.js" "..\Website\internal\JavaScript\graphite.blocks.navigation.resultsfilter.js" "..\Website\internal\JavaScript\graphite.blocks.accordion.js" "..\Website\internal\JavaScript\graphite.blocks.gmap3.js" "..\Website\internal\JavaScript\graphite.blocks.widgets.js" "..\Website\internal\Javascript\graphite.internal.js" "..\Website\internal\JavaScript\graphite.blocks.sitecore.js"

pause