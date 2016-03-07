# Transpile .Net 
Transpile POCOs into any language.

The classic case is a JavaScript application that accesses .Net JSON endpoints and needs to keeps its view model definittions in sync.  Although libaries like Bridge.Net and [NetJs](https://github.com/praeclarum/Netjs) completely transpile C# to JavaScript and TypeScript, including the inner logic, Transpile .Net is a lightweight one-trick pony that let's you fully customize the output including choosing which classes are transpiled. You can also set default values for all you properties.

Assign ITranspilable (or a self-defined type) to your C# models.  They'll be scaffolded to extract the structure and default values then used to fill in templates. The master template defines the resulting code page, and the model template defines how the models are produced inside the code page.

![Model Transpilation](https://github.com/hobozero/Transpile.Net/blob/master/Transpile.png)

## To create a new Transpiler
1.  Create a new 4.5.1 (or greater) class project, and reference the Transpiler assembly.
2.  Subclass TranspilerBase.
3.  Add ITranspilable to your C# DTOs.  You can choose your own parent type if desired.
4.  Create a folder named "templates" in your project and add a master and model file with this naming convention:
 	* *TranspilerSubClassName.Master.ext*  - The container.
	* *TranspilerSubClassName.Model.ext* - The models
    *	For example if your class in #2 above is ES5Transpiler, you'd name your master template *ES5Transpiler.Master.js*
	
5. Use the variables in your templates
* **[#GLOBAL_NAMESPACE]** - Where the top-level namespace for all your compiled models will be inserted
* **[#MODELS]** - Where the models should be inserted
* **[#NAMESPACE]** - Where model's namespace will be inserted
* **[#MODELNAME]** - Where the model's identifier will be inserted.
* **[#MODEL]** - Where the model should be inserted
* You can also define your own variables to be filled in later.

### Default Values
The System.ComponentModel.DefaultValueAttribute has been extended to properly initilize the property values of the resulting transpiled code.  From [MSDN](https://msdn.microsoft.com/en-us/library/system.componentmodel.defaultvalueattribute(v=vs.110).aspx)
>A DefaultValueAttribute will not cause a member to be automatically initialized with the attribute's value. You must set the initial value in your code.

Transpile .Net extends this behavior and completes the initialization.

```csharp
//This property will be auto-initialized to 99.9
[DefaultValue(99.9)]
public double FloatProperty { get; set; }
```

### Tests
Since Transpile .Net first **generates**, then **tests** JavaScript code using [Jasmine](http://jasmine.github.io/) powered by [Chutzpah](https://github.com/mmanela/chutzpah), the C# test project must be run before the JavaScript test project. Adding Mike Harrignton provides a solid set of [Chutzpah install](http://blogs.msdn.com/b/matt-harrington/archive/2014/10/27/javascript-unit-testing-using-the-chutzpah-test-runner-in-visual-studio.aspx) instructions.
