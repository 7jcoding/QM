using System;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            var lis = new UDPListener((s) => {
                Console.WriteLine(s);
            });
            lis.Begin("", 6666);

            Console.Read();
        }
    }
}
