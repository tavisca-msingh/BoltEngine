using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using System.Text.RegularExpressions;
namespace AWS
{
    public class SpeechService
    {
        string s3 = "s3://processaudiodoc/harvard.wav";
        private readonly string _bucketName = "processaudiodoc";
        ///[default]
        //aws_access_key_id = AKIA4Q642JVEQEE6EFUH
        //aws_secret_access_key = rLeP + YAcOy8yoFsLS31QflQg4wGECpp / vuXHdlO2
        public async Task<string> Process(string filePath)
        {
            var client = new AmazonTranscribeServiceClient(RegionEndpoint.USEast1);
            var outputKey = Guid.NewGuid().ToString("N").ToLower();
            outputKey = Regex.Replace(outputKey, "[0-9]", "");


            var startTranscriptionRequest = new StartTranscriptionJobRequest
            {
                TranscriptionJobName = outputKey,
                LanguageCode = "en-US",
                MediaFormat = "mp3",
                OutputBucketName= _bucketName,
                OutputKey= outputKey,
                Media = new Media { MediaFileUri = filePath },
          
            };

            var startTranscriptionResponse = await client.StartTranscriptionJobAsync(startTranscriptionRequest);
            Console.WriteLine($"Transcription job started. Job Name: {startTranscriptionResponse.TranscriptionJob.TranscriptionJobName}");

            TranscriptionJob transcriptionJob;
            while (true) {
                var getTranscriptionJobResponse = await client.GetTranscriptionJobAsync(new GetTranscriptionJobRequest
                {
                    TranscriptionJobName = startTranscriptionResponse.TranscriptionJob.TranscriptionJobName,

                });

                if (getTranscriptionJobResponse.TranscriptionJob.TranscriptionJobStatus == "COMPLETED")
                {
                    transcriptionJob = getTranscriptionJobResponse.TranscriptionJob;
                    Console.WriteLine("Transcription completed successfully!");
                    break;
                }

            }

           return await  GetS3Transcript(outputKey);

        }

        private async Task<string> GetS3Transcript(string key)
        {

            var s3Service = new S3Service();
            var s3Data = await s3Service.GetS3Object(key);
            var f = System.Text.Encoding.UTF8.GetString(s3Data.ToArray()); 

            return f;
        }
    }
}
