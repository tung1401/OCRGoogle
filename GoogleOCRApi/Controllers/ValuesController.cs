using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Xrm.Client.Services;
using Tesseract;

namespace GoogleOCRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var client = ImageAnnotatorClient.Create();
            var image = Image.FromUri("gs://cloud-vision-codelab/otter_crossing.jpg");
            var response = client.DetectText(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    Console.WriteLine(annotation.Description);
                }
            }


            //var jsonPath = @"C:\Users\DELL\Source\Repos2\dotnet-docs-samples\vision\api\QuickStart\OCRAPI-933ba4d331ac.json";
            //var credential = GoogleCredential.FromFile(jsonPath);

            //var client = new ImageAnnotatorClientBuilder
            //{
            //    Endpoint = new ServiceEndpoint("http://eu-vision.googleapis.com/v1/images:annotate"),
            //    CredentialsPath = jsonPath,


            //}.Build();


            //// Instantiates a client
            ////  var client = ImageAnnotatorClient.Create();
            //// Load the image file into memory

            //try
            //{
            //    var image2 = Image.FromFile("wakeupcat.jpg");
            //    var response2 = client.DetectText(image2);



            //    var image = Image.FromFile("wakeupcat.jpg");
            //    // Performs label detection on the image file
            //    var response = client.DetectLabels(image);
            //    foreach (var annotation in response)
            //    {
            //        if (annotation.Description != null)
            //            Console.WriteLine(annotation.Description);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}



            return new string[] { "value1", "value2" };











            /*  var client = new ImageAnnotatorClientBuilder
              {
                  Endpoint = new ServiceEndpoint("eu-vision.googleapis.com"),

              }.Build();

             // var client = ImageAnnotatorClient.Create();
              var image = Image.FromFile("C:\\Users\\DELL\\Pictures\\Enterprise-2018_12_11_ENT_GuavaPass.jpg");
              // Performs label detection on the image file
              var response = client.DetectLabels(image);
              foreach (var annotation in response)
              {
                  if (annotation.Description != null)
                      Console.WriteLine(annotation.Description);
              }

              return new string[] { "value1", "value2" };*/
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

          //  var image = Image.FromFile("wakeupcat.jpg");
            var data = @"C:\Users\DELL\source\repos\OCRGoogle\GoogleOCRApi\wakeupcat.jpg";
            string tessPath = @"C:\Users\DELL\source\repos\OCRGoogle\GoogleOCRApi";
            string result = "";

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = Path.Combine(path, "tessdata");
            path = path.Replace("file:\\", "");

            using (var engine = new TesseractEngine(path, "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(data))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }
          //  return String.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;

           return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
