<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home_page.aspx.cs" Inherits="PLTaskPlannerWeb.tasklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta charset="utf-8" />

    <title>Aufgaben</title>

    <link rel="stylesheet" href="Active.css" />

</head>

<body>

<form id="form1" runat="server">

    <div class="task-page">

        <h1>Deine Aufgaben</h1>

        <asp:Button
            ID="btnNewTask"
            runat="server"
            Text="Neue Aufgabe hinzufügen"
            PostBackUrl="New_task.aspx" 
            CssClass="new-task-button"/>

        <asp:DropDownList
            ID="ddlCategory"
            runat="server"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
            CssClass="category-dropdown">
        </asp:DropDownList>


        <br />
        <br />


        <asp:Repeater
            ID="rptTasks"
            runat="server">

            <ItemTemplate>

                <div class="task">

                    <h3 class="task-title">
                        <a href='Detail_page.aspx?TaskID=<%# Eval("TaskID") %>'>
                            <%# Eval("Title") %>
                        </a>
                    </h3>

                    <div class="task-info">
                    <div class="task-options task-options-category">
                        Kategorie: <%# GetCategoryName(Convert.ToInt32(Eval("CategoryID"))) %>
                    </div>                      

                    <div class="task-options">
                        Fällig am: <%# Eval("DueDate", "{0:dd.MM.yyyy}") %>
                    </div>
             
                    <div class="task-options">
                        <asp:ImageButton
                            ID="imgStatus"
                            runat="server"
                            CssClass="img"
                            OnClick="imgStatus_Click"
                            CommandArgument='<%# Eval("TaskID") %>'
                            ImageUrl='<%# Convert.ToBoolean(Eval("IsCompleted"))
                                ? "images/erledigt.png"
                                : "images/offen.png" %>'
                            CausesValidation="false" />
                    </div>

                    <div class="task-options">
                        <asp:ImageButton
                            ID="imgDelete"
                            runat="server"
                            CssClass="img"
                            ImageUrl="images/delete.png"
                            OnClick="imgDelete_Click"
                            CommandArgument='<%# Eval("TaskID") %>'
                            CausesValidation="false" />
                    </div>
            </div>

    </div> 

    <br />

            </ItemTemplate>

        </asp:Repeater>

 <asp:Label
     ID="lblMessage"
     runat="server">
 </asp:Label>

    </div>

</form>

</body>

</html>