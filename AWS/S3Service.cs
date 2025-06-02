using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AWS
{
    public class S3Service
    {

        private readonly string _bucketName = "processaudiodoc";
        public S3Service() { }

        public async Task UploadS3Object(string name,object data)
        {
            var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);


            var memory = new MemoryStream(GetBytes(data));
            var uploadRequest = new PutObjectRequest()
            {
                InputStream = memory,
                BucketName = _bucketName,
                Key = name,
            };

           var  response = await s3Client.PutObjectAsync(uploadRequest);
            if(response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Unable to upload");
            }

        }

        public async Task<MemoryStream> GetS3Object( string key)
        {

            var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);
            var request = new GetObjectRequest() { BucketName = _bucketName, Key = key };
            var data = await s3Client.GetObjectAsync(request);

            var mem = new MemoryStream();
            data.ResponseStream.CopyTo(mem);
           return mem;
        }


        private byte[] GetBytes(object data) 
        {

           return MessagePackSerializer.Serialize(data);


        }
    }
}
 