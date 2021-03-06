(function(global){
    //Set top-level namespace
    var currentns,topns = global.sveModel = global.sveModel || {};

    function defineNamespace(namespaces) {
        var ns;

        if (ns = namespaces[0]){
            var top = this[ns] = this[ns] || {};
            return defineNamespace.call(top, namespaces.slice(1));
        }
        else{
            return this;
        }
    };
    /*
        Simulate the behavior of the ES5 Object.Create property initialization.

        defaultVals: Enumerable properties containing the default values of the object.
            Arrays are defaulted to [].
        initVals: Enumerable properties containing the values to be initialized.
            The value must exist in defaultVals to be included in the object.
    */
    function overrideDefaults(source, target){
        for (var p in source){
            //Only initialize the property if it is one of the 
            if (target.hasOwnProperty(p)) {
                target[p] = source[p];
            }
        }
        return target;
    }

    /************ Create Models *****************/
    
//Prepare namespaces
currentns = defineNamespace.call(topns, "transpilation.test.models".split('.'));
    
//Initalize object
currentns.decoratedClassWithMoreProperties = function(init){
    var that = this;

    //Defaults for this type
    var o = {
  "floatProperty": 99.9,
  "stringProperty": "defaultValue",
  "stringArrayProperty": null
};

    //override default values if provided in constructor
    //Properties object with descriptors should be included if Object.Create() used to bypass constrcutor
    if (init){
        o = overrideDefaults.call(that, init, o);
    }
    return o;
};


//Prepare namespaces
currentns = defineNamespace.call(topns, "transpilation.test.models".split('.'));
    
//Initalize object
currentns.decimalClass = function(init){
    var that = this;

    //Defaults for this type
    var o = {
  "fakeDecimal": 8.0
};

    //override default values if provided in constructor
    //Properties object with descriptors should be included if Object.Create() used to bypass constrcutor
    if (init){
        o = overrideDefaults.call(that, init, o);
    }
    return o;
};


//Prepare namespaces
currentns = defineNamespace.call(topns, "transpilation.test.models".split('.'));
    
//Initalize object
currentns.decoratedClass = function(init){
    var that = this;

    //Defaults for this type
    var o = {
  "newName": null
};

    //override default values if provided in constructor
    //Properties object with descriptors should be included if Object.Create() used to bypass constrcutor
    if (init){
        o = overrideDefaults.call(that, init, o);
    }
    return o;
};



})(this);