CORNERED

An open-source portfolio project by Tamas Gregus. 

Input: New Input System
Render Pipeline: Universal RP
Unity: 2022.2.17f1

Naming conventions:
Prefixes in names:
C: Components
SO: ScriptableObject
UI: Component used as a UI
I: Interface
E: Enum

Folder structure:
Assets/Scenes folder has two subfolders: Private and Public. The reason for this, is because if I would use a testing scene (invisible for players, or would not be included in builds), those would be placed in here. 

Architecture:
Init scene sets up some managers used in other scenes with DontDestroyOnLoad. It also contains CMainLoader, which loads the first real game scene. 
Camera is in this first Init scene only to surely render something, instead of leaving glyphs on the screen. 
Manager classes all follow the Singleton pattern. EventManager is a class with generic methods to be able to broadcast events to subscribed objects. Basically it also works as the Observer pattern. 
CBeltController manages an object pool object, which is following the pattern and its used from Unity's API. 
GameDefinitions contain all of the enums. All of them have a Count value as the last element. It's used for avoiding unnecessary allocations created by Enum.GetValues(), because I can process all enum elements by managing them as integers in a loop. 
GameInput implements the New Input System related methods, which are automatically generated, when the editor user modifies the Input Action Asset (named as GameInputActions in the project). When the implemented methods get called by the Input System, the GameInput calls the relevant actions, which can be subscribed in various places in the project. 

Architectural examples:
CIngredient, CProduct, CShieldProduct, CWeaponProduct, CAdditionalProduct are meant to make the different kinds of ingredients separately manageable. For example, all ingredients are pickable. But only the products (shields, weapons, possibly new products added in the future) are equipable and usable in character inventories. 
CWeapon is managed by both player and the enemy (AI). The unique behaviors are in CPlayerWeapon and CEnemyWeapon. The player weapon processes inputs to shoot, and he can shoot every single time, when there isn't happening any cooldown. But the enemy shooting input comes from the AI. And while it can only shoot, when the weapon cooled down, it will only shoot, when it has direct "eye contact" with the player. 

External libraries:
Serialized Dictionary Lite:
https://assetstore.unity.com/packages/tools/utilities/serialized-dictionary-lite-110992

Game Design decisions:
In this game, there are bossfights. The player needs to defeat an enemy multiple times. 
To avoid repetition, I made the player to be able to assemble his own equipment for each fight. 
The equipment contain a mandatory weapon, and an optional shield and/or extra abilities. 
These can be assembled from an infinite conveyor belt, which generates ingredients for these equipments with various random chance values as set by the designer. 
The recipes are printed on the walls, which are also configurable by the designer. The in-game recipe list automatically gets regenerated if the recipes get changed. 
Stronger weapons and stronger shields need more time to assemble, because they need more ingredients. 
To make the player be able prepare for the fight, I have added a timer, which can be reset. 
I wanted to add some chance to use tactics in fight, so I have added an enemy equipment hint, which can help the player to construct his weapon and shield more effectively. 
To allow the fights have some variations, after defeating an enemy, new recipes become available. 
I also wanted to give the player an ultimate goal. So during the preparing times, the player can either invest time into assembling stronger equipments, or he can hold the exit door button for a period of time. It increases the percentage of the exit door opening, so he can finish the game, once it has been fulfilled. 
