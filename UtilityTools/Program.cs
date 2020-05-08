using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Reflection;

namespace UtilityTools
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.Run(new Form1());
        }

        //static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MySql.Data.dll"))
        //    {
        //        byte[] assemplyData = new byte[stream.Length];
        //        stream.Read(assemplyData, 0, assemplyData.Length);
        //        return Assembly.Load(assemplyData);
        //    }
        //}
    }
}
