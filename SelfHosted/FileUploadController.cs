using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.IO;
using System.Collections.ObjectModel;

namespace SelfHosted
{    
    public class FileUploadController: ApiController
    {
        static readonly string ServerUploadFolder = Path.GetTempPath();
        [HttpPost]
        public async Task<FileResult> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(ServerUploadFolder);
            await Request.Content.ReadAsMultipartAsync(streamProvider);
            foreach (MultipartFileData data in streamProvider.FileData)
            {
                string strFileName = data.Headers.ContentDisposition.FileName.Trim('"');
                File.Move(data.LocalFileName, ServerUploadFolder+"\\"+strFileName);                
            }
            //Collection<MultipartFileData> data = streamProvider.FileData;
            return new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),                
                Submitter = streamProvider.FormData["submitter"]
            };

        }
    }
}
