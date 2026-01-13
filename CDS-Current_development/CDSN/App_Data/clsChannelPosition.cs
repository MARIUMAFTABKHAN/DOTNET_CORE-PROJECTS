using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
public  class clsChannelPositions
{ 
    public  int ChannelId { get; set; }
    public  string ChannelType { get; set; }
    public  int CurPosition { get; set;}
    public  int Id { get; set;}
    public  string  IsAssigned   { get; set; }
    public  int PrevPosition { get; set;}
    public  string ShortName { get; set;}
    public  DateTime WEF { get; set; }    
    public  string TypeName { get; set;}       


}