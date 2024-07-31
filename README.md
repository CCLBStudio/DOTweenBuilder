# DOTweenBuilder
A tool for the Unity DOTween API (by Demigiant), allowing you to easily create complex effects without coding anything.

## Description
Let's create interesting DOTween effects ! You can build all the available tweens and set the parameters as you like.
Everything takes place in a single Monobehaviour, directly in the inspector.
<br><br>
Almost every parameter has two ways to set its value : the first is the default way of setting a variable in the inspector. The second is through a dedicated ScriptableObject that will hold the value. For more information, see the dedicated section.

## How To Use
When you want to create a new tween effect simply add the component DOTweenBuilder on one of your GameObjects. To add a new effect, click the "New" button and select the one you desire. Once selected, a new entry will be created in
the list and you can tweak its settings as you need. You will then need a reference to this DOTweenBuilder and call its Play() method.

## Installation
You can add this tool to your project by downloading the project as a zip file and then add it to your project, or by using the Unity Package Manager (recommended)

#### Requirement
You (obviously) need to have DOTween installed in your project. If you don't, make sure to install it before you import this repository.

#### Downloading The Zip File
If you want to go for the zip file method, simply download the project as a zip file, unzip it and add the DOTweenBuilder folder in your project. It can be added anywhere, as long as it is in the Assets folder.

#### Using The Package Manager (2018.3 or later) NOT YET AVAILABLE
For this method, open the Package Manager tab in your Unity project (Window/Package Manager). Click the "+" button (top left of the window) and select "Add package from git URL..."
Enter the following url : https://github.com/AymNAym/DOTweenBuilder.git
