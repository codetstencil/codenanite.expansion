using System.Collections.Generic;

namespace ZeraSystems.CodeStencil.Contracts
{
	public interface ICodeStencilCodeNanite
	{
		string Input { get; set;}
	    string Output { get; set; }
	    int Counter { get; set; }
        List<string> OutputList { get; set; }
        List<ISchemaItem> SchemaItem { get; set; }
	    List<IExpander> Expander { get; set; }
        List<string> InputList { get; set; }
        void ExecutePlugin();
	}
}

