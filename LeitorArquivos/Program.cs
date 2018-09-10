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

        public static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_diretorio);

            var listNames = FindFiles(dirInfo);

            SaveListNames(listNames);
        }

        private static void SaveListNames(List<string> relatorio)
        {
            string arquivo = Path.Combine(_diretorio, string.Format("Lista_de_arquivos_{0}.txt", DateTime.Now.ToString("dd-MM-yyyy")));

            Directory.CreateDirectory(_diretorio);

            File.Create(arquivo).Dispose();

            File.WriteAllLines(arquivo, relatorio);
        }

        private static List<string> FindFiles(DirectoryInfo dir)
        {
            List<string> listNames = new List<string>();

            long TotalSize = 0;

            foreach (FileInfo file in dir.GetFiles())
            {
                if (!file.Name.Equals("LeitorArquivos.exe"))
                {
                    TotalSize += file.Length;
                    string FileSize = GetFileSize(file.Length);

                    listNames.Add(string.Format("{0} | {1}", file.Name, FileSize));
                }
            }

            //foreach (DirectoryInfo subDir in dir.GetDirectories())
            //{
            //    FindFiles(subDir);
            //}

            listNames.Add(string.Format("Total: {0}", GetFileSize(TotalSize)));

            return listNames;
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
