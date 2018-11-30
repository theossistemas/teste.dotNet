using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace LC.Application.Book.DataTransferObject
{
    [DataContract]
    public class PhotoDTO
    {
        [DataMember(Name = "filename")]
        public string FileName { get; set; }
        [DataMember(Name = "filetype")]
        public string FileType { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
