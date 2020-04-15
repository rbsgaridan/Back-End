using System;

namespace YamangTao.Core.Document
{
    public interface IDocument
    {
        string GetDocumentType();
        DateTime? DateCreated { get; set; }
        DateTime? DateLastModified { get; set; }
        DateTime? DateLastPrinted { get; set; }
        
    }
}
