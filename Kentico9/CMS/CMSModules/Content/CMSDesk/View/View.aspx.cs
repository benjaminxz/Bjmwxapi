﻿using System;
using System.Web;

using CMS.ExtendedControls;
using CMS.Helpers;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.Base;
using CMS.SiteProvider;
using CMS.Membership;

public partial class CMSModules_Content_CMSDesk_View_View : CMSContentPage
{
    #region "Properties"

    /// <summary>
    /// Overridden messages placeholder for correct positioning
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
        set
        {
            plcMess = value;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// OnPreInit
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {
        PortalContext.ViewMode = ViewModeEnum.Preview;
        DocumentManager.RedirectForNonExistingDocument = false;

        base.OnPreInit(e);
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(Page);
        ScriptHelper.RegisterModule(Page, "CMS/HeaderShadow");

        // Setup Edit menu
        bool preview = PortalContext.ViewMode.IsPreview();

        editMenu.ShowProperties = false;
        editMenu.ShowSpellCheck = true;
        editMenu.ShowSave = !preview;
        editMenu.ShowCheckOut = !preview;
        editMenu.ShowCheckIn = !preview;
        editMenu.ShowUndoCheckOut = !preview;
        editMenu.ShowApplyWorkflow = !preview;
        editMenu.NodeID = NodeID;
        editMenu.CultureCode = CultureCode;
        editMenu.UseSmallIcons = true;
        editMenu.IsLiveSite = false;

        var pageUrl = string.Empty;

        pageUrl = DocumentUIHelper.GetViewPageUrl();
        ucView.ViewPage = pageUrl;
        ucView.RotateDevice = ValidationHelper.GetBoolean(CookieHelper.GetValue(CookieName.CurrentDeviceProfileRotate), false);

        const string deviceRotateScript = @"
$cmsj(document).ready(function () {
    if (window.CMSDeviceProfile) {
        CMSDeviceProfile.OnRotationFunction = (function() {
            CMSView.InitializeFrame(CMSView.PreviewWidth, CMSView.PreviewHeight, !CMSView.Rotated);
            CMSView.DeviceWindowResize();
        });
    }
});";

        ScriptHelper.RegisterStartupScript(this, typeof(string), "deviceRotateScript", deviceRotateScript, true);

        // Bind external buttons (i.e. Persona selector)
        var extensionTarget = editMenu as IExtensibleEditMenu;
        extensionTarget.InitializeExtenders("Content");

        if (Node.NodeIsContentOnly)
        {
            // Preview link is not valid after going through worflow because DocumentWorkflowCycleGUID has changed
            DocumentManager.OnAfterAction += (obj, args) => { ucView.ViewPage = Node.GetPreviewLink(MembershipContext.AuthenticatedUser.UserName); };
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Checks if given URL matches with the presentation URL of current site. Redirects to info page if not.
    /// </summary>
    /// <param name="url">URL to be checked.</param>
    private void CheckExternalPageUrl(string url)
    {
        var presentationUrl = SiteContext.CurrentSite.SitePresentationURL;
        if (!string.IsNullOrEmpty(presentationUrl) && url.StartsWithCSafe(presentationUrl))
        {
            return;
        }

        RedirectToInformation(GetString("preview.unknownpresentationurl"));
    }

    #endregion
}