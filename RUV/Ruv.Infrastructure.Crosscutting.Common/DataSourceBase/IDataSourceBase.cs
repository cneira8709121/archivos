using System.Collections.Generic;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Descripción breve de IDataSource
/// </summary>
public interface IDataSourceBase
{
    event Error ErrorConsulta;
	int VirtualItemCount();
    IList<object> GetData(int startRow, int maxRows);
    IList<object> GetData(int startRow, int maxRows, string sortColumns);
}