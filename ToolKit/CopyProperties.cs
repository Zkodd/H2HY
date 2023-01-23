using System;
using System.Linq;
using System.Reflection;

namespace H2HY.Toolkit
{

    /// <summary>
    /// Collection of statics methods for working with properties.
    /// </summary>
    public class CopyProperties
    {
        /// <summary>
        /// Copies all setable properties from one to another object. 
        /// Source: https://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <exception cref="Exception">Source or/and Destination Objects are null</exception>
        public static void Copy(object source, object destination)
        {
            if (source is null || destination is null)
            {
                throw new Exception("Source or/and Destination Objects are null");
            }
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                                && targetProperty != null
                                && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true)!.IsPrivate)
                                && (targetProperty.GetSetMethod()!.Attributes & MethodAttributes.Static) == 0
                                && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty };
            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }
    }
}
