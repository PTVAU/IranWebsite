using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// MultipartParser http://multipartparser.codeplex.com
/// Reads a multipart form data stream and returns the filename, content type and contents as a stream.
/// 2009 Anthony Super http://antscode.blogspot.com
/// </summary>
namespace CoreSerivce.Utilities
{
    public class MultipartParser
    {
        public MultipartParser(Stream stream)
        {
            Parse(stream, Encoding.UTF8);
        }

        public MultipartParser(Stream stream, Encoding encoding)
        {
            Parse(stream, encoding);
        }

        private void Parse(Stream stream, Encoding encoding)
        {
            Success = false;


            var data = ToByteArray(stream);


            var content = encoding.GetString(data);


            var delimiterEndIndex = content.IndexOf("\r\n");

            if (delimiterEndIndex > -1)
            {
                var delimiter = content.Substring(0, content.IndexOf("\r\n"));


                var re = new Regex(@"(?<=Content\-Type:)(.*?)(?=\r\n\r\n)");
                var contentTypeMatch = re.Match(content);


                re = new Regex(@"(?<=filename\=\"")(.*?)(?=\"")");
                var filenameMatch = re.Match(content);


                if (contentTypeMatch.Success && filenameMatch.Success)
                {
                    ContentType = contentTypeMatch.Value.Trim();
                    Filename = filenameMatch.Value.Trim();


                    var startIndex = contentTypeMatch.Index + contentTypeMatch.Length + "\r\n\r\n".Length;

                    var delimiterBytes = encoding.GetBytes("\r\n" + delimiter);
                    var endIndex = IndexOf(data, delimiterBytes, startIndex);

                    var contentLength = endIndex - startIndex;


                    var fileData = new byte[contentLength];

                    Buffer.BlockCopy(data, startIndex, fileData, 0, contentLength);

                    FileContents = fileData;
                    Success = true;
                    Extention = SetExtention(ContentType);
                }
            }
        }

        private int IndexOf(byte[] searchWithin, byte[] serachFor, int startIndex)
        {
            var index = 0;
            var startPos = Array.IndexOf(searchWithin, serachFor[0], startIndex);

            if (startPos != -1)
            {
                while ((startPos + index) < searchWithin.Length)
                {
                    if (searchWithin[startPos + index] == serachFor[index])
                    {
                        index++;
                        if (index == serachFor.Length)
                        {
                            return startPos;
                        }
                    }
                    else
                    {
                        startPos = Array.IndexOf<byte>(searchWithin, serachFor[0], startPos + index);
                        if (startPos == -1)
                        {
                            return -1;
                        }
                        index = 0;
                    }
                }
            }

            return -1;
        }

        private byte[] ToByteArray(Stream stream)
        {
            var buffer = new byte[32768];
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    var read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        return ms.ToArray();
                    }
                    ms.Write(buffer, 0, read);
                }
            }
        }

        public bool Success { get;
            private set; }

        public string ContentType { get;
            private set; }

        public string Filename { get;
            private set; }

        public byte[] FileContents { get;
            private set; }


        public string Extention { get;
            private set; }

        public string SetExtention(string ContentType)
        {
            switch (ContentType)
            {
                case "image/jpeg":
                    return ".jpg";

                case "image/png":
                    return ".png";

                default:
                    return "UNKNOWN";
            }
        }
    }
}
