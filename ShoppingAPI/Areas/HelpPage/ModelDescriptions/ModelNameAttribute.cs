using System;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Use this attribute to change the name of the <see cref="ModelDescription" /> generated for a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes the model name attribute.
        /// </summary>
        /// <param name="name"></param>
        public ModelNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name value.
        /// </summary>
        public string Name { get; }
    }
}