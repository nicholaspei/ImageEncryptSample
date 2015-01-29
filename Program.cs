using System;
using ImageEncryption;

namespace ImageEncryptionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red; ;
            DEncrypt4ImageHelper.EncryptFile(@"format-indent-less.png", @"format-indent-less.img","helloworld");

           Console.WriteLine("Find the encrypt file in debug folder");

            DEncrypt4ImageHelper.DecryptFile(@"format-indent-less.img", @"format-indent-less.png", "helloworld");

            Console.Write("decrypt file as stream and save it as original format!");
        }
    }
}
