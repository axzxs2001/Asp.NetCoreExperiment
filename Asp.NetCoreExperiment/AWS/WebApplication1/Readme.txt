1、安装AWSSDK.S3 Nuget包
2、申请 awsAccessKeyId 和 awsSecretAccessKey 
3、用程序创建一个Bucket
4、开发上传和下载
    public IActionResult Index()
        {
            //CreateABucket().Wait();
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            WritingAnObject(file).Wait();
            return View();
        }
        [HttpGet("/getfile")]
        public IActionResult GetFile()
        {
            ReadingAnObject().Wait();
            return Ok();
        }
        string awsAccessKeyId = "";
        string awsSecretAccessKey = "";
        string bucketName = "gsw";
        string fileName = "acb.txt";
        async Task CreateABucket()
        {
            try
            {
                using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast2))
                {
                    var request = new PutBucketRequest();
                    request.BucketName = bucketName;
                    await client.PutBucketAsync(request);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when creating a bucket with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
            }
        }


        async Task WritingAnObject(IFormFile file)
        {
            try
            {
                using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast2))
                {
                    var request = new PutObjectRequest()
                    {
                        InputStream = file.OpenReadStream(),
                        BucketName = bucketName,
                        Key = file.FileName
                    };
                    var response = await client.PutObjectAsync(request);             
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }
        }

        async Task ReadingAnObject()
        {
            try
            {
                using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast2))
                {
                    GetObjectRequest request = new GetObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = fileName
                    };

                    using (GetObjectResponse response = await client.GetObjectAsync(request))
                    {
                        string title = response.Metadata["x-amz-meta-title"];
                        Console.WriteLine("The object's title is {0}", title);
                        string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                        if (!System.IO.File.Exists(dest))
                        {

                            await response.WriteResponseStreamToFileAsync(dest, true, new System.Threading.CancellationToken());
                        }
                    }
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when reading an object", amazonS3Exception.Message);
                }
            }
        }

        async Task DeletingAnObject()
        {
            try
            {
                using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast2))
                {
                    var request = new DeleteObjectTaggingRequest()
                    {
                        BucketName = bucketName,
                        Key = fileName
                    };

                    await client.DeleteObjectTaggingAsync(request);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when deleting an object", amazonS3Exception.Message);
                }
            }
        }

        async Task ListingObjects()
        {
            try
            {
                using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast2))
                {
                    ListObjectsRequest request = new ListObjectsRequest();
                    request.BucketName = bucketName;
                    ListObjectsResponse response = await client.ListObjectsAsync(request);
                    foreach (S3Object entry in response.S3Objects)
                    {
                        Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                    }

                    // list only things starting with "foo"
                    request.Prefix = "foo";
                    response = await client.ListObjectsAsync(request);
                    foreach (S3Object entry in response.S3Objects)
                    {
                        Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                    }

                    // list only things that come after "bar" alphabetically
                    request.Prefix = null;
                    request.Marker = "bar";
                    response = await client.ListObjectsAsync(request);

                    foreach (S3Object entry in response.S3Objects)
                    {
                        Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                    }

                    // only list 3 things
                    request.Prefix = null;
                    request.Marker = null;
                    request.MaxKeys = 3;
                    response = await client.ListObjectsAsync(request);
                    foreach (S3Object entry in response.S3Objects)
                    {
                        Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                    }
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when listing objects", amazonS3Exception.Message);
                }
            }
        }