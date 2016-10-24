using System;
using System.Xml;
using PayaBL.Common.PortalCach;

public class ClearCacheTask //: ITask
{
    // Methods
    public void Execute(XmlNode node)
    {
        try
        {
            Caching.Clear();
        }
        catch (Exception)
        {
        }
    }
}

 

 
