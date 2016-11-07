using System;
using System.IO;
using System.Linq;
using System.Net;

using CMS.CustomTables;
using CMS.Ecommerce;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.UIControls;

using CMS.CustomTables;
public partial class bjm_termplate_test :  TemplatePage

{
    protected void Page_Load(object sender, EventArgs e)
    {
        //var newItem = new CustomTableItem("bjmwx.testCrud").f;
        var aText = CustomTableItemProvider.GetItems("bjmwx.testCrud").FirstOrDefault().ItemGUID;
        youname.Text = aText.ToString();

    }
}