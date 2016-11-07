﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UIControls;

public partial class bjm_master_MasterPage  : TemplateMasterPage
{
    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        PageManager = CMSPortalManager1;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        this.ltlTags.Text = this.HeaderTags;
    }
}
