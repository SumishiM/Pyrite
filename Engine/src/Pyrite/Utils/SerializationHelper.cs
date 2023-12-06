using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Pyrite.Utils
{
    public class SerializationHelper
    {
        public static T DeepCopy<T>([DisallowNull] T obj)
        {
            //GameLogger.Verify(c is not null);

            var settings = FileHelper.JsonSettings;
            if (JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj, settings), obj.GetType(), settings) is not T result)
            {
                throw new InvalidOperationException($"Unable to serialize {obj.GetType().Name} for editor!?");
            }

            return result;
        }

        public static void HandleSerializationError(object? sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            if (e is not Newtonsoft.Json.Serialization.ErrorEventArgs error ||
                error.ErrorContext.Member is not string memberName ||
                error.CurrentObject is null)
            {
                // We can't really do much about it :(
                return;
            }

            Type targetType = error.CurrentObject.GetType();
            //GameLogger.Error($"Error while loading field {memberName} of {targetType.Name}! Did you try setting PreviousSerializedName attribute?");

            error.ErrorContext.Handled = true;
        }
    }
}
