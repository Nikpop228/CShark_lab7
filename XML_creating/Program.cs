using LibAnimal;
using System.Reflection;
using System.Xml.Linq;
//using System;
class Program
{
    //const string path = "C:\\Users\\nikpop\\source\\repos\\CShark_lab7\\CShark_lab7\\bin\\Release\\net9.0\\CShark_lab7.dll";
    static void Main()
    {
        //var assemly = Assembly.LoadFrom(path);
        //var meth = assemly.GetExportedTypes().Single();
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
        if (type == typeof(eClassificationAnimal) || type == typeof(eFavouriteFood)) // только для перечислений
        {
            var fields = new XElement($"{type.Name}");
            foreach (var en in type.GetFields(BindingFlags.Public | BindingFlags.Static)) fields.Add(new XElement(en.Name));
            return fields;
        }

        var constructors_list = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var properties_list = type.GetProperties();
        MethodInfo[]? methods_list;
        /*if (type == typeof(Cow) || type == typeof(Lion) || type == typeof(Pig))*/ 
        methods_list = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
        //else methods_list = type.GetMethods();

        var kernel = new XElement(type.Name); // корневой элемент
        var properties = new XElement("Properties"); // элемент для свойств
        var methods = new XElement("Methods"); // элемент для методов
        
        foreach (var prop in properties_list) { properties.Add(new XElement(prop.Name)); } // добавляем св-ва
        foreach(var meth in methods_list) {  methods.Add(new XElement(meth.Name)); } // добавляем методы
        //foreach(var constr in constructors_list) { methods.Add(new XElement(constr.Name)); }
        
        kernel.Add(properties);
        kernel.Add(methods);
        return kernel;
    }
}