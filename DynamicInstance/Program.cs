﻿using System;
using System.Reflection;

namespace DynamicInstance
{
    class Profile
    {
        private string name;
        private string phone;
        public Profile()
        {
            name = "";
            phone = "";
        }

        public Profile(string name, string phone)
        {
            this.name = name;
            this.phone = phone;
        }

        public void Print()
        {
            Console.WriteLine($"{name}, {phone}");
        }

        public string Name { get; set; }
        public string Phone { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Type type = Type.GetType("DynamicInstance.Profile");
            MethodInfo methodInfo = type.GetMethod("Print");
            PropertyInfo nameProperty = type.GetProperty("Name");
            PropertyInfo phoneProperty = type.GetProperty("Phone");

            object profile = Activator.CreateInstance(type, "박상현", "512-1234");
            methodInfo.Invoke(profile, null); // null인수가 오는 자리에는 Invoke() 메소드가 호출할 메소드의 인수가 와야한다.                                             

            profile = Activator.CreateInstance(type);
            nameProperty.SetValue(profile, "박찬호", null);
            phoneProperty.SetValue(profile, "997-5511", null);

            Console.WriteLine("{0}, {1}",
                nameProperty.GetValue(profile, null),
                phoneProperty.GetValue(profile, null));
        }
    }
}
