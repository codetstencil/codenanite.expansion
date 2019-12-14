using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeraSystems.CodeStencil.Contracts
{
    public interface ICodeNaniteMetaData
    {
        string PluginName {get; }
        string FullNameSpace { get; }
        string Details {get; }
        string VersionNo { get; }
        DateTime DateAdded {get; }
        string Publisher { get; }
    }
}
