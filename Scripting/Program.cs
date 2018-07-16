using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
//using System.Web.Script.Serialization;
using IronPython.Compiler;
using IronPython.Hosting;
using IronPython.Modules;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Hosting.Providers;
using System.Text.RegularExpressions;
using Microsoft.Scripting.Runtime;

namespace Scripting
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"D:\ScriptTest\TempDebugScript.py";

            // save python script in file
            using (var sw = File.CreateText(filePath))
            {
                sw.Write(@"User.Age = 15");
            }
            // create object that will be passed to script scope
            User user = new User()
            {
                Age = 5,
                FirstName = "Radovan"
            };

            // trace
            Console.WriteLine(user.FirstName);
            Console.WriteLine(user.Age);

            // initiating engine and scope
            ScriptEngine scriptEngine;
            var options = new Dictionary<string, object>();
            options["LightWeightScopes"] = true;
            scriptEngine = Python.CreateEngine(options);
            ScriptScope scope = Python.GetBuiltinModule(scriptEngine);
            scope = scriptEngine.CreateScope();

            // adding variable to scope
            scope.SetVariable("User", user);

            //executing Python script
            scriptEngine.ExecuteFile(filePath, scope);

            // trace
            Console.WriteLine(user.FirstName);
            Console.WriteLine(user.Age);
            Console.ReadLine();
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
}
