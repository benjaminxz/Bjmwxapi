﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CMS.Base;
using CMS.Helpers;
using CMS.UIControls;

/// <summary>
/// Displays errors and exceptions related to Facebook integration.
/// </summary>
public partial class CMSModules_Membership_Controls_Facebook_Error : CMSUserControl
{

    #region "Public properties"

    /// <summary>
    /// Gets or sets the value indicating whether this control will show errors using the page messages.
    /// </summary>
    public bool MessagesEnabled
    {
        get
        {
            object enabled = ViewState["MessagesEnabled"];

            return enabled == null ? false : (bool)enabled;
        }
        set
        {
            ViewState["MessagesEnabled"] = value;
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Displays the specified exception to the user.
    /// </summary>
    /// <param name="exception">The exception to display.</param>
    public void Report(Exception exception)
    {
        if (SystemContext.DevelopmentMode)
        {
            DisplayError(HTMLHelper.HTMLEncode(GetString("fb.exception")), null, HTMLHelper.HTMLEncode(exception.Message ?? exception.ToString()));
        }
        else
        {
            DisplayError(HTMLHelper.HTMLEncode(GetString("fb.exception")));
        }
    }


    /// <summary>
    /// Displays the specified error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    public void Report(string message)
    {
        DisplayError(HTMLHelper.HTMLEncode(message));
    }


    /// <summary>
    /// Displays the specified error message and list of errors to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="errorMessages">The list of errors to display.</param>
    public void Report(string message, IEnumerable<string> errorMessages)
    {
        StringBuilder builder = new StringBuilder();
        if (errorMessages.Any())
        {
            builder.Append("<ul>");
            foreach (string errorMessage in errorMessages)
            {
                builder.Append("<li>");
                builder.Append(HTMLHelper.HTMLEncode(errorMessage));
                builder.Append("</li>");
            }
            builder.Append("</ul>");
        }

        DisplayError(HTMLHelper.HTMLEncode(message), builder.ToString(), null);
    }

    #endregion


    #region "Private methods"
    
    private void DisplayError(string message)
    {
        DisplayError(message, null, null);
    }


    private void DisplayError(string message, string description, string tooltip)
    {
        if (MessagesEnabled)
        {
            ShowError(message, description, tooltip);
        }
        else
        {
            HtmlGenericControl paragraph = new HtmlGenericControl("p")
            {
                InnerHtml = message
            };
            paragraph.Attributes.Add("class", "Red");
            if (!String.IsNullOrEmpty(tooltip))
            {
                paragraph.InnerHtml = String.Format("{0}<br /><b>{1}</b>", paragraph.InnerHtml, tooltip);
            }
            Controls.Add(paragraph);
            if (!String.IsNullOrEmpty(description))
            {
                Literal literal = new Literal
                {
                    Text = description
                };
                Controls.Add(literal);
            }
        }
    }

    #endregion

}