using Govno_2._0.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Govno_2._0
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Frame MainFrame { get; set; }
        public static FixitEntities context = new FixitEntities();
        public static User currentUser {  get; set; }
        public static Orders currentOrder { get; set; }
        public static Services currentService { get; set; }
    }
}
