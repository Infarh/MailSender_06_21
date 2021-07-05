using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MailSender.TestConsole
{
    class Program
    {
        //public string this[int index]
        //{
        //    get { }
        //    set { }
        //}

        static void Main(string[] args)
        {
            //Type program_type = typeof(Program);
            //Type program = new Program();
            //Type program_type2 = program.GetType().FullName;

            //Assembly asm;
            //Module module;
            //Type type;

            //MemberInfo member;
            //ConstructorInfo ctor;
            //MethodInfo method;
            //PropertyInfo property;
            //FieldInfo field;
            //EventInfo @event;

            //ParameterInfo parameter;

            Assembly test_lib = Assembly.LoadFile(Path.GetFullPath("testlib.dll"));

            var printer_type = test_lib.GetType("TestLib.Printer");
            var program_type = Type.GetType("MailSender.TestConsole.Program");

            var secret_printer_type = test_lib.GetType("TestLib.SecretPrinter");

            foreach (var method in printer_type.GetMethods())
            {
                Console.WriteLine("{0} {1}({2})",
                    method.ReturnType.Name, method.Name,
                    string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}")));
            }

            var ctor_info = printer_type.GetConstructor(new[] { typeof(string) });

            var printer_obj0 = ctor_info.Invoke(new object[] { "Hello:" });

            var printer_obj1 = Activator.CreateInstance(printer_type, "Hello>>");

            var print_info = printer_type.GetMethod("Print");

            print_info.Invoke(printer_obj0, new object[] { "Wrold!!!" });
            print_info.Invoke(printer_obj1, new object[] { "Wrold!!!" });

            var print_delegate1 = print_info.CreateDelegate<Action<string>>(printer_obj0);
            print_delegate1("123");

            var print_delegate2 = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), printer_obj0, print_info);
            print_delegate2("321");

            Console.Clear();
            var secret_printer_obj = Activator.CreateInstance(secret_printer_type);

            var secret_print_method = secret_printer_type.GetMethod("Print");
            secret_print_method.Invoke(secret_printer_obj, new object[] { "111" });
            secret_print_method.Invoke(secret_printer_obj, new object[] { "222" });
            secret_print_method.Invoke(secret_printer_obj, new object[] { "333" });

            var field_info = secret_printer_type.GetField("_Counter", BindingFlags.Instance | BindingFlags.NonPublic);
            var counter_value = (int)field_info.GetValue(secret_printer_obj);
            field_info.SetValue(secret_printer_obj, 145);
            secret_print_method.Invoke(secret_printer_obj, new object[] { "444" });

            dynamic secret_printer = secret_printer_obj;

            secret_printer.Print("QQQ");

            Console.Clear();

            var values = new object[]
            {
                123,
                3.141592653589793238,
                true,
                "Hello World!!!",
            };

            foreach (var value in values)
                Process(value);

        }

        private static void Process(object value)
        {
            //switch (value)
            //{
            //    case int i: ProcessValue(i); break;
            //    case double i: ProcessValue(i); break;
            //    case bool i: ProcessValue(i); break;
            //    case string i: ProcessValue(i); break;
            //}
            ProcessValue((dynamic)value);
        }

        private static void ProcessValue(int v) => Console.WriteLine("int:{0}", v);
        private static void ProcessValue(double v) => Console.WriteLine("double:{0}", v);
        private static void ProcessValue(bool v) => Console.WriteLine("bool:{0}", v);
        private static void ProcessValue(string v) => Console.WriteLine("str:{0}", v);
    }
}
