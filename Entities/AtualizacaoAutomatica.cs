using Entities.Base;
using System;
using System.Xml.Serialization;

namespace Entities
{
    [XmlRoot(ElementName = "atualizacaoAutomatica")]
    public class AtualizacaoAutomatica : IAtualizacaoAutomatica
    {
        [XmlElement(ElementName = "versao")]
        public Versao Versao { get; set; }

        [XmlElement(ElementName = "comandoExcluir")]
        public String ComandoExcluir { get; set; }

        [XmlElement(ElementName = "comandoCriar")]
        public String ComandoCriar { get; set; }
    }

    public class Versao : IEntity
    {
        [XmlIgnore]
        public Int64? Id { get; set; }

        [XmlAttribute(AttributeName = "guid")]
        public String Guid { get; set; }

        [XmlAttribute(AttributeName = "numero")]
        public Int64 Numero { get; set; }
    }
}
