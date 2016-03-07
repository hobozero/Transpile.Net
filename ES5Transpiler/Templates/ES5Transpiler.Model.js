
//Prepare namespaces
currentns = defineNamespace.call(topns, "/*[#NAMESPACE]*/".split('.'));
    
//Initialize object
currentns./*[#MODELNAME]*/ = function(init){
    var that = this;

    //Defaults for this type
    var o = /*[#MODEL]*/;

    //Override default values if provided in constructor.
    //Properties object with descriptors should be included if Object.Create() used to bypass constrcutor.
    if (init){
        o = overrideDefaults.call(that, init, o);
    }
    return o;
};

