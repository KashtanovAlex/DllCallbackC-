using Avalonia.Threading;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace dllCallback.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DelegateCow();

        [DllImport("C:\\Users\\kashtanov\\Desktop\\ArmDllTest-repo\\build\\bin\\RelWithDebInfo\\script_arm.dll")]
        public static extern void RegisterFunctions([MarshalAs(UnmanagedType.FunctionPtr)] DelegateCow a, [MarshalAs(UnmanagedType.FunctionPtr)] DelegateCow b);

        [DllImport("C:\\Users\\kashtanov\\Desktop\\ArmDllTest-repo\\build\\bin\\RelWithDebInfo\\script_arm.dll")]
        public static extern void Print();
        [DllImport("C:\\Users\\kashtanov\\Desktop\\ArmDllTest-repo\\build\\bin\\RelWithDebInfo\\script_arm.dll")]
        public static extern void PrintValue(int val);

        

        private string _greeting = "Welcome to Avalonia!";
        public string Greeting
        {
            get
            {
                return _greeting;
            }
            set
            {
                _greeting = value;
                OnPropertyChanged();
            }
        } 


        public void SendDll()
        {
            Person tom = new Person("SabininaMama", 37);
            var json = JsonConvert.SerializeObject(tom);

            DelegateCow callback = () =>
            {
                Dispatcher.UIThread.Post(() => SetValue(), DispatcherPriority.Background);
            };

            DelegateCow callback2 = () =>
            {
                Dispatcher.UIThread.Post(() => SetValue2(), DispatcherPriority.Background);
            };

            var before = DateTime.UtcNow;

            RegisterFunctions(callback, callback2);
            Print();
            PrintValue(228);
            Console.WriteLine(DateTime.UtcNow - before);
        }



        private void SetValue()
        {
            Greeting += "1";
        }
        private void SetValue2()
        {
            Greeting += "2";
        }
    }

    class Person
    {
        public string Name { get; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
