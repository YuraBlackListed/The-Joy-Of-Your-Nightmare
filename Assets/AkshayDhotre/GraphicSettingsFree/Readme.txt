Starting from Unity version 2019 the default display resolution dialogue became unavailable.
This is why I made a really basic but customizable graphic options menu.

Link to online documentation : https://bitbucket.org/akshay2001/basicgraphicssettignsmenu/wiki/Home

Features:

-Save the graphic settings to XML files, so players can edit the settings externally too.
-Ability to create new game specific option.

Instruction for use:

1)Drag "GraphicMenu.prefab" from the folder GraphicSettingsFree/Prefabs into the scene.
2)Click on the prefab you added in the scene and change the path where the file will be
  saved and the name of the file in the GraphicSettingSaveManager component attached to the GraphicMenu game object
3)The menu can be toggled by pressing the 'P' key on the keyboard. You can change it in the menu toggler component attached to the same object.
4)The graphic settings are loaded as the game starts and the settings are saved when you click the "Apply" button in-game.
5)Each of the three options, the resolution, screen mode, and quality level option has a fallback option.
  This option is loaded by default when the player edits the XML file externally and adds invalid data in the XML file
  For example: Adding a string or character where an integer/number was expected.
  <Screenwidth>1024a</Screenwidth> will result in using the fallback options.

For more info and a tutorial on how to create a new option check out the online documentation: 

Support:

In case you need any help or have any suggestions contact me at- akshayDhotreUAP@outlook.com