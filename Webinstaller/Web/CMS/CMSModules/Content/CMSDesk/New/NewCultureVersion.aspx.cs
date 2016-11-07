﻿using System;
using System.Text;

using CMS.Helpers;
using CMS.Localization;
using CMS.DocumentEngine;
using CMS.UIControls;

using TreeNode = CMS.DocumentEngine.TreeNode;

[Title("Content.NewCultureVersionTitle")]
public partial class CMSModules_Content_CMSDesk_New_NewCultureVersion : CMSContentPage
{
    #region "Properties"

    /// <summary>
    /// Gets the target culture code
    /// </summary>
    protected string RequiredCulture
    {
        get
        {
            return QueryHelper.GetString("culture", LocalizationContext.PreferredCultureCode);
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Adds the script to the page
    /// </summary>
    /// <param name="script">JavaScript code</param>
    public override void AddScript(string script)
    {
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "NewCultureVersionScript", script, true);
    }


    protected override void OnInit(EventArgs e)
    {
        // Do not check changes
        DocumentManager.RegisterSaveChangesScript = false;

        // Do not redirect for non-existing document
        DocumentManager.RedirectForNonExistingDocument = false;

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string culture = RequiredCulture;

        newCultureVersion.RequiresDialog = RequiresDialog;
        newCultureVersion.Tree = Tree;
        newCultureVersion.NodeID = NodeID;
        newCultureVersion.Mode = Mode;
        newCultureVersion.RequiredCulture = culture;

        // Register progress script
        ScriptHelper.RegisterLoader(Page);

        // Check if the culture is valid
        bool isCultureAllowed = CheckPreferredCulture(culture);
        if (!isCultureAllowed)
        {
            ShowWarning(GetString("licensevalidation.newdocumentcultureversion"));
            newCultureVersion.Visible = false;
            return;
        }

        if (NodeID > 0)
        {
            // Fill in the existing culture versions
            TreeNode node = Tree.SelectSingleNode(NodeID, TreeProvider.ALL_CULTURES);
            if (node != null)
            {
                EnsureDocumentBreadcrumbs(PageBreadcrumbs, action: PageTitle.TitleText);
            }
            else
            {
                RedirectToInformation("editeddocument.notexists");
            }
        }
        else
        {
            RedirectToInformation("editeddocument.notexists");
        }
    }
    
    #endregion
}