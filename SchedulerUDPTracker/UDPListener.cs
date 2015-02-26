using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1 {
    public class UDPListener {

        private Action<string> CallBack;

        public UDPListener(Action<string> callBack) {
            if (callBack == null)
                throw new ArgumentNullException("callBack");

            this.CallBack = callBack;
        }

        public void Begin(string host, int port) {
            Task.Factory.StartNew(() => {

                using (var client = new UdpClient(port)) {
                    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

                    IPEndPoint refEP = new IPEndPoint(IPAddress.Any, 0);
                    while (true) {
                        var bytes = client.Receive(ref refEP);
                        var msg = Encoding.UTF8.GetString(bytes);

                        this.CallBack(msg);
                    }
                }
            });
        }

    }
}
