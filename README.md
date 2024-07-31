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
