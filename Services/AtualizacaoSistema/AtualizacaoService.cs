using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Utils.Xml;

namespace Services.AtualizacaoSistema
{
    public class AtualizacaoService
    {
        private AtualizacaoService() { }

        public static void Iniciar()
        {
            IList<IAtualizacaoAutomatica> atualizacoes = RetornarListaClassesAtualizacao();

            AtualizacaoRepository repository = new AtualizacaoRepository();

            repository.AtualizarSistemaPorListaAtualizacoes(atualizacoes);
        }

        private static String[] PegarNomesXmlsAtualizacao()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            return assembly.GetManifestResourceNames()
                .Where(x => x.StartsWith($"{assembly.GetName().Name}.XmlAtualizacao") && x.EndsWith(".xml"))
                .ToArray();
        }

        private static String LerArquivoPeloNomeAssembly(String nome)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(nome))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static IAtualizacaoAutomatica ConverterXmlAtualizacaoParaClasse(String xml)
        {
            return XmlUtils.XmlToObject<AtualizacaoAutomatica>(xml);
        }

        private static IList<IAtualizacaoAutomatica> RetornarListaClassesAtualizacao()
        {
            IList<IAtualizacaoAutomatica> atualizacoes = new List<IAtualizacaoAutomatica>();

            String[] nomesXmls = PegarNomesXmlsAtualizacao();

            for (Int32 indice = 0; indice < nomesXmls.Count(); indice++)
            {
                String xml = LerArquivoPeloNomeAssembly(nomesXmls[indice]);

                atualizacoes.Add(ConverterXmlAtualizacaoParaClasse(xml));
            }

            return atualizacoes;
        }
    }
}
