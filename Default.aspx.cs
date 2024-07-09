using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Drawing;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.SqlServer.Server;
using System.Collections;
using System.Drawing.Imaging;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.WebSockets;

namespace YourNamespace
{
    public partial class Default : System.Web.UI.Page
    {
        private Dictionary<string, ToothInfo> toothDataDictionary = new Dictionary<string, ToothInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hasta bilgilerini ayarla
                lblPatientInfo.Text = "Patient: John Smith<br>ID: 123456<br>DOB: 01/01/1980<br>X-ray Date: 02/15/2023";

                // X-ray görüntüsünü ayarla

                //imgXray.ImageUrl = "https://live-cms.s3.eu-south-1.amazonaws.com/imagen_3_radiografia_8eacffe05c.jpg";

                // Diş butonlarını oluştur
                //rptTeethButtons.DataSource = Enumerable.Range(1, 16);
                //rptTeethButtons.DataBind();

                // Çekim tarihini ayarla
                lblCaptureDate.Text = "Capture Date: 02/15/2023";



                /// Buradaki buttonlar servisten gelecek



                // Bind the button data to the Repeater
                //ImageButtonsRepeater.DataSource = buttonDataList;
                //ImageButtonsRepeater.DataBind();
            }
        }

        protected async void BtnDraw_Click(object sender, EventArgs e)
        {
            await GetRandomImage();
        }
        public async Task GetRandomImage()
        {
            string imagePath = "C:\\Projeler\\Bussines\\KardelenYazilim\\Projeler\\C#\\Tooth Viewer\\Resources\\Images\\";

            Random random = new Random();

            using (System.Drawing.Image image = System.Drawing.Image.FromFile(Path.Combine(imagePath, random.Next(1, 7).ToString()) + ".png"))
            {
                byte[] imageBytes = Utils.ImageToByteArray(image);
                string imageBase64 = Convert.ToBase64String(imageBytes);
                ToothXrayRequestBody data = new ToothXrayRequestBody { ImageData = imageBase64 };
                string response = await Utils.SendPostRequestAsync("/api/v1/analyze/tooth-x-ray", data);
                ToothXRayResponse responseData = JsonConvert.DeserializeObject<ToothXRayResponse>(response);

                imgXray.ImageUrl = $"data:image/png;base64,{responseData.ImageData}";

                // Clear existing buttons
                buttonPlaceholder.Controls.Clear();

                // Create new buttons based on the response
                toothDataDictionary.Clear();
                foreach (var item in responseData.ToothData)
                {
                    Label lbl = new Label();
                    lbl.Text =  item.Key + " : " + item.Value;
                    lbl.ID = item.Key;
                    lbl.CssClass = "tooth-label";
                    buttonPlaceholder.Controls.Add(lbl);

                    // Generate mock data for each tooth
                    toothDataDictionary[item.Key] = new ToothInfo
                    {
                        PatientName = "John Doe", // You can replace this with actual patient name
                        Condition = item.Value // You can replace this with actual condition
                    };
                }

                // Show the buttons container
                buttonsScroll.Style["display"] = "block";
                toothInfoContainer.Style["display"] = "none";
            }
        }
        protected async void Test(object sender, EventArgs e)
        {
            await GetRandomImage();
        }
        protected void rptQuery_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var buttonData = e.Item.DataItem as ButtonData;
                var button = e.Item.FindControl("Button") as Button;

              
                button.Text = buttonData.Text;
            }
        }
   
        protected void ToothButton_Command(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string toothId = btn.ID;
            ShowToothInfo(toothId);
        }
        private void ShowToothInfo(string toothId)
        {
            if (toothDataDictionary.TryGetValue(toothId, out ToothInfo toothInfo))
            {
                toothInfoTitle.InnerText = $"Tooth {toothId} Information";
                patientName.InnerText = toothInfo.PatientName;
                toothCondition.InnerText = toothInfo.Condition;

                buttonsScroll.Style["display"] = "none";
                toothInfoContainer.Style["display"] = "block";
            }
        }

        protected void BtnCloseToothInfo_Click(object sender, EventArgs e)
        {
            toothInfoContainer.Style["display"] = "none";
            buttonsScroll.Style["display"] = "block";
        }
        protected void BtnBackToTeeth_Click(object sender, EventArgs e)
        {
            foreach (var item in toothDataDictionary)
            {
                Button btn = new Button();
                btn.Text = "Tooth " + item.Key;
                btn.CssClass = "tooth-button";
                btn.ID = item.Key;
                btn.Click += new EventHandler(ToothButton_Command);
                buttonPlaceholder.Controls.Add(btn);

                // Generate mock data for each tooth
           
            }


            toothInfoContainer.Style["display"] = "none";
            buttonsScroll.Style["display"] = "block";
        }

    }




    public static class Utils
    {
        public static string url = "http://127.0.0.1:8000/";

        private static readonly HttpClient _client = new HttpClient();

        public static async Task<string> SendGetRequestAsync(string endpoint, Dictionary<string, string> headers = null)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + endpoint);

                // Add headers if provided
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public static async Task<string> SendPostRequestAsync(string endpoint, ToothXrayRequestBody jsonPayload, Dictionary<string, string> headers = null)
        {
            try
            {


                HttpContent content = new StringContent(JsonConvert.SerializeObject(jsonPayload), Encoding.UTF8, "application/json");

                // Add headers if provided
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        content.Headers.Add(header.Key, header.Value);
                    }
                }


                HttpResponseMessage response = await _client.PostAsync(url + endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public static byte[] ImageToByteArray(System.Drawing.Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }
    



    }



    public class ToothXrayRequestBody
    {
        public string ImageData { get; set; }

    }

    public class ToothXRayResponse
    {
        public string ImageData { get; set; }
        public Dictionary<string, string> ToothData { get; set; }
    }
    public class ButtonData
    {
        public string Text { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
    }
    public class ToothInfo
    {
        public string PatientName { get; set; }
        public string Condition { get; set; }
    }
}
