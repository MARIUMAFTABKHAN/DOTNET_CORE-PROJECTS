
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDSN;
public class RoleItemResult
{
    public int ItemID { get; set; }
    public string MenuName { get; set; }
    public string ItemName { get; set; }
    public bool IsActive { get; set; }
    public int RoleID { get; set; }
}
