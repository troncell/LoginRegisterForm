using LoginRegisterForm.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LoginRegisterForm.Repository
{

    public class XMLDB
    {
        public DBSet<Product> Products { get; set; }
        public DBSet<User> Users { get; set; }
        public static readonly string XML_STORE = "xmlstore";

        public XMLDB()
        {
            var xmlDir = Path.Combine(Environment.CurrentDirectory, XML_STORE);
            if (!Directory.Exists(xmlDir))
                Directory.CreateDirectory(xmlDir);

            var type = typeof(XMLDB);
            var tables = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var t1 = type.GetProperties().FirstOrDefault();

            foreach (var table in tables)
            {
                var tableType = table.PropertyType;
                var tableName = tableType.GetGenericArguments().FirstOrDefault().Name;
                var xmlPath = Path.Combine(xmlDir, tableName + ".xml");
                var instance = Activator.CreateInstance(tableType,  xmlPath);
                table.SetValue(this, instance);
            }
        }

        public void SaveChanged()
        {
            var xmlDir = Path.Combine(Environment.CurrentDirectory, XML_STORE);
            if (!Directory.Exists(xmlDir))
                Directory.CreateDirectory(xmlDir);

            var type = typeof(XMLDB);
            var tables = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var table in tables)
            {
                (table.GetValue(this) as IDBSet).Save();
            }
        }
    }



    public class DBSet<T> : IEnumerable<T>, IDBSet
    {
        XDocument m_doc;
        string m_docPath;
        Type m_type;

        public DBSet(string path)
        {
            m_docPath = path;
            m_type = typeof(T);
            try
            {
                m_doc = XDocument.Load(m_docPath);
            }
            catch (Exception e)
            {
                m_doc = new XDocument();
                //add root element
                m_doc.Add(new XElement(m_type.Name + "s"));
                Debug.WriteLine("加载XDocument 失败", e);
            }
        }

        public void Add(T entity)
        {
            var newElement = new XElement(m_type.Name);
            foreach (var filed in m_type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var property = new XElement(filed.Name);
                property.Value = filed.GetValue(entity).ToString();
                newElement.Add(property);
            }
            m_doc.Root.Add(newElement);
        }


        public void Save()
        {
            m_doc.Save(m_docPath);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DBSetEnumerator<T>(m_doc);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class DBSetEnumerator<T> : IEnumerator<T>
    {
        List<T> m_items;
        int m_index;
        public DBSetEnumerator(XDocument doc)
        {
            m_items = new List<T>();
            foreach (var item in doc.Root.Elements(typeof(T).Name))
            {
                var instance = Activator.CreateInstance<T>();
                var type = typeof(T);
                foreach (var property in type.GetProperties())
                {
                    var value = item.Element(property.Name).Value;
                    property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
                }
                m_items.Add(instance);
            }
        }

        public T Current
        {
            get
            {
                return m_items.ElementAt(m_index);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
            m_items.Clear();
            m_index = -1;
        }

        public bool MoveNext()
        {
            m_index++;
            return m_index < m_items.Count;
        }

        public void Reset()
        {
            m_index = -1;
        }
    }

    public interface IDBSet
    {
        void Save();
    }
}
