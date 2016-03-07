/// <reference path="/Scripts/jasmine.js"/>
/// <reference path="./../Transpiled/jsmodel.js"/>



describe("decoratedClassWithMoreProperties", function () {

    var propertyUnderTest = 'stringProperty';

    it("should have a top-level namespace", function() {
        //Assert
        expect(sveModel).toBeDefined();
    });

    it("should have property named 'StringProperty'", function () {
        //Arrange
        console.log("should have property named 'StringProperty'");
        //Act
        var modelUnderTest = new sveModel.transpilation.test.models.decoratedClassWithMoreProperties();

        //Assert
        expect(modelUnderTest.hasOwnProperty(propertyUnderTest)).toBe(true);
    });

    it("should have property 'StringProperty' set to a default value", function () {
        //Arrange
        
        //Act
        var modelUnderTest = new sveModel.transpilation.test.models.decoratedClassWithMoreProperties();

        //Assert
        expect(modelUnderTest[propertyUnderTest]).toBe('defaultValue');
    });

    it("should override the defaultValue of StringProperty", function () {
        //Arrange
        var newVal = "New value";
        var initVals =
        {
            "stringProperty": newVal
        };

        debugger;
        //Act
        var modelUnderTest = new sveModel.transpilation.test.models.decoratedClassWithMoreProperties(initVals);

        //Assert
        expect(modelUnderTest[propertyUnderTest]).toBe(newVal);
    });
});