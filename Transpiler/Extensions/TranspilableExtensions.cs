using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Transpilation.Interfaces;

namespace Transpilation
{
    static class TranspilableExtensions
    {

        /// <summary>
        /// Reads the DefaultValue attribute of each property and sets the default value.
        /// </summary>
        /// <param name="model"></param>
        public static void SetDefaults(this object model)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(model))
            {
                DefaultValueAttribute myAttribute =
                    (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];

                if (myAttribute != null)
                {
                    //Instantiate a new Type if decorate with "new" keyword
                    if (myAttribute.Value.ToString().StartsWith("new "))
                    {
                        var propObj = Activator.CreateInstance(property.PropertyType) as ITranspilable;
                        if (propObj != null)
                        {
                            propObj.SetDefaults();
                            property.SetValue(model, propObj);
                        }
                    }
                    else
                    {
                        if (property.PropertyType == typeof(System.Decimal))
                        {
                            property.SetValue(model, Convert.ToDecimal(myAttribute.Value));
                            //throw new InvalidCastException(string.Format("Cannot convert value {0} to decimal since decimals cannot be accurately represented as doubles.", myAttribute.Value));
                        }
                        else
                        {
                            property.SetValue(model, myAttribute.Value);
                        }
                    }

                }
            }
        }
    }

    
}

