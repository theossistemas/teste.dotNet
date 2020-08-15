using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using Services.AtualizacaoSistema;
using System;
using System.Data.SqlClient;
using Utils.Connection;
using Utils.Xml;

namespace Tests
{
    [TestClass]
    public class AtualizacaoTest
    {
        [TestMethod]
        public void TestXml()
        {
            AtualizacaoAutomatica atualizacao = new AtualizacaoAutomatica
            {
                Versao = new Versao { Guid = "52CECA45-425E-428C-A9D8-4D89A8B31A5D", Numero = 1 },
                ComandoCriar = @"Teste",
                ComandoExcluir = @"Test"
            };

            String xml = XmlUtils.ObjectToXml(atualizacao);

            Console.WriteLine(xml);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SqlServerHelper.Initializer(new SqlConnectionStringBuilder
            {
                DataSource = @"DESKTOP-IMKSJ5J\SQLEXPRESS",
                InitialCatalog = "master",
                UserID = "sa",
                Password = "1234"
            });

            AtualizacaoService.Iniciar();
        }
    }
}
