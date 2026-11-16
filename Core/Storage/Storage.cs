using Core.Storage.Serialization;

namespace Core.Storage
{
    // Preuzeto sa 3. vjezbi (CRUDExample, Storage/Storage.cs), uz izmjenu putanje
    public class Storage<T> where T : ISerializable, new()
    {
        private readonly string _fileName;
        private readonly Serializer<T> _serializer = new();

        public Storage(string fileName)
        {
            string sep = Path.DirectorySeparatorChar.ToString();
            _fileName = Path.GetFullPath(
                AppContext.BaseDirectory + ".." + sep + ".." + sep + ".." + sep + ".." + sep + "Data" + sep + fileName);
        }

        public List<T> Load()
        {
            string? folder = Path.GetDirectoryName(_fileName);
            if (folder != null)
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(_fileName))
            {
                FileStream fs = File.Create(_fileName);
                fs.Close();
            }

            IEnumerable<string> lines = File.ReadLines(_fileName);
            List<T> objects = _serializer.FromCSV(lines);

            return objects;
        }

        public void Save(List<T> objects)
        {
            string serializedObjects = _serializer.ToCSV(objects);
            using (StreamWriter streamWriter = new StreamWriter(_fileName))
            {
                streamWriter.Write(serializedObjects);
            }
        }
    }
}