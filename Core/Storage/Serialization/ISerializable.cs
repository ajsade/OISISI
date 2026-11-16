// Preuzeto sa 3. vjezbi (CRUDExample, Storage/Serialization/Serializable.cs)
namespace Core.Storage.Serialization
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }
}