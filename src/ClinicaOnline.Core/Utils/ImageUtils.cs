using System;
using System.IO;
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ClinicaOnline.Core.Utils
{
    public static class ImageUtils
    {
        public static string ConvertImageToBase64(string imagePath)
        {
            var imageBytes = File.ReadAllBytes(imagePath);

            return Convert.ToBase64String(imageBytes);
        }

        public static string UploadImage(string connection, string imagePath, string container)
        {
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(ConvertImageToBase64(imagePath), "");
            byte[] imageBytes = Convert.FromBase64String(data);
            var blobClient = new BlobClient(connection, container, fileName);

            using (var stream = new MemoryStream(imageBytes))
            {
                var blobUploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = "image/jpeg"
                    }
                };

                blobClient.Upload(stream, blobUploadOptions);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}