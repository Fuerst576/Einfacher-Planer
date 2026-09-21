<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail_page.aspx.cs" Inherits="PLTaskPlannerWeb.taskdetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta charset="utf-8" />

    <title>Aufgabendetails</title>

    <link rel="stylesheet" href="Active.css" />

</head>

<body>

<form id="form1" runat="server">

        <a href="Home_page.aspx" class="back-link">
            Zurück zu meinen Aufgaben
        </a>

        <br />
        <br />


        <div class="task-row">

            <div class="task-field">


                <asp:TextBox
                    ID="txtTitle"
                    runat="server"
                    CssClass="form-input">
                </asp:TextBox>

            </div>


            <div class="task-field">

 

                <asp:DropDownList
                    ID="ddlCategory"
                    runat="server"
                    CssClass="form-dropdown">
                </asp:DropDownList>

            </div>


            <div class="task-field">



                <asp:TextBox
                    ID="txtDueDate"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-date">
                </asp:TextBox>

            </div>

        </div>


        <br />


        <div class="description-field">



            <br />

            <asp:TextBox
                ID="txtDescription"
                runat="server"
                TextMode="MultiLine"
                Rows="5"
                CssClass="form-textarea">
            </asp:TextBox>

        </div>


        <br />


        <div class="completed-field">

            <asp:CheckBox
                ID="chkCompleted"
                runat="server"
                Text=" Aufgabe erledigt" />

        </div>


        <br />
        <br />


        <asp:Button
            ID="btnSave"
            runat="server"
            Text="Änderungen speichern"
            OnClick="btnSave_Click"
            CssClass="form-button" />

        <br />
        <br />


        <asp:Label
            ID="lblMessage"
            runat="server">
        </asp:Label>


</form>

</body>

</html>