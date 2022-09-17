--- WITH ENTITY LOGIC YOU ARE ABLE TO ---

- create a definition class: contains static/constant/permanent values (Name, Description, Sprite, ...)
- create a data class: contains work fields/properties with dynamic data unique to the instance
- in the data class the user has access to all fields/properties of the definition class
- the user only needs to instantiate the concrete data class, loading of the definition object happens automatically
- only one definition object is loaded for any number of instances of the same data class
- the user can serialize and deserialize (save/load) only the custom values of the instantiated data class (Gold.CurrentAmount, Axe.CurrentDurability, ...), the rest of the data is stored in the definition object
- UI for easy entity management



--- INFORMATION ---

- for each entity you need at least 2 classes, one for the definition and one for the data
  Definition.Sword - Data.Sword
  Definition.Apple - Data.Apple

- thought:
- if there are a lot of entities the project will contain a lot of files
- but on the other hand, for each entity it is easy to write behavior that is unique to it because it has its own class



--- IMPORTANT / RESTRICTIONS / LIMITATIONS --- 

- the namespace format must match the folder structure where the .cs files are located

Entity.Definition -> ...\Entity\Definition\*.cs
Entity.Database   -> ...\Entity\Database\*.cs
Entity.Data       -> ...\Entity\Data\*.cs

Example.Entity.Definition -> ...\Example\Entity\Definition\*.cs
Example.Entity.Database   -> ...\Example\Entity\Database\*.cs
Example.Entity.Data       -> ...\Example\Entity\Data\*.cs

- the EntityDatabase attribute must be applied to the every database class
- each entity related class (Definition, Database, Data) must have its own .cs file; if there are multiple classes in one .cs file, the system will not work properly

- single use of a generic type in a database; if one type is used more than once, for example Definition.Currency in DB_1<Definition.Currency> and DB_2<Definition.Currency>, an error will occur
- DbContext must be instanciater on the scene before using any entity



--- DISCLAIMER: you need to know some programming to understand what's going on here, not everything is explained
--- STEPS FOR CREATING ENTITIES --- FOLLOW THE EXAMPLE FILES IN THE EXAMPLE FOLDER



- STEP 1: - Definition namespace - Entity.Definition

create an abstract Currency class derived from the Definition.DataEntity class
   -> public abstract class Currency : Definition.DataEntity

create a concrete class derived from the Currency class (Diamond, Gold)
   -> public class Gold : Definition.Currency



- STEP 2: - Database namespace - Entity.Database

create a CurrencyDatabase class derived from GearFramework.Common.Database<T> and pass in a specific generic type, in this case Definition.Currency
   -> public class CurrencyDatabase : GearFramework.Common.Database<Definition.Currency>

the EntityDatabase attribute must be applied to the class
   -> [EntityDatabase]

the database will contain all derived objects from the generic type



- STEP 3: - Data namespace - Entity.Data

create an abstract Currency class derived from the Data.DataEntity class
   -> public abstract class Currency : Data.DataEntity

create an generic abstract Currency<T> class derived from the Currency class, and limit the value of T to Definition.Currency
   -> public abstract class Currency<T> : Data.Currency where T : Definition.Currency

the generic abstract class Currency<T> needs to load data from the database and will do so with this line of code
   -> [NonSerialized] protected static T definition = Database.CurrencyDatabase.Instance.GetData<T>();

the other lines from the example are used to easily transfer data without casting, for example
   -> protected abstract Definition.Currency CurrencyDefinition { get; }

create concrete class derived from the generic abstract Currency<T> class, and for generic type pass a corresponding Definition type
   -> public class Gold : Data.Currency<Definition.Gold>

make Gold a serializable class so you can save it later
   -> [Serializable]



- STEP 4: - Reuse -

for any new currency you only need to create a Definition and a Data class

Definition namespace
   -> public class Diamond : Definition.Currency

Data namespace
   -> public class Diamond : Currency<Definition.Diamond>



- STEP 5: - Populte data -

entity editor: https://youtu.be/MuolyGKK5uw?t=89



---


- 'Item' example is more complex; uses interfaces instead of a common class because it branches into several unique groups
- note that much of the code of the abstract classes is similar
- the differences are type specific
  - Items should be loaded from ItemDatabase and Currencies should be loaded from CurrencyDatabase
  - also, Items don't have a MaxAmount field/prop as a Currency, but they do have a Stackable field/prop
- if you want you can put everything in one database 'public class DataEntityDatabase : Database<Definition.DataEntity> { }', but I personally like to separate the logical units
