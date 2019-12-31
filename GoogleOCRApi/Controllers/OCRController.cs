using System;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Mvc;

namespace GoogleOCRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController : ControllerBase
    {
        // POST: api/OCR
        [HttpPost]
        public ActionResult<List<OCRItem>> Post([FromBody] string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "gs://cloud-vision-codelab/otter_crossing.jpg";
            }

            var client = ImageAnnotatorClient.Create();
            var image = Image.FromUri(url);
            //var image = Image.FromFile("noey.jpg");
            var response = client.DetectText(image);
            var list = new List<OCRItem>();
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    list.Add(ConvertTextToList(annotation.Description));
                }
            }
            return list;
        }


        public OCRItem ConvertTextToList(string text)
        {
            var spText = text.Split('\n');
            var obj = new OCRItem { RawData = text };
            for (int i = 0; i < spText.Length; i++)
            {
                if (!string.IsNullOrEmpty(spText[i]))
                {
                    obj.DetectData.Add(spText[i]);
                }
            }
            return obj;
        }
    }

    public class OCRItem
    {
        public string RawData { get; set; }
        public List<string> DetectData { get; set; }

        public OCRItem()
        {
            DetectData = new List<string>();
        }
    }
}
