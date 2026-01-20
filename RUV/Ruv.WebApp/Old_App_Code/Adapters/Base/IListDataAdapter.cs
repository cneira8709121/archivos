using System.Collections.Generic;

namespace Ruv.WebApp.Presentation.Adapters.Base
{
    /// <summary>
    /// Describes a paged list adapter for display purposes
    /// </summary>
    internal interface IListDataAdapter<T>
    {
    
        int VirtualItemCount();

        IList<T> GetData();

        IList<T> GetData(int startRow, int maxRows);

    }

}