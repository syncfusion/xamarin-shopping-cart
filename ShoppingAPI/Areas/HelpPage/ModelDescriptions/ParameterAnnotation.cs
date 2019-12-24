using System;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// This class is responsible for ParameterAnnotation.
    /// </summary>
    public class ParameterAnnotation
    {
        /// <summary>
        /// Gets or sets the annotation attribute.
        /// </summary>
        public Attribute AnnotationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public string Documentation { get; set; }
    }
}