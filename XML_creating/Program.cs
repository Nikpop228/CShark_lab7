using LibAnimal;
using System.Reflection;
using System.Xml.Linq;

class Program
{
    //const string path = "C:\\Users\\nikpop\\source\\repos\\CShark_lab7\\CShark_lab7\\bin\\Release\\net9.0\\CShark_lab7.dll";
    static void Main()
    {
        var xml = new XDocument();

        var kernel = new XElement("LibAnimal");
        kernel.Add(CreateXmlElement(typeof(Animal)));
        kernel.Add(CreateXmlElement(typeof(Cow)));
        kernel.Add(CreateXmlElement(typeof(Lion)));
        kernel.Add(CreateXmlElement(typeof(Pig)));
        kernel.Add(CreateXmlElement(typeof(eClassificationAnimal)));
        kernel.Add(CreateXmlElement(typeof(eFavouriteFood)));
        xml.Add(kernel);
        Console.WriteLine(xml);
        xml.Save("StuctAnimal.xml");
    }

    static XElement CreateXmlElement(Type type)
    {
        if (type.IsEnum) //== typeof(eClassificationAnimal) || type == typeof(eFavouriteFood)) // только для перечислений
        {
            var fields = new XElement($"{type.Name}");
            foreach (var en in type.GetFields(BindingFlags.Public | BindingFlags.Static)) fields.Add(new XElement(en.Name));
            return fields;
        }

        var constructors_list = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var properties_list = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var methods_list = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
        var fields_list = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        var attributes_list = type.GetCustomAttributes(typeof(AnimalAttribute), false);

        var kernel = new XElement(type.Name); // корневой элемент
        var properties = new XElement("Properties"); // элемент для свойств
        var methods = new XElement("Methods"); // элемент для методов
        var fieldss = new XElement("Fields");

        foreach (var prop in properties_list) { properties.Add(new XElement("Property", prop.Name)); } // добавляем св-ва
        foreach(var meth in methods_list) { if (!meth.Name.Contains("get_") & !meth.Name.Contains("set_")) { methods.Add(new XElement("Method", meth.Name)); } } // добавляем методы, условие для фильтрации методов отн. к св-вам
        foreach(var constr in constructors_list) { methods.Add(new XElement("Constructor", type.Name)); } // добавляем конструкторы - используем type.Name - иначе ошибка
        foreach(var f in fields_list) { if (!f.Name.Contains("k__BackingField")) { fieldss.Add(new XElement("Field", f.Name)); } }

        foreach (AnimalAttribute a in attributes_list) kernel.Add(new XElement("Comment", a.Comment));
        kernel.Add(fieldss);
        kernel.Add(properties);
        kernel.Add(methods);

        return kernel;
    }
}