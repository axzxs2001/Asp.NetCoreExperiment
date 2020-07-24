using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeberShipDemo3.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            var a1 = new Microsoft.AspNet.Identity.PasswordHasher();
            var ppp = a1.HashPassword("Nss#222222");


            var ph = new Microsoft.AspNet.Identity.PasswordHasher<ApplicationUser>();
            var pwd = ph.HashPassword(new ApplicationUser()
            {
                UserName = "Nss0627Insert02",
                SecurityStamp = "18076d02-6d8f-420d-b150-50531f18123f"
            }, "Nss#222222");



            var sss = ph.VerifyHashedPassword(new ApplicationUser()
            {
                SecurityStamp = "08076d02-6d8f-420d-b150-50531f18105f"
            }, "ADhVPnM6XRHONiJbNP4zQEc0KzQUQl3a/d0XF4aub9yF+A49vck7qTEIrCbDAbZqww==", "Nss#123456").ToString();
            return new string[] { pwd, "ADhVPnM6XRHONiJbNP4zQEc0KzQUQl3a/d0XF4aub9yF+A49vck7qTEIrCbDAbZqww==", sss , ppp };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
    //private void Button1_Click(object sender, EventArgs e)
    //{




    //}

    //private void Button2_Click(object sender, EventArgs e)
    //{
    //    var ph = new Microsoft.AspNet.Identity.PasswordHasher<ApplicationUser>();
    //    MessageBox.Show(ph.VerifyHashedPassword(new ApplicationUser()
    //    {
    //        SecurityStamp = "45b85708-5851-45cf-998b-3563d4047d22"
    //    }, textBox1.Text, "111111!").ToString());
    //}

    public class ApplicationUser : Microsoft.AspNet.Identity.EntityFramework.IdentityUser
    {

        public string Creator { get; set; }


    }
}
