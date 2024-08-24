# DOTweenBuilder
A tool for the Unity DOTween plugin (by Demigiant), allowing you to easily create complex effects without coding anything.

## Description
Let's create interesting DOTween effects ! You can build all the available tweens and set the parameters as you like.
Everything takes place in a single Monobehaviour, directly in the inspector.
<br><br>
Almost every parameter has two ways to set its value : the first is the default way of setting a variable in the inspector. The second is through a dedicated ScriptableObject that will hold the value. For more information, see the dedicated section.

## How To Use
When you want to create a new tween effect simply add the component DOTweenBuilder on one of your GameObjects :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/DOTweenBuilder.png)
<br>

To add a new effect, click the "New" button and select the one you desire. Once selected, a new entry will be created in
the list and you can tweak its settings as you need :

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/NewEntry.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/NewEntry2.png)
<br>

You will then need a reference to this DOTweenBuilder and call its Play() method :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/Play.png)
<br>

## Installation
You can add this tool to your project by downloading the unity package file in the release section or by cloning the repository in your project.

#### Requirement
You (obviously) need to have DOTween installed in your project. If you don't, make sure to install it before you import this repository.

#### Downloading The Unity Package
The simplest solution is to download the .unitypackage file that is in the release section.
<br>
[![Download UnityPackage](https://img.shields.io/badge/download-unitypackage-blue.svg)](https://github.com/AymNAym/DOTweenBuilder/releases/download/v1.0.1/DOTweenBuilder-1.0.1.unitypackage)

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
<br><br>
You may have notice this symbol on the left of many variables :

<br>
<img src="https://github.com/AymNAym/DOTweenBuilderImages/blob/main/DOTween-Icon-Normal.png" alt="Mon Image" width="200" height="200"/>
<br>

This means that this value can be set in two different ways. The first is the default way to tweak a value in the inspector, and the second one is through a dedicated scriptable object :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/VariableDefault.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/VariableScriptableObject.png)
<br>

The Scriptable Object is just a very simple object that only contains one field of the type of the variable. In the example just above, the Scriptable Object contains one field of type Transform :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/ScriptableValue.png)
<br>

This allow you to reference this Scriptable Object in another script, and modify the value in it as you wish. The next time you will call the Play() method of this DOTweenBuilder, the new value will be used. For example, this can be usefull
when you want to disable/enable a specific tween depending on runtime conditions :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/DOTweenBuilderEnableBySo.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/UsingSoValueInScript.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/TesterInspector.png)


#### Important : In the editor, when exiting playmode, the Scriptable Object will automatically reset its value to the value it had when entering playmode. This mimic the behaviour of a Scriptable Object in a final build.

### Creating Custom Element
If you need, you can create a custom element that you will be able to use as a DOTweenBuilder element. Navigate to the DOTweenBuilder folder :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/MainFolder.png)

When you click the "Add New" button on a DOTweenBuilder component, it actually looks into this folder and uses the folder names to build the context menu that you see. Each script in every folder then populates the related context menu element :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/ContextMenuBuild.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/ContextMenuElementsBuild.png)
<br>

So, let's say you want to create a new element for a custom type named "MyCustomType". You need to create a new folder in the DOTweenBuilder folder that will contains the script for your custom element :
<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomFolder.png)

You then need to create the script that will be usable as a DOTweenBuilder element. The naming convention for this kind of script is DOTween{YOUR-EFFECT-NAME} :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/DOTweenCustomElement.png)
<br>

This script needs to derive from DOTweenGenericElement<,>. This is a generic type that takes two arguments : the first one is the type of your target, and the second one is the type of the value you want to tween. In our example
the first parameter is MyCustomType, which contains a public float "valueToTween". Our second parameter is then float :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementScript1.png)
<br>

As you see, the compilator gives you an error. Thats because you need to implement the asbtract method "Generate()" that will contains the tweening logic. This method returns a Tween object :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementScript2.png)

You can then write the logic you want for your element, using the DOTween methods :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementScript3.png)

Last but not leat, you need to add the [Serializable] tag to your class :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementScript4.png)

At this point you can use this component in any DOTweenBuilder :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementInContext.png)

Note that you can add serialized properties in your custom element :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementScript5.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementInContext2.png)

And if you want your settings to be swappable with a dedicated Scriptable Object (as seen in the previous section), you need to use the DOTweenVariable objects :<br>

![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementScript6.png) ![](https://github.com/AymNAym/DOTweenBuilderImages/blob/main/CustomElementInContext3.png)


























