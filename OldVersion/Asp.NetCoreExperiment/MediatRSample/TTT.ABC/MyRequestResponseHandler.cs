using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTT.ABC
{
    public class MyRequestResponseHandler : IRequestHandler<MyRequest, MyResponse>
    {
        private readonly TextWriter _writer;
        public MyRequestResponseHandler(TextWriter writer)
        {
            _writer = writer;
        }
        public async Task<MyResponse> Handle(MyRequest request, CancellationToken cancellationToken)
        {
            await _writer.WriteLineAsync($"这里收到请求： {request.Message}");
            return new MyResponse { Message = request.Message + " Response" + DateTime.Now };
        }
    }
}
