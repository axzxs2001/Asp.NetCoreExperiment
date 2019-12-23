using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {



            return View();
        }
        async Task CreateABucket()
        {
            try
            {
                var request = new PutBucketRequest();
                request.BucketName = bucketName;
                await client.PutBucketAsync(request);
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
        static string bucketName;
        static string keyName;
        static IAmazonS3 client;
        async Task WritingAnObject()
        {
            try
            {
                // simple object put
                var request = new PutObjectRequest()
                {
                    ContentBody = "this is a test",
                    BucketName = bucketName,
                    Key = keyName
                };

                var response = await client.PutObjectAsync(request);

                // put a more complex object with some metadata and http headers.
                var titledRequest = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = keyName
                };
                titledRequest.Metadata.Add("title", "the title");

                await client.PutObjectAsync(titledRequest);
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
