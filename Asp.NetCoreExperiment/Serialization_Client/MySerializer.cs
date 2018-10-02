using Orleans.CodeGeneration;
using Orleans.Serialization;
using System;
using System.Runtime.Serialization;

namespace Serialization_Client
{
    #region 方法一
    class Goods
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public double Quantity { get; set; }

        [CopierMethod]
        static private object Copy(object input, ICopyContext context)
        {
            var inputCopy = context.CheckObjectWhileCopying(input);

            if (inputCopy == null)
            {
                context.RecordCopy(input, inputCopy);
            }
            var copy = SerializationManager.DeepCopyInner(input, context);
            return copy;
        }

        [SerializerMethod]
        static private void Serialize(object input, ISerializationContext context, Type expected)
        {
            SerializationManager.SerializeInner(input, context, expected);
        }

        [DeserializerMethod]
        static private object Deserialize(Type expected, IDeserializationContext context)
        {
            return SerializationManager.DeserializeInner(expected, context);
        }
    }
    #endregion

    #region 方法二
    public class MySerializer : IExternalSerializer
    {
        public object DeepCopy(object source, ICopyContext context)
        {
            var fooCopy = SerializationManager.DeepCopyInner(source, context);
            throw new NotImplementedException();
        }

        public object Deserialize(Type expectedType, IDeserializationContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsSupportedType(Type itemType)
        {
            throw new NotImplementedException();
        }

        public void Serialize(object item, ISerializationContext context, Type expectedType)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 方法三
    public class User
    {
        public User BestFriend { get; set; }
        public string NickName { get; set; }
        public int FavoriteNumber { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }

    [Orleans.CodeGeneration.SerializerAttribute(typeof(User))]
    internal class UserSerializer
    {
        [CopierMethod]
        public static object DeepCopier(object original, ICopyContext context)
        {
            var input = (User)original;
            var result = new User();

            // Record 'result' as a copy of 'input'. Doing this immediately after construction allows for
            // data structures which have cyclic references or duplicate references.
            // For example, imagine that 'input.BestFriend' is set to 'input'. In that case, failing to record
            // the copy before trying to copy the 'BestFriend' field would result in infinite recursion.
            context.RecordCopy(original, result);

            // Deep-copy each of the fields.
            result.BestFriend = (User)SerializationManager.DeepCopyInner(input.BestFriend, context);
            result.NickName = input.NickName; // strings in .NET are immutable, so they can be shallow-copied.
            result.FavoriteNumber = input.FavoriteNumber; // ints are primitive value types, so they can be shallow-copied.
            result.BirthDate = (DateTimeOffset)SerializationManager.DeepCopyInner(input.BirthDate, context);

            return result;
        }

        [SerializerMethod]
        public static void Serializer(object untypedInput, ISerializationContext context, Type expected)
        {
            var input = (User)untypedInput;

            // Serialize each field.
            SerializationManager.SerializeInner(input.BestFriend, context);
            SerializationManager.SerializeInner(input.NickName, context);
            SerializationManager.SerializeInner(input.FavoriteNumber, context);
            SerializationManager.SerializeInner(input.BirthDate, context);
        }

        [DeserializerMethod]
        public static object Deserializer(Type expected, IDeserializationContext context)
        {
            var result = new User();

            // Record 'result' immediately after constructing it. As with with the deep copier, this
            // allows for cyclic references and de-duplication.
            context.RecordObject(result);

            // Deserialize each field in the order that they were serialized.
            result.BestFriend = SerializationManager.DeserializeInner<User>(context);
            result.NickName = SerializationManager.DeserializeInner<string>(context);
            result.FavoriteNumber = SerializationManager.DeserializeInner<int>(context);
            result.BirthDate = SerializationManager.DeserializeInner<DateTimeOffset>(context);

            return result;
        }
    }
    #endregion


    [Serializable]
    public class MyCustomException : Exception
    {
        public string MyProperty { get; }

        public MyCustomException(string myProperty, string message)
            : base(message)
        {
            this.MyProperty = myProperty;
        }

        public MyCustomException(string transactionId, string message, Exception innerException)
            : base(message, innerException)
        {
            this.MyProperty = transactionId;
        }

        public MyCustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.MyProperty = info.GetString(nameof(this.MyProperty));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(this.MyProperty), this.MyProperty);
        }
    }
}
