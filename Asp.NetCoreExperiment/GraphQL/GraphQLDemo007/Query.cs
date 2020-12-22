using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate.Execution;
using System.Threading;
using Microsoft.AspNetCore.CookiePolicy;

namespace GraphQLDemo07
{
    public class Subscription
    {
        [Subscribe]
        public string OnReview(
            [Topic] string episode,
            [EventMessage] string message) =>
            message;
    }
}
