using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMR
{
    public class MenuData
    {
        public int? RoleId { get; set; }      // Assuming RoleId is a string (adjust if it's an int)
        public string MenuName { get; set; }
        public string ItemName { get; set; }
        public int OrderBy { get; set; }        // Assuming OrderBy is an int
        public int ItemID { get; set; }
        public int MenuId { get; set; }
        public int MenuOrderBy { get; set; }    // Alias for Menus.OrderBy
        public string UserId { get; set; }      // Assuming UserId (ui.Id) is a string (adjust if it's an int)
        public string FormName { get; set; }

        //public int? RoleId { get; set; }      // Assuming RoleId is a string (adjust if it's an int)
        //public string MenuName { get; set; }
        //public string ItemName { get; set; }
        //public string FormName { get; set; }
        //public int ItemID { get; set; }

        //public int MenuId { get; set; }
        //public int ParentMenuId { get; set; }
        //public int OrderBy { get; set; }        // Assuming OrderBy is an int
        //public string ID { get; set; }      // Assuming UserId (ui.Id) is a string (adjust if it's an int)
        
    }
}