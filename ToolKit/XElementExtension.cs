using System.Linq;
using System.Xml.Linq;

namespace H2HY.Toolkit
{
    /// <summary>
    /// Extends XElement for simplified value access.
    /// </summary>
    public static class XElementExtension
    {
        /// <summary>
        /// Finds and returns the value of an child-element as string.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="elementName">elementname of the child</param>
        /// <returns>returns values as string or string.Empty</returns>
        public static string GetElementValue(this XElement e, string elementName)
        {
            var c = e.Elements().FirstOrDefault(i => i.Name.LocalName == elementName);
            if (c is not null)
            {
                return c.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Finds and returns the given attribit from the current Element.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="elementAttributName">attribut name</param>
        /// <returns>returns attribut as string or string.Empty</returns>
        public static string GetElementAttribut(this XElement e, string elementAttributName)
        {
            var c = e.Attributes().FirstOrDefault(i => i.Name == elementAttributName);
            if (c is not null)
            {
                return c.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}