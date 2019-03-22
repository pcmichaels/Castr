using System;

namespace CastDataAs
{
    public class CSV : ICastr
    {
        private string _csv;
        private string _delimiter;
        private string[] _fields;

        public CSV(string csv, string delimiter)
        {
            _csv = csv;
            _delimiter = delimiter;
            _fields = csv.Split(delimiter.ToCharArray());
        }

        public CSV(string csv, string delimiter, bool includesHeaders)
        {
            _csv = csv;
            _delimiter = delimiter;
            _fields = csv.Split(delimiter.ToCharArray());
        }

        public T CastAsClass<T>()
        {
            var newObject = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();
            int fieldIdx = 0;

            foreach (var prop in properties)
            {
                if (fieldIdx >= _fields.Length) break;
                prop.SetValue(newObject, _fields[fieldIdx++], null);
            }

            return newObject;
        }

        public T CastAsStruct<T>()
        {
            // Have to box the reference first
            var newObject = (object)Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();            
            int fieldIdx = 0;

            foreach(var prop in properties)
            {
                if (fieldIdx >= _fields.Length) break;
                prop.SetValue(newObject, _fields[fieldIdx++], null);
            }

            // Unbox and return
            return (T)newObject;
        }
    }
}
