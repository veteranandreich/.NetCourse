﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;

namespace CourseWork
{
    public class WordDocument: IDisposable
    {
        WordprocessingDocument doc;
        public WordDocument(string path)
        {
            doc = WordprocessingDocument.Open(path, true);
        }
        
        public WordDocument(Stream stream)
        {
            doc = WordprocessingDocument.Open(stream, true);
        }
        
        public void Encrypt(string key)
        {
            Body body = doc.MainDocumentPart.Document.Body;
            var ps = body.ChildElements;
            int offset = 0;
            int step = 0;
            foreach (var item in ps)
            {
                var pss = item.ChildElements;
                foreach (var item1 in pss)
                {
                    if (item1.GetFirstChild<Text>() != null)
                    {
                        Text text = item1.GetFirstChild<Text>();
                        text.Text = VigenereEncryptor.Encrypt(text.Text, key, offset, out step);
                        offset = step;
                    }
                }
            }
            Save();
        }

        public void Save()
        {
            doc.Save();
        }

        public void Dispose()
        {
            doc.Dispose();
        }
    }
}
