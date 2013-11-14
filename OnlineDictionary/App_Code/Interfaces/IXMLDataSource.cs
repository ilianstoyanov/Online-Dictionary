using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// IXMLDataSource (ReadOnly) = Contains string List of XML Data source 
/// </summary>
public interface IXMLDataSource
{
    List<string> XmlDataFileNames
    {
        get;
    }
}