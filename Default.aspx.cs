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
using Microsoft.Ajax.Utilities;
using System.Net;
using System.Security.Cryptography;

namespace YourNamespace
{
    public partial class Default : System.Web.UI.Page
    {
        private Dictionary<string, ToothInfo> toothDataDictionary = new Dictionary<string, ToothInfo>();


        public List<string> imageUrl = new List<string>();


        protected void Page_Load(object sender, EventArgs e)
        {
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240807.114145.575.202408071152419.1&seriesUID=1.2.840.20240807.114145.575.202408071152419.2&objectUID=1.2.840.10008.20240807114352419&contenxtType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240605.133724.411.202406060956162.1&seriesUID=1.2.840.20240605.133724.411.202406060956162.2&objectUID=1.2.840.10008.20240606092756162&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240709.140033.566.202407091447601.1&seriesUID=1.2.840.20240709.140033.566.202407091447601.2&objectUID=1.2.840.10008.20240709140347601&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240904.111029.058.202409041134248.1&seriesUID=1.2.840.20240904.111029.058.202409041134248.2&objectUID=1.2.840.10008.20240904111234248&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240827.152730.117.202408271522682.1&seriesUID=1.2.840.20240827.152730.117.202408271522682.2&objectUID=1.2.840.10008.20240827153022682&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240815.144704.499.202408151444334.1&seriesUID=1.2.840.20240815.144704.499.202408151444334.2&objectUID=1.2.840.10008.20240815144844334&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240820.083922.142.202408200825197.1&seriesUID=1.2.840.20240820.083922.142.202408200825197.2&objectUID=1.2.840.10008.20240820084125196&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240805.133941.391.202408051335181.1&seriesUID=1.2.840.20240805.133941.391.202408051335181.2&objectUID=1.2.840.10008.20240805134135181&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240719.095544.976.202407190909643.1&seriesUID=1.2.840.20240719.095544.976.202407190909643.2&objectUID=1.2.840.10008.20240719095709643&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240708.112228.985.202407081137911.1&seriesUID=1.2.840.20240708.112228.985.202407081137911.2&objectUID=1.2.840.10008.20240708112537911&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240716.095139.193.202407160927918.1&seriesUID=1.2.840.20240716.095139.193.202407160927918.2&objectUID=1.2.840.10008.20240716095327918&contentType=image/jpeg");
            imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240722.093425.690.202407220937022.1&seriesUID=1.2.840.20240722.093425.690.202407220937022.2&objectUID=1.2.840.10008.20240722093737022&contentType=image/jpeg");

            //imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240819.094055.673.202408190922869.1&seriesUID=1.2.840.20240819.094055.673.202408190922869.2&objectUID=1.2.840.10008.20240819094222869&contentType=image/png");



            //imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240708.093503.592.202407080954689.1&seriesUID=1.2.840.20240708.093503.592.202407080954689.2&objectUID=1.2.840.10008.20240708093654689&contentType=image/jpeg");
            //imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240806.141238.157.202408061430519.1&seriesUID=1.2.840.20240806.141238.157.202408061430519.2&objectUID=1.2.840.10008.20240806141430519&contentType=image/jpeg");
            //imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240902.105133.339.202409021003632.1&seriesUID=1.2.840.20240902.105133.339.202409021003632.2&objectUID=1.2.840.10008.20240902105703632&contentType=image/jpeg");
            //imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240604.134843.535.202406041447791.1&seriesUID=1.2.840.20240604.134843.535.202406041447791.2&objectUID=1.2.840.10008.20240604140047791&contentType=image/jpeg");
            //imageUrl.Add("https://pacs.konyasm.gov.tr:30028/gateway/pacs/dicom-web/wado?requestType=WADO&studyUID=1.2.840.20240604.134843.535.202406041447791.1&seriesUID=1.2.840.20240604.134843.535.202406041447791.2&objectUID=1.2.840.10008.20240604140047791&contentType=image/jpeg");
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
                //lblCaptureDate.Text = "Capture Date: 02/15/2023";
                lblCaptureDate.Text = "";



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



            AuthInfo autInfo = new AuthInfo();
            autInfo.institutionId = 1;
            autInfo.userName = "PACS";
            autInfo.passwordBase64 = "S2FybWVkMjAyNCst";

            Dictionary<string, string> header = new Dictionary<string, string>
                {
                    { "Authorization", "" }
                };


            header["Authorization"] = await Utils.SendPostRequestAsync("https://pacs.konyasm.gov.tr:30028/gateway/auth/api/v1/Token", JsonConvert.SerializeObject(autInfo));
            header["Authorization"] = "bearer " + JsonConvert.DeserializeObject<AuthResponse>(header["Authorization"]).accessToken;

            Random random= new Random();
            ToothXrayRequestBody data = new ToothXrayRequestBody { image_url = imageUrl[random.Next(0, imageUrl.Count -1)] };

            string response = await Utils.SendPostRequestAsync("http://localhost:8000/api/v1/teeth/analyze/tooth-x-ray-analysis", JsonConvert.SerializeObject(data), header);
            Console.WriteLine(response);
            ToothXRayResponse responseData = JsonConvert.DeserializeObject<ToothXRayResponse>(response);

            imgXray.ImageUrl = $"data:image/png;base64,{responseData.result[0].image}";

            string extraText = "";
            //Clear existing buttons
            buttonPlaceholder.Controls.Clear();
            // Create new buttons based on the response
            toothDataDictionary.Clear();
            foreach (var item in responseData.result[0].data)
            {
                extraText = "";
                Label lbl = new Label();
                for (int i = 0; i < item.annotations.Length; i++)
                    extraText += item.annotations[i].process_name + ", ";
                if (item.annotations.Length > 0)
                    extraText = "(" + extraText.Substring(0, extraText.Length - 2) + ")";
                lbl.Text = item.teeth_number + " : " + item.status + extraText;
                lbl.ID = item.teeth_number.ToString();
                lbl.CssClass = "tooth-label";
                buttonPlaceholder.Controls.Add(lbl);

                // Generate mock data for each tooth
                toothDataDictionary[item.teeth_number.ToString()] = new ToothInfo
                {
                    PatientName = "none", // You can replace this with actual patient name
                    Condition = item.status + extraText  // You can replace this with actual condition
                };
            }
            Session["list"] = toothDataDictionary;
            // Show the buttons container
            buttonsScroll.Style["display"] = "block";


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
            //if (toothDataDictionary.TryGetValue(toothId, out ToothInfo toothInfo))
            //{
            //    toothInfoTitle.InnerText = $"Tooth {toothId} Information";
            //    patientName.InnerText = toothInfo.PatientName;
            //    toothCondition.InnerText = toothInfo.Condition;

            //    buttonsScroll.Style["display"] = "none";
            //    toothInfoContainer.Style["display"] = "block";
            //}
        }

