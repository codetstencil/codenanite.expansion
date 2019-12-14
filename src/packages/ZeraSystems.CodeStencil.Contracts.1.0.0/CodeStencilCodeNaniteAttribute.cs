using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace ZeraSystems.CodeStencil.Contracts
{
    [MetadataAttribute]
    public class CodeStencilCodeNaniteAttribute : Attribute, ICodeStencilCodeNaniteAttribute
    {
        /// <summary>
        /// [0] - Publisher
        /// [1] - Title
        /// [2] - Description
        /// [3] - Version No.
        /// [4] - Label
        /// [5] - Release Date
        /// </summary>
        /// <param name="metadata"></param>
        public CodeStencilCodeNaniteAttribute(string[] metadata)
        {
            PluginDetails = metadata;
        }

        public string[] PluginDetails {get; set;}

    }
}
