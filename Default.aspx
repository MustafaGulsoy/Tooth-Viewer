<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YourNamespace.Default"  Async="true"  %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample Website</title>
    <style>
        .header {
            width: 100%;
            height: 150px;
            background-color: #f1f1f1;
            padding: 10px;
            box-sizing: border-box;
        }

        .header div {
            margin: 5px 0;
        }

        .navbar {
            width: 200px;
            float: left;
            background-color: #f1f1f1;
            height: calc(100vh - 150px);
            overflow-y: auto;
            transition: all 0.3s;
        }

        .container {
            margin-left: 200px;
            height: calc(100vh - 150px);
            padding: 20px;
            text-align: center;
            box-sizing: border-box;
        }

        .navbar button {
            width: 100%;
            margin: 5px 0;
        }

        .details {
            display: none;
        }

        .details.active {
            display: block;
        }

        .details input, .details select, .details p {
            display: block;
            margin: 10px 0;
        }

        .details .close-btn {
            display: inline-block;
            cursor: pointer;
            color: red;
            margin-bottom: 10px;
        }

        .container img {
            width: calc(100% - 30px);
            margin: 0 15px;
        }

        .image-container {
            position: relative;
            width: 100%;
            max-width: 600px;
            margin: auto;
        }

        .image-container img {
            width: 100%;
            height: auto;
        }

        .dynamic-button {
            position: absolute;
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
            opacity: 0;
            transition: opacity 0.3s;
        }

        .dynamic-button:hover {
            opacity: 1;
        }
    </style>
    <script>
        function showDetails() {
            document.getElementById('buttonList').style.display = 'none';
            document.getElementById('details').classList.add('active');
        }

        function hideDetails() {
            document.getElementById('details').classList.remove('active');
            document.getElementById('buttonList').style.display = 'block';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <div>
                <label for="kurumAdi">Kurum adı: Kurum adı</label>
            </div>
            <div>
                <label for="doktorAdi">Doktor adı: Doktor adı</label>
            </div>
            <div>
                <label for="hastaId">Hasta Id: Hasta Id</label>
            </div>
            <div>
                <asp:Button ID="Button1" runat="server" OnClick="Test" Text="Button" />
            </div>
        </div>
        <div class="navbar">
            <div id="buttonList">
                <asp:Repeater ID="ButtonRepeater" runat="server">
                    <ItemTemplate>
                        <asp:Button ID="Button" runat="server" Text='<%# Container.DataItem %>' OnClientClick="showDetails(this.id); return false;" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="details" class="details">
                <div class="close-btn" onclick="hideDetails()">X</div>
                <label for="p1">P1:</label>
                <input type="text" id="p1" />
                <label for="p2">P2:</label>
                <input type="text" id="p2" />
                <label for="p3">P3:</label>
                <select id="p3">
                    <option>Option 1</option>
                    <option>Option 2</option>
                </select>
                <p>P4: O</p>
                <p>P5: text</p>
                <p>P6: text</p>
            </div>
        </div>  
        <div class="container">
           <div class="image-container">
    <img id="DynamicImage" draggable="false" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJ97DbMhNpJMnM9ru_wJNyoacxx6ZgOujvqg&s" alt="Sample Image" runat="server" />
   
   
</div>
        </div>
    </form>
</body>
</html>
