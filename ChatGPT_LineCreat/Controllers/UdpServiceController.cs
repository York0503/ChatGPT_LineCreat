using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatGPT_LineCreat.Controllers
{
    public class UdpServiceController
    {
        public UdpClient udpClient;
        public IPEndPoint udpEndPoint;

        //public UdpServiceController(int port)
        //{
        //    udpEndPoint = new IPEndPoint(IPAddress.Any, port);
        //    udpClient = new UdpClient(udpEndPoint);
        //}

        //public string ReceiveData()
        //{
        //    byte[] receivedBytes = udpClient.Receive(ref udpEndPoint);
        //    return Encoding.ASCII.GetString(receivedBytes);
        //}

        //public void Close()
        //{
        //    udpClient.Close();
        //}
    }
}
