using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;
using io = System.IO;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    public static class FileHelper
    {
        public static byte[] ReadFile(string sPath, ref string cRawError)
        {
            byte[] bFile = null;
            try
            {
                bFile = io::File.ReadAllBytes(sPath);
            }
            catch (Exception e)
            {
                cRawError = e.Message;
            }
            return bFile;
        }

        public static string GetFileName(string sPath, string sFileName, ref string cRawError)
        {
            try
            {
                string[] sFiles = io::Directory.GetFiles(sPath, sFileName + ".*", io::SearchOption.TopDirectoryOnly);
                if (sFiles != null && sFiles.Length > 0)
                {
                    sFileName = sFiles.First();
                    sFileName = sFileName.Replace(sPath, string.Empty);
                }
                else sFileName = string.Empty;
            }
            catch (Exception e)
            {
                cRawError = e.Message;
            }
            return sFileName;
        }

        public static byte[] CompressFiles(Dictionary<string, FileInfo> contents, bool deleteAllFiles = false) {
            byte[] zipfile = null;
            using (var zip = new ZipFile())
            using (var st = new MemoryStream())
            {
                foreach (var file in contents)
                {
                    zip.AddEntry(Path.GetFileName(file.Key), File.ReadAllBytes(file.Value.FullName));
                    if (deleteAllFiles) File.Delete(file.Value.FullName);
                }
                zip.Save(st);
                zipfile = st.ToArray();
            }
            return zipfile;
        }

        public static byte[] CompressFiles(Dictionary<string, byte[]> contents) {
            byte[] zipfile = null;
            using (var zip = new ZipFile())
            using (var st = new MemoryStream()) {
                foreach (var file in contents) {
                    zip.AddEntry(file.Key, file.Value);
                }
                zip.Save(st);
                zipfile = st.ToArray();
            }
            return zipfile;
        }
    }
}
