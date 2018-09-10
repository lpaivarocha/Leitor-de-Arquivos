using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeitorArquivos
{
    public class Program
    {
        public static string _diretorio = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        public static List<string> _listNames = new List<string>();

        public static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_diretorio);

            FindFiles(dirInfo, false);

            SaveListNames();
        }

        private static void SaveListNames()
        {
            string arquivo = Path.Combine(_diretorio, string.Format("Lista_de_arquivos_{0}.txt", DateTime.Now.ToString("dd-MM-yyyy")));

            Directory.CreateDirectory(_diretorio);

            File.Create(arquivo).Dispose();

            File.WriteAllLines(arquivo, _listNames);
        }

        private static void FindFiles(DirectoryInfo dir, bool sub)
        {
            long TotalSize = 0;

            foreach (FileInfo file in dir.GetFiles())
            {
                var exe = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;

                if (!file.Name.Equals(exe))
                {
                    TotalSize += file.Length;
                    string FileSize = GetFileSize(file.Length);

                    if (!sub)
                        _listNames.Add(string.Format("{0} | {1}", file.Name, FileSize));
                    else
                        _listNames.Add(string.Format("{0} | {1}", file.FullName.Replace(_diretorio, string.Empty), FileSize));
                }
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                FindFiles(subDir, true);
            }

            _listNames.Add(string.Format("Total: {0}", GetFileSize(TotalSize)));
        }

        public static string GetFileSize(long Bytes)
        {
            if (Bytes >= 1073741824)
            {
                Decimal size = Decimal.Divide(Bytes, 1073741824);
                return String.Format("{0:##.##} GB", size);
            }
            else if (Bytes >= 1048576)
            {
                Decimal size = Decimal.Divide(Bytes, 1048576);
                return String.Format("{0:##.##} MB", size);
            }
            else if (Bytes >= 1024)
            {
                Decimal size = Decimal.Divide(Bytes, 1024);
                return String.Format("{0:##.##} KB", size);
            }
            else if (Bytes > 0 & Bytes < 1024)
            {
                Decimal size = Bytes;
                return String.Format("{0:##.##} Bytes", size);
            }
            else
            {
                return "0 Bytes";
            }
        }
    }
}
