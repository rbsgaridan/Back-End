using System;

namespace YamangTao.Core.Document
{
    public interface IDocument
    {
        string GetDocumentType();
        DateTime? DateCreated { get; set; }
        DateTime? DateLastModified { get; set; }
        DateTime? DateLastPrinted { get; set; }
        
        string PreviousHolder { get; set; } // Employee ID who submitted the document
        string CurrentHolder { get; set; } // Employee ID where the current document Resides
        string NextUser { get; set; } // Employee ID where the document is submitted next
        string Status { get; set; } // Draft, For Review, Return to sender, For Approval, Approved
    }
}
