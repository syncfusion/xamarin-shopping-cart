using System;
using System.Reflection;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// This interface is responsible for ModelDocumentationProvider.
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation based on MemberInfo.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation based on Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetDocumentation(Type type);
    }
}