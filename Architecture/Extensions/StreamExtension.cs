using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Architecture
{
    public static class StreamExtension
    {
        public static byte[] ConvertStreamToByteArray(this Stream stream, long legth)
        {
            byte[] buffer = new byte[legth];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        public static FileStreamResult ConvertToFileStremResult(this byte[] bytes, string contentType, string fileName)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);

                var reader = new StreamReader(stream);

                return new FileStreamResult(reader.BaseStream, string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType)
                {
                    FileDownloadName = fileName
                };
            }
        }
    }
}
