
using Orleans;
using Orleans.CodeGeneration;
using Orleans.Concurrency;
using Orleans.Runtime;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Reentrancy_Lib
{
    public interface IC : IGrainWithIntegerKey
    {
        Task<MyParmeter> Go(MyParmeter I);
    }


    [MayInterleave(nameof(ArgHasInterleaveAttribute))]
    public class C : Grain, IC
    {
        public async Task<MyParmeter> Go(MyParmeter myParmeter)
        {
            Console.WriteLine($"C中Go方法，I={myParmeter.I}");
            if (myParmeter.I == 1)
            {
                return myParmeter;
            }

            var grain = this.GrainFactory.GetGrain<IC>(0);
            var i = myParmeter.I;
            myParmeter.I = myParmeter.I - 1;
            var result = await grain.Go(myParmeter);
            myParmeter.I = result.I * i;
            return myParmeter;
        }
        public static bool ArgHasInterleaveAttribute(InvokeMethodRequest req)
        {
            var result = req.Arguments.Length == 1
                && req.Arguments[0]?.GetType().GetCustomAttribute<InterleaveAttribute>() != null;
            Console.WriteLine($"ArgHasInterleaveAttribute:{result}");
            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class InterleaveAttribute : Attribute { }

    [Interleave]
    public class MyParmeter
    {
        public int I
        { get; set; }
    }

}
