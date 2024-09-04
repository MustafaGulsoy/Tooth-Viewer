<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YourNamespace.Default" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dentist Viewer</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Palanquin:wght@100;200;300;400;500;600;700&display=swap" rel="stylesheet">

    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
        }

        .header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
            margin-top: 10px;
            border: 10px;
            padding-left: 20px;
            padding-right: 20px;
            border-radius: 14px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .patient-info {
            text-align: right;
        }

        .main-content {
            display: flex;
            align-items: stretch;
        }

        .image-container {
            width: 80%;
            position: relative;
            padding-top: 45%; /* 16:9 aspect ratio */
            margin-right: 10px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            border-radius: 14px;
        }

            .image-container img {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                object-fit: cover;
                border-radius: 14px;
            }

        .buttons-container {
            width: 20%;
            height: 0;
            padding-top: 45%; /* Match the aspect ratio of the image container */
            position: relative;
            overflow: hidden;
            background-color: white; /* Add this line */
            border-radius: 14px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .buttons-scroll {
            display: none; /* Add this line */
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            overflow-y: auto;
            overflow-x: hidden;
            background-color: transparent;
            padding: 10px;
            box-sizing: border-box;
        }



            .buttons-scroll::-webkit-scrollbar {
                width: 6px;
            }

            .buttons-scroll::-webkit-scrollbar-track {
                background: transparent;
            }

            .buttons-scroll::-webkit-scrollbar-thumb {
                background: rgba(0, 0, 0, 0.2);
                border-radius: 3px;
            }

        .tooth-button {
            border-radius: 8px;
            margin: 0 0 5px 0;
            background-color: blue;
            color: white;
            display: block;
            width: 100%;
            border: none;
            padding: 10px;
            text-align: center;
            cursor: pointer;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .tools {
            margin-top: 20px;
        }

        .footer {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
        }

        .icon-title-container {
            display: flex;
            align-items: center;
        }

            .icon-title-container i {
                margin-right: 10px;
                color: #32bc9b;
                font-size: 2em;
            }

            .icon-title-container h1 {
                font-family: 'Palanquin';
            }

        .tooth-info-container {
            display: none;
            width: 20%;
            height: 0;
            padding-top: 45%;
            position: relative;
            overflow: hidden;
            background-color: #f0f0f0;
            border-radius: 14px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
        }

        .tooth-info-content {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            padding: 20px;
            overflow-y: auto;
        }

        .close-button {
            position: absolute;
            top: 10px;
            right: 10px;
            background-color: #ff4d4d;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
        }

        .tooth-label {
            display: block;
            margin-bottom: 5px;
            padding: 10px;
            background-color: #f0f0f0;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

            .tooth-label:hover {
                background-color: #e0e0e0;
            }

        .column {
          
         
            display: flex;
            flex-direction: column;
            gap: 5px; /* Add some space between the checkboxes */
        }

        .green-button {
 background-color: #32bc9b; /* Belirtilen açık yeşil renk */
  border: none; /* Kenarlık yok */
  color: #fff; /* Yazı rengi beyaz */
  padding: 10px 20px; /* İç boşluk */
  text-align: center; /* Metni ortala */
  text-decoration: none; /* Alt çizgi yok */
  display: inline-block; /* Satır içi blok */
  font-size: 16px; /* Yazı boyutu */
  margin: 10px 2px; /* Dış boşluk */
  cursor: pointer; /* İmleci el şeklinde yapar */
  border-radius: 5px; /* Köşeleri yuvarlat */
  transition: background-color 0.3s, transform 0.3s; /* Geçiş efektleri */
}

/* Hover efekti */
.green-button:hover {
  background-color: #28a493; /* Hoverda daha koyu yeşil */
  transform: scale(1.03); /* Butonu biraz büyüt */
}

/* Aktif durum efekti */
.green-button:active {
 background-color: #248f80; /* Aktif durumda daha koyu yeşil */
  transform: scale(0.97); /* Butonu biraz küçült */
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <div class="icon-title-container">
                <i class="fas fa-tooth"></i>
                <h1>Dentist Viewer</h1>
            </div>
            <div class="patient-info">
                <asp:Label ID="lblPatientInfo" runat="server" />
            </div>
        </div>
        <div class="main-content">
            <div class="image-container">
                <asp:Image ID="imgXray" runat="server" AlternateText="Dental X-ray" draggable="false" />
            </div>
            <div class="buttons-container">

                <div id="buttonsScroll" class="buttons-scroll" runat="server">
                    <ul class="column">
                        <li>
                            <asp:CheckBox ID="HideSolid" runat="server" Text="Sağlıklı Dişleri Gizle" />
                        </li>
                        <li>
                            <asp:CheckBox ID="HideMissing" runat="server" Text="Çekilmiş Dişleri Gizle" /></li>
                        <li>
                            <asp:CheckBox ID="HideAnomaly" runat="server" Text="Anomalileri Gizle" /></li>


                        <asp:Button CssClass="tooth-button" OnClick="CheckFilters_OnCheckedChanged" runat="server" Text="Filtre" />
                        <asp:PlaceHolder ID="buttonPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>


                    <!-- Add more buttons as needed -->
                </div>
            </div>

        </div>


        <div class="tools">
            <asp:Button ID="btnDraw" CssClass="green-button" runat="server" Text="Change Image" OnClick="BtnDraw_Click" />
   <%--         <asp:Button ID="btnAddText" runat="server" Text="Add Text" />
            <asp:Button ID="btnMarkAreas" runat="server" Text="Mark Areas" />
            <asp:Button ID="btnCompareXrays" runat="server" Text="Compare X-rays" />--%>
        </div>
        <div class="footer">
            <asp:Label ID="lblCaptureDate" runat="server" />
            <div>
                <asp:Button ID="btnExport" runat="server" Text="Export" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" />
            </div>
        </div>
    </form>
</body>
</html>
