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

namespace YourNamespace
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Generate button texts
                List<string> buttonTexts = new List<string>();
                for (int i = 1; i <= 32; i++)
                {
                    buttonTexts.Add("Button " + i);
                }

                // Bind the button texts to the Repeater
                ButtonRepeater.DataSource = buttonTexts;
                ButtonRepeater.DataBind();



                /// Buradaki buttonlar servisten gelecek


                List<ButtonData> buttonDataList = new List<ButtonData>();
                Random rnd = new Random();
                for (int i = 1; i <= 32; i++)
                {
                    buttonDataList.Add(new ButtonData
                    {
                        Text = "Button " + i,
                        Top = rnd.Next(0, 300), // Random top position
                        Left = rnd.Next(0, 300) // Random left position
                    });
                }

                // Bind the button data to the Repeater
                //ImageButtonsRepeater.DataSource = buttonDataList;
                //ImageButtonsRepeater.DataBind();
            }
        }

        protected void ImageButtonsRepeater_ItemCommandAsync(object sender, EventArgs e)
        {
            
        }
        public async void GetRandomImage()
        {


            string imagePath = "C:\\Projeler\\Bussines\\KardelenYazilim\\Projeler\\C#\\Tooth Viewer\\Resources\\Images\\";

            Random random = new Random();
            ;

            // Load the PNG file
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(Path.Combine( imagePath , random.Next(1, 7).ToString()) + ".png"))
            {
                byte[] imageBytes = Utils.ImageToByteArray(image);
                ToothXrayRequestBody data = new ToothXrayRequestBody();
                data.ImageData = imageBytes;
                string response =  await Utils.SendPostRequestAsync("/api/v1/XRayAnalysis/tooth-x-ray-analyze", data);
                ToothXRayResponse data1 = JsonConvert.DeserializeObject<ToothXRayResponse>(response);

                string base64String = Convert.ToBase64String(data1.ImageData);
                ;
                DynamicImage.Src = $"data:image/png;base64,{base64String}";



            }


        }

        protected void Test(object sender, EventArgs e)
        {
             GetRandomImage();
        }
    }




    public static class Utils
    {
        public static string url = "https://localhost:7099";

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
        public byte[] ImageData { get; set; }
        public Dictionary<string, string> data { get; set; }
    }

    public class ToothXRayResponse
    {
        public byte[] ImageData { get; set; }
        public string ToothData { get; set; }
    }
    public class ButtonData
    {
        public string Text { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
    }
}
