using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.Events
{
    public class CaseSubmitEnvent : INotification
    {
        public string CaseID { get; private set; }
        public CaseSubmitEnvent(string caseID)
        {
            CaseID = CaseID;
        }
    }

}