using System;
using YamangTao.Core.Document;

namespace YamangTao.Dto.LND
{
    public class TrainingEffectivenessAssessmentDto : IDocument
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string TrainingCode { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }

        public int PR1 { get; set; }
        public int PR2 { get; set; }
        public int PR3 { get; set; }
        public int PR4 { get; set; }

        public string ParticipantToken { get; set; }

        public string D3 { get; set; }
        public int DR1 { get; set; }
        public int DR2 { get; set; }
        public int DR3 { get; set; }
        public int DR4 { get; set; }

        public string SupervisorToken { get; set; }

        public bool EmployeeHasImproved { get; set; }
        public string Recommendation { get; set; }
        public string HRToken { get; set; }

        public string PreviousHolder { get; set; } // Employee ID who submitted the document
        public string CurrentHolder { get; set; } // Employee ID where the current document Resides
        public string NextUser { get; set; } // Employee ID where the document is submitted next
        public string Status { get; set; } // Draft, For Review, Return to sender, For Approval, Approved
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }

        public string GetDocumentType()
        {
            return "Training Effectiveness Assessment Form";
        }
    }
}