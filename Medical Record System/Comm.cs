//Install-Package CassandraCSharpDriver
using Cassandra;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Medical_Record_System
{
    public class Comm
    {

        private string login;
        private string pass;
        private string server;
        private string keyspace;
        Cluster cluster;
        ISession session;

        public Comm(string login, string pass, string server, string keyspace)
        {
            this.login = login;
            this.pass = pass;
            this.server = server;
            this.keyspace = keyspace;
        }

        public Comm(ClusterInfo cluster)
        {
            this.login = cluster.username;
            this.pass = cluster.password;
            this.server = cluster.server;
            this.keyspace = cluster.keyspace;
        }

        /*public void Connect()
        {
            PlainTextAuthProvider auth = new PlainTextAuthProvider(login, pass);

            cluster = Cluster.Builder().AddContactPoint(server).WithAuthProvider(auth).Build();
            session = cluster.Connect(keyspace);
        }
        */
        public void Connect()
        {
            PlainTextAuthProvider auth = new PlainTextAuthProvider(login, pass);
            RemoteCertificateValidationCallback remoteCertificateValidationCallback = new RemoteCertificateValidationCallback(
                    (object sender,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors) =>
                    {
                        if (sslPolicyErrors != SslPolicyErrors.None)
                        {
                            if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    });
            SSLOptions opt = new SSLOptions(System.Security.Authentication.SslProtocols.Tls, false, remoteCertificateValidationCallback);
            cluster = Cluster.Builder().AddContactPoint(server).WithAuthProvider(auth).WithSSL(opt).Build();
            session = cluster.Connect(keyspace);
        }

        public bool CheckStatus()
        {

            try
            {
                Connect();
            }
            catch (System.Exception)
            {
                return false;
            }
            Close();
            return true;

        }

        public ISession GetSession()
        {
            return this.session;
        }

        public void SendCQL(string cql)
        {
            session.Execute(cql);
        }

        public void Close()
        {
            cluster.Dispose();
        }
    }
}
