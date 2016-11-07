<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="bjm_termplate_test" MasterPageFile="~/bjm/master/MasterPage.master" %>

<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="plcMain" runat="Server">
    <section>
        <article class="article article-container space alt large-padding-h large-padding-v">
            <h1>
                <asp:Label runat="server" ID="quizName"></asp:Label>
            </h1>
            Total Completion <asp:TextBox runat="server" ID="youname" ></asp:TextBox>
            <asp:Label runat="server" ID="quizCompletion"></asp:Label>
        </article>
    </section>

</asp:Content>

