using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork
{
    public class VigenereEncryptor
    {
        static string Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        public static string Encrypt(string text, string key, int offset, out int step)
        {
            string EncryptedText = "";
            int i = offset;
            foreach (char c in text)
            {
                if ((Alphabet.Contains(c)) && (Alphabet.Contains(Char.ToUpper(key[i]))))
                {
                    int shift = Alphabet.IndexOf(Char.ToUpper(key[i]));
                    int NewLetter = (Alphabet.IndexOf(c) + shift);
                    if (NewLetter > 32) NewLetter -= 33;
                    EncryptedText += Alphabet[NewLetter];
                    if (i == key.Length - 1) i = 0;
                    else i++;
                }
                else
                {
                    if ((Alphabet.ToLower().Contains(c)) && (Alphabet.Contains(Char.ToUpper(key[i]))))
                    {
                        int shift = Alphabet.IndexOf(Char.ToUpper(key[i]));
                        int NewLetter = (Alphabet.ToLower().IndexOf(c) + shift);
                        if (NewLetter > 32) NewLetter -= 33;
                        EncryptedText += Alphabet.ToLower()[NewLetter];
                        if (i == key.Length - 1) i = 0;
                        else i++;
                    }
                    else EncryptedText += c;
                }
            }
            step = i;
            return EncryptedText;
        }

        public static string Decrypt(string text, string key, int offset, out int step)
        {
            string DecryptedText = "";
            int i = offset;
            foreach (char c in text)
            {
                if (Alphabet.Contains(c))
                {
                    int shift = Alphabet.IndexOf(Char.ToUpper(key[i]));
                    int NewLetter = (Alphabet.IndexOf(c) - shift);
                    if (NewLetter < 0) NewLetter += 33;
                    DecryptedText += Alphabet[NewLetter];
                    if (i == key.Length - 1) i = 0;
                    else i++;
                }
                else
                {
                    if (Alphabet.ToLower().Contains(c))
                    {
                        int shift = Alphabet.IndexOf(Char.ToUpper(key[i]));
                        int NewLetter = (Alphabet.ToLower().IndexOf(c) - shift);
                        if (NewLetter < 0) NewLetter += 33;
                        DecryptedText += Alphabet.ToLower()[NewLetter];
                        if (i == key.Length - 1) i = 0;
                        else i++;
                    }
                    else DecryptedText += c;
                }
            }
            step = i;
            return DecryptedText;
        }
    }
}
