using System;
using System.IO;
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


        }
    }
}
