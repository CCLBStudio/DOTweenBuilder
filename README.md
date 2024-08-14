# DOTweenBuilder
A tool for the Unity DOTween plugin (by Demigiant), allowing you to easily create complex effects without coding anything.

## Description
Let's create interesting DOTween effects ! You can build all the available tweens and set the parameters as you like.
Everything takes place in a single Monobehaviour, directly in the inspector.
<br><br>
Almost every parameter has two ways to set its value : the first is the default way of setting a variable in the inspector. The second is through a dedicated ScriptableObject that will hold the value. For more information, see the dedicated section.

## How To Use
When you want to create a new tween effect simply add the component DOTweenBuilder on one of your GameObjects. To add a new effect, click the "New" button and select the one you desire. Once selected, a new entry will be created in
the list and you can tweak its settings as you need. You will then need a reference to this DOTweenBuilder and call its Play() method.

## Installation
You can add this tool to your project by downloading the unity package file in the release section or by cloning the repository in your project.

#### Requirement
You (obviously) need to have DOTween installed in your project. If you don't, make sure to install it before you import this repository.

#### Downloading The Unity Package
The simplest solution is to download the .unitypackage file that is in the release section.
<br>
[![Download UnityPackage](https://img.shields.io/badge/download-unitypackage-blue.svg)](https://github.com/AymNAym/DOTweenBuilder/releases/download/v1.0.0/DOTweenBuilder-1.0.0.unitypackage)

#### Cloning The Project
You can also clone the project anywhere you want in your Unity project. The url for the git clone command is the following : https://github.com/AymNAym/DOTweenBuilder.git

#### Downloading The Zip File
If you want to go for the zip file method, simply download the project as a zip file, unzip it and add the DOTweenBuilder folder in your project. It can be added anywhere, as long as it is in the Assets folder.

## Documentation
### WARNING
#### The DOTween Builder system is strongly dependant on its folder hierachy. You can move the main folder anywhere you want in your project, but do not change its internal structure.
When you call the Play() method you basically create a new Sequence and you populate it with the tweens you set up. It means that once you called the Play() method, any setting changed will not be taken into account unless you recall the Play() method.
<br>
Mostly every parameter is tagged with a Tooltip attribute to help you understand its goal.

### DOTween Builder Settings
Most of the settings of this component are straightforward, except maybe the Play Conflict Strategy : this setting determines how the builder will behave if you call the Play() method while a current sequence is still playing.

### Element Settings
Each existing element has a combination of common and unique settings. If you are not sure what a setting does, refer yourself to the official DOTween documentation : https://dotween.demigiant.com/documentation.php