        protected void BtnCloseToothInfo_Click(object sender, EventArgs e)
        {
            //toothInfoContainer.Style["display"] = "none";
            //buttonsScroll.Style["display"] = "block";
        }
        protected void CheckFilters_OnCheckedChanged(object sender, EventArgs e)
        {
            bool hideSolids = HideSolid.Checked;
            bool hideAnomaly = HideAnomaly.Checked;
            bool hideMissing = HideMissing.Checked;
            toothDataDictionary = Session["list"] as Dictionary<string, ToothInfo>;
            foreach (var item in toothDataDictionary)
            {

                if (hideSolids && item.Value.Condition.Contains("Sağlam") && !item.Value.Condition.Contains(" )"))
                    continue;


                if (hideAnomaly && (item.Value.Condition.Contains(" )")))

                    continue;

                if (hideMissing && item.Value.Condition.Contains("Kayıp Diş"))
                    continue;

                Label lbl = new Label();
                lbl.Text = item.Key + " : " + item.Value.Condition;
                lbl.ID = item.Key;
                lbl.CssClass = "tooth-label";
                buttonPlaceholder.Controls.Add(lbl);


                // Generate mock data for each tooth

            }


        }

    }




    public static class Utils
    {
        public static string url = "http://127.0.0.1:8000";

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
        public static async Task<string> SendPostRequestAsync(string endpoint, string jsonPayload, Dictionary<string, string> headers = null)
        {
            try
            {


                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Add headers if provided
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        _client.DefaultRequestHeaders.Remove(header.Key);

                        _client.DefaultRequestHeaders.Add(header.Key, header.Value);

                    }
                }


                HttpResponseMessage response = await _client.PostAsync(endpoint, content);

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
        public string image_url { get; set; }

    }



    public class ToothXRayResponse
    {
        public ToothXRayData[] result { get; set; }
    }

    public class ToothXRayData
    {
        public string image { get; set; }
        public ToothData[] data { get; set; }
    }
    public class ToothData
    {
        public int teeth_number { get; set; }
        public string status { get; set; }
        public Annotation[] annotations { get; set; }
        public ill[] illness { get; set; }
    }
    public class Annotation
    {
        public string process_name { get; set; }
        public float x_start { get; set; }
        public float y_start { get; set; }
        public float x_end { get; set; }
        public float y_end { get; set; }

    }
    public class ill
    {
        public string process_name { get; set; }
        public float x_start { get; set; }
        public float y_start { get; set; }
        public float x_end { get; set; }
        public float y_end { get; set; }

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
    public class AuthInfo
    {
        public int institutionId { get; set; }
        public string userName { get; set; }
        public string passwordBase64 { get; set; }
    }
    public class AuthResponse
    {
        public int sid { get; set; }
        public int institutionId { get; set; }
        public int userId { get; set; }
        public string userFullname { get; set; }
        public string isPhysician { get; set; }
        public string isAdmin { get; set; }
        public string isSysAdmin { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string expires { get; set; }
    }

}
