
////////////////////////////////////////////////////////
//      These EcmaScript 5 models were compiled using 
//      Model Transpiler.Net.
//
//      Variables in the format /*[#keyname]*/ are 
//      replaced at compile with the values in a supplied 
//      dictionary.
//
////////////////////////////////////////////////////////

//Generated at
///*[#TIME_CREATED]*/
(function(global){
    //Set top-level namespace
    var currentns,topns = global./*[#GLOBAL_NAMESPACE]*/ || {};

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
    /*[#MODELS]*/

})(this);